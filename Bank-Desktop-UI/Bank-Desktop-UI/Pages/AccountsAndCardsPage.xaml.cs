using Bank_Desktop_UI.Models;
using System.Windows.Controls;
using System.Windows;
using static Bank_Desktop_UI.Helpers.UIElements;
using static Bank_Desktop_UI.Extensions.StringExtensions;
using static Bank_Desktop_UI.MockedData;
using Bank_Desktop_UI.Extensions;
using Bank_Desktop_UI.Http_Request;
namespace Bank_Desktop_UI.Pages
{
    /// <summary>
    /// Interaction logic for AccountsAndCards.xaml
    /// </summary>
    public partial class AccountsAndCardsPage : Page 
    {
        public double AccountValue { get; set; }
        private List<Account> Accounts { get; set; } = new List<Account>();
        private List<Creditcard> Creditcards { get; set; } = new List<Creditcard>();


        public AccountsAndCardsPage()
        {

            InitializeComponent();
            Initial();
        }

        private async void Initial()
        {
            AccountValue = 0;

            await LoadAccounts();
            await LoadCreditcards();

            DisplayAccounts(Accounts);
            DisplayCreditcards(Creditcards);

            AccountValue = Accounts.Sum(s => s.Money);

            DataContext = this;
        }

        private async Task LoadCreditcards()
        {
            var userId = BaseInfo.Id;
            var creditcardList = await HttpRequests.SendHttpGetRequest<List<Creditcard>>(HttpRequests.Endpoints.Creditcard, userId.ToString());
            if (creditcardList != null && creditcardList.Any())
            {
                Creditcards = creditcardList;
            }
        }

        private async Task LoadAccounts()
        {
            var userId = BaseInfo.Id;
            var accountList = await HttpRequests.SendHttpGetRequest<List<Account>>(HttpRequests.Endpoints.Account, userId.ToString());
            if (accountList != null && accountList.Any())
            {
                Accounts = accountList;
            }
        }

        private void DisplayAccounts(List<Account> accounts)
        {
            var gridList = new List<Grid>();
            foreach (var account in accounts)
            {
                var grid = CreateAccountGrid(account);
                if (grid != null)
                    gridList.Add(grid);
            }

            foreach (var grid in gridList)
            {
                StackPanelAccounts.Children.Add(grid);
            }
        }

        private Grid? CreateAccountGrid(Account Account)
        {
            if (!VerifyAccount(Account))
                return null;

            var lbName = CreateLabel(Account.Name, 0, 0, VerticalAlignment.Top, HorizontalAlignment.Left, 1, 1, FontWeights.Bold);
            var lbAccountNr = CreateLabel(Account.AccountNumber.ToString(), 1, 0);
            var lbMoney = CreateLabel(Account.Money.ToString(), 0, 1, VerticalAlignment.Center, HorizontalAlignment.Left, 2);

            var grid = GetBaseGrid(2, 2);
            grid.Children.Add(lbName);
            grid.Children.Add(lbAccountNr);
            grid.Children.Add(lbMoney);
            return grid;
        }

        private bool VerifyAccount(Account Account)
        {
            var res = true;
            if (string.IsNullOrEmpty(Account.Name))
                res = false;

            if (Account.AccountNumber.ToString().Length != 10)
                res = false;

            return res;
        }

        private void DisplayCreditcards(List<Creditcard> creditcards)
        {
            var gridList = new List<Grid>();
            foreach (var creditcard in creditcards)
            {
                var grid = CreateCreditCardGrid(creditcard);
                if (grid != null)
                    gridList.Add(grid);
            }

            foreach (var grid in gridList)
            {
                StackPanelCreditcards.Children.Add(grid);
            }
        }

        private Grid? CreateCreditCardGrid(Creditcard Creditcard)
        {
            if (!VerifyCreditcard(Creditcard))
                return null;

            var lbName = CreateLabel(Creditcard.Name, 0, 0, VerticalAlignment.Top, HorizontalAlignment.Left, 1, 1, FontWeights.Bold);

            var annonymizedCreditcard = Creditcard.CardNo.ToString().Anonymize(8, 0, 4, 12, 4);
            var lbCreditcard = CreateLabel(annonymizedCreditcard, 0, 1);

            var grid = GetBaseGrid(1, 2);
            grid.Children.Add(lbName);
            grid.Children.Add(lbCreditcard);
            return grid;
        }

        private bool VerifyCreditcard(Creditcard Creditcard)
        {
            var res = true;
            if (string.IsNullOrEmpty(Creditcard.Name))
                res = false;

            if (Creditcard.CardNo.ToString().Length != 16)
                res = false;

            return res;
        }



    }
}
