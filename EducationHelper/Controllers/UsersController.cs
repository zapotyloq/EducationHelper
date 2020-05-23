using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        [HttpGet("Profile")]
        public async Task<ActionResult<User>> Profile()
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);
            return new ObjectResult(user);
        }
        [HttpGet("RemoveFromVote")]
        [Authorize]
        public async Task<ActionResult<User>> RemoveFromVote(int voteId, int userId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);

            db.UserVotes.Remove(db.UserVotes.FirstOrDefault(ue => ue.UserId == userId && ue.VoteId == voteId));
            await db.SaveChangesAsync();

            return Ok(user);
        }
        [HttpGet("RemoveFromEvent")]
        [Authorize]
        public async Task<ActionResult<User>> RemoveFromEvent(int eventId, int userId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);

            db.UserEvents.Remove(db.UserEvents.FirstOrDefault(ue => ue.UserId == userId && ue.EventId == eventId));
            await db.SaveChangesAsync();

            return Ok(user);
        }
        [HttpGet("RemoveFromNew")]
        [Authorize]
        public async Task<ActionResult<User>> RemoveFromNew(int newId, int userId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);

            db.UserNews.Remove(db.UserNews.FirstOrDefault(ue => ue.UserId == userId && ue.NewId == newId));
            await db.SaveChangesAsync();

            return Ok(user);
        }
        [HttpGet("AddToVote/")]
        [Authorize]
        public async Task<ActionResult<User>> AddToEvent(int voteId, int userId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);

            db.UserVotes.Add(new UserVote { VoteId = voteId, UserId = userId});
            await db.SaveChangesAsync();

            return Ok(user);
        }
        [HttpGet("AddToNew/")]
        [Authorize]
        public async Task<ActionResult<User>> AddToNew(int newId, int userId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);

            db.UserNews.Add(new UserNew { NewId = newId, UserId = userId });
            await db.SaveChangesAsync();

            return Ok(user);
        }
        [HttpGet("GetForEvent/{eventId}")]
        [Authorize]
        public async Task<IEnumerable<User>> GetForEvent(int eventId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);
            Event _event = await db.Events.FirstAsync(p => p.Id == eventId);

            List<User> users = UsersInEvent(eventId).Result.ToList();
            return users;
        }
        [HttpGet("GetForNew/{newId}")]
        [Authorize]
        public async Task<IEnumerable<User>> GetForNew(int newId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);
            New _new = await db.News.FirstAsync(p => p.Id == newId);

            List<User> users = UsersInNew(newId).Result.ToList();
            return users;
        }
        [HttpGet("GetForVote/{voteId}")]
        [Authorize]
        public async Task<IEnumerable<User>> GetForVote(int voteId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);
            Vote vote = await db.Votes.FirstAsync(p => p.Id == voteId);

            List<User> users = UsersInVote(voteId).Result.ToList();
            return users;
        }
        public async Task<IEnumerable<User>> UsersInEvent(int eventId) {
            List<User> users = new List<User>();
            db.UserEvents.Where(p => p.EventId == eventId).ToList().ForEach(ue => users.Add(db.Users.First(u => u.Id == ue.UserId)));
            return users;
        }
        public async Task<IEnumerable<User>> UsersInVote(int voteId)
        {
            List<User> users = new List<User>();
            db.UserVotes.Where(p => p.VoteId == voteId).ToList().ForEach(ue => users.Add(db.Users.First(u => u.Id == ue.UserId)));
            return users;
        }
        public async Task<IEnumerable<User>> UsersInNew(int newId)
        {
            List<User> users = new List<User>();
            db.UserNews.Where(p => p.NewId == newId).ToList().ForEach(ue => users.Add(db.Users.First(u => u.Id == ue.UserId)));
            return users;
        }

        [HttpGet("GetUsersIsNotEvent/{eventId}")]
        [Authorize]
        public async Task<IEnumerable<User>> GetUsersIsNotEvent(int eventId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);
            Event _event = await db.Events.FirstAsync(p => p.Id == eventId);

            List<User> users = db.Users.ToList();
            List<User> usersInEvent = UsersInEvent(eventId).Result.ToList();

            List<User> result = new List<User>();

            if (user != null && _event != null)
            {
                if (user.Role == "1") {
                    result = users.Except(usersInEvent).ToList();
                    //db.Users.ToList().ForEach(u => db.UserEvents.Where(ue => ue.EventId == eventId).ToList().ForEach(ue => { if (ue.UserId != u.Id) users.Add(u); }));
                }
                if (user.Role == "3")
                {
                    UserClass userClass = db.UserClasses.FirstOrDefault(p => p.UserId == user.Id);
                    Class _class = db.Classes.FirstOrDefault(p => p.Id == userClass.ClassId);
                    //List<User> usersInClass = db.UserClasses.Where(ue => ue.ClasId == userClass.Id);
                    result = users.Except(usersInEvent).ToList().Join(db.UserClasses.ToList(),u => u.Id, i => i.UserId, (u,i) => u).ToList();
                    //db.Users.ToList().ForEach(u => db.UserEvents.Where(ue => ue.EventId == eventId).ToList().ForEach(ue => { if (ue.UserId != u.Id && db.UserClasses.FirstOrDefault(p => p.Id == u.Id).ClasId == _class.Id)users.Add(u); }));
                }
            }
            return result.Distinct();
        }
        [HttpGet("GetUsersIsNotVote/{voteId}")]
        [Authorize]
        public async Task<IEnumerable<User>> GetUsersIsNotVote(int voteId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);
            Vote vote = await db.Votes.FirstAsync(p => p.Id == voteId);

            List<User> users = db.Users.ToList();
            List<User> usersInVote = UsersInVote(voteId).Result.ToList();

            List<User> result = new List<User>();

            if (user != null && vote != null)
            {
                if (user.Role == "1")
                {
                    result = users.Except(usersInVote).ToList();
                    //db.Users.ToList().ForEach(u => db.UserEvents.Where(ue => ue.EventId == eventId).ToList().ForEach(ue => { if (ue.UserId != u.Id) users.Add(u); }));
                }
                if (user.Role == "3")
                {
                    UserClass userClass = db.UserClasses.FirstOrDefault(p => p.UserId == user.Id);
                    Class _class = db.Classes.FirstOrDefault(p => p.Id == userClass.ClassId);
                    //List<User> usersInClass = db.UserClasses.Where(ue => ue.ClasId == userClass.Id);
                    result = users.Except(usersInVote).ToList().Join(db.UserClasses.ToList(), u => u.Id, i => i.UserId, (u, i) => u).ToList();
                    //db.Users.ToList().ForEach(u => db.UserEvents.Where(ue => ue.EventId == eventId).ToList().ForEach(ue => { if (ue.UserId != u.Id && db.UserClasses.FirstOrDefault(p => p.Id == u.Id).ClasId == _class.Id)users.Add(u); }));
                }
            }
            return result.Distinct();
        }

        [HttpGet("GetUsersIsNotNew/{newId}")]
        [Authorize]
        public async Task<IEnumerable<User>> GetUsersIsNotNew(int newId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);
            New _new = await db.News.FirstAsync(p => p.Id == newId);

            List<User> users = db.Users.ToList();
            List<User> usersInNew = UsersInNew(newId).Result.ToList();

            List<User> result = new List<User>();

            if (user != null && _new != null)
            {
                if (user.Role == "1")
                {
                    result = users.Except(usersInNew).ToList();
                    //db.Users.ToList().ForEach(u => db.UserEvents.Where(ue => ue.EventId == eventId).ToList().ForEach(ue => { if (ue.UserId != u.Id) users.Add(u); }));
                }
                if (user.Role == "3")
                {
                    UserClass userClass = db.UserClasses.FirstOrDefault(p => p.UserId == user.Id);
                    Class _class = db.Classes.FirstOrDefault(p => p.Id == userClass.ClassId);
                    //List<User> usersInClass = db.UserClasses.Where(ue => ue.ClasId == userClass.Id);
                    result = users.Except(usersInNew).ToList().Join(db.UserClasses.ToList(), u => u.Id, i => i.UserId, (u, i) => u).ToList();
                    //db.Users.ToList().ForEach(u => db.UserEvents.Where(ue => ue.EventId == eventId).ToList().ForEach(ue => { if (ue.UserId != u.Id && db.UserClasses.FirstOrDefault(p => p.Id == u.Id).ClasId == _class.Id)users.Add(u); }));
                }
            }
            return result.Distinct();
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

        [HttpGet("GetUsersIsNotClass/{classId}")]
        [Authorize]
        public async Task<IEnumerable<User>> GetUsersIsNotClass(int classId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);
            Class _class = await db.Classes.FirstAsync(p => p.Id == classId);

            List<User> users = db.Users.ToList();
            List<User> usersInClass = UsersInClass(classId).Result.ToList();

            List<User> result = new List<User>();

            if (user != null && _class != null)
            {
                if (user.Role == "1")
                {
                    result = users.Except(usersInClass).ToList();
                    //db.Users.ToList().ForEach(u => db.UserEvents.Where(ue => ue.EventId == eventId).ToList().ForEach(ue => { if (ue.UserId != u.Id) users.Add(u); }));
                }
                if (user.Role == "3")
                {
                    UserClass userClass = db.UserClasses.FirstOrDefault(p => p.UserId == user.Id);
                    Class a_class = db.Classes.FirstOrDefault(p => p.Id == userClass.ClassId);
                    //List<User> usersInClass = db.UserClasses.Where(ue => ue.ClasId == userClass.Id);
                    result = users.Except(usersInClass).ToList().Join(db.UserClasses.ToList(), u => u.Id, i => i.UserId, (u, i) => u).ToList();
                    //db.Users.ToList().ForEach(u => db.UserEvents.Where(ue => ue.EventId == eventId).ToList().ForEach(ue => { if (ue.UserId != u.Id && db.UserClasses.FirstOrDefault(p => p.Id == u.Id).ClasId == _class.Id)users.Add(u); }));
                }
            }
            return result.Distinct();
        }
        [HttpGet("guic/{classId}")]
        public async Task<IEnumerable<User>> UsersInClass(int classId)
        {
            List<User> users = new List<User>();
            db.UserClasses.Where(p => p.ClassId ==classId).ToList().ForEach(ue => users.Add(db.Users.First(u => u.Id == ue.UserId)));
            return users;
        }
        [HttpGet("addutc/")]
        public async Task<ActionResult<UserClass>> AddUserToClass(int userId, int classId)
        {
            UserClass userClass = new UserClass
            {
                UserId = userId,
                ClassId = classId,
            };
            db.Add(userClass);
            await db.SaveChangesAsync();
            return userClass;
        }
        [HttpDelete("delufc/")]
        public async Task<ActionResult<UserClass>> DeleteUserFromClass(int userId, int classId)
        {
            UserClass userClass = db.UserClasses.FirstOrDefault(p => p.UserId == userId && p.ClassId == classId);
            db.Remove(userClass);
            await db.SaveChangesAsync();
            return userClass;
        }
    }
}