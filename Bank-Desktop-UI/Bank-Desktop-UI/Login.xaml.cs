using Bank_Desktop_UI.Http_Request;
using Bank_Desktop_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace Bank_Desktop_UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public string EmailAddress { get; set; } = "";
        public Login()
        {
            InitializeComponent();
            DataContext = this;
            TextboxEmail.Focus();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            SendLogin();
        }

        private async void SendLogin()
        {
            var userInfo = await VerifyLogin();
            if (userInfo != null)
            {
                MainWindow mainWindow = new MainWindow(userInfo);
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Invalid Login Attempt");
            }
        }

        private async Task<UserLoginResponse?> VerifyLogin()
        {
            var loginRequest = new LoginRequest(EmailAddress, PwBoxPassword.Password);
            var userInfo = await HttpRequests.SendHttpPostRequest<UserLoginResponse>(HttpRequests.Endpoints.UserAuthentication, "", loginRequest);
            if (userInfo != null)
                return userInfo;

            return null;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Signup newWindow = new Signup();
            newWindow.Show();
            Close();
        }

        private void EnterPressed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendLogin();
            }
        }
    }
}
