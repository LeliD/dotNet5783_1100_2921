using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();

        private ObservableCollection<BO.ProductItem?> productItems;
        public CatalogWindow()
        {
            InitializeComponent();
            productItems = new ObservableCollection<BO.ProductItem?>(bl.Product.GetListedProductsForCustomer());
            ListViewProducts.ItemsSource = productItems;
        }
    }
}
