using Bank_Desktop_UI.Models;
using System.Globalization;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Bank_Desktop_UI.MockedData;
using static Bank_Desktop_UI.Helpers.UIElements;
using Bank_Desktop_UI.Http_Request;
namespace Bank_Desktop_UI.Pages
{
    public partial class TransactionsPage : Page
    {
        public string SearchContent { get; set; } = string.Empty;
        public string DatetimeValue { get; set; } = DateTime.Now.ToString("dd-MM-yy HH:mm", CultureInfo.InvariantCulture);
        private List<Account> Accounts { get; set; } = new List<Account>();
        private List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public List<ComboboxItemObj> AccountsToDisplay { get; set; } = new List<ComboboxItemObj> { };
        public TransactionsPage()
        {
            InitializeComponent();
            Initial();
        }

        private async void Initial()
        {
            await LoadAccounts();
            await LoadTransactions();

            LoadDropdownAccounts();
            DataContext = this;
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

        private async Task LoadTransactions()
        {
            var userId = BaseInfo.Id;
            var transactionList = await HttpRequests.SendHttpGetRequest<List<Transaction>>(HttpRequests.Endpoints.Transaction, userId.ToString());
            if (transactionList != null && transactionList.Any())
            {
                Transactions = transactionList;
            }
        }

        private void LoadDropdownAccounts()
        {
            var defaultSelector = new ComboboxItemObj("Select All", 0);
            AccountsToDisplay.Add(defaultSelector);

            foreach (var account in Accounts)
            {
                var accountToDsiplay = new ComboboxItemObj($"{account.Name} - {account.AccountNumber}", account.Id);
                AccountsToDisplay.Add(accountToDsiplay);
            }

            ComboboxAccounts.SelectedIndex = 0;
        }

        private void Search()
        {
            StackpanelTranactionsToDisplay.Children.Clear();
            var selectedAccount = ComboboxAccounts.SelectedValue;

            if (selectedAccount is ComboboxItemObj comboboxAccountItem == false)
                return;

            var transactionsToDisplay = Transactions;
            if (comboboxAccountItem.Value != 0)
                transactionsToDisplay = Transactions.Where(s => s.FromAccountId == comboboxAccountItem.Value || s.ToAccountId == comboboxAccountItem.Value).ToList();

            if (SearchContent != "")
            {
                var search = SearchContent.ToLower();
                transactionsToDisplay = transactionsToDisplay.Where(s =>
                       s.Name.ToLower().Contains(search)  
                    || s.SenderName.ToLower().Contains(search)).ToList();
            }

            var accountListIds = Accounts.Select(s => s.Id);
            var transactionsSenderReciever = new List<Transaction>();

            foreach (var item in transactionsToDisplay)
            {
                if (accountListIds.Contains(item.FromAccountId))
                {
                    transactionsSenderReciever.Add(item);
                }

                if (accountListIds.Contains(item.ToAccountId))
                {
                    var newItem = new Transaction()
                    {
                        Amount = item.Amount * -1,
                        Sender = false,
                        FromAccountId = item.FromAccountId,
                        ToAccountId = item.ToAccountId,
                        Id = item.Id,
                        Name = item.Name,
                        SenderName = item.SenderName,
                        Timestamp = item.Timestamp
                    };
                    transactionsSenderReciever.Add(newItem);
                }
            }
            transactionsToDisplay = transactionsSenderReciever;

            DisplayTransactions(transactionsToDisplay);
        }

        private void DisplayTransactions(List<Transaction> TransactionsToDisplay)
        {
            var gridList = new List<Grid>();
            foreach (var transaction in TransactionsToDisplay)
            {

                var lbName = CreateLabel(transaction.Name);
                var lbSenderName = CreateLabel(transaction.SenderName, 0, 1);
                var lbTimestamp = CreateLabel(transaction.Timestamp.ToString("dd-MM-yy HH:mm", CultureInfo.InvariantCulture), 0,2, VerticalAlignment.Top, HorizontalAlignment.Center);
                var lbStatus = CreateLabel(transaction.Sender ? "Sent" : "Recieved", 0, 3, VerticalAlignment.Top, HorizontalAlignment.Center);
                var lbAmount = CreateLabel(transaction.Amount.ToString(), 0, 4, VerticalAlignment.Top, HorizontalAlignment.Center);

                var grid = TransactionGridBase();
                grid.Children.Add(lbName);
                grid.Children.Add(lbSenderName);
                grid.Children.Add(lbTimestamp);
                grid.Children.Add(lbStatus);
                grid.Children.Add(lbAmount);
                gridList.Add(grid);
            }

            foreach (var grid in gridList)
            {
                StackpanelTranactionsToDisplay.Children.Add(grid);
            }
        }

        private static Grid TransactionGridBase()
        {
            Grid grid = new()
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)},
                }
            };
            return grid;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void ComboboxAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void TxtSearchbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search();
            }
        }
    }

}
