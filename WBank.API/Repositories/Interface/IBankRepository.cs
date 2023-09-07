using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WBank.API.Models.Domain;
using WBank.API.Models.DTO;

namespace WBank.API.Repositories.Interface
{
    public interface IBankRepository
    {
        Task<Account> CreateAsync(Account account);
        Task<Account?> LoginAsync(LoginRequestDto request);
        Task<Account?> GetAccountAsync(Guid Id);
        public bool TransferFunds(TransferRequestDto request);
        public bool AddFunds(AddFundsRequestDto request);
        public TransferHistoryDto? GetTransferHistory(Guid accountId);
    }
}
