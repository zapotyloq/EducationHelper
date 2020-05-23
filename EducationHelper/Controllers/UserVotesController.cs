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
    public class UserVotesController : ControllerBase
    {
        Models.AppContext db;
        public UserVotesController(Models.AppContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserVote>> Get()
        {
            return db.UserVotes.ToList();
        }

        // GET /5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserVote>> Get(int id)
        {
            User user = db.Users.FirstOrDefault(p => p.Login == User.Identity.Name);
            UserVote res = await db.UserVotes.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
                return NotFound();
            return new ObjectResult(res);
        }
        [HttpGet("ByvoteId/{id}")]
        [Authorize]
        public async Task<ActionResult<UserVote>> ByVoteId(int id)
        {
            User user = db.Users.FirstOrDefault(p => p.Login == User.Identity.Name);
            UserVote res = await db.UserVotes.FirstOrDefaultAsync(x => x.VoteId == id && x.UserId == Convert.ToInt32(user.Id));
            if (res == null)
                return NotFound();
            return new ObjectResult(res);
        }
        [HttpGet("GetByIds")]
        [Authorize]
        public async Task<ActionResult<UserVote>> Get(int voteid, int userid)
        {
            User user = db.Users.FirstOrDefault(p => p.Login == User.Identity.Name);
            UserVote res = await db.UserVotes.FirstOrDefaultAsync(x => x.VoteId == voteid && x.UserId == userid);
            return new ObjectResult(res);
        }
        
        // POST 
        [HttpPost]
        public async Task<ActionResult<UserVote>> Post(UserVote userVote)
        {
            if (userVote == null)
            {
                return BadRequest();
            }

            db.UserVotes.Add(userVote);
            await db.SaveChangesAsync();
            return Ok(userVote);
        }

        // PUT
        [HttpPut]
        public async Task<ActionResult<UserVote>> Put(UserVote userVote)
        {
            if (userVote == null)
            {
                return BadRequest();
            }
            if (!db.UserVotes.Any(x => x.Id == userVote.Id))
            {
                return NotFound();
            }

            db.Update(userVote);
            await db.SaveChangesAsync();
            return Ok(userVote);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserVote>> Delete(int id)
        {
            UserVote userVote = db.UserVotes.FirstOrDefault(x => x.Id == id);
            if (userVote == null)
            {
                return NotFound();
            }
            db.UserVotes.Remove(userVote);
            await db.SaveChangesAsync();
            return Ok(userVote);
        }
    }
}