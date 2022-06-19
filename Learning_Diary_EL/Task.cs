using System;
using System.Collections.Generic;

namespace Learning_Diary_EL
{
    public class Task
    {
        public Task(int id, string title, string description, DateTime deadline, int priority)
        {
            Id = id;
            Title = title;
            Description = description;
            Notes = new List<string>();
            Deadline = deadline;
            Done = false;
            Priority = priority;
        }

        public int Id { get; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public List<string> Notes { get; set; }
        public DateTime Deadline { get; private set; }
        public bool Done { get; private set; }
        public int Priority { get; private set; }

        public void CompleteTask()
        {
            Done = true;
        }

        public void AddNote(string note)
        {
            Notes.Add(note);
        }

        public void EditTaskInfo(Dictionary<string, string> inputs)
        {
            Console.WriteLine("\n" + inputs["title"] + this.Title);
            Console.WriteLine(inputs["enternewtitle"]);
            this.Title = Console.ReadLine();

            Console.WriteLine("\n" + inputs["description"] + this.Description);
            Console.WriteLine(inputs["enternewdesc"]);
            this.Description = Console.ReadLine();

            Console.WriteLine("\n" + inputs["deadline"] + this.Deadline.ToShortDateString());
            this.Deadline = UserUI.GetDateTime(inputs["enternewdl"], inputs["invalid"]);

            int choice = UserUI.GetInt("\n" + inputs["entertaskprio"], inputs["invalid"]);
            this.Priority = choice;

            Console.WriteLine("\n" + inputs["taskeditsuccess"]);
            Console.WriteLine(inputs["pressanykey"]);
            Console.ReadKey();
        }

        public void PrintNotes(Dictionary<string, string> inputs)
        {
            Console.WriteLine();
            foreach (string n in Notes)
            {
                Console.WriteLine(n);
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
                inputs["taskid"] +  "{0}\n" + inputs["title"] + "{1}\n" + inputs["description"] + "{2}\nDeadline: {3}\n" + inputs["prio"] + "{4}\n" + inputs["finished"] + "{5}", Id, Title, Description, Deadline.ToShortDateString(), Priority, stringBool);
        }
    }
}