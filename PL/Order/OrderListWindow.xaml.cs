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
        BlApi.IBl bl = BlApi.Factory.Get();

        public OrderListWindow()
        {
            InitializeComponent();
            ShowListOfOrders();
        }
        private void ShowListOfOrders()
        {
            orderForListDataGrid.ItemsSource = bl.Order.GetOrdersForManager();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            ShowListOfOrders();
        }

        private void orderForListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList? orderForList = orderForListDataGrid.SelectedItem as BO.OrderForList;
            if (orderForList != null)
            {
                OrderWindow ow = new OrderWindow(orderForList.ID,PL.GeneralMode.Editing);//create new OrderWindow of the selected order
                ow.ShowDialog();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow plw = new AdminWindow();//create new ProductListWindow
            plw.ShowDialog();
        }
    }
}
