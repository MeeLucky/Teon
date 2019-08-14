using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using Microsoft.Win32;

namespace Tests_for_students
{
    /// <summary>
    /// Логика взаимодействия для Create_EditTest.xaml
    /// </summary>
    public partial class Create_EditTest : Window
    {

        static Test test = new Test(null);//patch = null

        public Create_EditTest(Test t, bool isNew = false)
        {
            InitializeComponent();
            if (isNew)
                this.Title = "Создание теста";
            else
                this.Title = "Редактирование теста";
            test = t;
            C_TestName.Content = test.testName;
            RefreshQList();
        }
        
        private void AddQuest_Click (object v, RoutedEventArgs e)
        {
            int num = test.qList.Count() + 1;
            test.qList.Add(new Question($"Вопрос {num}", new[] { "", }, new[] { 0, }));
            RefreshQList();
        }
        
        private void RefreshQList ()
        {

            C_QList.Children.RemoveRange(0, C_QList.Children.Count);
            

            foreach (var item in test.qList)
            {
                Button quest = new Button();
                quest.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 238, 238, 238));
                quest.Content = item.GetQuestion();
                quest.Name = "quest";
                quest.Click += EditQuest;
                quest.Tag = test.qList.IndexOf(item);
                quest.Margin = new Thickness(5, 5, 5, 0);

                C_QList.Children.Add(quest);
            }
        }
        
        private void EditQuest (object v, RoutedEventArgs e)
        {
            C_TestOption.Visibility = Visibility.Collapsed;
            C_QuestField.Visibility = Visibility.Visible;

            Button btn = (Button)v;
            int id = Convert.ToInt32(btn.Tag);

            C_Quest.Tag = id;
            C_Quest.Text = test.qList[id].GetQuestion();

            C_Answers.Children.Clear();
            
            string[] ans = test.qList[id].GetAnswers();
            int[] mask = test.qList[id].GetMask();
            int len = 0;
            if (ans != null)
                len = ans.Length;

            for (int i = 0; i < len; i++)
            {
                StackPanel str = new StackPanel();
                str.Orientation = Orientation.Horizontal;
                str.Margin = new Thickness(0, 10, 0, 0);

                Button del = new Button();
                del.Content = "X";
                del.Foreground = new SolidColorBrush(Colors.Red);
                del.Margin = new Thickness(0, 0, 10, 0);
                del.Background = new SolidColorBrush(Colors.White);
                del.FontSize = 12;
                del.Width = 20;
                del.Height = 20;
                del.HorizontalAlignment = HorizontalAlignment.Center;
                del.VerticalAlignment = VerticalAlignment.Top;
                del.FontWeight = FontWeights.Bold;
                del.Click += deletAnswer;

                TextBox balls = new TextBox();
                balls.Text = mask[i].ToString();
                balls.FontSize = 14;
                balls.Width = 30;
                balls.Text = mask[i].ToString();
                balls.ToolTip = "Если оставить поле пустым, оно будет раным 0";

                TextBox answer = new TextBox();
                answer.Text = ans[i];
                answer.FontSize = 14;
                answer.Margin = new Thickness(10, 0, 10, 0);
                answer.MinWidth = 200;
                double w = this.Width - 350;
                answer.MaxWidth = w;

                str.Children.Add(del);
                str.Children.Add(balls);
                str.Children.Add(answer);
                C_Answers.Children.Add(str);
             }
        }

        private void delQuest_Click(object v, RoutedEventArgs e)
        {
            Button b = (Button)v;
            int id = Convert.ToInt32(C_Quest.Tag);
            test.qList.RemoveAt(id);
            RefreshQList();
            C_QuestField.Visibility = Visibility.Collapsed;
        }

        private void GoMain_Click(object v, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void deletAnswer(object v, RoutedEventArgs e)
        {
            Button b = (Button)v;                       //Нажатая кнопка
            StackPanel s = (StackPanel)b.Parent;        //родитель нажатой кнопки
            StackPanel ParentS = (StackPanel)s.Parent;  //родитель родителя нажатой кнопки
            ParentS.Children.Remove(s);                 //удаление родителя нажатой кнопки из родителя родителя нажатой кнопки
        }


        private void SaveQuest(object v, RoutedEventArgs e)
        {
            Button b = (Button)v;
            int id = Convert.ToInt32(C_Quest.Tag);
            int len = C_Answers.Children.Count;
            string quest = C_Quest.Text.Trim(); ;
            string[] ans = new string[len];
            int[] mask = new int[len];

            for(int i = 0; i < len; i++)
            {
                StackPanel str = (StackPanel)C_Answers.Children[i];
                TextBox TBball = (TextBox)str.Children[1];
                TextBox TBanswer = (TextBox)str.Children[2];

                if (TBball.Text.Trim() == "")
                    mask[i] = 0;
                else
                    mask[i] = Convert.ToInt32(TBball.Text);

                ans[i] = TBanswer.Text.ToString();
            }

            test.qList[id] = new Question(quest, ans, mask);

            C_QuestField.Visibility = Visibility.Collapsed;
            RefreshQList();
        }

        private void AddAnswer (object v, RoutedEventArgs e)
        {
            StackPanel str = new StackPanel();
            str.Orientation = Orientation.Horizontal;
            str.Margin = new Thickness(0, 10, 0, 0);

            Button del = new Button();
            del.Content = "X";
            del.Foreground = new SolidColorBrush(Colors.Red);
            del.Margin = new Thickness(0, 0, 10, 0);
            del.Background = new SolidColorBrush(Colors.White);
            del.FontSize = 12;
            del.Width = 20;
            del.Height = 20;
            del.HorizontalAlignment = HorizontalAlignment.Center;
            del.VerticalAlignment = VerticalAlignment.Top;
            del.FontWeight = FontWeights.Bold;
            del.Click += deletAnswer;

            TextBox balls = new TextBox();
            balls.Text = "0";
            balls.FontSize = 14;
            balls.Width = 30;

            TextBox answer = new TextBox();
            answer.Text = "";
            answer.FontSize = 14;
            answer.Margin = new Thickness(10, 0, 0, 0);
            answer.MinWidth = 200;
            double w = this.Width - 350;
            answer.MaxWidth = w;


            str.Children.Add(del);
            str.Children.Add(balls);
            str.Children.Add(answer);
            C_Answers.Children.Add(str);
        }

        private void SetTest_Click (object v, RoutedEventArgs e)
        {
            C_QuestField.Visibility = Visibility.Collapsed;
            C_TestOption.Visibility = Visibility.Visible;

            C_OptName.Text = test.testName;
            C_OptAuthor.Text = test.author;
            C_OptDesk.Text = test.desckription;
        }

        private void SaveTestInfo_Click(object v, RoutedEventArgs e)
        {
            C_TestName.Content = C_OptName.Text;

            test.testName = C_OptName.Text;
            test.author = C_OptAuthor.Text;
            test.desckription = C_OptDesk.Text;
            if (C_Pass.Password.ToString().Trim() != "")
            {
                test.password = C_Pass.Password.ToString().Trim();
            }

            C_TestOption.Visibility = Visibility.Collapsed;
        }


        private void SaveNewTest_Click (object v, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != false)
            {
                string path = sfd.FileName;
                int dotPos = path.Substring(0, path.Length).IndexOf(".");
                if (dotPos != -1)
                    path = path.Substring(0, path.Length - (path.Length - dotPos));
                path += ".teon";

                test.SetPath(path);
                test.SaveTest();
            }
            else
                return;
        }
    }
}
