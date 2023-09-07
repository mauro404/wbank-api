namespace WBank.API.Models.DTO
{
    public class AddFundsRequestDto
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
