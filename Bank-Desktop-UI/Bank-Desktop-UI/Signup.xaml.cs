using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media;
using Bank_Desktop_UI.Http_Request;
using Bank_Desktop_UI.Models;
namespace Bank_Desktop_UI
{
    public partial class Signup : Window
    {
        public string EmailAddress { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;

        public Signup()
        {
            InitializeComponent();
            DataContext = this;
            TextboxEmail.Focus();
        }

        private void BtnSignup_Click(object sender, RoutedEventArgs e)
        {
            SignUp();
        }

        private async void SignUp()
        {
            var userInfo = await VerifySignup();
            if (userInfo != null)
            {
                var newWindow = new MainWindow(userInfo);
                newWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Failed to create user");
            }
        }

        private async Task<UserLoginResponse?> VerifySignup()
        {
            if (!Validation())
                return null;

            var newUser = new CreateUser(EmailAddress, PasswordboxPw.Password, Firstname, Lastname);
            var userInfo = await HttpRequests.SendHttpPostRequest<UserLoginResponse>(HttpRequests.Endpoints.User, "", newUser);
            return userInfo;
        }

        private bool Validation()
        {
            if (PasswordboxPw.Password != PasswordboxPwConfirm.Password)
            {
                PasswordboxPw.BorderBrush = Brushes.Red;
                PasswordboxPwConfirm.BorderBrush = Brushes.Red;
                return false;
            }
            return true;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new Login();
            newWindow.Show();
            Close();
        }

        private void Event_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (!BtnSignup.IsEnabled && 
                !string.IsNullOrWhiteSpace(EmailAddress) && 
                !string.IsNullOrWhiteSpace(Firstname) &&
                !string.IsNullOrWhiteSpace(Lastname) &&
                !string.IsNullOrWhiteSpace(PasswordboxPw.Password) &&
                !string.IsNullOrWhiteSpace(PasswordboxPwConfirm.Password))
                    BtnSignup.IsEnabled = true;

            if (e.Key == System.Windows.Input.Key.Enter)
            {
                SignUp();
            }
        }

        private void Event_LostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox txtBox)
            {
                if (string.IsNullOrEmpty(txtBox.Text))
                {
                    txtBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    BtnSignup.IsEnabled = false;
                }
                else
                    txtBox.BorderBrush = new SolidColorBrush(Colors.Black);
            }
            else if (sender is PasswordBox pwBox)
            {
                if (string.IsNullOrEmpty(pwBox.Password))
                {
                    pwBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    BtnSignup.IsEnabled = false;

                }
                else
                    pwBox.BorderBrush = new SolidColorBrush(Colors.Black);
            }
        }
    }
}
