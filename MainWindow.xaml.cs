using System;
using System.Windows;

namespace PolApp
{
    public partial class MainWindow : Window
    {
        private User _user;

        public MainWindow()
        {
            InitializeComponent();

            _user = Manager.currentUser;
            if (_user == null)
            {
                // если по какой-то причине пользователь не задан — возвращаемся на логин
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                Close();
                return;
            }

            LoginText.Text =
                $"Роль: {_user.Role.nameRole}\n" +
                $"Логин: {_user.loginUser}";

            if (_user.idRole == 1)
            {
                // менеджер -> партнеры
                MainFrame.NavigationService.Navigate(new Pages.PartnersPage());
            }
            else
            {
                // партнер -> продукция
                MainFrame.NavigationService.Navigate(new Pages.ProductsPage());
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // сброс пользователя (важно ещё сбросить глобально, если ты его там хранишь)
            Manager.currentUser = null;
            _user = null;

            var loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
                MainFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            BackButton.Visibility = MainFrame.CanGoBack
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}