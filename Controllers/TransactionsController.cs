using Microsoft.AspNetCore.Mvc;
using AccountManagementModels;
using GCashAPI.Models;

namespace GCashAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private static accountManagementModels account = new accountManagementModels()
        {

            PIN = "1234"

        };


        
        [HttpGet]
        public IActionResult GetAccount()
        {
            return Ok(account);
        }

        
        [HttpGet("history")]
        public IActionResult GetHistory()
        {
            return Ok(account.Transactions);
        }

        
        [HttpPost("deposit")]
        public IActionResult Deposit([FromBody] AmountRequest request)
        {
            if (request.Amount <= 0)
            {
                return BadRequest("Amount must be greater than zero.");
            }

            account.AddBalance(request.Amount);

            return Ok(new
            {
                Message = "Deposit Successful",
                account.Balance,
                account.Transactions
            });
        }

        
        [HttpPost("withdraw")]
        public IActionResult Withdraw([FromBody] AmountRequest request)
        {
            if (request.Amount <= 0)
            {
                return BadRequest("Amount must be greater than zero.");
            }

            if (account.Balance < request.Amount)
            {
                return BadRequest("Insufficient Balance");
            }

            account.DeductBalance(request.Amount, "Withdrawal");

            return Ok(new
            {
                Message = "Withdrawal Successful",
                account.Balance,
                account.Transactions
            });
        }

        
        [HttpPost("paybill")]
        public IActionResult PayBill([FromBody] PayBillRequest request)
        {
            if (request.Amount <= 0)
            {
                return BadRequest("Amount must be greater than zero.");
            }

            if (account.Balance < request.Amount)
            {
                return BadRequest("Insufficient Balance!");
            }

            account.DeductBalance(
                request.Amount,
                $"Payment to {request.Biller}"
            );

            return Ok(new
            {
                Message = "Bill Payment Successful",
                account.Balance,
                account.Transactions
            });
        }

        
        [HttpPut("updatebalance")]
        public IActionResult UpdateBalance([FromBody] AmountRequest request)
        {
            if (request.Amount < 0)
            {
                return BadRequest("Balance cannot be negative.");
            }

            account.Balance = request.Amount;

            return Ok(new
            {
                Message = "Balance Updated",
                account.Balance
            });
        }

      
        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            if (id < 1 || id > account.Transactions.Count)
            {
                return NotFound("Transaction not found.");
            }

            string removed = account.Transactions[id - 1];

            account.Transactions.RemoveAt(id - 1);

            return Ok(new
            {
                Message = "Transaction Deleted",
                DeletedTransaction = removed,
                account.Transactions
            });
        }
    }
}