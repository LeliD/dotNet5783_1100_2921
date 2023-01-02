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


        public BO.Cart? MyCart
        {
            get { return (BO.Cart)GetValue(cartProperty); }
            set { SetValue(cartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for cart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cartProperty =
            DependencyProperty.Register("cart", typeof(BO.Cart), typeof(CartWindow), new PropertyMetadata(null));


        //private BO.Cart myCart = new BO.Cart();
        public CartWindow(BO.Cart cart)
        {
            MyCart = cart;
            InitializeComponent();
            lvCart.ItemsSource=cart.Items;
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
            MyCart=bl.Cart.UpdateAmountOfProductInCart(MyCart, orderItem.ProductID, orderItem.Amount + 1);
            lvCart.Items.Refresh();
            tbTotalPrice.Text = MyCart.TotalPrice.ToString();
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem orderItem = (BO.OrderItem)((Button)sender).DataContext;
            MyCart = bl.Cart.UpdateAmountOfProductInCart(MyCart, orderItem.ProductID, orderItem.Amount - 1);
            lvCart.Items.Refresh();
            lvCart.ItemsSource = MyCart.Items;
            tbTotalPrice.Text = MyCart.TotalPrice.ToString();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem orderItem = (BO.OrderItem)((Button)sender).DataContext;
            MyCart = bl.Cart.UpdateAmountOfProductInCart(MyCart, orderItem.ProductID, 0);
            lvCart.Items.Refresh();
            lvCart.ItemsSource = MyCart.Items;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            CatalogWindow cw = new CatalogWindow(MyCart);//create new ProductListWindow
            cw.ShowDialog();
        }
    }
}
