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
            using (var db = new Learning_DiaryContext())
            {
                List<Topic> topics = db.Topic.Select(topic => topic).ToList();
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
            using (var db = new Learning_DiaryContext())
            {
                Topic topic = db.Topic.Find(id);
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
            using (var db = new Learning_DiaryContext())
            {
                db.Topic.Add(topic);
                db.SaveChanges();
            }

            return CreatedAtAction(nameof(Create), new { id = topic.Id }, topic);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var db = new Learning_DiaryContext())
            {
                var topicToDelete = db.Topic.Find(id);
                if (topicToDelete is null)
                {
                    return NotFound();
                }
                db.Topic.Remove(topicToDelete);
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
            using (var db = new Learning_DiaryContext())
            {
                Topic topicToUpdate = db.Topic.Find(id);
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
