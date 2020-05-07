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
        [HttpGet("RemoveFromEvent/")]
        [Authorize]
        public async Task<ActionResult<User>> RemoveFromEvent(int eventId, int userId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);

            db.UserEvents.Remove(db.UserEvents.FirstOrDefault(ue => ue.UserId == userId && ue.EventId == eventId));
            await db.SaveChangesAsync();

            return Ok(user);
        }
        [HttpGet("AddToEvent/")]
        [Authorize]
        public async Task<ActionResult<User>> AddToEvent(int eventId, int userId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);

            db.UserEvents.Add(new UserEvent { EventId = eventId, UserId = userId, Progress = 0, Total = 0 });
            await db.SaveChangesAsync();

            return Ok(user);
        }
        [HttpGet("GetForEvent/{eventId}")]
        [Authorize]
        public async Task<IEnumerable<User>> GetForEvent(int eventId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);
            Event _event = await db.Events.FirstAsync(p => p.Id == eventId);

            List<User> users = new List<User>();

            if (user != null && _event != null)
            {
                db.UserEvents.Where(p => p.EventId == eventId).ToList().ForEach(ue => users.Add(db.Users.First(u => u.Id == ue.UserId)));
            }
            return users;
        }

        [HttpGet("GetUsersIsNotEvent/{eventId}")]
        [Authorize]
        public async Task<IEnumerable<User>> GetUsersIsNotEvent(int eventId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);
            Event _event = await db.Events.FirstAsync(p => p.Id == eventId);

            List<User> users = new List<User>();

            if (user != null && _event != null)
            {
                if (user.Role == "1")
                    await db.Users.ForEachAsync(u => db.UserEvents.Where(ue => ue.EventId == eventId).ToList().ForEach(ue => { if (ue.UserId != u.Id) users.Add(u); }));
                if (user.Role == "3")
                {
                    UserClass userClass = db.UserClasses.FirstOrDefault(p => p.UserId == user.Id);
                    Class _class = db.Classes.FirstOrDefault(p => p.Id == userClass.ClasId);
                    await db.Users.ForEachAsync(u => db.UserEvents.Where(ue => ue.EventId == eventId).ToList().ForEach(ue => { if (ue.UserId != u.Id && db.UserClasses.FirstOrDefault(p => p.Id == u.Id).ClasId == _class.Id) users.Add(u); }));
                }
            }
            return users;
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