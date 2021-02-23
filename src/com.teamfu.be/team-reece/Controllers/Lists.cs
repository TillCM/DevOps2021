using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using team_reece.Models;

namespace team_reece.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListsController : ControllerBase
    {

       public teamfuContext _context;

        public ListsController(teamfuContext context )
        {
          _context = new teamfuContext();
        }
        

        [HttpGet]
        public List<ToDo> Get()
        {
            
            return  _context.ToDos.ToList();
        }

         // GET: api/Lists/5
        [HttpGet("{id}", Name = "Get")]
        public ToDo Get(int id)
        {
            return _context.ToDos.Find(id);
        }

         // POST: api/Lists
        [HttpPost]
        public IActionResult Post([FromBody] ToDo model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, model);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }   
        }


        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ToDo model)
        {
            _context.ToDos.Add(model);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
    }
}