using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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

        [HttpGet("GetForUserEvent")]
        [Authorize]
        public ActionResult<IEnumerable<UserEventDocument>> GetForUserevent(int usereventid)
        {
            return db.UserEventDocuments.Where(p => p.UserEventId == usereventid).ToList();
        }

        [HttpGet("test")]
        [Authorize]
        public bool Getsnt()
        {
            FileStream fs = new FileStream(@"E:\1.png", FileMode.Open, FileAccess.Read);

            //Initialize a byte array with size of stream
            byte[] imgByteArr = new byte[fs.Length];

            //Read data from the file stream and put into the byte array
            fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));

            //Close a file stream
            fs.Close();
            db.UserEventDocuments.First().File = imgByteArr;
            db.SaveChanges();
            return true;
        }

        [HttpGet("GetByEventId")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserEventDocument>>> GetByEventId(int eventid)
        {
            List<UserEventDocument> res = new List<UserEventDocument>();
            db.UserEvents.Where(p => p.EventId == eventid).ToList().ForEach(ue => db.UserEventDocuments.Where(p => p.UserEventId == ue.Id).ToList().ForEach(f => res.Add(f)));
            return new ObjectResult(res);
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
        //[Authorize]
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