using Bank_Desktop_UI.Http_Request;
using Bank_Desktop_UI.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Bank_Desktop_UI
{
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
            Signup newWindow = new();
            newWindow.Show();
            Close();
        }

        private void Event_KeyUp(object sender, KeyEventArgs e)
        {
            if (!BtnLogin.IsEnabled && !string.IsNullOrWhiteSpace(TextboxEmail.Text) && !string.IsNullOrWhiteSpace(PwBoxPassword.Password))
                BtnLogin.IsEnabled = true;

            if (BtnLogin.IsEnabled && e.Key == Key.Enter)
            {
                SendLogin();
            }
        }

        private void Event_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox txtBox)
            {
                if (string.IsNullOrEmpty(txtBox.Text))
                {
                    txtBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    BtnLogin.IsEnabled = false;
                }
                else
                    txtBox.BorderBrush = new SolidColorBrush(Colors.Black);
            }
            else if (sender is PasswordBox pwBox)
            {
                if (string.IsNullOrEmpty(pwBox.Password))
                {
                    pwBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    BtnLogin.IsEnabled = false;

                }
                else
                    pwBox.BorderBrush = new SolidColorBrush(Colors.Black);
            }
        }
    }
}
