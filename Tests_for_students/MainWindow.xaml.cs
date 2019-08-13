using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tests_for_students
{
    public partial class MainWindow : Window
    {
        public MainWindow ()
        {
            InitializeComponent();
        }

        private void Student_Click (object v, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Collapsed;
            Student.Visibility = Visibility.Visible;
            BackMain.Visibility = Visibility.Visible;
        }

        private void Teacher_Click(object v, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Collapsed;
            Teacher.Visibility = Visibility.Visible;
            BackMain.Visibility = Visibility.Visible;
        }

        private void BackMain_Click(object v, RoutedEventArgs e)
        {
            Teacher.Visibility = Visibility.Collapsed;
            Student.Visibility = Visibility.Collapsed;
            Main.Visibility = Visibility.Visible;
            BackMain.Visibility = Visibility.Collapsed;
        }

        /*Student*/

        Test test;
        bool testSelected = false;
        bool tryName = false;

        private void openTest_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                test = Test.OpenTest(openFileDialog.FileName);
                if (test == null)
                {
                    MessageBox.Show("Ошибка открытия.");
                    return;
                }
                C_testName.Text = test.testName;
                C_testDesk.Text = test.desckription;
                testSelected = true;
            }
            else
            {
                testSelected = false;
            }
        }

        private void StartTest_Click(object sender, RoutedEventArgs e)
        {
            if (C_UserName.Text == "" && !tryName)
            {
                MessageBox.Show("Если не указать имя, то вы продолжите как аноним!");
                tryName = true;
                return;
            }

            if (testSelected)
            {
                testWindow testWin = new testWindow(test);
                testWin.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберете тест!");
            }
        }

    /*Student*/

    /*Teacher*/
    
        private void EditTest_Click(object v, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
                test = Test.OpenTest(ofd.FileName);
            else
                return;

            TextBlock pasTip = new TextBlock();
            pasTip.FontSize = 16;
            pasTip.Text = "Введите пароль для этого теста.";
            pasTip.HorizontalAlignment = HorizontalAlignment.Center;
            PasswordBox pas = new PasswordBox();
            pas.Width = 300;
            pas.HorizontalAlignment = HorizontalAlignment.Center;
            Button next = new Button();
            next.Margin = new Thickness(0, 10, 0, 0);
            next.Width = 70;
            next.Content = "Дальше";
            next.Click += Next_Click;

            passFild.Children.Clear();
            passFild.Children.Add(pasTip);
            passFild.Children.Add(pas);
            passFild.Children.Add(next);
        }

        bool noCount = false;
        private void Next_Click(object v, RoutedEventArgs e)
        {
            PasswordBox pas = (PasswordBox)passFild.Children[1];

            if (pas.Password.ToString() == test.password)
            {
                new Create_EditTest(test).Show();
                this.Close();
            }
            else
            {
                if (!noCount)
                {
                    TextBlock no = new TextBlock();
                    no.FontSize = 17;
                    no.Foreground = new SolidColorBrush(Colors.Red);
                    no.Text = "Неправильный пароль!";
                    no.TextAlignment = TextAlignment.Center;
                    passFild.Children.Add(no);
                    noCount = true;
                }
            }

        }

        private void CreateTest_Click(object v, RoutedEventArgs e)
        {
            new Create_EditTest(new Test(null), true).Show();
            this.Close();
        }

    /*Teacher*/
    }
}
