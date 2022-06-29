using System.Collections.Generic;
using System.Linq;
using Learning_Diary_EL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Learning_Diary_EL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Topic>> GetAll()
        {
            using (var db = new Learning_Diary_ELContext())
            {
                List<Topic> topics = db.Topics.Select(topic => topic).ToList();
                if (!topics.Any())
                {
                    return NotFound();
                }
                return topics;
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Topic> Get(int id)
        {
            using (var db = new Learning_Diary_ELContext())
            {
                Models.Topic topic = db.Topics.Find(id);
                if (topic is null)
                {
                    return NotFound();
                }
                return topic;
            }
        }

        [HttpPost]
        public IActionResult Create(Topic topic)
        {
            using (var db = new Learning_Diary_ELContext())
            {
                db.Topics.Add(topic);
                db.SaveChanges();
            }

            return CreatedAtAction(nameof(Create), new { id = topic.Id }, topic);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var db = new Learning_Diary_ELContext())
            {
                var topicToDelete = db.Topics.Find(id);
                if (topicToDelete is null)
                {
                    return NotFound();
                }
                db.Topics.Remove(topicToDelete);
                db.SaveChanges();
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Topic topic)
        {
            if (id != topic.Id)
            {
                return BadRequest();
            }
            using (var db = new Learning_Diary_ELContext())
            {
                Topic topicToUpdate = db.Topics.Find(id);
                if (topicToUpdate is null)
                {
                    return NotFound();
                }
                topicToUpdate.Title = topic.Title;
                topicToUpdate.Description = topic.Description;
                topicToUpdate.Source = topic.Source;
                topicToUpdate.EstimatedTimeToMaster = topic.EstimatedTimeToMaster;
                topicToUpdate.CompletionDate = topic.CompletionDate;
                topicToUpdate.Id = topic.Id;
                topicToUpdate.InProgress = topic.InProgress;
                topicToUpdate.StartLearningDate = topic.StartLearningDate;
                topicToUpdate.TimeSpent = topic.TimeSpent;
                db.SaveChanges();
            }

            return NoContent();
        }
    }
}
