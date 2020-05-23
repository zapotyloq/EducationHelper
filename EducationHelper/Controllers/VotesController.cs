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
    public class VotesController : ControllerBase
    {
        Models.AppContext db;
        public VotesController(Models.AppContext context)
        {
            db = context;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Vote>> Get()
        {
            User user = db.Users.FirstOrDefault(p => p.Login == User.Identity.Name);
            List<Vote> result = new List<Vote>();

            if (user != null)
            {
                switch (Convert.ToInt32(user.Role))
                {
                    case 1: result = db.Votes.ToList(); break;
                    case 2: //Обычный пользователь
                        {
                            db.UserVotes.Where(p => p.UserId == user.Id).ToList().ForEach(ue => result.Add(db.Votes.FirstOrDefault(p => p.Id == ue.VoteId)));
                        }
                        break;
                    case 3:
                        {
                            //добавяем где учавствует
                            List<UserVote> userEvents = db.UserVotes.Where(p => p.UserId == user.Id).ToList();

                            if(userEvents.Any(ue => db.Votes.FirstOrDefault(e => e.Id == ue.VoteId && e.AuthorId != user.Id) != null))
                                userEvents.ForEach(ue => result.Add(db.Votes.FirstOrDefault(p => p.Id == ue.VoteId && p.AuthorId != user.Id))); ;
                            //добавляем где автор
                            db.Votes.Where(p => p.AuthorId == user.Id).ToList().ForEach(e => result.Add(e));
                        }
                        break;
                }
            }
            return result;
        }

        // GET /5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vote>> Get(int id)
        {
            Vote res = await db.Votes.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
                return NotFound();
            return new ObjectResult(res);
        }

        // POST 
        [HttpPost]
        public async Task<ActionResult<Vote>> Post(Vote vote)
        {
            if (vote == null)
            {
                return BadRequest();
            }
            User user = db.Users.FirstOrDefault(p => p.Login == User.Identity.Name);
            vote.AuthorId = user.Id;
            db.Votes.Add(vote);
            await db.SaveChangesAsync();
            return Ok(vote);
        }

        // PUT
        [HttpPut]
        public async Task<ActionResult<Vote>> Put(Vote vote)
        {
            if (vote == null)
            {
                return BadRequest();
            }
            if (!db.Votes.Any(x => x.Id == vote.Id))
            {
                return NotFound();
            }

            db.Update(vote);
            await db.SaveChangesAsync();
            return Ok(vote);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vote>> Delete(int id)
        {
            Vote vote= db.Votes.FirstOrDefault(x => x.Id == id);
            if (vote == null)
            {
                return NotFound();
            }
            db.Votes.Remove(vote);
            await db.SaveChangesAsync();
            return Ok(vote);
        }
    }
}