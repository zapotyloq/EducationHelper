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
    public class UserEventDocumentsController : ControllerBase
    {
        Models.AppContext db;
        public UserEventDocumentsController(Models.AppContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserEventDocument>> Get()
        {
            return db.UserEventDocuments.ToList();
        }

        // GET /5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserEventDocument>> Get(int id)
        {
            UserEventDocument res = await db.UserEventDocuments.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
                return NotFound();
            return new ObjectResult(res);
        }

        // POST 
        [HttpPost]
        public async Task<ActionResult<UserEventDocument>> Post(UserEventDocument userEventDocument)
        {
            if (userEventDocument == null)
            {
                return BadRequest();
            }

            db.UserEventDocuments.Add(userEventDocument);
            await db.SaveChangesAsync();
            return Ok(userEventDocument);
        }

        // PUT
        [HttpPut]
        public async Task<ActionResult<UserEventDocument>> Put(UserEventDocument userEventDocument)
        {
            if (userEventDocument == null)
            {
                return BadRequest();
            }
            if (!db.UserEventDocuments.Any(x => x.Id == userEventDocument.Id))
            {
                return NotFound();
            }

            db.Update(userEventDocument);
            await db.SaveChangesAsync();
            return Ok(userEventDocument);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserEventDocument>> Delete(int id)
        {
            UserEventDocument userEventDocument = db.UserEventDocuments.FirstOrDefault(x => x.Id == id);
            if (userEventDocument == null)
            {
                return NotFound();
            }
            db.UserEventDocuments.Remove(userEventDocument);
            await db.SaveChangesAsync();
            return Ok(userEventDocument);
        }
    }
}