using AutoMapper;
using BankingSystem.DAL.Models;
using BankingSystem.Entities;
using BankingSystem.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionType = BankingSystem.Entities.TransactionType;

namespace BankingSystem.Service
{
    public class TransactionService : ITransactionService
    {
        private BankingSystemContext _context;
        private IMapper _mapper;
        public TransactionService(BankingSystemContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public bool CreateTransaction(Transaction transaction)
        {
            if(transaction == null)
            {
                throw new ArgumentNullException("Transaction cannot be empty");
            }
            var account = _context.GetAccount(transaction.AccountId);
            if (account == null)
            {
                //We can create custom exception specific to constraints if needed
                throw new InvalidOperationException("Invalid account");
            }
            if (transaction.TransactionType == TransactionType.Deposit && transaction.Amount > 10000)
            {
                //We can create custom exception specific to constraints if needed
                throw new InvalidOperationException("Cannot Deposit more than $10,000");
            }
            if (transaction.TransactionType == TransactionType.Withdrawal)
            {
                decimal allowedBalance = account.Balance*90/100;
                if (transaction.Amount >= allowedBalance || account.Balance-transaction.Amount <=100)
                {
                    //We can create custom exception specific to constraints if needed
                    throw new InvalidOperationException("Cannot Withdraw given amount.");
                }
            }

            return _context.CreateTransaction(_mapper.Map<DAL.Models.TransactionModel>(transaction));
        }

        public Transaction GetTransaction(int transactionId)
        {
            var transaction = _context.GetTransaction(transactionId);
            return _mapper.Map<Transaction>(transaction);
        }

        public IEnumerable<Transaction> GetTransactionsByPersonId(int userId)
        {
            var transactions = _context.GetTransactionsByUser(userId);
            return _mapper.Map<IEnumerable<Transaction>>(transactions);
        }
        public IEnumerable<Transaction> GetTransactionsByAccount(int accountId)
        {
            var transactions = _context.GetTransactionsByAccount(accountId);
            return _mapper.Map<IEnumerable<Transaction>>(transactions);
        }
    }
}
