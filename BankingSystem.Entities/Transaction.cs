using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Entities
{
    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }
    public class Transaction
    {
        public int TransactionID { get; set; }
        public TransactionType TransactionType { get; set; }
        public int AccountId { get; set; }
        public int PersonId { get; set; }
        public decimal Amount { get; set; }
    }
}
