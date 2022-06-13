using System;
using System.Collections.Generic;

namespace Learning_Diary_EL
{
    public class Topic
    {
        public int Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double EstimatedTimeToMaster { get; set; }
        public double TimeSpent { get; private set; }
        public string Source { get; set; }
        public DateTime StartLearningDate { get; }
        public bool InProgress { get; private set; }
        public DateTime CompletionDate { get; private set; }

        public Dictionary<int, Task> Tasks { get; set; }

        public Topic(string title, string description, double estimatedTimeToMaster, string source, int id)
        {
            Id = id;
            Title = title;
            Description = description;
            EstimatedTimeToMaster = estimatedTimeToMaster;
            TimeSpent = double.MinValue;
            Source = source;
            StartLearningDate = DateTime.Now;
            InProgress = true;
            CompletionDate = DateTime.MaxValue;
            Tasks = new Dictionary<int, Task>();
        }
        public void CompleteTopic()
        {
            InProgress = false;
            CompletionDate = DateTime.Now;
            TimeSpan spentTime = CompletionDate - StartLearningDate;
            TimeSpent = spentTime.TotalDays;
        }
        public void AddTask(Dictionary<string, string> inputs)
        {
            Console.WriteLine(inputs["entertaskname"]);
            string name = Console.ReadLine();
            Console.WriteLine(inputs["entertaskdesc"]);
            string description = Console.ReadLine();
            Console.WriteLine(inputs["entertaskdl"]);
            string date = Console.ReadLine();
            String[] dateString = date.Split("/");
            int year = int.Parse(dateString[0]);
            int months = int.Parse(dateString[1]);
            int days = int.Parse(dateString[2]);
            DateTime deadline = new DateTime(year, months, days);
            Console.WriteLine(inputs["entertaskprio"]);
            int choice = int.Parse(Console.ReadLine());

            Random r = new Random();
            int id = r.Next(1, 1000000);

            Task newTask = new Task(name, description, deadline, choice, id);
            Tasks[id] = newTask;
        }

        public void EditTopicInfo(Dictionary<string, string> inputs)
        {
            Console.WriteLine();
            Console.WriteLine(inputs["title"] + this.Title);
            Console.WriteLine(inputs["enternewtitle"]);
            this.Title = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine(inputs["description"] + this.Description);
            Console.WriteLine(inputs["enternewdesc"]);
            this.Description = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine(inputs["daysmaster"] + this.EstimatedTimeToMaster);
            Console.WriteLine(inputs["enterdays"]);
            this.EstimatedTimeToMaster = int.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine(inputs["source"] + this.Source);
            Console.WriteLine(inputs["entersource"]);
            this.Source = Console.ReadLine();
        }
        public void PrintTasks(Dictionary<string, string> inputs)
        {
            foreach (int key in Tasks.Keys)
            {
                Console.WriteLine(Tasks[key].ToString(inputs));
                Console.WriteLine();
            }
        }
        public void PrintShortTasks()
        {
            foreach (int key in Tasks.Keys)
            {
                Console.WriteLine(Tasks[key].Id + ": " + Tasks[key].Title);
                Console.WriteLine();
            }
        }
        public string ToString(Dictionary<string, string> inputs)
        {
            if (!InProgress)
            {
                return string.Format("ID : {0}\n" +
                                     inputs["title"] + "{1}\n" +
                                     inputs["description"] + "{2}\n" +
                                     inputs["daysmaster"] + "{3}\n" +
                                     inputs["daysspent"] + "{4}\n" + 
                                     inputs["sourcemat"] + "{5}\n" +
                                     inputs["started"] + "{6}\n" +
                                     inputs["inprogress"] + inputs["no"] + "\n" +
                                     inputs["completed"] + "{7}", Id, Title, Description, EstimatedTimeToMaster, String.Format("{0:0.#}", TimeSpent),
                    Source, StartLearningDate.Date.ToShortDateString(), CompletionDate.Date.ToShortDateString());
            }

            TimeSpan spentTime = DateTime.Now - StartLearningDate;
            TimeSpent = spentTime.TotalDays;

            return string.Format("ID : {0}\n" +
                                 inputs["title"] + "{1}\n" +
                                 inputs["description"] + "{2}\n" +
                                 inputs["daysmaster"] + "{3}\n" +
                                 inputs["daysspent"] + "{4}\n" +
                                 inputs["sourcemat"] + "{5}\n" +
                                 inputs["started"] + "{6}\n" +
                                 inputs["inprogress"] + inputs["yes"] + "\n", Id, Title, Description, EstimatedTimeToMaster, String.Format("{0:0.#}", TimeSpent),
                Source, StartLearningDate.Date.ToShortDateString());
        }
        public class Task
        {
            public int Id { get; }
            public string Title { get; set; }
            public string Description { get; set; }
            public List<string> Notes { get; set; }
            public DateTime Deadline { get; private set; }
            public bool Done { get; private set; }
            public int Priority { get; set; }

            //public enum PriorityLevel
            //{
            //    Low,
            //    Medium,
            //    High
            //}
            public Task(string title, string description, DateTime deadline, int priority, int id)
            {
                Id = id;
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

            public void EditTaskInfo(Dictionary<string, string> inputs)
            {
                Console.WriteLine();
                Console.WriteLine(inputs["title"] + this.Title);
                Console.WriteLine(inputs["enternewtitle"]);
                this.Title = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine(inputs["description"] + this.Description);
                Console.WriteLine(inputs["enternewdesc"]);
                this.Description = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine(inputs["deadline"] + this.Deadline.ToShortDateString());
                Console.WriteLine(inputs["enternewdl"]);
                string date = Console.ReadLine();
                String[] dateString = date.Split("/");
                int year = int.Parse(dateString[0]);
                int months = int.Parse(dateString[1]);
                int days = int.Parse(dateString[2]);
                this.Deadline = new DateTime(year, months, days);
                Console.WriteLine();
                Console.WriteLine(inputs["entertaskprio"]);
                int choice = int.Parse(Console.ReadLine());
                this.Priority = choice;
                Console.WriteLine();
                Console.WriteLine(inputs["taskeditsuccess"]);
            }
            public void PrintNotes(Dictionary<string, string> inputs)
            {
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("*" + new string(' ', 11) + inputs["notestitle"] + new string(' ', 12) + "*");
                Console.WriteLine(new string('*', 30));
                foreach (string n in Notes)
                {
                    Console.WriteLine(n);
                }
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
}


