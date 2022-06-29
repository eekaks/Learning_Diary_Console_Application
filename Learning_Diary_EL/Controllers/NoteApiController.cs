using System.Collections.Generic;
using System.Linq;
using Learning_Diary_EL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Learning_Diary_EL.Controllers
{
    [ApiController]
    public class NoteApiController : ControllerBase
    {
        [HttpGet("notes/all")]
        public ActionResult<List<Note>> Get()
        {
            using (var db = new Learning_Diary_ELContext())
            {
                List<Note> notes = db.Notes.Select(note => note).ToList();
                return notes;
            }
        }
        [HttpGet("notes/{id}")]
        public ActionResult<Note> Get(int id)
        {
            using (var db = new Learning_Diary_ELContext())
            {
                Note note = db.Notes.Find(id);
                return note;
            }
        }
        [HttpPost("notes/create")]
        public IActionResult Create(Note note)
        {
            using (var db = new Learning_Diary_ELContext())
            {
                db.Notes.Add(note);
                db.SaveChanges();
            }

            return Accepted();
        }
        [HttpDelete("notes/delete")]
        public IActionResult Delete(Note note)
        {
            using (var db = new Learning_Diary_ELContext())
            {
                db.Notes.Remove(note);
                db.SaveChanges();
            }

            return Accepted();
        }
    }
}