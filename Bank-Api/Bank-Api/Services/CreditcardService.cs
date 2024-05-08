using Bank_Api.Context;
using Bank_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank_Api.Services
{
    public class CreditcardService : ICreditcardService
    {
        private readonly BankDbContext _context;
        public CreditcardService(BankDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Creditcard>> GetAccountsByUserId(int UserId)
        {
            return await Task.Run(() => _context.Creditcard.Include(i => i.Account.UserInfo).Where(s => s.Account.UserInfo.Id == UserId));
        }
    }

    public interface ICreditcardService
    {
        public Task<IEnumerable<Creditcard>> GetAccountsByUserId(int UserId);
    }
}
