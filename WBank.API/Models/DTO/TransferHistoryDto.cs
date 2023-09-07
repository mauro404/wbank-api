namespace WBank.API.Models.DTO
{
    public class TransferHistoryDto
    {
        public Guid Id { get; set; }
        public List<TransferRecordDto> TransferRecords { get; set; }
    }
}
