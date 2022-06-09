using System;
using System.Collections.Generic;

namespace Learning_Diary_EL
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double EstimatedTimeToMaster { get; set; }
        public double TimeSpent { get; private set; }
        public string Source { get; set; }
        public DateTime StartLearningDate { get; }
        public bool InProgress { get; private set; }
        public DateTime CompletionDate { get; private set; }

        public List<Task> Tasks { get; set; }

        public Topic(string title, string description, double estimatedTimeToMaster, string source)
        {
            Random r = new Random();
            Id = r.Next(1, 1000000);
            Title = title;
            Description = description;
            EstimatedTimeToMaster = estimatedTimeToMaster;
            TimeSpent = double.MinValue;
            Source = source;
            StartLearningDate = DateTime.Now;
            InProgress = true;
            CompletionDate = DateTime.MaxValue;
            Tasks = new List<Task>();
        }
        public void CompleteTopic()
        {
            InProgress = false;
            CompletionDate = DateTime.Now;
            TimeSpan spentTime = CompletionDate - StartLearningDate;
            TimeSpent = spentTime.TotalDays;
        }
        public void AddTask()
        {
            Console.WriteLine("Enter task name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter task description: ");
            string description = Console.ReadLine();
            Console.WriteLine("What is the deadline for the task? Give in following format: YYYY/MM/DD");
            string date = Console.ReadLine();
            String[] dateString = date.Split("/");
            int year = int.Parse(dateString[0]);
            int months = int.Parse(dateString[1]);
            int days = int.Parse(dateString[2]);
            DateTime deadline = new DateTime(year, months, days);
            Console.WriteLine("What is the priority of this task? 1 - Low, 2 - Medium, 3 - High");
            int choice = int.Parse(Console.ReadLine());

            Task newTask = new Task(name, description, deadline, choice);
            Tasks.Add(newTask);
        }
        public void PrintTasks()
        {
            foreach (Task t in Tasks)
            {
                Console.WriteLine(t);
                Console.WriteLine();
            }
        }
        public override string ToString()
        {
            if (!InProgress)
            {
                return string.Format("ID : {0}\n" +
                                     "Title: {1}\n" +
                                     "Description: {2}\n" +
                                     "Days to master: {3}\n" +
                                     "Days spent: {4}\n" + 
                                     "Source material: {5}\n" +
                                     "Started: {6}\n" +
                                     "In progress: No\n" +
                                     "Completed: {7}", Id, Title, Description, EstimatedTimeToMaster, String.Format("{0:0.#}", TimeSpent),
                    Source, StartLearningDate.Date.ToShortDateString(), CompletionDate.Date.ToShortDateString());
            }

            TimeSpan spentTime = DateTime.Now - StartLearningDate;
            TimeSpent = spentTime.TotalDays;

            return string.Format("ID : {0}\n" +
                                 "Title: {1}\n" +
                                 "Description: {2}\n" +
                                 "Days to master: {3}\n" +
                                 "Days spent: {4}\n" +
                                 "Source material: {5}\n" +
                                 "Started: {6}\n" +
                                 "In progress: Yes\n", Id, Title, Description, EstimatedTimeToMaster, String.Format("{0:0.#}", TimeSpent),
                Source, StartLearningDate.Date.ToShortDateString());
        }
        public class Task
        {
            public int Id { get; private set; }
            public string Title { get; }
            public string Description { get; }
            public List<string> Notes { get; private set; }
            public DateTime Deadline { get; private set; }
            public bool Done { get; private set; }
            public int Priority { get; set; }

            //public enum PriorityLevel
            //{
            //    Low,
            //    Medium,
            //    High
            //}
            public Task(string title, string description, DateTime deadline, int priority)
            {
                Random r = new Random();
                Id = r.Next(1, 1000000);
                Title = title;
                Description = description;
                Notes = new List<string>();
                Deadline = deadline;
                Done = false;
                Priority = priority;
            }
            public void CompleteTask()
            {
                Done = true;
            }
            public void AddNote(string note)
            {
                Notes.Add(note);
            }
            public void PrintNotes()
            {
                foreach (string n in Notes)
                {
                    Console.WriteLine(n);
                }
            }
            public override string ToString()
            {
                string stringBool = String.Empty;

                if (Done)
                {
                    stringBool = "Yes";
                }
                else
                {
                    stringBool = "No";
                }
                return string.Format(
                    "Task id: {0}\nTitle: {1}\nDescription{2}\nDeadline: {3}\nPriority: {4}\nFinished: {5}", Id, Title, Description, Deadline.ToShortDateString(), Priority, stringBool);
            }
        }
    }
}


