using Bank_Api.Context;
using Bank_Api.Helpers;
using Bank_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank_Api.Services
{
    public class UserService : IUserService
    {
        private readonly BankDbContext _context;
        private readonly IGenerateJWTTokenHelper _jwtHelper;
        public UserService(BankDbContext context, IGenerateJWTTokenHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public async Task<LoginResonse> VerifyLogin(LoginRequest userAuthentication)
        {
            var user = await _context.UserAuthentication.Include(i => i.UserInfo).FirstOrDefaultAsync(s => s.Email == userAuthentication.Email && s.Password == userAuthentication.Password);

            if (user != null)
            {
                var token = _jwtHelper.GenerateJWTToken(user.UserInfo);
                if (string.IsNullOrEmpty(token))
                    throw new InvalidOperationException("Error creating token");

                return new LoginResonse(user.UserInfo, token);
            }

            throw new ArgumentException("Invalid login");
        }

        public async Task<UserInfo> CreateUser(CreateUser NewUserRequest)
        {
            var user = FormNewUserRequest(NewUserRequest);
            var res = await _context.UserAuthentication.AddAsync(await user);
            await _context.SaveChangesAsync();
            return res.Entity.UserInfo;
        }

        private async Task ValidateUserRequest(CreateUser NewUserRequest)
        {
            var existingUser = await _context.UserAuthentication.FirstOrDefaultAsync(s => s.Email == NewUserRequest.Email);
            if (existingUser != null)
                throw new ArgumentException("Email already exist");

            if (string.IsNullOrEmpty(NewUserRequest.Firstname) ||
                string.IsNullOrEmpty(NewUserRequest.Lastname) ||
                string.IsNullOrEmpty(NewUserRequest.Email) ||
                string.IsNullOrEmpty(NewUserRequest.Password))
                throw new ArgumentException("Invalid user creation info");

        }

        private async Task<Creditcard> FormCreditcardForNewUser()
        {
            var lastCreditcardNo = 1000000000000000;
            var lastCreditcard = await _context.Creditcard.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (lastCreditcard != null && lastCreditcard.CardNo >= lastCreditcardNo)
                lastCreditcardNo = lastCreditcard.CardNo + 1;

            var creditcard = new Creditcard
            {
                Name = "Default Creditcard",
                CardNo = lastCreditcardNo,
            };

            return creditcard;
        }

        private async Task<List<Account>> FormAccountsForNewUser(Creditcard Creditcard)
        {
            var accountList = new List<Account>();
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
                    Creditcard
                }
            };

            var secondaryAccount = new Account
            {
                AccountNumber = accountNo + 1,
                Money = 10000,
                Name = "Secondary Account"
            };

            accountList.Add(mainAccount);
            accountList.Add(secondaryAccount);
            return accountList;
        }

        private UserInfo FormUserInfoForNewUser(CreateUser NewUserRequest, List<Account> Accounts)
        {
            ValidateUserRequest(NewUserRequest);
            if (Accounts.Count == 0)
                throw new InvalidOperationException("Failed to create accounts");

            var newUserInfo = new UserInfo
            {
                Firstname = NewUserRequest.Firstname,
                Lastname = NewUserRequest.Lastname,
                Accounts = Accounts
            };

            return newUserInfo;
        }

        private UserAuthentication FormUserAuthenticationForNewUser(CreateUser NewUserRequest, UserInfo NewUserInfo)
        {
            ValidateUserRequest(NewUserRequest);
            if (NewUserRequest.Firstname != NewUserInfo.Firstname ||
                NewUserRequest.Lastname != NewUserInfo.Lastname) 
                    throw new InvalidOperationException("Failed to create User input Correctly");

            if(string.IsNullOrWhiteSpace(NewUserRequest.Email) ||
                string.IsNullOrWhiteSpace(NewUserRequest.Password))
                throw new ArgumentException("Invalid username or password");

            var newUserAuthentication = new UserAuthentication
            {
                Email = NewUserRequest.Email,
                Password = NewUserRequest.Password,
                UserInfo = NewUserInfo
            };

            return newUserAuthentication;
        }

        internal async Task<UserAuthentication> FormNewUserRequest(CreateUser NewUserRequest)
        {
            await ValidateUserRequest(NewUserRequest);
            var creditcard = await FormCreditcardForNewUser();
            var accounts = await FormAccountsForNewUser(creditcard);
            var userInfo = FormUserInfoForNewUser(NewUserRequest, accounts);
            var userAuthentication = FormUserAuthenticationForNewUser(NewUserRequest, userInfo);
            return userAuthentication;
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
        public Task<LoginResonse> VerifyLogin(LoginRequest userAuthentication);
        public Task<UserInfo> CreateUser(CreateUser NewUserRequest);
        public Task<IEnumerable<UserInfo>> GetAllUserInfos();
        public Task<UserInfo> GetUser(int Id);

    }
}
