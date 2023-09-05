using BankingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Service.Interfaces
{
    public interface ITransactionService
    {
        bool CreateTransaction(Transaction transaction);
        Transaction GetTransaction(int transactionId);
        IEnumerable<Transaction> GetTransactionsByPersonId(int personId);
    }
}
