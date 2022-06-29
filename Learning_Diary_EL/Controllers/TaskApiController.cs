using System.Collections.Generic;
using System.Linq;
using Learning_Diary_EL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Learning_Diary_EL.Controllers
{
    [ApiController]
    public class TaskApiController : ControllerBase
    {
        [HttpGet("tasks/all")]
        public ActionResult<List<Models.Task>> Get()
        {
            using (var db = new Learning_Diary_ELContext())
            {
                List<Models.Task> tasks = db.Tasks.Select(task => task).ToList();
                return tasks;
            }
        }
        [HttpGet("tasks/{id}")]
        public ActionResult<Models.Task> Get(int id)
        {
            using (var db = new Learning_Diary_ELContext())
            {
                Models.Task task = db.Tasks.Find(id);
                return task;
            }
        }
        [HttpPost("tasks/create")]
        public IActionResult Create(Models.Task task)
        {
            using (var db = new Learning_Diary_ELContext())
            {
                db.Tasks.Add(task);
                db.SaveChanges();
            }

            return Accepted();
        }
        [HttpDelete("tasks/delete")]
        public IActionResult Delete(Models.Task task)
        {
            using (var db = new Learning_Diary_ELContext())
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
            }

            return Accepted();
        }
    }
}