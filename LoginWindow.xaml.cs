using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PolApp
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<User> users = MasterPolEntities1.GetContext().Users.ToList();
                var u = users.FirstOrDefault(p => p.loginUser == LoginBox.Text &&
                                                 p.passwordUser == PassBox.Password);

                if (u != null)
                {
                    Manager.currentUser = u;
                    MessageBox.Show("Авторизация пройдена");

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // а не ex.Message.ToString();
            }
        }
    }
}