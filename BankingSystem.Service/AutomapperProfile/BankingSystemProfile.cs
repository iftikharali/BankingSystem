using AutoMapper;
using BankingSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Service.AutomapperProfile
{
    public class BankingSystemProfile : Profile
    {
        public BankingSystemProfile() {
            //Account profile
            CreateMap<DAL.Models.AccountModel, Account>();
            CreateMap<Account, DAL.Models.AccountModel>();

            //Transaction profile
            CreateMap<DAL.Models.TransactionModel, Transaction>();
            CreateMap<Transaction, DAL.Models.TransactionModel>();

        }
    }
}
