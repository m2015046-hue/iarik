using System.Linq;
using System.Windows.Controls;

namespace PolApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductHistoryPage.xaml
    /// </summary>
    public partial class ProductHistoryPage : Page
    {
        public ProductHistoryPage(Partner selectedPartner)
        {
            InitializeComponent();
            DataContext = selectedPartner;
            //выводим только те продукты,сделанные тем партнером, на которого мы нажали
            HistoryDataGrid.ItemsSource = MasterPolEntities1.GetContext().Partner_products.Where(p => p.idPartner == selectedPartner.idPartner).ToList();
            CountText.Text = $"Общее количество реализованной продукции: {selectedPartner.TotalProductsSold}";
        }
    }
}
