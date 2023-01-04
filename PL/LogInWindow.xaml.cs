using PL.Cart;
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
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        public LogInWindow()
        {
            InitializeComponent();
        }
        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userName = tbUserName.Text;
                string passcode = pbPasscode.Password;
                BO.User user = bl.User.GetById(userName);
                if (user.AdminAccess)
                {
                    AdminWindow aw = new AdminWindow();//create new ProductListWindow
                    aw.ShowDialog();
                }
                else
                {
                    BO.Cart cart = new BO.Cart() { CustomerAddress=user.UserAddress, CustomerEmail=user.UserEmail, CustomerName=user.Name , Items= new List<BO.OrderItem>() };
                    CatalogWindow cw = new CatalogWindow(cart);//create new ProductListWindow
                    cw.ShowDialog();
                }

            }
            catch (BO.BlMissingEntityException)
            {
                MessageBox.Show("User name is wrong", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
