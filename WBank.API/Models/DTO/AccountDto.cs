using System.Transactions;
using WBank.API.Models.Domain;

namespace WBank.API.Models.DTO
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public List<AccountTransaction> AccountTransactions { get; set; }// = new List<Transaction>();
    }
}
