using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationHelper.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        Models.AppContext db;
        public NewsController(Models.AppContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<New>> Get()
        {
            return db.News.ToList();
        }

        // GET /5
        [HttpGet("{id}")]
        public async Task<ActionResult<New>> Get(int id)
        {
            New res = await db.News.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
                return NotFound();
            return new ObjectResult(res);
        }

        // POST 
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<New>> Post(New _new)
        {
            User user = db.Users.FirstOrDefault(p => p.Login == User.Identity.Name);
            if (_new == null)
            {
                return BadRequest();
            }
            _new.AuthorId = user.Id;
            db.News.Add(_new);
            await db.SaveChangesAsync();
            return Ok(_new);
        }

        // PUT
        [HttpPut]
        public async Task<ActionResult<New>> Put(New _new)
        {
            if (_new == null)
            {
                return BadRequest();
            }
            if (!db.News.Any(x => x.Id == _new.Id))
            {
                return NotFound();
            }

            db.Update(_new);
            await db.SaveChangesAsync();
            return Ok(_new);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<New>> Delete(int id)
        {
            New _new = db.News.FirstOrDefault(x => x.Id == id);
            if (_new == null)
            {
                return NotFound();
            }
            db.News.Remove(_new);
            await db.SaveChangesAsync();
            return Ok(_new);
        }
    }
}