using BO;
using PL.Product;
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
        /// <summary>
        /// bl is an instance of IBl
        /// </summary>
        BlApi.IBl bl = BlApi.Factory.Get();

        //private ObservableCollection<BO.ProductItem?> productItems;
        private BO.Cart MyCart { get; set; }
        //public BO.Cart MyCart
        //{
        //    get { return (BO.Cart)GetValue(cartProperty); }
        //    set { SetValue(cartProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for cart.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty cartProperty =
        //    DependencyProperty.Register("cart", typeof(BO.Cart), typeof(ProductItemWindow), new PropertyMetadata(null));
        public CatalogWindow(BO.Cart cart)
        {
            InitializeComponent();
            //productItems = new ObservableCollection<BO.ProductItem?>(bl.Product.GetListedProductsForCustomer());
            listViewProducts.ItemsSource = bl.Product.GetListedProductsForCustomer();
            MyCart = cart;
        }
        /// <summary>
        /// Opens ProductItem Window of the selected product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem? productItem = listViewProducts.SelectedItem as BO.ProductItem;//the selected product
            if (productItem != null)
            {
                ProductItemWindow pw = new ProductItemWindow(productItem.ID,MyCart);//create new ProductWindow of the selected product
                Close(); 
                pw.ShowDialog();
            }
        }
        /// <summary>
        /// Opens Cart Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGoToCart_Click(object sender, RoutedEventArgs e)
        {
           CartWindow cw = new CartWindow(MyCart);//create new CartWindow
           Close(); 
           cw.ShowDialog();
        }
        /// <summary>
        /// Opens Main Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow plw = new MainWindow();//create new MainWindow
            Close(); 
            plw.ShowDialog();
        }

      
    }
}
