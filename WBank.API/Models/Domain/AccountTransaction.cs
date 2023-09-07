namespace WBank.API.Models.Domain
{
    public class AccountTransaction
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public int ReceiverAccountNumber { get; set; }
        public DateTime TransactionTime { get; set; }
        public Guid SenderAccountId { get; set; }
    }
}
