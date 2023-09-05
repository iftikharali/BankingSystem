using AutoMapper;
using BankingSystem.DAL.Models;
using BankingSystem.Entities;
using BankingSystem.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Service
{
    public class AccountService : IAccountService
    {
        private BankingSystemContext _context;
        private IMapper _mapper;
        public AccountService(BankingSystemContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }
        public bool Create(Account account)
        {
            if(account == null)
            {
                throw new ArgumentNullException("Invalid account");
            }
            if(_context.GetPersonsAccount(account.AccountId, account.PersonID) != null)
            {
                throw new InvalidOperationException("Account already exists with give account number");
            }
            if(account.Balance < 100)
            {
                throw new InvalidOperationException("Minimum amount should be above $100");
            }
            return _context.CreateAccount(_mapper.Map<DAL.Models.AccountModel>(account));
        }

        public bool Delete(int id)
        {
            if (_context.GetAccount(id) == null)
            {
                throw new InvalidOperationException("Account does not exists with give account number");
            }
            return _context.DeleteAccount(id);
        }

        public Account GetAccount(int id)
        {
            var account = _context.GetAccount(id);
            return _mapper.Map<Account>(account);
        }

        public IEnumerable<Account> GetAllAccounts(int personId)
        {
            var accounts = _context.GetAccountsByUser(personId);
            return _mapper.Map<IEnumerable<Account>>(accounts);
        }

    }
}
