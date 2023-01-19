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


        public List<BO.OrderForList?> ordersList
        {
            get { return (List<BO.OrderForList?>)GetValue(ordersListProperty); }
            set { SetValue(ordersListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ordersList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ordersListProperty =
            DependencyProperty.Register("ordersList", typeof(List<BO.OrderForList?>), typeof(OrderListWindow), new PropertyMetadata(null));


        /// <summary>
        /// Constructor
        /// </summary>
        public OrderListWindow()
        {
            InitializeComponent();
            //window activited
        }
        /// <summary>
        /// function to show the orders list
        /// </summary>
        private void ShowListOfOrders()
        {
            ordersList= bl.Order.GetOrdersForManager().ToList();
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
        /// <summary>
        /// Opens Order Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderForList? orderForList = orderForListDataGrid.SelectedItem as BO.OrderForList;
            if (orderForList != null)
            {
                OrderWindow ow = new OrderWindow(orderForList.ID, PL.GeneralMode.Editing);//create new OrderWindow of the selected order
                Close();
                ow.ShowDialog();
            }
        }
    }
}
