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
        /// <summary>
        /// A Thread
        /// </summary>
        BackgroundWorker worker;
        /// <summary>
        /// isTimerRun for thread running
        /// </summary>
        private bool isTimerRun;
        /// <summary>
        /// time of the thread
        /// </summary>
        public DateTime time;

        /// <summary>
        /// Orders for Manager
        /// </summary>
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
            orderForList = new ObservableCollection<BO.OrderForList?>(bl.Order.GetOrdersForManager());//Orders For Manager
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged; ;
            worker.WorkerReportsProgress = true; //Progress
            worker.WorkerSupportsCancellation = true;//Cancellation
        }
        /// <summary>
        /// Worker_ProgressChanged for the worker thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            var orders = bl.Order.GetOrdersForManager();//Orders For Manager
            BO.Order boOrder;//A single order
            TimeSpan shipTime = new TimeSpan(3, 0, 0, 0);//3 days for shipping time range
            TimeSpan deliveryTime = new TimeSpan(7, 0, 0, 0);//7 days for delivering time range
            foreach (var order in orders.ToList())//Passes over the orders
            {
                boOrder = bl.Order.GetOrderByID(order!.ID); //get order from dal
                if (boOrder.ShipDate == null)
                {
                    if (boOrder.OrderDate + shipTime <= time)//time of shipping has come
                    {
                        bl.Order.UpdateShipDate(boOrder.ID);//Update its ShipDate
                        //orderForList = new ObservableCollection<BO.OrderForList?>(bl.Order.GetOrdersForManager());//bring the list again from dal
                    }
                }
                else
                    if (boOrder.DeliveryDate == null)
                {
                    if (boOrder.ShipDate + deliveryTime <= time)//time of delivering has come
                    {
                        bl.Order.UpdateDeliveryDate(boOrder.ID);//Update its DeliveryDate
                        //orderForList = new ObservableCollection<BO.OrderForList?>(bl.Order.GetOrdersForManager());//bring the list again from dal
                    }
                }
                orderForList = new ObservableCollection<BO.OrderForList?>(bl.Order.GetOrdersForManager());//bring the list again from dal

                //Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// Worker_DoWork for the worker thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            time = DateTime.Now; //time initializes in DateTime.Now
            while (isTimerRun)
            {
                worker.ReportProgress(1);
                Thread.Sleep(2000);
                time = time.AddHours(17);//Promoting time in t
            }
        }
        /// <summary>
        /// Starting the thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (worker.IsBusy != true)
            // Start the asynchronous operation.
            {
                isTimerRun = true;
                worker.RunWorkerAsync();
            }
           
        }
        /// <summary>
        /// Stoping the thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (worker.WorkerSupportsCancellation == true)
                // Raising the flag to stop the asynchronous operation.
                isTimerRun = false;
        }

        /// <summary>
        /// Stoping the thread by window closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (worker.WorkerSupportsCancellation == true)
            {
                // Raising the flag to stop the asynchronous operation.
                isTimerRun = false;
            }
        }
    }
}
