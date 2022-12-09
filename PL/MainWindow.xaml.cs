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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using BlImplementation;
using PL.Product;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Build an instance of MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///bl is an instance of IBl
        /// </summary>
        IBl bl = new Bl();
       
        public MainWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// button of list of products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnProduftList_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow plw = new ProductListWindow();//create new ProductListWindow
            plw.Show();
        }
    }
}
