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

        BlApi.IBl bl = BlApi.Factory.Get();
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void BtnProductList_Click(object sender, RoutedEventArgs e)
        {
            ProductListWindow plw = new ProductListWindow();//create new ProductListWindow
            plw.ShowDialog();
        }

        private void BtnOrderList_Click(object sender, RoutedEventArgs e)
        {
            OrderListWindow olw = new OrderListWindow();//create new ProductListWindow
            olw.ShowDialog();
        }
    }
}
