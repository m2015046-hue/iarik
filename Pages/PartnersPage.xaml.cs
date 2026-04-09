using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PolApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PartnersPage.xaml
    /// </summary>
    public partial class PartnersPage : Page
    {
        public PartnersPage()
        {
            InitializeComponent();
            ListBoxPartners.ItemsSource = MasterPolEntities1.GetContext().Partners.ToList();
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPartnerPage((sender as Button).DataContext as Partner));//переносим данные
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditPartnerPage(null));//не переносим данные
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPartner = ListBoxPartners.SelectedItems.Cast<Partner>().ToList();
            MessageBoxResult messageBoxResult = MessageBox.Show($"Удалить {selectedPartner.Count()} записей? ", "Удаление", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                try
                {//удаляем выбранное количество записей
                    Partner x = selectedPartner[0];
                    MasterPolEntities1.GetContext().Partners.Remove(x);
                    MasterPolEntities1.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены");
                    List<Partner> partners = MasterPolEntities1.GetContext().Partners.ToList();
                    ListBoxPartners.ItemsSource = null;//обновление списка
                    ListBoxPartners.ItemsSource = partners;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {//если была добавлена новая записть, то страница обновляется и показывает актуальные данные
                MasterPolEntities1.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ListBoxPartners.ItemsSource = MasterPolEntities1.GetContext().Partners.ToList();
            }
        }
        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductHistoryPage((sender as Button).DataContext as Partner));//переносим данные
        }
    }
}
