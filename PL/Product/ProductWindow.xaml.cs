﻿using System;
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
using PL;
using BO;
using Microsoft.Win32;
using System.IO;
using System.Net.Security;
using PL.Order;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    enum Mode { ADD, UPDATE, CustomerDisplay } //3 states to open the window
    public partial class ProductWindow : Window
    {
        /// <summary>
        /// bl is an instance of IBl
        /// </summary>
        BlApi.IBl bl = BlApi.Factory.Get();
        Mode mode;
        GeneralMode generalMode; //for display or editing mode 
        int? orderId;
        public BO.Product? boProduct
        {
            get { return (BO.Product?)GetValue(boProductProperty); }
            set { SetValue(boProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for boProduct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty boProductProperty =
            DependencyProperty.Register("boProduct", typeof(BO.Product), typeof(ProductWindow), new PropertyMetadata(null));



        /// <summary>
        /// Constructor, Building an instance of ProductWindow 
        /// </summary>
        public ProductWindow()//GeneralMode.Editing and Mode.ADD
        {
            InitializeComponent();
            generalMode = PL.GeneralMode.Editing;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));//Initializes CategorySelector in Categories 
            btnAdd_UpdateProduct.Content = "Add";//Content of the botton is "Add" for adding a product
            btnAddImage.Content = "Add picture";//Content of the botton is  for adding an image
            mode = Mode.ADD;
            boProduct = new BO.Product();
            btnRemove.Visibility = Visibility.Hidden;//In adding there is no need of removing button
            orderId = null;
        }
        /// <summary>
        /// Constructor, Building an instance of ProductWindow 
        /// </summary>
        /// <param name="id">Id of product to initialize the ProductWindow</param>
        public ProductWindow(int id, GeneralMode modeG, int? oid=null)//Mode.UPDATE and GeneralMode is Editing for updating or displaying a product
        {
            InitializeComponent();
            generalMode = modeG;
            orderId = oid;
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            boProduct = bl.Product.ProductDetailsForManager(id);//Getting the product by its id
            tbId.IsEnabled = false; //Id isn't allowed to be changed
            if (generalMode == PL.GeneralMode.Editing)
            {
                btnRemove.Visibility = Visibility.Visible;
                btnAdd_UpdateProduct.Content = "Update";//Content of the botton is "Update" for Updating a product
                mode = Mode.UPDATE;
            }
            else //generalMode is for display only (showing profuct details for manager from dataGrid of orderItem)
            {
                mode = Mode.CustomerDisplay;
                btnRemove.Visibility = Visibility.Hidden;
                btnBack.Content = "← Back to Order";
                btnAdd_UpdateProduct.Visibility = Visibility.Hidden;
                btnAddImage.Visibility = Visibility.Hidden;
                tbName.IsEnabled = false;
                CategorySelector.IsEnabled = false;
                tbPrice.IsEnabled = false;
                tbInStock.IsEnabled = false;
            }

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
            int i = 0; // inStock of product
            BO.Category c = BO.Category.BATHROOM;
            double p = 0;
            bool isDataCorrect = true; //A variable for data check
            BO.Category.TryParse(CategorySelector.Text, out c);
            lblWrongId.Visibility = Visibility.Visible;
            lblWrongPrice.Visibility = Visibility.Visible;
            lblWrongInStock.Visibility = Visibility.Visible;
            lblWrongName.Visibility = Visibility.Visible;
            lblMissingCategory.Visibility = Visibility.Visible;
            if (tbId.Text == "") //If tbId is empty
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
            if (tbName.Text == "")//If tbName is empty
            {
                tbName.BorderBrush = Brushes.Red;
                lblWrongName.Content = "Missing Name";
                isDataCorrect = false;
            }
            if (!isDataCorrect)//If one of the arguments is wrong
                return;
            //The all arguments are correct:
            if (mode == Mode.ADD) //In case there is a need to add a product
            {
                try
                {
                    if (boProduct!.ImageRelativeName != null)
                    {
                        string imageName = boProduct.ImageRelativeName.Substring(boProduct.ImageRelativeName.LastIndexOf("\\")); //image path
                        if (!File.Exists(Environment.CurrentDirectory[..^4] + @"\Images\" + imageName)) //if image doesn't exist in images' file
                        {
                            File.Copy(boProduct.ImageRelativeName, Environment.CurrentDirectory[..^4] + @"\Images\" + imageName);//copy the image to images' file
                        }
                        boProduct.ImageRelativeName = @"\Images\" + imageName;
                    }
                    bl.Product.AddProduct(boProduct!);
                    MessageBox.Show("New Product was added successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ProductWindow pw = new ProductWindow(boProduct.ID, GeneralMode.Editing);//create new ProductWindow of the selected product
                    Close();
                    pw.ShowDialog();
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
                if (mode == Mode.UPDATE) //In case there is a need to update a product
                {
                    try
                    {
                        if (boProduct!.ImageRelativeName != null)
                        {
                            string imageName = boProduct.ImageRelativeName.Substring(boProduct.ImageRelativeName.LastIndexOf("\\")); //image path
                            if (!File.Exists(Environment.CurrentDirectory[..^4] + @"\Images\" + imageName)) //if image doesn't exist in images' file
                            {
                                File.Copy(boProduct.ImageRelativeName, Environment.CurrentDirectory[..^4] + @"\Images\" + imageName);//copy the image to images' file
                            }
                            boProduct.ImageRelativeName = @"\Images\" + imageName;
                        }

                        bl.Product.UpdateProduct(boProduct!);
                        MessageBox.Show("The Product was updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        
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
        /// TextChanged event of tbId textBox
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
        /// TextChanged event of tbName textBox 
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
        /// TextChanged event of tbPrice textBox 
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
        /// Button to remove product 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the product?", "ProductRemove", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    bl.Product.DeleteProduct(boProduct!.ID);
                    Close();
                }
                catch (BO.BlAlreadyExistEntityException ex)
                {
                    MessageBox.Show(ex.Message, "AlreadyExistEntityException", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
        /// Button to add image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Image files|*.png;*.jpg";//enables only files that end with pbj or jp
            if (o.ShowDialog() == true)
            {
                NewImage.ImageSource = new BitmapImage(new Uri(o.FileName));//take an image from the computer
                boProduct!.ImageRelativeName = o.FileName;
            }
        }
        /// <summary>
        /// Opens Product List or Order Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackToProductsListOrOrder_Click(object sender, RoutedEventArgs e)
        {
            if (generalMode == PL.GeneralMode.Editing)
            {
                ProductListWindow productListWindow = new ProductListWindow();//create new ProductListWindow
                Close();
                productListWindow.ShowDialog();
            }
            else
            {
                if(orderId!=null)
                {
                    OrderWindow ow = new OrderWindow((int)orderId, PL.GeneralMode.Editing);//create new OrderWindow of the selected order
                    Close();
                    ow.ShowDialog();                                                    
                }
            }

        }
        /// <summary>
        /// enable to key down only numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox? text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;
            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
            e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
            || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right || e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            //allow control system keys
            if (Char.IsControl(c)) return;
            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox
                            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
            return;
        }
       
    }
}

