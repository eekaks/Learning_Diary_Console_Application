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
            using (var db = new Learning_DiaryContext())
            {
                List<Models.Task> tasks = db.Task.Select(task => task).ToList();
                return tasks;
            }
        }
        [HttpGet("tasks/{id}")]
        public ActionResult<Models.Task> Get(int id)
        {
            using (var db = new Learning_DiaryContext())
            {
                Models.Task task = db.Task.Find(id);
                return task;
            }
        }
        [HttpPost("tasks/create")]
        public IActionResult Create(Models.Task task)
        {
            using (var db = new Learning_DiaryContext())
            {
                db.Task.Add(task);
                db.SaveChanges();
            }

            return Accepted();
        }
        [HttpDelete("tasks/delete")]
        public IActionResult Delete(Models.Task task)
        {
            using (var db = new Learning_DiaryContext())
            {
                db.Task.Remove(task);
                db.SaveChanges();
            }

            return Accepted();
        }
    }
}