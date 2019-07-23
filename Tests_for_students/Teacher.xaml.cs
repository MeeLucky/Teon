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
    /// Логика взаимодействия для Teacher.xaml
    /// </summary>
    public partial class Teacher : Window
    {
        public Teacher()
        {
            InitializeComponent();
        }

        private void EditTest_Click (object v, RoutedEventArgs e)
        {
            Test test = new Test(null);

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
                test = Test.OpenTest(ofd.FileName);

            Create_EditTest CEt = new Create_EditTest(test);
            CEt.Show();
            this.Close();
        }

        private void CreateTest_Click (object v, RoutedEventArgs e)
        {
            Create_EditTest cet = new Create_EditTest(null);
            cet.Show();
            this.Close();
        }
    }
}
