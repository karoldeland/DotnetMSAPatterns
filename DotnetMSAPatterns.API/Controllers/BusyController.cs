using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotnetMSAPatterns.API.Controllers
{
    [Route("api/[controller]")]
    public class BusyController : Controller
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
            var seconds = DateTime.Now.Second;
            var roundedToNearest5seconds = Math.Round(seconds / 5.0) * 5;

            return roundedToNearest5seconds % 2 > 0;
        }
    }
}
