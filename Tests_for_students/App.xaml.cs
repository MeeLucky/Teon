using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Tests_for_students
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            string fileName = e.Args?.FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                Test test = Test.OpenTest(fileName);
                new testWindow(test).Show();
            }
            else
            {
                new MainWindow().Show();
            }
                

            
        }
    }
}
