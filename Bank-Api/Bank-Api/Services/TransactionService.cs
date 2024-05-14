using Bank_Api.Context;
using Bank_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank_Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankDbContext _context;
        public TransactionService(BankDbContext context) 
        {
            _context = context;
        }

        public async Task<TransactionResponse> CreateTranscation(TransactionRequest NewTransactionRequest, int SenderUserID)
        {
            var fromAccount = await _context.Account.Include(i => i.UserInfo).FirstOrDefaultAsync(s => s.AccountNumber == NewTransactionRequest.FromAccountId && s.UserInfo.Id == SenderUserID);
            var toAccount = await _context.Account.FirstOrDefaultAsync(s => s.AccountNumber == NewTransactionRequest.ToAccountId);
            if (fromAccount == null || toAccount == null || fromAccount == toAccount)
                throw new ArgumentException("Invalid account numbers");

            if(fromAccount.Money < NewTransactionRequest.Amount || NewTransactionRequest.Amount <= 0)
                throw new ArgumentException("Invalid amount of money to send");

            fromAccount.Money -= NewTransactionRequest.Amount;
            toAccount.Money += NewTransactionRequest.Amount; 

            var transaction = new Transaction
            {
                FromAccount = fromAccount,
                ToAccount = toAccount,
                Amount = NewTransactionRequest.Amount * -1,
                Name = $"{fromAccount.UserInfo.Firstname} {fromAccount.UserInfo.Lastname}",
                Timestamp = DateTime.Now,
                Sender = true
            };

            var newTranaction = _context.Transaction.Add(transaction);
            _context.SaveChanges();
            return new TransactionResponse(newTranaction.Entity);
        }

        public async Task<IEnumerable<TransactionResponse>> GetAllTransactionsFromUser(int UserId)
        {
            return await Task.Run(() => _context.Transaction
            .Include(i => i.FromAccount.UserInfo)
            .Include(i => i.ToAccount.UserInfo)
            .Where(s => s.FromAccount.UserInfo.Id == UserId || s.ToAccount.UserInfo.Id == UserId)
            .Select(s => new TransactionResponse(s)));
        }
    }

    public interface ITransactionService
    {
        public Task<TransactionResponse> CreateTranscation(TransactionRequest NewTransactionRequest, int SenderUserID);
        public Task<IEnumerable<TransactionResponse>> GetAllTransactionsFromUser(int UserId);

    }
}
