using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace Learning_Diary_EL.Models
{
    public partial class Task
    {
        public int Id { get; set; }
        public int Topic { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int Priority { get; set; }
        public bool Done { get; set; }

        public Task(int id, int topic, string title, string description, DateTime deadline, int priority)
        {
            Id = id;
            Topic = topic;
            Title = title;
            Description = description;
            Deadline = deadline;
            Done = false;
            Priority = priority;
        }
        public void CompleteTask()
        {
            using (var db = new Learning_Diary_ELContext())
            {
                Models.Task task = db.Tasks.Find(this.Id);
                task.Done = true;
                db.SaveChanges();
            }
        }

        public void AddNote(string noteToAdd)
        {
            using (var db = new Learning_Diary_ELContext())
            {
                int noteId = 0;
                List<Note> notes = db.Notes.ToList();

                foreach (Note note in notes)
                {
                    if (note.Id > noteId)
                    {
                        noteId = note.Id;
                    }
                }

                noteId++;
                db.Notes.Add(new Note() { Id = noteId, Task = this.Id, Note1 = noteToAdd });
                db.SaveChanges();
            }
        }

        public void EditTaskInfo(Dictionary<string, string> inputs)
        {
            Console.WriteLine("\n" + inputs["title"] + this.Title);
            Console.WriteLine(inputs["enternewtitle"]);
            string title = Console.ReadLine();

            Console.WriteLine("\n" + inputs["description"] + this.Description);
            Console.WriteLine(inputs["enternewdesc"]);
            string description = Console.ReadLine();

            Console.WriteLine("\n" + inputs["deadline"] + this.Deadline.ToShortDateString());
            DateTime deadline = UserUI.GetDateTime(inputs["enternewdl"], inputs["invalid"]);

            int choice = UserUI.GetInt("\n" + inputs["entertaskprio"], inputs["invalid"]);

            using (var db = new Learning_Diary_ELContext())
            {
                Models.Task task = db.Tasks.Find(this.Id);
                task.Title = title;
                task.Description = description;
                task.Deadline = deadline;
                task.Priority = choice;
                db.SaveChanges();
            }

            Console.WriteLine("\n" + inputs["taskeditsuccess"]);
            Console.WriteLine(inputs["pressanykey"]);
            Console.ReadKey();
        }

        public void PrintNotes(Dictionary<string, string> inputs)
        {
            Console.WriteLine();
            using (var db = new Learning_Diary_ELContext())
            {
                var notes = db.Notes.Where(x => x.Task == this.Id);
                foreach (var note in notes)
                {
                    Console.WriteLine(note.Note1);
                }
            }
            Console.WriteLine("\n" + inputs["pressanykey"]);
            Console.ReadKey();
        }

        public string ToString(Dictionary<string, string> inputs)
        {
            string stringBool = String.Empty;

            if (Done)
            {
                stringBool = inputs["yes"];
            }
            else
            {
                stringBool = inputs["no"];
            }
            return string.Format(
                inputs["taskid"] + "{0}\n" + inputs["title"] + "{1}\n" + inputs["description"] + "{2}\nDeadline: {3}\n" + inputs["prio"] + "{4}\n" + inputs["finished"] + "{5}", Id, Title, Description, Deadline.ToShortDateString(), Priority, stringBool);
        }
    }
}
