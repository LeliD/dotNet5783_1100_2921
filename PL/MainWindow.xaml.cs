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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.Cart;
using PL.Order;
using PL.Product;

namespace PL
{
    public enum GeneralMode {Editing,Display}
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///bl is an instance of IBl
        /// </summary>
        BlApi.IBl bl = BlApi.Factory.Get();

        public MainWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Click event. The function button of openning the window's list of products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnProduftList_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow plw = new AdminWindow();//create new ProductListWindow
            plw.ShowDialog();
        }

        private void btnTracking_Click(object sender, RoutedEventArgs e)
        {
            int id;
            bool check = int.TryParse(tbIDOrderTrack.Text, out id);
            if(check)
            {
                OrderTrackingWindow otw = new OrderTrackingWindow(id);//create new ProductListWindow
                otw.ShowDialog();
            }
            
        }

        private void tbIDOrderTrack_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;
            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
            e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
            || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right|| e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
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

        private void btnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            BO.Cart cart=new BO.Cart(); 
            CatalogWindow cw = new CatalogWindow(cart);//create new ProductListWindow
            cw.ShowDialog();
        }
    }
}
