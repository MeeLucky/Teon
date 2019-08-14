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
using MessageBox = System.Windows.Forms.MessageBox;

namespace Tests_for_students
{
    public partial class testWindow : Window
    {
        Test test;
        string[] userTest;
        DateTime startTest;

        public testWindow(Test t)
        {
            InitializeComponent();

            test = t;

            this.Title = t.testName;

            userTest = new string [test.qList.Count+1];
            int len = userTest.Length;
            for (int i = 1; i < len; i++)
                userTest[i] = "0";

            ShowTest();

            startTest = DateTime.Now;//dfine здесь потому что на медленных компах большие тесты могут открываться долго
        }

        private void ShowTest()
        {
            int num = 0;
            foreach (var item in test.qList)
            {
                num++;
                WrapPanel quest = new WrapPanel();
                quest.Orientation = Orientation.Vertical;
                quest.Margin = new Thickness(10, 0, 0, 0);
                quest.Width = this.Width * 2;//почему его нужно умножать на 2 чтобы он был во всю ширену экрана?

                TextBlock question = new TextBlock();
                question.Text = Convert.ToString(num) + ") " + item.GetQuestion();
                question.FontSize = 20;
                question.TextWrapping = TextWrapping.Wrap;
                quest.Children.Add(question);

                string[] answer = item.GetAnswers();
                int[] Mask = item.GetMask();
                int len = answer.Length;

                for(int i = 0; i < len; i++)
                {
                    RadioButton ans = new RadioButton();
                    ans.GroupName =  item.GetQuestion();

                    TextBlock content = new TextBlock();
                    content.TextWrapping = TextWrapping.Wrap;
                    content.Text = answer[i];
                    ans.Content = content;

                    ans.Width = this.Width * 1.7;
                    ans.HorizontalAlignment = HorizontalAlignment.Left;
                    ans.MouseEnter += RBMEnter;
                    ans.MouseLeave += RBMLeave;
                    ans.Tag = num + "|" + Convert.ToString(Mask[i]);
                    ans.Padding = new Thickness(0, 2, 0, 2);
                    ans.Click += selected;

                    quest.Children.Add(ans);
                }

                WrapPanel hr = new WrapPanel();
                hr.Height = 1;
                hr.Background = new SolidColorBrush(Colors.Gray);
                hr.Width = this.Width * 2;

                root.Children.Add(quest);
                root.Children.Add(hr);

            }

            Button endTest = new Button();
            endTest.Content = "Закончить тест";
            endTest.Width = 200; 
            endTest.FontSize = 19;
            endTest.Margin = new Thickness(0, 10, 0, 10);
            endTest.Click += goToResult;
            root.Children.Add(endTest);
        }

        public void RBMLeave(object v, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)v;
            rb.FontWeight = FontWeights.Normal;
        }

        public void RBMEnter (object v, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)v;
            rb.FontWeight = FontWeights.UltraBold;
        }

        public void selected(object v, RoutedEventArgs e)
        {
            RadioButton btn = (RadioButton)v;
            string tag = (string)btn.Tag;

            int delIndex = tag.IndexOf("|");
            int num = Convert.ToInt32(tag.Substring(0, delIndex));
            string answer = tag.Substring(delIndex+1);

            if (answer != "0")
                userTest[num] = answer;
            else
                userTest[num] = "0";
        }

        public void goToResult(object v, RoutedEventArgs e)
        {
            DateTime[] times = new DateTime[2];
            times[0] = startTest;
            times[1] = DateTime.Now;

            if(C_UserName.Text.Trim() != "")
                test.userName = C_UserName.Text.Trim();
            else
            {
                System.Windows.Forms.DialogResult dialogResult = 
                    MessageBox.Show("Вы хотите закончить тест как аноним? ", 
                    "Не указано ФИО", 
                    System.Windows.Forms.MessageBoxButtons.YesNo);
                if (dialogResult == System.Windows.Forms.DialogResult.No)
                    return;
            }
            TestResult tr = new TestResult(test, userTest, times);
            tr.Show();
            this.Close();
        }
    }
}
