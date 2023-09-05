using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public int PersonID { get; set; }
        public decimal Balance { get; set; }
    }
}
