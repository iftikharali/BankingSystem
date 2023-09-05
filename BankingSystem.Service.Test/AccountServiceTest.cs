using AutoMapper;
using BankingSystem.DAL.Models;
using BankingSystem.Entities;
using Moq;

namespace BankingSystem.Service.Test
{
    public class AccountServiceTest
    {
        private Mock<BankingSystemContext> context;
        private AccountService InitializeService()
        {
            var mapperConfiguration = new MapperConfiguration(conf =>
            {
                conf.AddProfile(new AutomapperProfile.BankingSystemProfile());
            });
            var mapper = mapperConfiguration.CreateMapper();
            context = new Mock<BankingSystemContext>();
            return new AccountService(context.Object, mapper);
        }
        [Fact]
        public void AccountService_Create_ArgumentNullException()
        {
            //Arrange
            var service = InitializeService();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => service.Create(null));
        }
        [Fact]
        public void AccountService_Create_InvalidOperationException()
        {
            //Arrange
            var service = InitializeService();
            context.Setup(x => x.GetPersonsAccount(It.IsAny<int>(), It.IsAny<int>())).Returns(new AccountModel());
            //Act
            //Assert
            Assert.Throws<InvalidOperationException>(() => service.Create(new Account()));
        }
        [Fact]
        public void AccountService_Create_MinimumBalanceViolation()
        {
            //Arrange
            var service = InitializeService();
            context.Setup(x => x.GetPersonsAccount(It.IsAny<int>(), It.IsAny<int>())).Returns(new AccountModel());
            //Act
            //Assert
            Assert.Throws<InvalidOperationException>(() => service.Create(new Account { Balance = 90 }));

        }
        [Fact]
        public void AccountService_Create_Success()
        {
            //Arrange
            var service = InitializeService();
            var accountModel = new DAL.Models.AccountModel
            {
                AccountId = 1,
                Balance = 1,
                PersonID = 1
            };
            context.Setup(x => x.CreateAccount(It.IsAny<DAL.Models.AccountModel>())).Returns(true);
            //Act
            var result = service.Create(new Account { Balance = 900 });
            //Assert
            Assert.True(result);

        }

        #region Delete method test

        [Fact]
        public void AccountService_Delete_Failed()
        {
            //Arrange
            var service = InitializeService();
            context.Setup(x => x.GetAccount(It.IsAny<int>())).Returns((DAL.Models.AccountModel)null);
            //Act
            //Assert
            Assert.Throws<InvalidOperationException>(() => service.Delete(2));

        }

        [Fact]
        public void AccountService_Delete_Success()
        {
            //Arrange
            var service = InitializeService();
            var accountModel = new DAL.Models.AccountModel
            {
                AccountId = 1,
                Balance = 1,
                PersonID = 1
            };
            context.Setup(x => x.DeleteAccount(It.IsAny<int>())).Returns(true);
            context.Setup(x => x.GetAccount(It.IsAny<int>())).Returns(accountModel);
            //Act
            var result = service.Delete(1);
            //Assert
            Assert.True(result);

        }
        #endregion

        #region GetAccount method test

        [Fact]
        public void AccountService_GetAccount_Success()
        {
            //Arrange
            var service = InitializeService();
            var accountModel = new DAL.Models.AccountModel
            {
                AccountId = 1,
                Balance = 1,
                PersonID = 1
            };
            context.Setup(x => x.GetAccount(It.IsAny<int>())).Returns(accountModel);
            //Act
            var result = service.GetAccount(3);
            //Assert
            Assert.Equal(result.AccountId, accountModel.AccountId);

        }
        #endregion
        #region GetAllAccounts method test

        [Fact]
        public void AccountService_GetAllAccounts_Success()
        {
            //Arrange
            var service = InitializeService();
            var accountModel = new DAL.Models.AccountModel
            {
                AccountId = 1,
                Balance = 1,
                PersonID = 1
            };
            context.Setup(x => x.GetAccountsByUser(1)).Returns(new List<DAL.Models.AccountModel> { accountModel });
            //Act
            var result = service.GetAllAccounts(1).ToList();
            //Assert
            Assert.Equal(result[0].AccountId, accountModel.AccountId);

        }
        #endregion
    }
}