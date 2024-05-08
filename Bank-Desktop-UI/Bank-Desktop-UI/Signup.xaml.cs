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
            ClearErrors();
            if (!Validation())
                return null;

            var newUser = new CreateUser(EmailAddress, PasswordboxPw.Password, Firstname, Lastname);
            var userInfo = await HttpRequests.SendHttpPostRequest<UserLoginResponse>(HttpRequests.Endpoints.User, "", newUser);
            return userInfo;
        }

        private bool Validation()
        {
            if (string.IsNullOrWhiteSpace(EmailAddress))
            {
                TextboxEmail.BorderBrush = Brushes.Red;
                TextboxEmail.BorderThickness = new Thickness(2);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Firstname))
            {
                TextboxFirstname.BorderBrush = Brushes.Red;
                TextboxFirstname.BorderThickness = new Thickness(2);
                return false;
            }

            if (string.IsNullOrWhiteSpace(Lastname))
            {
                TextboxLastname.BorderBrush = Brushes.Red;
                TextboxLastname.BorderThickness = new Thickness(2);
                return false;
            }

            if (string.IsNullOrWhiteSpace(PasswordboxPw.Password))
            {
                PasswordboxPw.BorderBrush = Brushes.Red;
                PasswordboxPw.BorderThickness = new Thickness(2);
                return false;
            }

            if (string.IsNullOrWhiteSpace(PasswordboxPwConfirm.Password))
            {
                PasswordboxPwConfirm.BorderBrush = Brushes.Red;
                PasswordboxPwConfirm.BorderThickness = new Thickness(2);
                return false;
            }

            if (PasswordboxPw.Password != PasswordboxPwConfirm.Password)
            {
                PasswordboxPw.BorderBrush = Brushes.Red;
                PasswordboxPw.BorderThickness = new Thickness(2);

                PasswordboxPwConfirm.BorderBrush = Brushes.Red;
                PasswordboxPwConfirm.BorderThickness = new Thickness(2);
                return false;
            }

            return true;
        }

        private void ClearErrors()
        {
            TextboxEmail.BorderThickness = new Thickness(1);
            TextboxEmail.BorderBrush = Brushes.Black;

            TextboxFirstname.BorderThickness = new Thickness(1);
            TextboxFirstname.BorderBrush = Brushes.Black;

            TextboxLastname.BorderThickness = new Thickness(1);
            TextboxLastname.BorderBrush = Brushes.Black;

            PasswordboxPw.BorderThickness = new Thickness(1);
            PasswordboxPw.BorderBrush = Brushes.Black;

            PasswordboxPwConfirm.BorderThickness = new Thickness(1);
            PasswordboxPwConfirm.BorderBrush = Brushes.Black;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new Login();
            newWindow.Show();
            Close();
        }

        private void EnterPressed_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                SignUp();
            }
        }
    }
}
