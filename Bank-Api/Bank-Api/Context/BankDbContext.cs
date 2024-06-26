using Bank_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank_Api.Context
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) 
        { 
        
        }

        public BankDbContext()
        {

        }

        public virtual DbSet<UserAuthentication> UserAuthentication { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<Account> Account{ get; set; }
        public virtual DbSet<Creditcard> Creditcard { get; set; }
        public virtual DbSet<Transaction> Transaction{ get; set; }
    }
}
