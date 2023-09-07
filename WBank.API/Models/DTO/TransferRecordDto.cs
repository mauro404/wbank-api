namespace WBank.API.Models.DTO
{
    public class TransferRecordDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public int ReceiverAccountNumber { get; set; }
        public DateTime TransactionTime { get; set; }
    }
}
