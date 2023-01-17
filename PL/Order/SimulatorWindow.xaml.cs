using BO;
using PL.Cart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Simulator.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {
        /// <summary>
        /// bl is an instance of IBl
        /// </summary>
        BlApi.IBl bl = BlApi.Factory.Get();


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
        }
    }
}
