using System;
using System.Collections.Generic;
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



namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        /// <summary>
        /// bl is an instance of IBl
        /// </summary>
        BlApi.IBl bl = BlApi.Factory.Get();
        /// <summary>
        ///Build an instance of ProductListWindow
        /// </summary>
        public ProductListWindow()
        {
            //For enabling "All" option which isn't a formal category
            InitializeComponent();
            CategorySelector.Items.Add(BO.Category.BATHROOM);//Add cateroty to comboBox
            CategorySelector.Items.Add(BO.Category.KITCHEN);//Add cateroty to comboBox
            CategorySelector.Items.Add(BO.Category.BEDROOM);//Add cateroty to comboBox
            CategorySelector.Items.Add(BO.Category.LIVING_ROOM);//Add cateroty to comboBox
            CategorySelector.Items.Add(BO.Category.KIDS);//Add cateroty to comboBox
            CategorySelector.Items.Add("All");//Add cateroty to comboBox
            CategorySelector.SelectedItem = "All";
        }
        /// <summary>
        /// Category selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="BO.BlNullPropertyException"></exception>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowListOfProducts();
        }
        /// <summary>
        /// Click event.The function open the window of adding new product 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {

            ProductWindow pw = new ProductWindow();//create new ProductWindow
            Close(); 
            pw.ShowDialog();
        }
        /// <summary>
        /// function to show the products list of the selected catefory
        /// </summary>
        private void ShowListOfProducts()
        {
            BO.Category? category = CategorySelector.SelectedItem as BO.Category?;
            if (category == null)
            {
                productForListDataGrid.ItemsSource = bl.Product.GetListedProductsForManager();
            }
            else
            {
                productForListDataGrid.ItemsSource = bl.Product.GetListedProductsForManager(x => (BO.Category)(x?.Category)! == category);
            }
        }
        /// <summary>
        /// show products list every time when return to this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Activated(object sender, EventArgs e)
        {
            ShowListOfProducts();
        }
        /// <summary>
        /// Open Product Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            BO.ProductForList? productForList = productForListDataGrid.SelectedItem as BO.ProductForList;
            if (productForList != null)
            {
                ProductWindow pw = new ProductWindow(productForList.ID, GeneralMode.Editing);//create new ProductWindow of the selected product
                Close(); 
                pw.ShowDialog();
            }
        }
        /// <summary>
        /// Opens MainWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow plw = new MainWindow();//create new MainWindow
            Close();
            plw.ShowDialog();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow aw = new AdminWindow();//create new AdminWindow
            Close();
            aw.ShowDialog();
        }
    }
}