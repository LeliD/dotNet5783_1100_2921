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
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        /// <summary>
        /// bl is an instance of IBl
        /// </summary>
        BlApi.IBl bl = BlApi.Factory.Get();


        public BO.OrderTracking? boOrderTracking
        {
            get { return (BO.OrderTracking?)GetValue(boOrderTrackingProperty); }
            set { SetValue(boOrderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for boOrderTracking.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty boOrderTrackingProperty =
            DependencyProperty.Register("boOrderTracking", typeof(BO.OrderTracking), typeof(OrderTrackingWindow), new PropertyMetadata(null));


        public OrderTrackingWindow()
        {
            InitializeComponent();
        }
        public OrderTrackingWindow(int id)
        {
            InitializeComponent();
            boOrderTracking=bl.Order.OrderTrack(id);
        }

        private void btnOrderDetails_Click(object sender, RoutedEventArgs e)
        {
            if(boOrderTracking!=null)
            {
                OrderWindow ow = new OrderWindow(boOrderTracking.ID, GeneralMode.Display);//create new ProductListWindow
                Close(); 
                ow.ShowDialog();
            }
           
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            MainWindow plw = new MainWindow();//create new ProductListWindow
            Close();
            plw.ShowDialog();
        }
    }
}
