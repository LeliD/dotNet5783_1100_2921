﻿using PL.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
using System.Xml.Linq;

namespace PL
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        BlApi.IBl bl = BlApi.Factory.Get();
        public SignInWindow()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            string wrongInput="";
            string name = tbName.Text;
            string userEmail = tbUserEmail.Text;
            string userAddress= tbUserAddress.Text;
            string userName = tbUserName.Text;
            string passcode= tbPasscode.Password;
            if (name == "")
            {
                wrongInput += "Name is missing\n";
                tbName.BorderBrush = Brushes.Red;
                //MessageBox.Show("Name is missing", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            bool isValidEmail = true;
            try
            {
                var mail = new MailAddress(userEmail);
                isValidEmail = mail.Host.Contains(".");
                if(!isValidEmail)
                {
                    wrongInput += "UserEmail is invalid\n";
                    tbUserEmail.BorderBrush = Brushes.Red;
                }
            }
            catch (Exception)
            {
                wrongInput += "UserEmail is invalid\n";
                tbUserEmail.BorderBrush = Brushes.Red;
            }
            if (userEmail == "")
            {
                wrongInput += "UserEmail is Missing\n";
                tbUserEmail.BorderBrush = Brushes.Red;
                //MessageBox.Show("Username is Missing", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
            if (userAddress == "")
            {
                wrongInput += "UserAddress is Missing\n";
                tbUserAddress.BorderBrush = Brushes.Red;
                //MessageBox.Show("Username was not entered", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (userName == "")
            {
                wrongInput += "Username is Missing\n";
                tbUserName.BorderBrush = Brushes.Red;
                //MessageBox.Show("Username was not entered", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (passcode == "")
            {
                wrongInput += "Password is Missing\n";
                tbPasscode.BorderBrush = Brushes.Red;
                //MessageBox.Show("Passcode was not entered", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if(wrongInput!="")
            {
                MessageBox.Show(wrongInput, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                BO.User user = new BO.User() { Name = name,UserName = userName, UserEmail = userEmail, UserAddress = userAddress, Passcode = passcode, AdminAccess = false };
                bl.User.Add(user);
                MessageBox.Show("Signing in has ended successfully👌", "Good Luck", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.BlAlreadyExistEntityException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}
