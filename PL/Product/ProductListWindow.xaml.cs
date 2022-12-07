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
        IBl bl = new Bl();
        public ProductListWindow()
        {
            InitializeComponent();
            ProductListView.ItemsSource = bl.Product.GetListedProductsForManager(); ;
            //CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            CategorySelector.Items.Add(BO.Category.BATHROOM);
            CategorySelector.Items.Add(BO.Category.KITCHEN);
            CategorySelector.Items.Add(BO.Category.BEDROOM);
            CategorySelector.Items.Add(BO.Category.LIVING_ROOM);
            CategorySelector.Items.Add(BO.Category.KIDS);
            CategorySelector.Items.Add("None");
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategorySelector.SelectedItem=="None")
                ProductListView.ItemsSource = bl.Product.GetListedProductsForManager();
            else
            {
                BO.Category category = (BO.Category)CategorySelector.SelectedItem;
                ProductListView.ItemsSource = bl.Product.GetListedProductsForManager(x => (BO.Category)((x?.Category) ?? throw new BO.BlNullPropertyException("Null Category")) == category);
            }
               
            CategorySelector.Items.Remove(CategorySelector.SelectedItem);
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {

            ProductWindow pw = new ProductWindow();
            pw.Show();
        }

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //BO.Product p = (ProductListView)sender;
            
            BO.ProductForList p= (BO.ProductForList)ProductListView.SelectedItem;
            ProductWindow pw = new ProductWindow(p);
            pw.Show();
        }
    }
}