﻿using BO;
using PL.Product;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();

        //private ObservableCollection<BO.ProductItem?> productItems;
        private BO.Cart MyCart { get; set; }
        public CatalogWindow(BO.Cart cart)
        {
            InitializeComponent();
            //productItems = new ObservableCollection<BO.ProductItem?>(bl.Product.GetListedProductsForCustomer());
            listViewProducts.ItemsSource = bl.Product.GetListedProductsForCustomer();
            MyCart = cart;
        }

        private void listViewProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem? productItem = listViewProducts.SelectedItem as BO.ProductItem;
            if (productItem != null)
            {
                ProductItemWindow pw = new ProductItemWindow(productItem.ID,MyCart);//create new ProductWindow of the selected product
                pw.ShowDialog();
            }
        }
    }
}
