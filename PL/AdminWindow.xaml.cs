using PL.Order;
using PL.Product;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        /// <summary>
        /// bl is an instance of IBl
        /// </summary>
        BlApi.IBl bl = BlApi.Factory.Get();
        /// <summary>
        /// Constructor,Building an instance of AdminWindow
        /// </summary>
        public AdminWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Opens Product List Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnProductList_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow plw = new ProductListWindow();//create new ProductListWindow
            Close(); 
            plw.ShowDialog();
        }
        /// <summary>
        /// Opens Order List Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOrderList_Click(object sender, RoutedEventArgs e)
        {
            OrderListWindow olw = new OrderListWindow();//create new OrderListWindow
            Close(); 
            olw.ShowDialog();
        }
        /// <summary>
        /// Button to add a new admin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddManager_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow(AdminAccess.Yes);//create new SignUpWindow
            //Close(); ?
            signUpWindow.ShowDialog();
        }
        /// <summary>
        /// Opens Main Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow plw = new MainWindow();//create new ProductListWindow
            Close();
            plw.ShowDialog();
        }

    }
}
