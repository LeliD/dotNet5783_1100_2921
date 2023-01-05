using BlApi;
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


        public BO.Cart MyCart//הורדתי סימן שאלה
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
            if (cart.Items == null || cart.Items.Count() == 0)
            {
                tbEmptyCart.Visibility = Visibility.Visible;
                btnMakeAnOrder.Visibility = Visibility.Hidden;
            }
            lvCart.ItemsSource=cart.Items;
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem orderItem = (BO.OrderItem)((Button)sender).DataContext;//get the current orderItem
                BO.Product boProduct = bl.Product.ProductDetailsForManager(orderItem.ProductID);//get the product to add to the cart
                if (boProduct.InStock < orderItem.Amount + 1)
                {
                    Button? btnPlus = sender as Button;
                    if (btnPlus != null)
                        btnPlus.IsEnabled = false;
                    return;
                }
                MyCart = bl.Cart.UpdateAmountOfProductInCart(MyCart, orderItem.ProductID, orderItem.Amount + 1);
                lvCart.Items.Refresh();
                tbTotalPrice.Text = MyCart.TotalPrice.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem orderItem = (BO.OrderItem)((Button)sender).DataContext;
                MyCart = bl.Cart.UpdateAmountOfProductInCart(MyCart, orderItem.ProductID, orderItem.Amount - 1);
                lvCart.Items.Refresh();
                lvCart.ItemsSource = MyCart.Items;//in removing there is a need to reload the itemsSource since the product my be removed
                tbTotalPrice.Text = MyCart.TotalPrice.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.OrderItem orderItem = (BO.OrderItem)((Button)sender).DataContext;
                MyCart = bl.Cart.UpdateAmountOfProductInCart(MyCart, orderItem.ProductID, 0);
                lvCart.Items.Refresh();
                lvCart.ItemsSource = MyCart.Items;
                tbTotalPrice.Text = MyCart.TotalPrice.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            
            CatalogWindow cw = new CatalogWindow(MyCart);//create new ProductListWindow
            Close();
            cw.ShowDialog();
        }

        private void btnMakeAnOrder_Click(object sender, RoutedEventArgs e)
        {
            int orderId; 
            try
            {
                orderId=bl.Cart.MakeOrder(MyCart);
                MessageBox.Show("Your purchase was ended successfully!\n Thanks for buying at our store\n Your order id for tracking is:"+ orderId, "👍", MessageBoxButton.OK, MessageBoxImage.Information);
                MyCart.Items=new List<BO.OrderItem>();
                MyCart.TotalPrice=0;    
                tbEmptyCart.Visibility = Visibility.Visible;
                btnMakeAnOrder.Visibility = Visibility.Hidden;
                lvCart.Items.Refresh();
                lvCart.ItemsSource = MyCart.Items;
                tbTotalPrice.Text = MyCart.TotalPrice.ToString();
            }
            
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
