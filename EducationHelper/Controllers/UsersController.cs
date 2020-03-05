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
    public class UsersController : ControllerBase
    {
        Models.AppContext db;
        public UsersController(Models.AppContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return db.Users.ToList();
        }

        // GET /5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            User res = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
                return NotFound();
            return new ObjectResult(res);
        }

        // POST 
        [HttpPost]
        public async Task<ActionResult<User>> Post(User _user)
        {
            if (_user == null)
            {
                return BadRequest();
            }

            db.Users.Add(_user);
            await db.SaveChangesAsync();
            return Ok(_user);
        }

        // PUT
        [HttpPut]
        public async Task<ActionResult<User>> Put(User _user)
        {
            if (_user == null)
            {
                return BadRequest();
            }
            if (!db.Users.Any(x => x.Id == _user.Id))
            {
                return NotFound();
            }

            db.Update(_user);
            await db.SaveChangesAsync();
            return Ok(_user);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> Delete(int id)
        {
            User _user = db.Users.FirstOrDefault(x => x.Id == id);
            if (_user == null)
            {
                return NotFound();
            }
            db.Users.Remove(_user);
            await db.SaveChangesAsync();
            return Ok(_user);
        }
    }
}