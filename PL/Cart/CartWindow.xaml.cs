using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        private BO.Cart myCart = new BO.Cart();
        public CartWindow(BO.Cart cart)
        {
            InitializeComponent();
            lvCart.ItemsSource=cart.Items;
            myCart=cart;    
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem orderItem=(BO.OrderItem)((Button)sender).DataContext;
            myCart=bl.Cart.UpdateAmountOfProductInCart(myCart, orderItem.ProductID, orderItem.Amount + 1);
            lvCart.Items.Refresh();
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem orderItem = (BO.OrderItem)((Button)sender).DataContext;
            myCart = bl.Cart.UpdateAmountOfProductInCart(myCart, orderItem.ProductID, orderItem.Amount - 1);
            lvCart.Items.Refresh();
            lvCart.ItemsSource = myCart.Items;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem orderItem = (BO.OrderItem)((Button)sender).DataContext;
            myCart = bl.Cart.UpdateAmountOfProductInCart(myCart, orderItem.ProductID, 0);
            lvCart.Items.Refresh();
            lvCart.ItemsSource = myCart.Items;
        }
     
    }
}
