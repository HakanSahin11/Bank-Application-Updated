using System.Text.Json.Serialization;

namespace Bank_Api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Sender { get; set; }
        public double Amount { get; set; }
        [JsonIgnore]
        public virtual Account FromAccount { get; set; }
        [JsonIgnore]
        public virtual Account ToAccount { get; set; }
        [JsonIgnore]
        public virtual Creditcard Creditcard { get; set; }
    }

    public class TransactionResponse : Transaction
    {
        public TransactionResponse(Transaction transaction)
        {
            Id = transaction.Id;
            Name = transaction.Name;
            Timestamp = transaction.Timestamp;
            Sender = transaction.Sender;
            Amount = transaction.Amount;
            FromAccountId = transaction.FromAccount.Id;
            ToAccountId = transaction.ToAccount.Id;
            SenderName = $"{transaction.FromAccount.UserInfo.Firstname} {transaction.FromAccount.UserInfo.Firstname}";
        }

        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public string SenderName { get; set; }
    }

    public class TransactionRequest
    {
        public double Amount { get; set; }
        public long FromAccountId { get; set; }
        public long ToAccountId { get; set; }
    }
}
