using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace Learning_Diary_EL.Models
{
    public class Task
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
        public async System.Threading.Tasks.Task CompleteTask()
        {
<<<<<<< HEAD
            using (var db = new Learning_Diary_ConsoleAppContext())
=======
            using (var db = new Learning_DiaryContext())
>>>>>>> 9a8a0700e33facd885f56571a99e59f8ec1849aa
            {
                Models.Task task = db.Task.Find(this.Id);
                task.Done = true;
                db.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task AddNote(string noteToAdd)
        {
<<<<<<< HEAD
            using (var db = new Learning_Diary_ConsoleAppContext())
=======
            using (var db = new Learning_DiaryContext())
>>>>>>> 9a8a0700e33facd885f56571a99e59f8ec1849aa
            {
                int noteId;
                if (!db.Note.Any())
                {
                    noteId = 0;
                }
                else
                {
                    noteId = db.Note.Max(note => note.Id);
                }
<<<<<<< HEAD
                db.Notes.Add(new Models.Note() { Id = noteId+1, Task = this.Id, Note1 = noteToAdd});
=======
                db.Note.Add(new Note() { Id = noteId+1, Task = this.Id, Note1 = noteToAdd });
>>>>>>> 9a8a0700e33facd885f56571a99e59f8ec1849aa
                db.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task EditTaskInfo(Dictionary<string, string> inputs)
        {
            Console.WriteLine("\n" + inputs["title"] + this.Title);
            Console.WriteLine(inputs["enternewtitle"]);
            string title = Console.ReadLine();

            Console.WriteLine("\n" + inputs["description"] + this.Description);
            Console.WriteLine(inputs["enternewdesc"]);
            string description = Console.ReadLine();

            Console.WriteLine("\n" + inputs["deadline"] + this.Deadline.ToShortDateString());
            DateTime deadline = ConsoleAppUi.GetDateTime(inputs["enternewdl"], inputs["invalid"]);

            int choice = ConsoleAppUi.GetInt("\n" + inputs["entertaskprio"], inputs["invalid"]);

<<<<<<< HEAD
            using (var db = new Learning_Diary_ConsoleAppContext())
=======
            using (var db = new Learning_DiaryContext())
>>>>>>> 9a8a0700e33facd885f56571a99e59f8ec1849aa
            {
                Models.Task task = db.Task.Find(this.Id);
                task.Title = title;
                task.Description = description;
                task.Deadline = deadline;
                task.Priority = choice;
                db.SaveChangesAsync();
            }

            Console.WriteLine("\n" + inputs["taskeditsuccess"]);
            Console.WriteLine(inputs["pressanykey"]);
            Console.ReadKey();
        }

        public void PrintNotes(Dictionary<string, string> inputs)
        {
            Console.WriteLine();
<<<<<<< HEAD
            using (var db = new Learning_Diary_ConsoleAppContext())
=======
            using (var db = new Learning_DiaryContext())
>>>>>>> 9a8a0700e33facd885f56571a99e59f8ec1849aa
            {
                var notes = db.Note.Where(x => x.Task == this.Id);
                foreach (Note note in notes)
                {
                    Console.WriteLine(note.Note1);
                }
            }
            Console.WriteLine("\n" + inputs["pressanykey"]);
            Console.ReadKey();
        }

        public string ToString(Dictionary<string, string> inputs)
        {
            string stringBool;

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
