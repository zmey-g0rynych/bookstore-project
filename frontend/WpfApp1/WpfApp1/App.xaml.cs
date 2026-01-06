using System;
using System.Windows;

namespace WpfApp1
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Язык по умолчанию (пока пользователь не выбрал регион)
            LoadLanguage("ru-RU");

            var welcomeWindow = new WelcomeWindow();
            welcomeWindow.Show();
        }

        public static void LoadLanguage(string language)
        {
            var dict = new ResourceDictionary();

            switch (language)
            {
                case "ru-RU":
                    dict.Source = new Uri("Resources/Strings.ru.xaml", UriKind.Relative);
                    break;

                case "uk-UA":
                    dict.Source = new Uri("Resources/Strings.ua.xaml", UriKind.Relative);
                    break;

                case "en-GB":
                    dict.Source = new Uri("Resources/Strings.en.xaml", UriKind.Relative);
                    break;
            }

            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dict);
        }
    }
}
