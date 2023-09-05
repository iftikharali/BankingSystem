using BankingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Service.Interfaces
{
    public interface IAccountService
    {
        Account GetAccount(int id);
        IEnumerable<Account> GetAllAccounts(int personId);
        bool Create(Account account);
        bool Delete(int id);
    }
}
