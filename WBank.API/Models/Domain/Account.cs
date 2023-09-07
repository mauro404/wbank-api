using System.Transactions;

namespace WBank.API.Models.Domain
{
    public class Account
    {
        public Guid Id { get; set; }
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public List<AccountTransaction> AccountTransactions { get; set; } = new List<AccountTransaction>();

    }
}
