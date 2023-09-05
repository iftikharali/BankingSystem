using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.DAL.Models
{
    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }
    public class TransactionModel
    {
        public int TransactionID { get; set; }
        public int AccountId { get; set; }
        public int PersonId { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
