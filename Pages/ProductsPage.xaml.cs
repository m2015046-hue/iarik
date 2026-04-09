using System.Linq;
using System.Windows.Controls;

namespace PolApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();
            ListBoxProducts.ItemsSource = MasterPolEntities1.GetContext().Products.ToList();
        }
    }
}
