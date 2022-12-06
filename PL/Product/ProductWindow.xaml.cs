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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
using BlImplementation;
using BO;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        IBl bl = new Bl();
        public ProductWindow()
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            int id;
            bool check;
            check = int.TryParse(tbId.Text, out id);
            if (!check)
                MessageBox.Show("The value must be int");
            Category c;
            check = Category.TryParse(CategorySelector.Text, out c);
            if (!check)
                MessageBox.Show("The value must be int");
            bl.Product.AddProduct(new BO.Product() { ID= id,Category= (Convert)CategorySelector.Text, Name= tbName.Text,

            });
            
        }
    }
}
