using Bank_Api.Context;
using Bank_Api.Helpers;
using Bank_Api.Models;
using Bank_Api.Services;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bank_API_Tests
{
    //Todo
    public class UserServiceTests
    {
        private readonly UserService _serviceInTest;
        public UserServiceTests()
        {
            var contextMock = new Mock<BankDbContext>();
            var userInfoList = new List<UserInfo>();
            var userAuthenticationList = new List<UserAuthentication>();


            for (int i = 0; i < 10; i++)
            {
                var user = new UserInfo { Firstname = $"Test {i}", Lastname = $"User {i}", };
                var auth = new UserAuthentication { Email = $"Test@User{i}.dk", Password = "12345", UserInfo = user };

                userInfoList.Add(user);
                userAuthenticationList.Add(auth);
            }

            contextMock.Setup<DbSet<UserInfo>>(x => x.UserInfo)
                .ReturnsDbSet(userInfoList);

            contextMock.Setup<DbSet<UserAuthentication>>(x => x.UserAuthentication)
                .ReturnsDbSet(userAuthenticationList);

            var jwtTokenHelper = new Mock<IGenerateJWTTokenHelper>();
            jwtTokenHelper.Setup(x => x.GenerateJWTToken(userInfoList.First())).Returns("aaaaaaaa");

            _serviceInTest = new UserService(contextMock.Object, jwtTokenHelper.Object);
        }

        [Fact]
        public async void VerifyLogin_Valid()
        {
            var request = new LoginRequest
            {
                Email = "Test@User0.dk",
                Password = "12345"
            };

            var result = await _serviceInTest.VerifyLogin(request);
            Assert.NotNull(result);
            Assert.NotEmpty(result.Firstname);
            Assert.NotEmpty(result.Lastname);
            Assert.NotEmpty(result.Token);

        }
    }
}
