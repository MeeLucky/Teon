using Microsoft.Win32;
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
    /// <summary>
    /// Логика взаимодействия для student.xaml
    /// </summary>
    public partial class student : Window
    {

        Test test;

        public student()
        {
            InitializeComponent();
        }

        private void openTest_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                test = Test.OpenTest(openFileDialog.FileName);
                C_testName.Text = test.testName;
            }
        }

        static bool tryName = false;
        private void StartTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                test.userName = C_UserName.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Выберите тест!");
            }
            if (C_UserName.Text == "" && !tryName)
            {
                MessageBox.Show("Если не указать имя, то вы продолжите как аноним!");
                tryName = true;
            }
            else
            {
                testWindow testWin = new testWindow(test);
                testWin.Show();
                this.Close();
            }

        }
    }
}
