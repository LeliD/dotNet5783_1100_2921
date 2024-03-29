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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BO;
using PL.Cart;
namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        /// <summary>
        /// bl is an instance of IBl
        /// </summary>
        BlApi.IBl bl = BlApi.Factory.Get();
        /// <summary>
        /// customer cart
        /// </summary>
        private BO.Cart MyCart { get; set; }

        public BO.ProductItem? boProductItem
        {
            get { return (BO.ProductItem)GetValue(boProductItemProperty); }
            set { SetValue(boProductItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for boProductItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty boProductItemProperty =
            DependencyProperty.Register("boProductItem", typeof(BO.ProductItem), typeof(ProductItemWindow), new PropertyMetadata(null));

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cart"></param>
        public ProductItemWindow(int id,BO.Cart cart)
        {
            InitializeComponent();
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));//Initializes CategorySelector in Categories 
            boProductItem = bl.Product.ProductDetailsForCustomer(id,cart);
            MyCart = cart;
        }
        /// <summary>
        /// Button to add product item to cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyCart = bl.Cart.AddProductToCart(MyCart, boProductItem!.ID);
                boProductItem = bl.Product.ProductDetailsForCustomer(boProductItem.ID, MyCart);// חייב להביא אותו שוב בשביל שדה כמות שיתעדכן
                if (!boProductItem.InStock)
                    btnAddToCart.IsEnabled = false;
            }
            catch (BO.BlOutOfStockException)
            {
                btnAddToCart.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Back to Catalog Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CatalogWindow cw = new CatalogWindow(MyCart);//create new CatalogWindow
            Close();
            cw.ShowDialog();
        }
        /// <summary>
        /// Opens CartWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cw = new CartWindow(MyCart);//create new CartWindow
            Close();
            cw.ShowDialog();
        }
    }
}
