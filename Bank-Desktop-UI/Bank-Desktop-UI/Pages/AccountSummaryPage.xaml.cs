using Bank_Desktop_UI.Http_Request;
using Bank_Desktop_UI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bank_Desktop_UI.Pages
{
    /// <summary>
    /// Interaction logic for AccountSummary.xaml
    /// </summary>
    public partial class AccountSummaryPage : Page
    {
        public string DatetimeValue { get; set; } = DateTime.Now.ToString("dd-MM-yy HH:mm", CultureInfo.InvariantCulture);
        public string NameValue { get; set; } = "";
        public double AccountValue { get; set; } = 0;

        public AccountSummaryPage()
        {
            InitializeComponent();
            Initial();
        }

        private async void Initial()
        {
            NameValue = $"Hi, {BaseInfo.Firstname}";

            var userId = BaseInfo.Id;
            var accountList = await HttpRequests.SendHttpGetRequest<List<Account>>(HttpRequests.Endpoints.Account, userId.ToString());
            if(accountList != null && accountList.Any() )
            {
                AccountValue = accountList.Sum(s => s.Money);
            }

            DataContext = this;
        }
    }
}
