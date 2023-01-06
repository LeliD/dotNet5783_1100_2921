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
        /// <summary>
        /// bl is an instance of IBl
        /// </summary>
        BlApi.IBl bl = BlApi.Factory.Get();
        /// <summary>
        /// Constructor,Building an instance of LogInWindow
        /// </summary>
        public LogInWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// button to Log In as a new user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string userName = tbUserName.Text;
                string passcode = pbPasscode.Password;
                BO.User user = bl.User.GetByUserName(userName);
                if (user.AdminAccess)//if it's an admin
                {
                    AdminWindow aw = new AdminWindow();//create new AdminWindow
                    Close(); 
                    aw.ShowDialog();
                }
                else //if it isn't an admin
                {
                    BO.Cart cart = new BO.Cart() { CustomerAddress=user.UserAddress, CustomerEmail=user.UserEmail, CustomerName=user.Name , Items= new List<BO.OrderItem>() }; //initialization cart to this user
                    CatalogWindow cw = new CatalogWindow(cart);//create new CatalogWindow
                    Close(); 
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
    }
}
