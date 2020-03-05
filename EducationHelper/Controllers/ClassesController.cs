using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationHelper.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        Models.AppContext db;
        public ClassesController(Models.AppContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Class>> Get()
        {
            return db.Classes.ToList();
        }

        // GET /5
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> Get(int id)
        {
            Class res = await db.Classes.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
                return NotFound();
            return new ObjectResult(res);
        }

        // POST 
        [HttpPost]
        public async Task<ActionResult<User>> Post(Class _class)
        {
            if (_class == null)
            {
                return BadRequest();
            }

            db.Classes.Add(_class);
            await db.SaveChangesAsync();
            return Ok(_class);
        }

        // PUT
        [HttpPut]
        public async Task<ActionResult<Class>> Put(Class _class)
        {
            if (_class == null)
            {
                return BadRequest();
            }
            if (!db.Classes.Any(x => x.Id == _class.Id))
            {
                return NotFound();
            }

            db.Update(_class);
            await db.SaveChangesAsync();
            return Ok(_class);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<Class>> Delete(int id)
        {
            Class _class = db.Classes.FirstOrDefault(x => x.Id == id);
            if (_class == null)
            {
                return NotFound();
            }
            db.Classes.Remove(_class);
            await db.SaveChangesAsync();
            return Ok(_class);
        }
    }
}