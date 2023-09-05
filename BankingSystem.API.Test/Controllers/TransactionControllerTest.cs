using BankingSystem.API.Controllers;
using BankingSystem.Entities;
using BankingSystem.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.API.Test.Controllers
{
    public class TransactionControllerTest
    {
        Mock<ITransactionService> service;
        private TransactionController InitializeController()
        {
            service = new Mock<ITransactionService>();
            return new TransactionController(service.Object);
        }
        [Fact]
        public void TransactionController_GetByPersonId_Success()
        {
            //Arrange
            var controller = InitializeController();
            var transaction = new Entities.Transaction
            {
                AccountId = 1,
                Amount = 100,
                PersonId = 1,
                TransactionID = 1,
                TransactionType  = TransactionType.Deposit
            };
            service.Setup(x => x.GetTransactionsByPersonId(It.IsAny<int>())).Returns(new List<Transaction> { transaction });

            //Act
            var okResult = controller.GetByPersonId(1) as OkObjectResult;
            var result = okResult?.Value as List<Entities.Transaction>;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Count(), 1);
            Assert.Equal(result[0].AccountId, 1);
        }
        [Fact]
        public void TransactionController_GetByPersonId_NotFound()
        {
            //Arrange
            var controller = InitializeController();
            var transaction = new Entities.Transaction
            {
                AccountId = 1,
                Amount = 100,
                PersonId = 1,
                TransactionID = 1,
                TransactionType = TransactionType.Deposit
            };
            service.Setup(x => x.GetTransactionsByPersonId(It.IsAny<int>())).Returns((List<Transaction>)null);

            //Act
            var result = controller.GetByPersonId(1) as NotFoundResult;

            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void TransactionController_GetByPersonId_BadRequest()
        {
            //Arrange
            var controller = InitializeController();
            service.Setup(x => x.GetTransactionsByPersonId(It.IsAny<int>())).Throws(new InvalidOperationException());

            //Act
            var result = controller.GetByPersonId(1) as BadRequestObjectResult;

            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void TransactionController_Get_TransactionNotFound()
        {
            //Arrange
            var controller = InitializeController();
            service.Setup(x => x.GetTransaction(It.IsAny<int>())).Returns((Transaction)null);

            //Act
            var result = controller.Get(1) as NotFoundResult;

            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void TransactionController_Get_BadRequest()
        {
            //Arrange
            var controller = InitializeController();
            service.Setup(x => x.GetTransaction(It.IsAny<int>())).Throws(new InvalidOperationException());

            //Act
            var result = controller.Get(1) as BadRequestObjectResult;

            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void TransactionController_Get_Success()
        {
            //Arrange
            var controller = InitializeController();
            var transaction = new Transaction
            {
                AccountId = 1,
                Amount = 100,
                PersonId = 1,
                TransactionID = 1,
                TransactionType = TransactionType.Deposit
            };
            service.Setup(x => x.GetTransaction(It.IsAny<int>())).Returns( transaction );

            //Act
            var okResult = controller.Get(1) as OkObjectResult;
            var result = okResult?.Value as Transaction;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.AccountId);
        }
        [Fact]
        public void TransactionController_Create_Fail()
        {
            //Arrange
            var controller = InitializeController();
            var transaction = new Transaction
            {
                AccountId = 1,
                Amount = 100,
                PersonId = 1,
                TransactionID = 1,
                TransactionType = TransactionType.Deposit
            };
            service.Setup(x => x.CreateTransaction(It.IsAny<Transaction>())).Returns(false);

            //Act
            var result = controller.Post(transaction) as BadRequestResult;

            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void TransactionController_Create_Fail_For_Exception()
        {
            //Arrange
            var controller = InitializeController();
            var transaction = new Transaction
            {
                AccountId = 1,
                Amount = 100,
                PersonId = 1,
                TransactionID = 1,
                TransactionType = TransactionType.Deposit
            };
            service.Setup(x => x.CreateTransaction(It.IsAny<Transaction>())).Throws(new InvalidOperationException());

            //Act
            var result = controller.Post(transaction) as BadRequestObjectResult;

            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void TransactionController_Create_Success()
        {
            //Arrange
            var controller = InitializeController();
            var transaction = new Transaction
            {
                AccountId = 1,
                Amount = 100,
                PersonId = 1,
                TransactionID = 1,
                TransactionType = TransactionType.Deposit
            };
            service.Setup(x => x.CreateTransaction(It.IsAny<Transaction>())).Returns(true);

            //Act
            var okResult = controller.Post(transaction) as OkResult;

            //Assert
            Assert.NotNull(okResult);
        }
    }
}

