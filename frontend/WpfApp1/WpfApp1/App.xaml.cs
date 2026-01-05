using System;
using System.Windows;

namespace WpfApp1
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var welcomeWindow = new WelcomeWindow();
            welcomeWindow.Show();
        }
    }


}
