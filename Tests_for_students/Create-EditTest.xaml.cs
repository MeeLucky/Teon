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

        public Create_EditTest(Test t)
        {
            InitializeComponent();
            test = t;
            C_TestName.Content = test.testName;
            RefreshQList();
        }

        private void AddQuest_Click (object v, RoutedEventArgs e)
        {
            int num = test.qList.Count() + 1;
            test.qList.Add(new Question($"Вопрос {num}", new[] { "Beastewttwr", "Endi" }, new[] { 0, 1}));
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

        StackPanel field2, field1;//костыль :)

        private void EditQuest (object v, RoutedEventArgs e)
        {
            C_Right.Children.Clear();
            Button btn = (Button)v;
            int id = Convert.ToInt32(btn.Tag);


            field1 = new StackPanel();
            field1.Orientation = Orientation.Vertical;

            TextBox quest = new TextBox();
            quest.Text = test.qList[id].GetQuestion();
            quest.TextWrapping = TextWrapping.Wrap;
            quest.AcceptsReturn = true;
            quest.Margin = new Thickness(10);
            quest.FontSize = 15;
            field1.Children.Add(quest);
            field1.Tag = "question";


            field2 = new StackPanel();
            field2.Orientation = Orientation.Vertical;
            field2.Name = "field2";
            field2.Margin = new Thickness(10,0,0,0);

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
                balls.Text = "0";

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
                field2.Children.Add(str);
            }

            Button addAns = new Button();
            addAns.Content = "Добавить варинат ответа";
            addAns.Click += AddAnswer;
            addAns.Width = 200;
            addAns.HorizontalAlignment = HorizontalAlignment.Left;
            addAns.FontSize = 14;
            addAns.Margin = new Thickness(30, 30, 0, 0);



            Button ok = new Button();
            ok.Content = "Ok";
            ok.Tag = id;
            ok.Click += SaveQuest;
            ok.Width = 50;
            ok.HorizontalAlignment = HorizontalAlignment.Left;
            ok.FontSize = 14;
            ok.Margin = new Thickness(30, 30, 0, 0);

            C_Right.Children.Add(field1);
            C_Right.Children.Add(field2);
            C_Right.Children.Add(addAns);
            C_Right.Children.Add(ok);
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
            int id = Convert.ToInt32(b.Tag);
            int len = field2.Children.Count;
            string quest;
            string[] ans = new string[len];
            int[] mask = new int[len];

            TextBox tb = (TextBox)field1.Children[0];
            quest = tb.Text.Trim();
            
            for(int i = 0; i < len; i++)
            {
                StackPanel str = (StackPanel)field2.Children[i];

                TextBox TBball = (TextBox)str.Children[1];
                if (TBball.Text.Trim() == "")
                    mask[i] = 0;
                else
                    mask[i] = Convert.ToInt32(TBball.Text);

                TextBox TBanswer = (TextBox)str.Children[2];
                ans[i] = TBball.Text.ToString();
            }

            test.qList[id] = new Question(quest, ans, mask);

            C_Right.Children.Clear();
            field1 = null;
            field2 = null;
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
            field2.Children.Add(str);
        }

        private void SetTest_Click (object v, RoutedEventArgs e)
        {
            TextBox testName = new TextBox();
            testName.FontSize = 16;
            testName.Text = test.testName;
            testName.Margin = new Thickness(20, 0, 20, 0);

            TextBox author = new TextBox();
            author.FontSize = 16;
            author.Text = test.author;
            author.Margin = new Thickness(20, 0, 20, 0);

            TextBox desk = new TextBox();
            desk.FontSize = 16;
            desk.Text = test.desckription;
            desk.Margin = new Thickness(20, 0, 20, 0);

            Button SaveTestInfo = new Button();
            SaveTestInfo.Click += SaveTestInfo_Click;
            SaveTestInfo.Width = 175;
            SaveTestInfo.HorizontalAlignment = HorizontalAlignment.Left;
            SaveTestInfo.Content = "Сохранить";
            SaveTestInfo.Margin = new Thickness(30, 20, 0, 0);

            //tips
            TextBlock testNameTip = new TextBlock();
            testNameTip.Text = "Название теста";
            testNameTip.FontSize = 16;
            testNameTip.Margin = new Thickness(30, 35, 0, 0);
            //testNameTip
            TextBlock authorTip = new TextBlock();
            authorTip.Text = "ФИО автора";
            authorTip.FontSize = 16;
            authorTip.Margin = new Thickness(30, 0, 0, 0);
            //authorTip
            TextBlock deskTip = new TextBlock();
            deskTip.Text = "Описание теста";
            deskTip.FontSize = 16;
            deskTip.Margin = new Thickness(30, 0, 0, 0);
            //deskTip

            C_Right.Children.Clear();
            C_Right.Children.Add(testNameTip);
            C_Right.Children.Add(testName);
            C_Right.Children.Add(authorTip);
            C_Right.Children.Add(author);
            C_Right.Children.Add(deskTip);
            C_Right.Children.Add(desk);
            C_Right.Children.Add(SaveTestInfo);
        }

        private void SaveTestInfo_Click(object v, RoutedEventArgs e)
        {
            Button btn = (Button)v;
            StackPanel root = (StackPanel)btn.Parent;

            TextBox testName = (TextBox)root.Children[1];
            TextBox author = (TextBox)root.Children[3];
            TextBox desk = (TextBox)root.Children[5];

            StackPanel btnParent = (StackPanel)btn.Parent;
            C_TestName.Content = testName.Text;

            test.testName = testName.Text;
            test.author = author.Text;
            test.desckription = desk.Text;

            btnParent.Children.Clear();
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
                path += ".dat";

                test.SetPath(path);
                test.SaveTest();
            }
            else
                return;
        }
    }
}
