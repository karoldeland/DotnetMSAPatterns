using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotnetMSAPatterns.API.Controllers
{
    [Route("api/[controller]")]
    public class FlakyStringController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            bool mustFail = GetFailureFlag();
                        
            if (mustFail)
                return StatusCode(500, "Fail");

            return Ok("Success");
        }

        private bool GetFailureFlag()
        {
            var second = DateTime.Now.Second;

            return second % 2 > 0;
        }
    }
}
