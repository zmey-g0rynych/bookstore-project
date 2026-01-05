using System.Windows;

namespace WpfApp1
{
    public partial class WelcomeWindow : Window
    {
        public decimal EnteredBalance { get; private set; }
        public bool IsConfirmed { get; private set; } = false;

        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(BalanceInput.Text, out decimal value) && value >= 0)
            {
                var mainWindow = new MainWindow(value);
                mainWindow.Show();       // Показываем главное окно
                this.Close();            // Закрываем окно приветствия
            }
            else
            {
                MessageBox.Show("Введите корректную сумму.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }

}
