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
        public ProductWindow(BO.ProductForList p)
        {
            
            InitializeComponent();
            tbId.Text = p.ID.ToString();
            CategorySelector.SelectedItem = p.Category;
            tbName.Text = p.Name;
            tbPrice.Text = p.Price.ToString();
            tbInStock.Text = bl.Product.ProductDetailsForManager(p.ID).InStock.ToString();
               
        }
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            bool check;
            int id, i;
            BO.Category c;
            double p;
            check = int.TryParse(tbId.Text, out id);
            if (!check)
            {
                MessageBox.Show("The field id must be int");
                return;
            }
            BO.Category.TryParse(CategorySelector.Text, out c);
          
            check = double.TryParse(tbPrice.Text, out p);
            if (!check)
            {
                MessageBox.Show("The field price must be double");
                return;
            }
            check = int.TryParse(tbInStock.Text, out i);
            if (!check)
            {
                MessageBox.Show("The field InStock must be int");
                return;
            }
            try
            {
                bl.Product.AddProduct(new BO.Product()
                {
                    ID = id,
                    Category=c,
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
