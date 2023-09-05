using BankingSystem.Entities;
using BankingSystem.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accountService;
        public AccountController(IAccountService accountService) {
            _accountService = accountService;
        }
        // GET: api/<AccountController>
        [HttpGet("personAccount/{personId}")]
        public IActionResult GetPersonsAccount(int personId)
        {
            try
            {
                var accounts = _accountService.GetAllAccounts(personId);
                if(accounts == null || accounts.Count() == 0) {
                    return NotFound();
                }
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<AccountController>/5
        [HttpGet("{accountId}")]
        public IActionResult Get(int accountId)
        {
            try
            {
                var account = _accountService.GetAccount(accountId);
                if(account == null) {
                    return NotFound();
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<AccountController>
        [HttpPost]
        public IActionResult Post([FromBody] Account account)
        {
            try
            {
                if (_accountService.Create(account))
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{accountId}")]
        public IActionResult Delete(int accountId)
        {
            try { 
                if(_accountService.Delete(accountId))
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
