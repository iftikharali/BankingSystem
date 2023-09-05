using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DAL.Models
{
    public class BankingSystemContext
    {
        List<AccountModel> accounts = new List<AccountModel>();
        List<TransactionModel> transactions = new List<TransactionModel>();

        public virtual AccountModel GetAccount(int accountId)
        {
            return accounts.FirstOrDefault(x => x.AccountId == accountId);
        }
        public virtual AccountModel GetPersonsAccount(int personId, int accountId)
        {
            return accounts.FirstOrDefault(x => x.AccountId == accountId && x.PersonID == personId);
        }

        public virtual List<AccountModel > GetAccountsByUser(int userId) {
            return accounts.Where(x => x.PersonID == userId).ToList();
        }

        public virtual bool CreateAccount(AccountModel account)
        {
            try
            {
                accounts.Add(account);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual bool DeleteAccount(int accountId)
        {
            try
            {
                if(accounts.Remove(GetAccount(accountId)))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public virtual TransactionModel GetTransaction(int transactionId)
        {
            return transactions.FirstOrDefault(x => x.TransactionID == transactionId);
        }

        public virtual List<TransactionModel > GetTransactionsByUser(int userId)
        {
            return transactions.Where(x => x.PersonId == userId).ToList();
        }
        public virtual List<TransactionModel> GetTransactionsByAccount(int accountId) { 
            return transactions.Where(x => x.AccountId == accountId).ToList();
        }
        public virtual bool CreateTransaction(TransactionModel transaction)
        {
            try
            {
                var account = GetAccount(transaction.AccountId);
                if (transaction.TransactionType == TransactionType.Deposit)
                {
                    account.Balance += transaction.Amount;
                }
                if (transaction.TransactionType == TransactionType.Withdrawal)
                {
                    account.Balance -= transaction.Amount;
                }
                transaction.CreatedDate = DateTime.UtcNow;
                transactions.Add(transaction);
                return true;
            }catch
            {
                return false;
            }
        }
    }
}
