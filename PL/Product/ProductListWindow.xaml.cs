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
using BlApi;
using BlImplementation;

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
        IBl bl = new Bl();
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
            CategorySelector.Items.Add("None");//Add cateroty to comboBox
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategorySelector.SelectedItem == "None")
                ProductListView.ItemsSource = bl.Product.GetListedProductsForManager();
            else
            {
                BO.Category category = (BO.Category)CategorySelector.SelectedItem;
                ProductListView.ItemsSource = bl.Product.GetListedProductsForManager(x => (BO.Category)((x?.Category) ?? throw new BO.BlNullPropertyException("Null Category")) == category);
            }

            
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {

            ProductWindow pw = new ProductWindow();
            pw.Show();
        }

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList p= (BO.ProductForList)ProductListView.SelectedItem;
            ProductWindow pw = new ProductWindow(p.ID);
            pw.Show();
        }

      
    }
}