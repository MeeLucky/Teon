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

namespace Tests_for_students
{
    public partial class TestResult : Window
    {
        Test test;
        public TestResult(Test t, string [] r, DateTime [] times)
        {
            InitializeComponent();
            test = t;

            C_TestName.Text = t.testName;
            if (t.userName != "")
                C_UserName.Text = t.userName;
            else
                C_UserName.Text = "Аноним";

            int maxScore = 0;
            foreach (var item in t.qList)
            {
                int [] balls = item.GetMask();
                maxScore += balls.Max();
            }

            int userScore = 0;
            foreach (var item in r)
                userScore += Convert.ToInt32(item);

            double proc = Math.Round(100 / (double)maxScore * (double)userScore, 0);

            C_Score.Text = $"{userScore}/{maxScore}    {proc}%";

            C_StartTime.Text = times[0].ToLongTimeString();
            C_EndTime.Text = times[1].ToLongTimeString();
            TimeSpan span = times[1] - times[0];
            double minuts = Math.Round(span.TotalMinutes, 1);
            C_TravelTime.Text = $"{minuts.ToString()} мин.";
        }

        private void Restart_Click (object v, RoutedEventArgs e)
        {
            test.userName = C_UserName.Text;

            testWindow testWin = new testWindow(test);
            testWin.Show();
            this.Close();
        }

        private void ToMainMenu_Click (object v, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
