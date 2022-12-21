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
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetListedProductsForManager();//Initialization of listView in list of products for manager
            CategorySelector.Items.Add(BO.Category.BATHROOM);//Add cateroty to comboBox
            CategorySelector.Items.Add(BO.Category.KITCHEN);//Add cateroty to comboBox
            CategorySelector.Items.Add(BO.Category.BEDROOM);//Add cateroty to comboBox
            CategorySelector.Items.Add(BO.Category.LIVING_ROOM);//Add cateroty to comboBox
            CategorySelector.Items.Add(BO.Category.KIDS);//Add cateroty to comboBox
            CategorySelector.Items.Add("All");//Add cateroty to comboBox
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="BO.BlNullPropertyException"></exception>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategorySelector.SelectedItem == "All")
                ProductListView.ItemsSource = bl.Product.GetListedProductsForManager();
            else
            {
                BO.Category category = (BO.Category)CategorySelector.SelectedItem;
                ProductListView.ItemsSource = bl.Product.GetListedProductsForManager(x => (BO.Category)((x?.Category) ?? throw new BO.BlNullPropertyException("Null Category")) == category);
            }

        }
        /// <summary>
        /// Click event.The function open the window of adding new product 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {

            ProductWindow pw = new ProductWindow();//create new ProductWindow
            pw.Show();
        }
        /// <summary>
        /// Click event. The function open the window of updating new product 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList p= (BO.ProductForList)ProductListView.SelectedItem;//the product that was selected
            ProductWindow pw = new ProductWindow(p.ID);//create new ProductWindow of the selected product
            pw.Show();
        }

      
    }
}