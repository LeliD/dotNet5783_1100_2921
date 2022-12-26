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
        BlApi.IBl bl = BlApi.Factory.Get();
        public BO.Order? boOrder
        {
            get { return (BO.Order?)GetValue(boOrderProperty); }
            set { SetValue(boOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for boOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty boOrderProperty =
            DependencyProperty.Register("boOrder", typeof(BO.Order), typeof(OrderWindow), new PropertyMetadata(null));




        public OrderWindow()
        {
            InitializeComponent();
        }

        public OrderWindow(int id)
        {

            InitializeComponent();
            //CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            boOrder = bl.Order.GetOrderByID(id);


            //tbId.IsEnabled = false; //Id isn't allowed to be changed
            //btnAdd_UpdateProduct.Content = "Update";//Content of the botton is "Update" for Updating a product
        }

    }
}
