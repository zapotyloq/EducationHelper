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
            User user = db.Users.FirstOrDefault(p => p.Login == User.Identity.Name);
            List<New> result = new List<New>();

            if (user != null)
            {
                switch (Convert.ToInt32(user.Role))
                {
                    case 1: result = db.News.ToList(); break;
                    case 2: //Обычный пользователь
                        {
                            db.UserNews.Where(p => p.UserId == user.Id).ToList().ForEach(ue => result.Add(db.News.FirstOrDefault(p => p.Id == ue.NewId)));
                        }
                        break;
                    case 3:
                        {
                            //добавяем где учавствует
                            List<UserNew> userEvents = db.UserNews.Where(p => p.UserId == user.Id).ToList();

                            if (userEvents.Any(ue => db.News.FirstOrDefault(e => e.Id == ue.UserId && e.AuthorId != user.Id) != null))
                                userEvents.ForEach(ue => result.Add(db.News.FirstOrDefault(p => p.Id == ue.NewId && p.AuthorId != user.Id))); ;
                            //добавляем где автор
                            db.News.Where(p => p.AuthorId == user.Id).ToList().ForEach(e => result.Add(e));
                        }
                        break;
                }
            }
            return result;
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