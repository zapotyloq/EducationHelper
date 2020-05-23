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
    public class VoteOptionsController : ControllerBase
    {
        Models.AppContext db;
        public VoteOptionsController(Models.AppContext context)
        {
            db = context;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<VoteOption>> Get()
        {
            User user = db.Users.FirstOrDefault(p => p.Login == User.Identity.Name);
            List<VoteOption> result = new List<VoteOption>();

            if (user != null)
            {
                result = db.VoteOptions.ToList();
            }
            return result;
        }

        // GET /5
        [HttpGet("{id}")]
        public async Task<ActionResult<VoteOption>> Get(int id)
        {
            VoteOption res = await db.VoteOptions.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
                return NotFound();
            return new ObjectResult(res);
        }

        // POST 
        [HttpPost]
        public async Task<ActionResult<VoteOption>> Post(VoteOption voteOption)
        {
            if (voteOption == null)
            {
                return BadRequest();
            }

            db.VoteOptions.Add(voteOption);
            await db.SaveChangesAsync();
            return Ok(voteOption);
        }

        // PUT
        [HttpPut]
        public async Task<ActionResult<VoteOption>> Put(VoteOption voteOption)
        {
            if (voteOption == null)
            {
                return BadRequest();
            }
            if (!db.VoteOptions.Any(x => x.Id == voteOption.Id))
            {
                return NotFound();
            }

            db.Update(voteOption);
            await db.SaveChangesAsync();
            return Ok(voteOption);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<VoteOption>> Delete(int id)
        {
            VoteOption voteOption= db.VoteOptions.FirstOrDefault(x => x.Id == id);
            if (voteOption == null)
            {
                return NotFound();
            }
            db.VoteOptions.Remove(voteOption);
            await db.SaveChangesAsync();
            return Ok(voteOption);
        }
        [HttpGet("GetForVote/{voteId}")]
        [Authorize]
        public async Task<IEnumerable<VoteOption>> GetForVote(int voteId)
        {
            User user = await db.Users.FirstOrDefaultAsync(p => p.Login == User.Identity.Name);

            List<VoteOption> vo = db.VoteOptions.Where(p => p.VoteId == voteId).ToList();
            return vo;
        }
    }
}