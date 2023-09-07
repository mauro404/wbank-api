namespace WBank.API.Models.DTO
{
    public class AccountInfoDto
    {
        public Guid Id { get; set; }
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
    }
}
