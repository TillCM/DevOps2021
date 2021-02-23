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
 
    public class ListsController : ControllerBase
    {
        
        private readonly  teamfuContext db;

        public Lists(teamfuContext context)
        {
            db = context;
        }

        

        [HttpGet]
        public ActionResult<IEnumerable<Task>> Get()
        {
           return 
        }

       
        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody] Task model)
        {
            try
            {
                db.Task.Add(model);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, model);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
    }
}