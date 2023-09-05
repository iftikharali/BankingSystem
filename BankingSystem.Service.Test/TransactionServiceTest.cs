using AutoMapper;
using BankingSystem.DAL.Models;
using BankingSystem.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Service.Test
{
    public class TransactionServiceTest
    {
        private Mock<BankingSystemContext> context;
        private TransactionService InitializeService()
        {
            var mapperConfiguration = new MapperConfiguration(conf =>
            {
                conf.AddProfile(new AutomapperProfile.BankingSystemProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();
            context = new Mock<BankingSystemContext>();
            return new TransactionService(context.Object, mapper);
        }
        [Fact]
        public void TransactionService_Create_ArgumentNullException()
        {
            //Arrange
            var service = InitializeService();
            Transaction transaction = null;

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => service.CreateTransaction(transaction));
        }
        [Fact]
        public void TransactionService_Deposit_to_InvalidAccount()
        {
            //Arrange
            var service = InitializeService();
            context.Setup(x => x.GetAccount(It.IsAny<int>())).Returns((AccountModel)null);
            Transaction transaction = new Transaction { AccountId = 2, Amount= 100, PersonId=1, TransactionID= 1, TransactionType = Entities.TransactionType.Deposit};

            //Act
            //Assert
            Assert.Throws<InvalidOperationException>(() => service.CreateTransaction(transaction));
        }
        [Fact]
        public void TransactionService_Deposit_MaxAmount_Exceeded()
        {
            //Arrange
            var service = InitializeService();
            context.Setup(x => x.GetAccount(It.IsAny<int>())).Returns(new AccountModel());
            Transaction transaction = new Transaction { AccountId = 2, Amount = 10001, PersonId = 1, TransactionID = 1, TransactionType = Entities.TransactionType.Deposit };

            //Act
            //Assert
            Assert.Throws<InvalidOperationException>(() => service.CreateTransaction(transaction));
        }
        [Fact]
        public void TransactionService_Deposit_Success()
        {
            //Arrange
            var service = InitializeService();
            context.Setup(x => x.GetAccount(It.IsAny<int>())).Returns(new AccountModel());
            context.Setup(x => x.CreateTransaction(It.IsAny<TransactionModel>())).Returns(true);
            Transaction transaction = new Transaction { AccountId = 2, Amount = 100, PersonId = 1, TransactionID = 1, TransactionType = Entities.TransactionType.Deposit };

            //Act
            var result = service.CreateTransaction(transaction);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void TransactionService_WithDrawal_90_Percent_Amount_Exceeded()
        {
            //Arrange
            var service = InitializeService();
            context.Setup(x => x.GetAccount(It.IsAny<int>())).Returns(new AccountModel { AccountId = 1, Balance = 1000, PersonID = 1 });
            Transaction transaction = new Transaction { AccountId = 1, Amount = 901, PersonId = 1, TransactionID = 1, TransactionType = Entities.TransactionType.Withdrawal };

            //Act
            //Assert
            Assert.Throws<InvalidOperationException>(() => service.CreateTransaction(transaction));
        }
        [Fact]
        public void TransactionService_WithDrawal_MinAmount_Exceeded()
        {
            //Arrange
            var service = InitializeService();
            context.Setup(x => x.GetAccount(It.IsAny<int>())).Returns(new AccountModel { AccountId = 1, Balance = 110, PersonID = 1 });
            Transaction transaction = new Transaction { AccountId = 1, Amount = 50, PersonId = 1, TransactionID = 1, TransactionType = Entities.TransactionType.Withdrawal };

            //Act
            //Assert
            Assert.Throws<InvalidOperationException>(() => service.CreateTransaction(transaction));
        }
        [Fact]
        public void TransactionService_WithDrawal_Success()
        {
            //Arrange
            var service = InitializeService();
            context.Setup(x => x.GetAccount(It.IsAny<int>())).Returns(new AccountModel { AccountId = 1, Balance = 1000, PersonID = 1 });
            Transaction transaction = new Transaction { AccountId = 1, Amount = 900, PersonId = 1, TransactionID = 1, TransactionType = Entities.TransactionType.Withdrawal };

            //Act
            //Assert
            Assert.Throws<InvalidOperationException>(() => service.CreateTransaction(transaction));
        }
    }
}
