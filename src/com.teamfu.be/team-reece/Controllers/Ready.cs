using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace team_reece.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReadyController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            
         return Ok();
        }
    }
}