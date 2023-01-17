using BO;
using PL.Cart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Simulator.xaml
    /// </summary>
    
    public partial class SimulatorWindow : Window
    {
        /// <summary>
        /// bl is an instance of IBl
        /// </summary>
        BlApi.IBl bl = BlApi.Factory.Get();
        BackgroundWorker worker;
        private bool isTimerRun=true;
        public DateTime time;


        public ObservableCollection<BO.OrderForList?> orderForList
        {
            get { return (ObservableCollection<BO.OrderForList?>)GetValue(orderForListProperty); }
            set { SetValue(orderForListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for orderForList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderForListProperty =
            DependencyProperty.Register("orderForList", typeof(ObservableCollection<BO.OrderForList?>), typeof(SimulatorWindow), new PropertyMetadata(null));


       
        public SimulatorWindow()
        {
            InitializeComponent();
            orderForList = new ObservableCollection<BO.OrderForList?>(bl.Order.GetOrdersForManager());
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged; ;
            worker.WorkerReportsProgress = true; 
            worker.WorkerSupportsCancellation = true;

        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            
            var orders=bl.Order.GetOrdersForManager();
            BO.Order boOrder;
            TimeSpan shipTime = new TimeSpan(3, 0, 0, 0);//3 days
            TimeSpan deliveryTime = new TimeSpan(7, 0, 0, 0);//3 days
            foreach (var order in orders.ToList())
            {
                boOrder = bl.Order.GetOrderByID(order!.ID);
                if (boOrder.ShipDate == null)
                {
                    if (boOrder.OrderDate + shipTime <= time)
                    {
                        bl.Order.UpdateShipDate(boOrder.ID);
                        orderForList = new ObservableCollection<BO.OrderForList?>(bl.Order.GetOrdersForManager());
                    }
                }
                else
                    if (boOrder.DeliveryDate == null)
                {
                    if (boOrder.ShipDate + deliveryTime <= time)
                    {
                        bl.Order.UpdateDeliveryDate(boOrder.ID);
                        orderForList = new ObservableCollection<BO.OrderForList?>(bl.Order.GetOrdersForManager());
                    }
                }
                //Thread.Sleep(1000);
                
            }
           
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            time = DateTime.Now;
            while (isTimerRun)
            {
                worker.ReportProgress(1);
                Thread.Sleep(2000);
                time = time.AddDays(2);
            }
           
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (worker.IsBusy != true)
            // Start the asynchronous operation.
            {
                isTimerRun = true;
                worker.RunWorkerAsync();
            }
           
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (worker.WorkerSupportsCancellation == true)
            // Cancel the asynchronous operation.
            {
                isTimerRun = false;
                //worker.CancelAsync();
            }
                
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (worker.WorkerSupportsCancellation == true)
                isTimerRun = false;
            // Cancel the asynchronous operation.
            //worker.CancelAsync();

        }
    }
}
