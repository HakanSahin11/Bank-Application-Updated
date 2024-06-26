using Bank_Api.Context;
using Bank_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank_Api.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankDbContext _context;
        public AccountService(BankDbContext context)
        {
            _context = context;
        }

        public async Task<Account> CreateAccount(Account account)
        {
            if(!VerifyNewAccountInfo(account))
                throw new ArgumentException($"Error, invalid info entered for account creation on: {nameof(CreateAccount)}");

            var newAccount = _context.Account.AddAsync(account);
            return (await newAccount).Entity;
        }

        public async Task<IEnumerable<Account>> GetAccountsByUserId(int UserId)
        {
            return await Task.Run(() => _context.Account.Include(i => i.UserInfo).Where(s => s.UserInfo.Id == UserId));
        }

        private bool VerifyNewAccountInfo(Account account)
        {
            if (account == null)
                return false;

            if (string.IsNullOrWhiteSpace(account.Name))
                return false;

            if (account.Id != 0)
                return false;

            if(account.UserInfo == null) 
                return false;

            return true;

        }
    }

    public interface IAccountService
    {
        public Task<IEnumerable<Account>> GetAccountsByUserId(int UserId);
        public Task<Account> CreateAccount(Account account);
    }
}
