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
            bool check;
            int id,i;
            BO.Category c;
            double p;
            check = int.TryParse(tbId.Text, out id);
            if (!check)
                MessageBox.Show("The value must be int");
            check = BO.Category.TryParse(CategorySelector.Text, out c);
            if (!check)
                MessageBox.Show("The value must be category");
            check = double.TryParse(tbPrice.Text, out p); 
            if (!check)
                MessageBox.Show("The value must be double");
            check = int.TryParse(tbInStock.Text, out i);
            if (!check)
                MessageBox.Show("The value must be int");
            try
            {
                bl.Product.AddProduct(new BO.Product()
                {
                    ID = id,
                    Category = c,
                    Name = tbName.Text,
                    Price=p,
                    InStock=i
                  
                    //Price = double.Parse(tbPrice.Text),
                    //InStock = int.Parse(tbInStock.Text)

                });
            }
            catch (BO.BlAlreadyExistEntityException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.BlDetailInvalidException ex)
            {
               MessageBox.Show(ex.Message);
            }
            catch(BO.BlWrongCategoryException ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            
        }
    }
}
