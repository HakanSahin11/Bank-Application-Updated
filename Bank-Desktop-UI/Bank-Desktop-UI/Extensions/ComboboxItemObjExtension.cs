using Bank_Desktop_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Desktop_UI.Extensions
{
    public static class ComboboxItemObjExtension
    {
        public static List<ComboboxItemObj> GetComboboxItemToDisplayAccounts(this List<ComboboxItemObj> ItemsToDisplay, string DefaultText, List<Account> Accounts)
        {
            var defaultSelector = new ComboboxItemObj(DefaultText, 0);
            ItemsToDisplay.Add(defaultSelector);

            foreach (var account in Accounts)
            {
                var item = new ComboboxItemObj($"{account.Name} - {account.AccountNumber}", account.AccountNumber);
                ItemsToDisplay.Add(item);
            }
            return ItemsToDisplay;
        }
    }
}
