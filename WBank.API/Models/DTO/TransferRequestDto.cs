namespace WBank.API.Models.DTO
{
    public class TransferRequestDto
    {
        public Guid SenderAccountId { get; set; }
        public int ReceiverAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
