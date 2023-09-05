using BankingSystem.API.Controllers;
using BankingSystem.Entities;
using BankingSystem.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BankingSystem.API.Test.Controllers
{
    public class AccountControllerTest
    {
        Mock<IAccountService> service;
        private AccountController InitializeController()
        {
            service = new Mock<IAccountService>();
            return new AccountController(service.Object);
        }

        #region GetPersonsAccount method test
        [Fact]
        public void AccountController_GetPersonsAccount_Success()
        {
            //Arrange
            var controller = InitializeController();
            var account = new Entities.Account
            {
                AccountId = 1,
                PersonID = 1,
                Balance = 100
            };
            service.Setup(x => x.GetAllAccounts(It.IsAny<int>())).Returns(new List<Account> { account });

            //Act
            var okResult = controller.GetPersonsAccount(1) as OkObjectResult;
            var result = okResult?.Value as List<Entities.Account>;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Count(), 1);
            Assert.Equal(result[0].AccountId, 1);
        }
        [Fact]
        public void AccountController_GetPersonsAccount_NoFound()
        {
            //Arrange
            var controller = InitializeController();
            var account = new Entities.Account
            {
                AccountId = 1,
                PersonID = 1,
                Balance = 100
            };
            service.Setup(x => x.GetAllAccounts(2)).Returns(new List<Account> { account });

            //Act
            var okResult = controller.Get(1);


            //Assert
            Assert.IsType<NotFoundResult>(okResult);
        }
        [Fact]
        public void AccountController_GetPersonsAccount_NoFound_For_ServiceException()
        {
            //Arrange
            var controller = InitializeController();
            var account = new Entities.Account
            {
                AccountId = 1,
                PersonID = 1,
                Balance = 100
            };
            service.Setup(x => x.GetAllAccounts(It.IsAny<int>())).Throws(new Exception("Account not found"));

            //Act
            var okResult = controller.Get(1);

            //Assert
            Assert.IsType<NotFoundResult>(okResult);
        }
        #endregion

        #region GET method test
        [Fact]
        public void AccountController_Get_Success()
        {
            //Arrange
            var controller = InitializeController();
            var account = new Entities.Account
            {
                AccountId = 1,
                PersonID = 1,
                Balance = 100
            };
            service.Setup(x => x.GetAccount(It.IsAny<int>())).Returns(account);

            //Act
            var okResult = controller.Get(1) as OkObjectResult;
            var result = okResult?.Value as Entities.Account;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.AccountId, account.AccountId);
        }
        [Fact]
        public void AccountController_Get_NoFound()
        {
            //Arrange
            var controller = InitializeController();
            var account = new Entities.Account
            {
                AccountId = 1,
                PersonID = 1,
                Balance = 100
            };
            service.Setup(x => x.GetAccount(2)).Returns(account);

            //Act
            var okResult = controller.Get(1);
            

            //Assert
            Assert.IsType<NotFoundResult>(okResult);
        }
        #endregion

        #region POST method test
        [Fact]
        public void AccountController_Post_Success()
        {
            //Arrange
            var controller = InitializeController();
            var account = new Entities.Account
            {
                AccountId = 1,
                PersonID = 1,
                Balance = 100
            };
            service.Setup(x => x.Create(It.IsAny<Account>())).Returns(true);

            //Act
            var okResult = controller.Post(account) as OkResult;


            //Assert
            Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
        }
        [Fact]
        public void AccountController_Post_Failed()
        {
            //Arrange
            var controller = InitializeController();
            var account = new Entities.Account
            {
                AccountId = 1,
                PersonID = 1,
                Balance = 100
            };
            service.Setup(x => x.Create(It.IsAny<Account>())).Throws(new Exception("Account already exists"));

            //Act
            var okResult = controller.Post(account) as BadRequestObjectResult;

            //Assert
            Assert.Equal(okResult.Value,"Account already exists");
        }
        #endregion

        #region Delete method test
        [Fact]
        public void AccountController_Delete_Success()
        {
            //Arrange
            var controller = InitializeController();
            var account = new Entities.Account
            {
                AccountId = 1,
                PersonID = 1,
                Balance = 100
            };
            service.Setup(x => x.Delete(It.IsAny<int>())).Returns(true);

            //Act
            var okResult = controller.Delete(1) as OkResult;


            //Assert
            Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
        }
        [Fact]
        public void AccountController_Delete_Failed()
        {
            //Arrange
            var controller = InitializeController();
            var account = new Entities.Account
            {
                AccountId = 1,
                PersonID = 1,
                Balance = 100
            };
            service.Setup(x => x.Delete(It.IsAny<int>())).Throws(new Exception("Account Not found"));

            //Act
            var okResult = controller.Delete(1) as BadRequestObjectResult;

            //Assert
            Assert.Equal(okResult?.Value, "Account Not found");
        }
        #endregion
    }
}