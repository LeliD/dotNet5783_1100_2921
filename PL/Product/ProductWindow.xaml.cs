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

using BO;


namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    enum Mode {ADD,UPDATE}
    public partial class ProductWindow : Window
    {
        /// <summary>
        /// bl is an instance of IBl
        /// </summary>
        BlApi.IBl bl = BlApi.Factory.Get();
        Mode mode;


        public BO.Product? boProduct
        {
            get { return (BO.Product?)GetValue(boProductProperty); }
            set { SetValue(boProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for boProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty boProductProperty =
            DependencyProperty.Register("boProduct", typeof(BO.Product), typeof(ProductWindow), new PropertyMetadata(null));



        /// <summary>
        /// Building an instance of ProductWindow 
        /// </summary>
        public ProductWindow()
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));//Initializes CategorySelector in Categories 
            btnAdd_UpdateProduct.Content = "Add";//Content of the botton is "Add" for adding a product
            mode = Mode.ADD;
            boProduct=new BO.Product();
        }
        /// <summary>
        /// Building an instance of ProductWindow 
        /// </summary>
        /// <param name="id">Id of product to initialize the ProductWindow</param>
        public ProductWindow(int id,PL.Mode generalMode)
        {

          InitializeComponent();
          CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
          boProduct=bl.Product.ProductDetailsForManager(id);

            //    BO.Product p = bl.Product.ProductDetailsForManager(id);//Getting the product by its id
            //    tbId.Text = p.ID.ToString();
            //    CategorySelector.SelectedItem = p.Category;
            //    tbName.Text = p.Name;
            //    tbPrice.Text = p.Price.ToString();
            //    tbInStock.Text = p.InStock.ToString();
           tbId.IsEnabled = false; //Id isn't allowed to be changed
           btnAdd_UpdateProduct.Content = "Update";//Content of the botton is "Update" for Updating a product
           mode = Mode.UPDATE;
        }
        /// <summary>
        /// Click event. The function adds or updates product according to the window's openning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            bool check; //A variable for checking try parsing functions
            int id = 0; //id of product
            int i=0; // inStock of product
            BO.Category c=BO.Category.BATHROOM;
            double p = 0;
            bool isDataCorrect = true; //A variable for data check
            BO.Category.TryParse(CategorySelector.Text, out c);
            //lblWrongId.Content = ""; //Initializes the content of lblWrongId to be empty
            lblWrongId.Visibility = Visibility.Visible;
            //lblWrongPrice.Content = ""; //Initializes the content of lblWrongPrice to be empty
            lblWrongPrice.Visibility = Visibility.Visible;
            //lblWrongInStock.Content = ""; //Initializes the content of lblWrongInStock to be empty
            lblWrongInStock.Visibility = Visibility.Visible;
            //lblWrongName.Content = ""; //Initializes the content of lblWrongName to be empty
            lblWrongName.Visibility = Visibility.Visible;
            //lblMissingCategory.Content = ""; //Initializes the content of lblMissingCategory to be empty
            lblMissingCategory.Visibility = Visibility.Visible;
            if (tbId.Text=="") //If tbId is empty
            {
                tbId.BorderBrush = Brushes.Red;
                lblWrongId.Content = "Missing Id";
                isDataCorrect = false;
            }
            else //If tbId isn't empty
            {
                check = int.TryParse(tbId.Text, out id);
                if (!check)
                {
                    tbId.BorderBrush = Brushes.Red;
                    lblWrongId.Content = "Wrong Id";
                    isDataCorrect = false;
                }
            }
            if (tbPrice.Text == "") //If tbPrice is empty
            {
                tbPrice.BorderBrush = Brushes.Red;
                lblWrongPrice.Content = "Missing Price";
                isDataCorrect = false;
            }
            else   //If tbPrice isn't empty
            {
                check = double.TryParse(tbPrice.Text, out p);
                if (!check || p < 0)
                {
                    tbPrice.BorderBrush = Brushes.Red;
                    lblWrongPrice.Content = "Wrong Price";
                    isDataCorrect = false;
                }
            }
            if (tbInStock.Text == "")//If InStock is empty
            {
                tbInStock.BorderBrush = Brushes.Red;
                lblWrongInStock.Content = "Missing InStock";
                isDataCorrect = false;
            }
            else //If InStock isn't empty
            {
                check = int.TryParse(tbInStock.Text, out i);
                if (!check || i < 0)
                {
                    tbInStock.BorderBrush = Brushes.Red;
                    lblWrongInStock.Content = "Wrong InStock";
                    isDataCorrect = false;
                }
            }
            if(tbName.Text=="")//If tbName is empty
            {
                tbName.BorderBrush = Brushes.Red;
                lblWrongName.Content = "Missing Name";
                isDataCorrect = false;
            }
            if (!isDataCorrect)//If one of the arguments is wrong
                return;
            //The all arguments are correct:
            if (mode== Mode.ADD) //In case there is a need to add a product
            {
                try
                {
                    bl.Product.AddProduct(boProduct!);
                    MessageBox.Show("New Product was added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
            {
                if (mode==Mode.UPDATE) //In case there is a need to update a product
                {
                    try
                    {
                        bl.Product.UpdateProduct(boProduct!);
                        MessageBox.Show("The Product was updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
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
        }



        /// <summary>
        /// TextChanged event of tbId textBow
        /// The function turns off the tbId.BorderBrush and makes its suitable label hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbId.BorderBrush == Brushes.Red)
            {
                tbId.BorderBrush = Brushes.DimGray;
                lblWrongId.Content = "";
                lblWrongId.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// TextChanged event of tbName textBow 
        /// The function turns off the tbName.BorderBrush and makes its suitable label hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbName.BorderBrush == Brushes.Red)
            {
                tbName.BorderBrush = Brushes.DimGray;
                lblWrongName.Content = "";
                lblWrongName.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// TextChanged event of tbPrice textBow 
        /// The function turns off the tbPrice.BorderBrush and makes its suitable label hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbPrice.BorderBrush == Brushes.Red)
            {
                tbPrice.BorderBrush = Brushes.DimGray;
                lblWrongPrice.Content = "";
                lblWrongPrice.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// TextChanged event of tbPrice tbInStock 
        /// The function turns off the tbInStock.BorderBrush and makes its suitable label hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbInStock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbInStock.BorderBrush == Brushes.Red)
            {
                tbInStock.BorderBrush = Brushes.DimGray;
                lblWrongInStock.Content = "";
                lblWrongInStock.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// SelectionChanged event of CategorySelector
        /// The function makes lblMissingCategory label hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblMissingCategory.Content = "";
            lblMissingCategory.Visibility = Visibility.Hidden; 
        }
    }
}
