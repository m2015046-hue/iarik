using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PolApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditPartnerPage.xaml
    /// </summary>
    public partial class AddEditPartnerPage : Page
    {
        Partner _currentPartner = new Partner();
        public AddEditPartnerPage(Partner selectedPartner)
        {
            InitializeComponent();
            if (selectedPartner != null)
            {//если мы редактируем
                _currentPartner = selectedPartner;
            }
            DataContext = _currentPartner;//переносим данные
            TypeBox.ItemsSource = MasterPolEntities1.GetContext().Partners_type.ToList();//данные для комбобокса
        }
        private StringBuilder CheckFileds()
        {//проверка на заполненные поля
            StringBuilder s = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentPartner.namePartner))
                s.AppendLine("Поле Название партнера пустое");
            if (string.IsNullOrWhiteSpace(_currentPartner.adress))
                s.AppendLine("Поле Адрес пустое");
            if (string.IsNullOrWhiteSpace(_currentPartner.phone))
                s.AppendLine("Поле Телефон пустое");
            if (_currentPartner.inn.ToString().Length != 10)
                s.AppendLine("Заполните поле ИНН правильно");
            if (string.IsNullOrWhiteSpace(_currentPartner.email))
                s.AppendLine("Поле Email пустое");
            if (_currentPartner.rate < 1 || _currentPartner.rate > 10)
                s.AppendLine("Рейтинг должен быть в диапазоне от 1 до 10");
            if (_currentPartner.rate is null)
                s.AppendLine("Поле рейтинг пустое");
            if (string.IsNullOrWhiteSpace(_currentPartner.surnameDirector))
                s.AppendLine("Поле Фамилия директора пустое");
            if (string.IsNullOrWhiteSpace(_currentPartner.nameDirector))
                s.AppendLine("Поле Имя директора пустое");
            if (string.IsNullOrWhiteSpace(_currentPartner.patronymicDirector))
                s.AppendLine("Поле Отчество директора пустое");
            if (_currentPartner.Partners_type is null)
                s.AppendLine("Выберите тип партнера");
            return s;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder _error = CheckFileds();//ошибка
            if (_error.Length > 0)
            {
                MessageBox.Show(_error.ToString());
                return;
            }
            if (_currentPartner.idPartner == 0)
            {//если новый пользователь, то добавляем в бд
                MasterPolEntities1.GetContext().Partners.Add(_currentPartner);
            }
            try
            {//сохраняем
                MasterPolEntities1.GetContext().SaveChanges();
                MessageBox.Show("Запись добавлена");
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }
    }
}
