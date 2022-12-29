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

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {

        BlApi.IBl bl = BlApi.Factory.Get();
        public BO.ProductItem? boProductItem
        {
            get { return (BO.ProductItem)GetValue(boProductItemProperty); }
            set { SetValue(boProductItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for boProductItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty boProductItemProperty =
            DependencyProperty.Register("boProductItem", typeof(BO.ProductItem), typeof(ProductItemWindow), new PropertyMetadata(null));


        public ProductItemWindow()
        {
            InitializeComponent();
        }
        public ProductItemWindow(int id,BO.Cart cart)
        {
            InitializeComponent();
            boProductItem = bl.Product.ProductDetailsForCustomer(id,cart);
        }
    }
}
