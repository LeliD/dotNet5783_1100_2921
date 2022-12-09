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
using DO;

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
            btnAdd_UpdateProduct.Content = "Add";
            
        }
      
        public ProductWindow(int id)
        {

            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            BO.Product p = bl.Product.ProductDetailsForManager(id);
            tbId.Text = p.ID.ToString();
            CategorySelector.SelectedItem = p.Category;
            tbName.Text = p.Name;
            tbPrice.Text = p.Price.ToString();
            tbInStock.Text = p.InStock.ToString();
            tbId.IsEnabled = false;
            btnAdd_UpdateProduct.Content = "Update";
        }

        private void btnAdd_UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            bool check;
            int id=0, i=0;
            BO.Category c=BO.Category.BATHROOM;
            double p = 0;
            bool isDataCorrect = true;
            BO.Category.TryParse(CategorySelector.Text, out c);
            lblWrongId.Visibility = Visibility.Visible;
            lblWrongPrice.Visibility = Visibility.Visible;
            lblWrongInStock.Visibility = Visibility.Visible;
            lblWrongName.Visibility = Visibility.Visible;
            lblMissingCategory.Visibility = Visibility.Visible;
            if (tbId.Text=="")
            {
                tbId.BorderBrush = Brushes.Red;
                lblWrongId.Content = "Missing Id";
                isDataCorrect = false;
            }
            else
            {
                check = int.TryParse(tbId.Text, out id);
                if (!check)
                {
                    //MessageBox.Show("The field id must be int","LogicError",MessageBoxButton.OK,MessageBoxImage.Error);
                    tbId.BorderBrush = Brushes.Red;
                    lblWrongId.Content = "Wrong Id";
                    isDataCorrect = false;
                }
            }
            if (tbPrice.Text == "")
            {
                tbPrice.BorderBrush = Brushes.Red;
                lblWrongPrice.Content = "Missing Price";
                isDataCorrect = false;
            }
            else
            {
                check = double.TryParse(tbPrice.Text, out p);
                if (!check)
                {
                    tbPrice.BorderBrush = Brushes.Red;
                    lblWrongPrice.Content = "Wrong Price";
                    isDataCorrect = false;
                }
            }
            if (tbInStock.Text == "")
            {
                tbInStock.BorderBrush = Brushes.Red;
                lblWrongInStock.Content = "Missing InStock";
                isDataCorrect = false;
            }
            else
            {
                check = int.TryParse(tbInStock.Text, out i);
                if (!check)
                {
                    tbInStock.BorderBrush = Brushes.Red;
                    lblWrongInStock.Content = "Wrong InStock";
                    isDataCorrect = false;
                }
            }
            if(tbName.Text=="")
            {
                tbName.BorderBrush = Brushes.Red;
                lblWrongName.Content = "Missing Name";
                isDataCorrect = false;
            }
            if (!isDataCorrect)
                return;
            if (btnAdd_UpdateProduct.Content == "Add")
            {
                try
                {
                    bl.Product.AddProduct(new BO.Product()
                    {
                        ID = id,
                        Category = c,
                        Name = tbName.Text,
                        Price = p,
                        InStock = i

                    });
                    MessageBox.Show("New Product was added successfully");
                    Close();
                }
                catch (BO.BlAlreadyExistEntityException ex)
                {
                    MessageBox.Show(ex.Message, "AlreadyExistEntityException", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BlDetailInvalidException ex)
                {
                    MessageBox.Show(ex.Message, "DetailInvalidException", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BlWrongCategoryException ex)
                {
                    MessageBox.Show(ex.Message, "WrongCategoryException", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
                 if (btnAdd_UpdateProduct.Content == "Update")
            {
                try
                {
                    bl.Product.UpdateProduct(new BO.Product()
                    {
                        ID = id,
                        Category = c,
                        Name = tbName.Text,
                        Price = p,
                        InStock = i

                        //Price = double.Parse(tbPrice.Text),
                        //InStock = int.Parse(tbInStock.Text)

                    });
                    MessageBox.Show("The Product was added successfully");
                    Close();
                }
                catch (BO.BlMissingEntityException ex)
                {
                    MessageBox.Show(ex.Message, "MissingEntityException", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BlDetailInvalidException ex)
                {
                    MessageBox.Show(ex.Message, "DetailInvalidException", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (BO.BlWrongCategoryException ex)
                {
                    MessageBox.Show(ex.Message, "WrongCategoryException", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }


        }

        //private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        //{
        //    bool check;
        //    int id, i;
        //    BO.Category c;
        //    double p;
        //    int.TryParse(tbId.Text, out id);

        //    BO.Category.TryParse(CategorySelector.Text, out c);

        //    check = double.TryParse(tbPrice.Text, out p);
        //    if (!check)
        //    {
        //        MessageBox.Show("The field price must be double", "LogicError", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return;
        //    }
        //    check = int.TryParse(tbInStock.Text, out i);
        //    if (!check)
        //    {
        //        MessageBox.Show("The field InStock must be int", "LogicError", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return;
        //    }
        //    try
        //    {
        //        bl.Product.UpdateProduct(new BO.Product()
        //        {
        //            ID = id,
        //            Category = c,
        //            Name = tbName.Text,
        //            Price = p,
        //            InStock = i

        //            Price = double.Parse(tbPrice.Text),
        //            InStock = int.Parse(tbInStock.Text)

        //        });
        //        MessageBox.Show("The Product was added successfully");
        //        Close();
        //    }
        //    catch (BO.BlMissingEntityException ex)
        //    {
        //        MessageBox.Show(ex.Message, "MissingEntityException", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    catch (BO.BlDetailInvalidException ex)
        //    {
        //        MessageBox.Show(ex.Message, "DetailInvalidException", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //    catch (BO.BlWrongCategoryException ex)
        //    {
        //        MessageBox.Show(ex.Message, "WrongCategoryException", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }

        //}

        private void tbId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbId.BorderBrush == Brushes.Red)
            {
                tbId.BorderBrush = Brushes.DimGray;
                lblWrongId.Visibility = Visibility.Hidden;
            }
        }

        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbName.BorderBrush == Brushes.Red)
            {
                tbName.BorderBrush = Brushes.DimGray;
                lblWrongName.Visibility = Visibility.Hidden;
            }
        }

        private void tbPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbPrice.BorderBrush == Brushes.Red)
            {
                tbPrice.BorderBrush = Brushes.DimGray;
                lblWrongPrice.Visibility = Visibility.Hidden;
            }
        }

        private void tbInStock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbInStock.BorderBrush == Brushes.Red)
            {
                tbInStock.BorderBrush = Brushes.DimGray;
                lblWrongInStock.Visibility = Visibility.Hidden;
            }
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblMissingCategory.Visibility = Visibility.Hidden;
        }
    }
}
