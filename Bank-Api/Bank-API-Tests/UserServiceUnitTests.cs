using Bank_Api.Context;
using Bank_Api.Helpers;
using Bank_Api.Models;
using Bank_Api.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Bank_API_Tests
{
    //Todo
    public class UserServiceTests
    {
        private readonly UserService _serviceInTest;
        private static readonly List<UserInfo> UserInfoList = new List<UserInfo>();
        private static readonly List<UserAuthentication> UserAuthenticationList = new List<UserAuthentication>();
        private static readonly List<Account> AccountList = new List<Account>();
        private static readonly List<Creditcard> CreditcardList = new List<Creditcard>();

        public UserServiceTests()
        {
            var contextMock = new Mock<BankDbContext>();
            var jwtTokenHelper = new Mock<IGenerateJWTTokenHelper>();

            for (int i = 0; i < 15; i++)
            {
                var user = new UserInfo { Firstname = $"Test {i}", Lastname = $"User {i}"};
                var auth = new UserAuthentication { Email = $"Test@User{i}.dk", Password = "12345", UserInfo = user };
                var account = new Account { AccountNumber = 1000000000 + i, Id = i, Money = 10000, Name = "Account " + i, UserInfo = user };
                var creditCard = new Creditcard { CardNo = 1000000000000000 + i, Id = i, Name = "Creditcard " + i,  Account = account};

                jwtTokenHelper.Setup(x => x.GenerateJWTToken(It.Is<UserInfo>(u => u.Firstname == user.Firstname && u.Lastname == user.Lastname))).Returns("aaaaaaaa");

                if(i < 10)
                {
                    UserInfoList.Add(user);
                    UserAuthenticationList.Add(auth);
                    AccountList.Add(account);
                    CreditcardList.Add(creditCard);
                }
            }

            contextMock.Setup<DbSet<UserInfo>>(x => x.UserInfo)
                .ReturnsDbSet(UserInfoList);

            contextMock.Setup<DbSet<UserAuthentication>>(x => x.UserAuthentication)
                .ReturnsDbSet(UserAuthenticationList);

            contextMock.Setup<DbSet<Account>>(x => x.Account)
                .ReturnsDbSet(AccountList);

            contextMock.Setup<DbSet<Creditcard>>(x => x.Creditcard)
                .ReturnsDbSet(CreditcardList);

            _serviceInTest = new UserService(contextMock.Object, jwtTokenHelper.Object);
        }

        [Theory]
        [InlineData("Test@User0.dk", "12345")]
        [InlineData("Test@User1.dk", "12345")]
        [InlineData("Test@User2.dk", "12345")]
        [InlineData("Test@User3.dk", "12345")]
        [InlineData("Test@User4.dk", "12345")]
        public async void VerifyLogin_Is_Valid(string Email, string Password)
        {
            var request = new LoginRequest
            {
                Email = Email,
                Password = Password
            };

            var result = await _serviceInTest.VerifyLogin(request);

            result.Should().NotBeNull();
            result.Firstname.Should().NotBeNullOrEmpty();
            result.Lastname.Should().NotBeNullOrEmpty();
            result.Token.Should().NotBeNullOrEmpty();
        }

        [Theory]
        [InlineData("Test@User0.dk", "123456")]
        [InlineData("Test@User1.dk", "123456")]
        [InlineData("Test@User2.dk", "123456")]
        [InlineData("Test@User3.dk", "123456")]
        [InlineData("Test@User4.dk", "123456")]
        public async void VerifyLogin_IsNot_Valid(string Email, string Password)
        {
            var request = new LoginRequest
            {
                Email = Email,
                Password = Password
            };

            var act = async () => { await _serviceInTest.VerifyLogin(request); };
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Invalid login");
        }

        [Theory]
        [InlineData("Test@User10.dk", "12345", "Test", "Test")]
        [InlineData("Test@User11.dk", "12345", "Test", "Test")]
        [InlineData("Test@User12.dk", "12345", "Test", "Test")]
        [InlineData("Test@User13.dk", "12345", "Test", "Test")]
        [InlineData("Test@User14.dk", "12345", "Test", "Test")]
        public async void FormNewUserRequest_Is_Valid(string Email, string Password, string Firstname, string Lastname)
        {
            var request = new CreateUser
            {
                Email = Email,
                Password = Password,
                Firstname = Firstname,
                Lastname = Lastname
            };

            var result = await _serviceInTest.FormNewUserRequest(request);
            result.Should().NotBeNull();
            result.Email.Should().BeEquivalentTo(request.Email);
            result.Password.Should().BeEquivalentTo(request.Password);
            result.UserInfo.Firstname.Should().BeEquivalentTo(request.Firstname);
            result.UserInfo.Lastname.Should().BeEquivalentTo(request.Lastname);
        }

        [Theory]
        [InlineData("Test@User0.dk", "123456", "Test", "Test")]
        [InlineData("Test@User1.dk", "123456", "Test", "Test")]
        [InlineData("Test@User2.dk", "123456", "Test", "Test")]
        [InlineData("Test@User3.dk", "123456", "Test", "Test")]
        [InlineData("Test@User4.dk", "123456", "Test", "Test")]
        public async Task FormNewUserRequest_Where_EmailTaken(string Email, string Password, string Firstname, string Lastname)
        {
            var request = new CreateUser
            {
                Email = Email,
                Password = Password,
                Firstname = Firstname,
                Lastname = Lastname
            };

            var act = async () => { await _serviceInTest.FormNewUserRequest(request); };
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Email already exist");
        }

        [Fact]
        public async void GetAllUserInfos_Is_Valid()
        {
            Assert.True(false);

        }

        [Fact]
        public async void GetAllUserInfos_IsNot_Valid()
        {
            Assert.True(false);

        }

        [Fact]
        public async void GetUser_Is_Valid()
        {
            Assert.True(false);

        }

        [Fact]
        public async void GetUser_IsNot_Valid()
        {
            Assert.True(false);

        }

    }
}
