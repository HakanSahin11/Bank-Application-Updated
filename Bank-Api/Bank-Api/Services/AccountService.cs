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

        public async Task<IEnumerable<Account>> GetAccountsByUserId(int UserId)
        {
            return await Task.Run(() => _context.Account.Include(i => i.UserInfo).Where(s => s.UserInfo.Id == UserId));
        }
    }

    public interface IAccountService
    {
        public Task<IEnumerable<Account>> GetAccountsByUserId(int UserId);

    }
}
