using Bank_Desktop_UI.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bank_Desktop_UI
{
    public partial class MainWindow : Window
    {
        public UserLoginResponse _BasicUserInfo {  get; set; }
        public MainWindow(UserLoginResponse basicUserInfo)
        {
            InitializeComponent();
            _BasicUserInfo = basicUserInfo;

            BaseInfo.Id = _BasicUserInfo.Id;
            BaseInfo.Firstname = _BasicUserInfo.Firstname;
            BaseInfo.Lastname = _BasicUserInfo.Lastname;
            BaseInfo.Token = _BasicUserInfo.Token;
        }

        private void Sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Sidebar.SelectedItem is NavButton selected)
            {
                navframe.Navigate(selected.Navlink);
            }
        }
    }
}