using Bank_Desktop_UI.Http_Request;
using Bank_Desktop_UI.Models;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static Bank_Desktop_UI.Extensions.ComboboxItemObjExtension;
namespace Bank_Desktop_UI.Pages
{
    public partial class TransfersPage : Page
    {
        public bool InternalTransfer { get; set; } = true;
        public string Name { get; set; } = "";
        private List<Account> Accounts { get; set; } = new List<Account>();
        public List<ComboboxItemObj> AccountsToDisplay { get; set; } = new List<ComboboxItemObj>();
        public TransfersPage()
        {
            InitializeComponent();
            Initial();
        }

        private async void Initial()
        {
            await LoadAccounts();

            AccountsToDisplay.GetComboboxItemToDisplayAccounts("Select Account", Accounts);
            ComboboxFrom.SelectedIndex = 0;

            if (InternalTransfer)
            {
                ComboboxTo.ItemsSource = AccountsToDisplay;
                ComboboxTo.SelectedIndex = 0;
            }

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

        private async void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            ClearErrors();
            if (!VerifySendCriterias())
                return;

            var userId = BaseInfo.Id;

            var transactionToMake = FormTransactionRequest();
            var response = await HttpRequests.SendHttpPostRequest<Transaction>(HttpRequests.Endpoints.Transaction, userId.ToString(), transactionToMake);

            if (response != null)
            {
                MessageBox.Show("Transaction succeeded");
            }
            else
            {
                MessageBox.Show("Transaction failed");
            }
        }

        private TransactionRequest? FormTransactionRequest()
        {
            var name = TextboxName.Text;
            var amount = double.Parse(TextboxAmountToSend.Text);

            var fromAccountNo = ((ComboboxItemObj)ComboboxFrom.SelectedValue).Value;
            long toAccountNo;

            if (InternalTransfer)
                toAccountNo = ((ComboboxItemObj)ComboboxTo.SelectedValue).Value;

            else
                toAccountNo = long.Parse(TextboxTo.Text);

            var transaction = new TransactionRequest
            {
                Amount = amount,
                FromAccountId = fromAccountNo,
                ToAccountId = toAccountNo,
                Name = name
            };

            return transaction;
        }

        #region Error Handling
        private bool VerifySendCriterias()
        {
            if (ComboboxFrom.SelectedIndex == 0 || ComboboxFrom.SelectedValue is not ComboboxItemObj itemValueFrom)
            {
                BorderComboboxFrom.BorderBrush = Brushes.Red;
                BorderComboboxFrom.BorderThickness = new Thickness(2);
                return false;
            }

            if (InternalTransfer && ComboboxTo.SelectedIndex == 0 || ComboboxTo.SelectedValue is not ComboboxItemObj itemValueTo)
            {
                BorderComboboxTo.BorderBrush = Brushes.Red;
                BorderComboboxTo.BorderThickness = new Thickness(2);
                return false;
            }

            if (!InternalTransfer && TextboxTo.Text.Length != 10)
            {
                TextboxTo.BorderBrush = Brushes.Red;
                TextboxTo.BorderThickness = new Thickness(2);
                return false;
            }

            if (string.IsNullOrWhiteSpace(TextboxName.Text))
            {
                TextboxName.BorderBrush = Brushes.Red;
                TextboxName.BorderThickness = new Thickness(2);
                return false;
            }

            if ((!double.TryParse(TextboxAmountToSend.Text, out double amountToSend) || amountToSend <= 0))
            {
                TextboxAmountToSend.BorderBrush = Brushes.Red;
                TextboxAmountToSend.BorderThickness = new Thickness(2);
                return false;
            }

            if (InternalTransfer)
            {
                if (itemValueFrom.Value == itemValueTo.Value)
                {
                    BorderComboboxFrom.BorderBrush = Brushes.Red;
                    BorderComboboxFrom.BorderThickness = new Thickness(2);

                    BorderComboboxTo.BorderBrush = Brushes.Red;
                    BorderComboboxTo.BorderThickness = new Thickness(2);
                    return false;
                }
            }

            else
            {
                if (!long.TryParse(TextboxTo.Text, out long toValue) || itemValueFrom.Value == toValue)
                {
                    TextboxTo.BorderBrush = Brushes.Red;
                    TextboxTo.BorderThickness = new Thickness(2);
                    return false;
                }
            }
            return true;
        }

        private void ClearErrors()
        {
            BorderComboboxFrom.BorderThickness = new Thickness(0);
            BorderComboboxFrom.BorderBrush = Brushes.Black;

            BorderComboboxTo.BorderThickness = new Thickness(0);
            BorderComboboxTo.BorderBrush = Brushes.Black;

            TextboxName.BorderThickness = new Thickness(1);
            TextboxName.BorderBrush = Brushes.Black;

            TextboxAmountToSend.BorderThickness = new Thickness(1);
            TextboxAmountToSend.BorderBrush = Brushes.Black;

            TextboxTo.BorderThickness = new Thickness(1);
            TextboxTo.BorderBrush = Brushes.Black;
        }

        #endregion

        private void CheckboxInternalTransfer_Changed(object sender, RoutedEventArgs e)
        {
            if (InternalTransfer)
            {
                TextboxName.Text = $"{BaseInfo.Firstname} {BaseInfo.Lastname}";
                TextboxName.IsEnabled = false;

                ComboboxTo.Visibility = Visibility.Visible;
                TextboxTo.Visibility = Visibility.Hidden;
            }
            else
            {
                TextboxName.Text = "";
                TextboxName.IsEnabled = true;

                ComboboxTo.Visibility = Visibility.Hidden;
                TextboxTo.Visibility = Visibility.Visible;
            }
        }

        private void Textbox_NumbersOnly(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
    }
}
