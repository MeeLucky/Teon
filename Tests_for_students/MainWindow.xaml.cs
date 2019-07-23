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
            student teacher = new student();
            teacher.Show();
            this.Close();
        }

        private void Teacher_Click(object v, RoutedEventArgs e)
        {
            Teacher teacher = new Teacher();
            teacher.Show();
            this.Close();
        }
    }
}
