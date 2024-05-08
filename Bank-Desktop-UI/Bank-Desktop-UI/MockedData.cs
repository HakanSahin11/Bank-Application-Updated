using Bank_Desktop_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Desktop_UI
{
    public static class MockedData
    {
        private static Random Rnd = new Random();
        public static List<Creditcard> GetMockedCreditcardList()
        {
            var creditcards = new List<Creditcard>()
            {
                new()
                {
                    Id = Rnd.Next(1,1000000),
                    CardNo = Rnd.NextInt64(1111111111111111, 9999999999999999),
                    Name = "Main Visa Card"
                },

                new()
                {
                    Id = Rnd.Next(1, 1000000),
                    CardNo = Rnd.NextInt64(1111111111111111, 9999999999999999),
                    Name = "Visa Card"
                },

                new()
                {
                    Id = Rnd.Next(1, 1000000),
                    CardNo = Rnd.NextInt64(1111111111111111, 9999999999999999),
                    Name = "Spare Card"
                },
            };
            return creditcards;
        }
        public static List<Account> GetMockedAccountsList()
        {
            var accounts = new List<Account>()
            {
                new()
                {
                    Id = 11111111,
                    Name = "Main Account",
                    Money = Rnd.Next(100, 50000),
                    AccountNumber = 1111111111
                },
                new()
                {
                    Id = 22222222,
                    Name = "Savings Account",
                    Money = Rnd.Next(100, 50000),
                    AccountNumber = 2222222222
                },
                new()
                {
                    Id = 33333333,
                    Name = "Company Account",
                    Money = Rnd.Next(100, 50000),
                    AccountNumber = 3333333333
                },
            };
            return accounts;
        }

        public static List<Transaction> GetMockedTransactionsList()
        {
            var transactions = new List<Transaction>()
            {
                new()
                {
                    Id = Rnd.Next(1,1000000),
                    Name = "Lidl",
                    FromAccountId = 11111111,
                    ToAccountId = 33333333,
                    Amount = Rnd.Next(10,1500),
                    Sender = true,
                    Timestamp = DateTime.Now.AddHours(-1),
                },
                new()
                {
                    Id = Rnd.Next(1,1000000),
                    Name = "Føtex",
                    FromAccountId = 11111111,
                    ToAccountId = 33333333,
                    Amount = Rnd.Next(10,1500),
                    Sender = true,
                    Timestamp = DateTime.Now.AddHours(-2),
                },
                new()
                {
                    Id = Rnd.Next(1,1000000),
                    Name = "Bilka",
                    FromAccountId = 11111111,
                    ToAccountId = 22222222,
                    Amount = Rnd.Next(10,1500),
                    Sender = true,
                    Timestamp = DateTime.Now.AddHours(-10),
                },
                new()
                {
                    Id = Rnd.Next(1,1000000),
                    Name = "Proshop",
                    FromAccountId = 11111111,
                    ToAccountId = 33333333,
                    Amount = Rnd.Next(10,1500),
                    Sender = true,
                    Timestamp = DateTime.Now.AddDays(-1),
                },
                new()
                {
                    Id = Rnd.Next(1,1000000),
                    Name = "Rema",
                    FromAccountId = 22222222,
                    ToAccountId = 33333333,
                    Amount = Rnd.Next(10,1500),
                    Sender = true,
                    Timestamp = DateTime.Now.AddDays(-2),
                },
                new()
                {
                    Id = Rnd.Next(1,1000000),
                    Name = "Føtex",
                    FromAccountId = 33333333,
                    ToAccountId = 22222222,
                    Amount = Rnd.Next(10,1500),
                    Sender = true,
                    Timestamp = DateTime.Now.AddDays(-2).AddHours(-1),
                },
                new()
                {
                    Id = Rnd.Next(1,1000000),
                    Name = "Amazon",
                    FromAccountId = 22222222,
                    ToAccountId = 33333333,
                    Amount = Rnd.Next(10,1500),
                    Sender = true,
                    Timestamp = DateTime.Now.AddDays(-2).AddHours(-4),
                },
                new()
                {
                    Id = Rnd.Next(1,1000000),
                    Name = "Netflix",
                    FromAccountId = 33333333,
                    ToAccountId = 22222222,
                    Amount = Rnd.Next(10,1500),
                    Sender = false,
                    Timestamp = DateTime.Now.AddDays(-4).AddHours(-1),
                },
                new()
                {
                    Id = Rnd.Next(1,1000000),
                    Name = "Føtex",
                    FromAccountId = 11111111,
                    ToAccountId = 22222222,
                    Amount = Rnd.Next(10,1500),
                    Sender = false,
                    Timestamp = DateTime.Now.AddDays(-6).AddHours(-1),
                },
                new()
                {
                    Id = Rnd.Next(1,1000000),
                    Name = "Amazon",
                    FromAccountId = 33333333,
                    ToAccountId = 11111111,
                    Amount = Rnd.Next(10,1500),
                    Sender = true,
                    Timestamp = DateTime.Now.AddDays(-7).AddHours(-1),
                },
                new()
                {
                    Id = Rnd.Next(1,1000000),
                    Name = "Bilka",
                    FromAccountId = 22222222,
                    ToAccountId = 11111111,
                    Amount = Rnd.Next(10,1500),
                    Sender = false,
                    Timestamp = DateTime.Now.AddDays(-1).AddHours(-1),
                },
            };

            return transactions;
        }
    }
}
