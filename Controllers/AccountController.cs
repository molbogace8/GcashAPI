using Microsoft.AspNetCore.Mvc;
using AccountManagementModels;

namespace GCashAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private static accountManagementModels account = new accountManagementModels();

        [HttpGet]
        public IActionResult GetAccount()
        {
            return Ok(account);
        }
    }
}