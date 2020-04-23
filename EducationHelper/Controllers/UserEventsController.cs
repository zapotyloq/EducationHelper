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
    public class UserEventsController : ControllerBase
    {
        Models.AppContext db;
        public UserEventsController(Models.AppContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserEvent>> Get()
        {
            return db.UserEvents.ToList();
        }

        // GET /5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserEvent>> Get(int id)
        {
            UserEvent res = await db.UserEvents.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
                return NotFound();
            return new ObjectResult(res);
        }

        // POST 
        [HttpPost]
        public async Task<ActionResult<UserEvent>> Post(UserEvent userEvent)
        {
            if (userEvent == null)
            {
                return BadRequest();
            }

            db.UserEvents.Add(userEvent);
            await db.SaveChangesAsync();
            return Ok(userEvent);
        }

        // PUT
        [HttpPut]
        public async Task<ActionResult<UserEvent>> Put(UserEvent userEvent)
        {
            if (userEvent == null)
            {
                return BadRequest();
            }
            if (!db.UserEvents.Any(x => x.Id == userEvent.Id))
            {
                return NotFound();
            }

            db.Update(userEvent);
            await db.SaveChangesAsync();
            return Ok(userEvent);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserEvent>> Delete(int id)
        {
            UserEvent userEvent = db.UserEvents.FirstOrDefault(x => x.Id == id);
            if (userEvent == null)
            {
                return NotFound();
            }
            db.UserEvents.Remove(userEvent);
            await db.SaveChangesAsync();
            return Ok(userEvent);
        }
    }
}