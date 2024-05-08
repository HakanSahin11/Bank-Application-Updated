using Bank_Api.Context;
using Bank_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank_Api.Services
{
    public class UserService : IUserService
    {
        private readonly BankDbContext _context;
        public UserService(BankDbContext context)
        {
            _context = context;
        }

        public async Task<UserAuthentication> VerifyLogin(LoginRequest userAuthentication)
        {
            var user = await _context.UserAuthentication.Include(i => i.UserInfo).FirstOrDefaultAsync(s => s.Email == userAuthentication.Email && s.Password == userAuthentication.Password);

            if (user != null)
                return user;

            throw new ArgumentException("Invalid login");
        }

        public async Task<UserInfo> CreateUser(CreateUser NewUserRequest)
        {
            var existingUser = await _context.UserAuthentication.FirstOrDefaultAsync(s => s.Email == NewUserRequest.Email);
            if (existingUser != null)
                throw new ArgumentException("Email already exist");

            var lastCreditcardNo = 1000000000000000;
            var lastCreditcard = await _context.Creditcard.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (lastCreditcard != null && lastCreditcard.CardNo >= lastCreditcardNo)
                lastCreditcardNo = lastCreditcard.CardNo + 1;

            var creditcard = new Creditcard
            {
                Name = "Default Creditcard",
                CardNo = lastCreditcardNo,
            };

            long accountNo = 1000000000;
            var lastAccount = await _context.Account.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (lastAccount != null && lastAccount.AccountNumber >= accountNo)
                accountNo = lastAccount.AccountNumber + 1;

            var mainAccount = new Account
            {
                AccountNumber = accountNo,
                Money = 10000,
                Name = "Default Account",
                CreditCards = new List<Creditcard> 
                { 
                    creditcard 
                }
            };

            var secondaryAccount = new Account
            {
                AccountNumber = accountNo + 1,
                Money = 10000,
                Name = "Secondary Account"
            };

            var newUserInfo = new UserInfo
            {
                Firstname = NewUserRequest.Firstname,
                Lastname = NewUserRequest.Lastname,
                Accounts = new List<Account>
                {
                    mainAccount,
                    secondaryAccount,
                }
            };

            var newUserAuthentication = new UserAuthentication
            {
                Email = NewUserRequest.Email,
                Password = NewUserRequest.Password,
                UserInfo = newUserInfo
            };

            var res = await _context.UserAuthentication.AddAsync(newUserAuthentication);
            await _context.SaveChangesAsync();
            return res.Entity.UserInfo;
        }

        public async Task<IEnumerable<UserInfo>> GetAllUserInfos()
        {
            return await Task.Run(() => _context.UserInfo);
        }

        public async Task<UserInfo> GetUser(int Id)
        {
            return await _context.UserInfo.FirstOrDefaultAsync(s => s.Id == Id);
        }
    }

    public interface IUserService
    {
        public Task<UserAuthentication> VerifyLogin(LoginRequest userAuthentication);
        public Task<UserInfo> CreateUser(CreateUser NewUserRequest);
        public Task<IEnumerable<UserInfo>> GetAllUserInfos();
        public Task<UserInfo> GetUser(int Id);

    }
}
