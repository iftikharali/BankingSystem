using BankingSystem.DAL.Models;

namespace BankingSystem.DAL.Test.Models
{
    public class BankingSystemContextTest
    {
        private BankingSystemContext _context;
        public BankingSystemContextTest() {
            ConfigureContext();
        }
        private void ConfigureContext()
        {
            _context = new BankingSystemContext();
            _context.CreateAccount(new AccountModel { AccountId = 1, Balance = 100, PersonID = 1 });
            _context.CreateTransaction(new TransactionModel { TransactionID = 1, AccountId = 1, Amount=100, PersonId = 1 });

        }
        #region Account test
        [Fact]
        public void BankingSystemContext_GetAccount_Success()
        {
            //Act
            var result = _context.GetAccount(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.Balance);
        }
        [Fact]
        public void BankingSystemContext_GetPersonsAccount_Success()
        {
            //Act
            var result = _context.GetPersonsAccount(1,1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.Balance);
        }
        [Fact]
        public void BankingSystemContext_CreateAccount_Success()
        {
            //Act
            var result = _context.CreateAccount(new AccountModel { AccountId = 6, Balance = 100, PersonID = 2 });

            //Assert
            Assert.True(result);
        }
        [Fact]
        public void BankingSystemContext_DeleteAccount_Fail()
        {
            //Act
            var result = _context.DeleteAccount(22);

            //Assert
            Assert.False(result);
        }
        [Fact]
        public void BankingSystemContext_DeleteAccount_Success()
        {
            //Act
            var result = _context.DeleteAccount(1);

            //Assert
            Assert.True(result);
        }
        #endregion
        #region Transaction Test
        [Fact]
        public void BankingSystemContext_GetTransaction_Success()
        {
            //Act
            var result = _context.GetTransaction(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.TransactionID);
        }
        [Fact]
        public void BankingSystemContext_GetTransaction_NotFound()
        {
            //Act
            var result = _context.GetTransaction(2);

            //Assert
            Assert.Null(result);
        }
        [Fact]
        public void BankingSystemContext_GetTransactionByUser_Success()
        {
            //Act
            var result = _context.GetTransactionsByUser(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
        }
        [Fact]
        public void BankingSystemContext_GetTransactionByUser_NotFound()
        {
            //Act
            var result = _context.GetTransactionsByUser(2);

            //Assert
            Assert.Equal(0,result.Count);
        }
        [Fact]
        public void BankingSystemContext_GetTransactionByAccount_Success()
        {
            //Act
            var result = _context.GetTransactionsByAccount(1);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
        }
        [Fact]
        public void BankingSystemContext_GetTransactionByAccount_NotFound()
        {
            //Act
            var result = _context.GetTransactionsByAccount(2);

            //Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void BankingSystemContext_CreateTransaction_Success()
        {
            //Act
            var result = _context.CreateTransaction(new TransactionModel { TransactionID = 2, AccountId = 1, TransactionType = TransactionType.Deposit, Amount = 100, PersonId = 1 });
            var account = _context.GetAccount(1);
            
            //Assert
            Assert.True(result);
            Assert.Equal(300, account.Balance);
        }
        [Fact]
        public void BankingSystemContext_CreateTransaction_Fail()
        {
            //Act
            var result = _context.CreateTransaction(new TransactionModel { TransactionID = 2, AccountId = 1, TransactionType = TransactionType.Withdrawal, Amount = 110, PersonId = 1 });
            var account = _context.GetAccount(1);
            
            //Assert
            Assert.True(result);
            Assert.Equal(90, account.Balance);
        }
        #endregion
    }
}