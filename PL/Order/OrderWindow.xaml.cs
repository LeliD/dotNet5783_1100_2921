using BO;
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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        /// <summary>
        /// bl is an instance of IBl
        /// </summary>
        BlApi.IBl bl = BlApi.Factory.Get();
        GeneralMode generalMode;
        public BO.Order? boOrder
        {
            get { return (BO.Order?)GetValue(boOrderProperty); }
            set { SetValue(boOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for boOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty boOrderProperty =
            DependencyProperty.Register("boOrder", typeof(BO.Order), typeof(OrderWindow), new PropertyMetadata(null));



        /// <summary>
        /// empty Constructor
        /// </summary>
        public OrderWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mode"></param>
        public OrderWindow(int id, GeneralMode mode)
        {

            InitializeComponent();
            generalMode = mode;
            boOrder = bl.Order.GetOrderByID(id);
            if (generalMode == PL.GeneralMode.Editing)//window fo admin
                if (boOrder.ShipDate == null)
                    btnUpdateDeliveryDateOrder.Visibility = Visibility.Hidden;
                else
                {
                    btnUpdateShipDateOrder.Visibility = Visibility.Hidden;
                    if (boOrder.DeliveryDate != null)
                        btnUpdateDeliveryDateOrder.Visibility = Visibility.Hidden;
                }
            else //window of customer
            {
                btnUpdateShipDateOrder.Visibility = Visibility.Hidden;
                btnUpdateDeliveryDateOrder.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// Button to update ship date of Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateShipDateOrder_Click(object sender, RoutedEventArgs e)
        {
            if(boOrder != null)
            {
                boOrder = bl.Order.UpdateShipDate(boOrder.ID);
            }
            btnUpdateShipDateOrder.Visibility = Visibility.Hidden;
            btnUpdateDeliveryDateOrder.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Button to update Delivery date of Order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateDeliveryDateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (boOrder != null)
            {
                boOrder = bl.Order.UpdateDeliveryDate(boOrder.ID);
            }
            btnUpdateDeliveryDateOrder.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// Mouse Double Click of orderItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void orderItemDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (generalMode == PL.GeneralMode.Display)//from customer tracking there is no option to press
                return;
            BO.OrderItem? orderItem = orderItemDataGrid.SelectedItem as BO.OrderItem;
            if (orderItem != null)
            {
                ProductWindow pw = new ProductWindow(orderItem.ProductID, GeneralMode.Display,boOrder.ID);//create new ProductWindow of the selected product
                Close(); 
                pw.ShowDialog();
            }
        }
        /// <summary>
        /// Back to OrderListWindow if it is an admin or to OrderTrackingWindow if it is a customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if(generalMode== PL.GeneralMode.Editing)
            {
                OrderListWindow ol=new OrderListWindow();//create new OrderListWindow
                Close();    
                ol.ShowDialog();    
            }
            else 
            {
                if(boOrder != null)
                {
                    OrderTrackingWindow ot = new OrderTrackingWindow(boOrder.ID);//create new OrderListWindow
                    Close();
                    ot.ShowDialog();
                }
             
            }
        }
    }
}
