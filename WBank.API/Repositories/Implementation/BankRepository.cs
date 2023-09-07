using WBank.API.Data;
using WBank.API.Models.Domain;
using WBank.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using WBank.API.Models.DTO;

namespace WBank.API.Repositories.Implementation
{
    public class BankRepository : IBankRepository
    {
        private readonly ApplicationDbContext dbContext;
        public BankRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AddFunds(AddFundsRequestDto request)
        {
            var account = dbContext.Accounts.FirstOrDefault(a => a.Id == request.AccountId);

            if (account == null)
            {
                return false; // Transaction failed;
            }

            account.Balance += request.Amount;
            dbContext.SaveChanges();

            return true; // Transaction successful;
        }

        public async Task<Account> CreateAsync(Account account)
        {
            await dbContext.Accounts.AddAsync(account);
            await dbContext.SaveChangesAsync();

            return account;
        }

        public async Task<Account?> GetAccountAsync(Guid Id)
        {
            return await dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == Id);
        }

        public TransferHistoryDto? GetTransferHistory(Guid accountId)
        {
            var account = dbContext.Accounts.Include(a => a.AccountTransactions).FirstOrDefault(a => a.Id == accountId);

            if (account == null)
                return null; // Account not found

            var transferRecords = account.AccountTransactions.Select(t => new TransferRecordDto
            {
                Id = t.Id,
                Amount = t.Amount,
                ReceiverAccountNumber = t.ReceiverAccountNumber,
                TransactionTime = t.TransactionTime
            }).ToList();

            var transferHistory = new TransferHistoryDto
            {
                Id = accountId,
                TransferRecords = transferRecords
            };

            return transferHistory;
        }

        public async Task<Account?> LoginAsync(LoginRequestDto loginRequest)
        {
            return await dbContext.Accounts.FirstOrDefaultAsync(a => a.Email == loginRequest.Email && a.Password == loginRequest.Password);
        }

         public bool TransferFunds(TransferRequestDto request)
        {
            var senderAccount = dbContext.Accounts.FirstOrDefault(a => a.Id == request.SenderAccountId);
            var receiverAccount = dbContext.Accounts.FirstOrDefault(a => a.AccountNumber == request.ReceiverAccountNumber);

            if (senderAccount == null || receiverAccount == null)
            {
                return false; // Sender or receiver account not found
            }

            if (senderAccount.Balance < request.Amount)
            {
                return false; // Insufficient balance
            }

            senderAccount.Balance -= request.Amount;
            receiverAccount.Balance += request.Amount;

            // Create a transaction record
            var transaction = new AccountTransaction
            {
                Amount = request.Amount,
                ReceiverAccountNumber = request.ReceiverAccountNumber,
                TransactionTime = DateTime.UtcNow,
                SenderAccountId = request.SenderAccountId
            };

            dbContext.AccountTransactions.Add(transaction);
            dbContext.SaveChanges();

            return true; // Transaction successful
        }
    }
}
