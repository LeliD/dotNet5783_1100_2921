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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        /// <summary>
        /// bl is an instance of IBl
        /// </summary>
        BlApi.IBl bl = BlApi.Factory.Get();
        /// <summary>
        /// Constructor
        /// </summary>
        public OrderListWindow()
        {
            InitializeComponent();
            ShowListOfOrders();
        }
        /// <summary>
        /// function to show the orders list
        /// </summary>
        private void ShowListOfOrders()
        {
            orderForListDataGrid.ItemsSource = bl.Order.GetOrdersForManager();
        }
        /// <summary>
        /// show orders list every time when return to this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Activated(object sender, EventArgs e)
        {
            ShowListOfOrders();
        }
        /// <summary>
        /// Opens Order Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void orderForListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList? orderForList = orderForListDataGrid.SelectedItem as BO.OrderForList;
            if (orderForList != null)
            {
                OrderWindow ow = new OrderWindow(orderForList.ID,PL.GeneralMode.Editing);//create new OrderWindow of the selected order
                Close(); 
                ow.ShowDialog();
            }
        }
        /// <summary>
        /// Opens Admin Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow aw = new AdminWindow();//create new AdminWindow
            Close(); 
            aw.ShowDialog();
        }
        /// <summary>
        /// Opens Main Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow plw = new MainWindow();//create new MainWindow
            Close();
            plw.ShowDialog();
        }
    }
}
