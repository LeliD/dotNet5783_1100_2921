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
using PL.Order;
using PL.Product;

namespace PL
{
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
    }
}
