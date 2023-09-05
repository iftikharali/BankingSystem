using BankingSystem.Entities;
using BankingSystem.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService) { 
            _transactionService = transactionService;
        }
        // GET: api/<TransactionController>
        [HttpGet("transactionByPerson{personId}")]
        public IActionResult GetByPersonId(int personId)
        {
            try
            {
                var transactions = _transactionService.GetTransactionsByPersonId(personId);
                if(transactions == null || transactions.Count() == 0) {
                    return NotFound();
                }
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<TransactionController>/5
        [HttpGet("{transactionId}")]
        public IActionResult Get(int transactionId)
        {
            try
            {
                var transaction = _transactionService.GetTransaction(transactionId);
                if(transaction == null) {
                    return NotFound();
                }
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<TransactionController>
        [HttpPost]
        public IActionResult Post([FromBody] Transaction transaction)
        {
            try
            {
                if (_transactionService.CreateTransaction(transaction)) 
                    return Ok();
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
                
        }

    }
}
