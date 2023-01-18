using BO;
using PL.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing.IndexedProperties;
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


        public ObservableCollection<BO.ProductItem?> productItems
        {
            get { return (ObservableCollection<BO.ProductItem?>)GetValue(productItemsProperty); }
            set { SetValue(productItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productItemsProperty =
            DependencyProperty.Register("productItems", typeof(ObservableCollection<BO.ProductItem?>), typeof(CatalogWindow), new PropertyMetadata(null));

        //private BO.Cart MyCart { get; set; }


        public BO.Cart MyCart
        {
            get { return (BO.Cart)GetValue(MyCartProperty); }
            set { SetValue(MyCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyCartProperty =
            DependencyProperty.Register("MyCart", typeof(BO.Cart), typeof(CatalogWindow), new PropertyMetadata(null));



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cart"></param>
        public CatalogWindow(BO.Cart cart)
        {
            MyCart = cart;
            InitializeComponent();
            productItems = new ObservableCollection<BO.ProductItem?>(bl.Product.GetListedProductsForCustomer());
           // listViewProducts.ItemsSource = productItems;
            //listViewProducts.ItemsSource = bl.Product.GetListedProductsForCustomer();
           
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
        /// <summary>
        /// pring products from dal filtered by category 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblCategoryFilter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                TextBlock? textBox = sender as TextBlock;
                if (textBox != null)
                    productItems = new ObservableCollection<BO.ProductItem?>(bl.Product.GetListedProductsForCustomer(x => (BO.Category)(x?.Category)! == Enum.Parse<BO.Category>(textBox.Text.ToString())));

            }
            catch (Exception)
            {
                MessageBox.Show("ConverterError", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        /// <summary>
        /// bring all products from dal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblAll_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            productItems = new ObservableCollection<BO.ProductItem?>(bl.Product.GetListedProductsForCustomer());
        }
        /// <summary>
        /// grouping products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbGrouped_CheckedOrUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox? checkBox = sender as CheckBox;
            if (checkBox != null)
                if (checkBox.IsChecked == true)
                    productItems = new ObservableCollection<BO.ProductItem?>(bl.Product.GetGroupedListedProductsForCustomer());
                else
                    productItems = new ObservableCollection<BO.ProductItem?>(bl.Product.GetListedProductsForCustomer());
        }

      
    }
}
