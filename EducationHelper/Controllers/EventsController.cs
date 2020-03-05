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
    public class EventsController : ControllerBase
    {
        Models.AppContext db;
        public EventsController(Models.AppContext context)
        {
            db = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Event>> Get()
        {
            return db.Events.ToList();
        }

        // GET /5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            Event res = await db.Events.FirstOrDefaultAsync(x => x.Id == id);
            if (res == null)
                return NotFound();
            return new ObjectResult(res);
        }

        // POST 
        [HttpPost]
        public async Task<ActionResult<Event>> Post(Event _event)
        {
            if (_event == null)
            {
                return BadRequest();
            }

            db.Events.Add(_event);
            await db.SaveChangesAsync();
            return Ok(_event);
        }

        // PUT
        [HttpPut]
        public async Task<ActionResult<Event>> Put(Event _event)
        {
            if (_event == null)
            {
                return BadRequest();
            }
            if (!db.Events.Any(x => x.Id == _event.Id))
            {
                return NotFound();
            }

            db.Update(_event);
            await db.SaveChangesAsync();
            return Ok(_event);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> Delete(int id)
        {
            Event _event = db.Events.FirstOrDefault(x => x.Id == id);
            if (_event == null)
            {
                return NotFound();
            }
            db.Events.Remove(_event);
            await db.SaveChangesAsync();
            return Ok(_event);
        }
    }
}