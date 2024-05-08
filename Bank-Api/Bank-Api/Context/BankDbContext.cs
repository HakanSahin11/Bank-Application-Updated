using Bank_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank_Api.Context
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) 
        { 
        
        }

        public DbSet<UserAuthentication> UserAuthentication { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Account> Account{ get; set; }
        public DbSet<Creditcard> Creditcard { get; set; }
        public DbSet<Transaction> Transaction{ get; set; }
    }
}
