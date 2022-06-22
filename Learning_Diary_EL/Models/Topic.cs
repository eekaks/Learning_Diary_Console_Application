using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace Learning_Diary_EL.Models
{
    public partial class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int EstimatedTimeToMaster { get; set; }
        public int TimeSpent { get; set; }
        public string Source { get; set; }
        public DateTime StartLearningDate { get; set; }
        public bool InProgress { get; set; }
        public DateTime CompletionDate { get; set; }

        public Topic(string title, string description, int estimatedTimeToMaster, string source, int id)
        {
            Id = id;
            Title = title;
            Description = description;
            EstimatedTimeToMaster = estimatedTimeToMaster;
            TimeSpent = int.MinValue;
            Source = source;
            StartLearningDate = DateTime.Now;
            InProgress = true;
            CompletionDate = DateTime.MaxValue;
        }
        public void CompleteTopic()
        {
            using (var db = new Learning_Diary_ELContext())
            {
                Models.Topic topic = db.Topics.Find(this.Id);
                topic.InProgress = false;
                topic.CompletionDate = DateTime.Now;
                TimeSpan spentTime = CompletionDate - topic.StartLearningDate;
                topic.TimeSpent = (int)spentTime.TotalDays;

                db.SaveChanges();
            }
            
        }
        public void AddTask(Dictionary<string, string> inputs)
        {
            Console.WriteLine(inputs["entertaskname"]);
            string name = Console.ReadLine();
            Console.WriteLine(inputs["entertaskdesc"]);
            string description = Console.ReadLine();
            DateTime deadline = UserUI.GetDateTime(inputs["entertaskdl"], inputs["invalid"]);
            int choice = UserUI.GetInt(inputs["entertaskprio"], inputs["invalid"]);

            using (var db = new Learning_Diary_ELContext())
            {
                int taskId;
                if (db.Tasks.Where(task => task.Id == this.Id).Count() == 0)
                {
                    taskId = 0;
                }
                else
                {
                    taskId = db.Tasks.Max(task => task.Id);
                }
                db.Tasks.Add(new Models.Task(taskId + 1, this.Id, name, description, deadline, choice));
                db.SaveChanges();
            }
            Console.WriteLine(inputs["taskaddsuccess"]);
            Console.WriteLine(inputs["pressanykey"]);
            Console.ReadKey();
        }

        public void EditTopicInfo(Dictionary<string, string> inputs)
        {
            Console.WriteLine("\n" + inputs["title"] + this.Title);
            Console.WriteLine(inputs["enternewtitle"]);
            string title = Console.ReadLine();

            Console.WriteLine("\n" + inputs["description"] + this.Description);
            Console.WriteLine(inputs["enternewdesc"]);
            string description = Console.ReadLine();

            Console.WriteLine("\n" + inputs["daysmaster"] + this.EstimatedTimeToMaster);
            int estimatedtimetomaster = UserUI.GetInt(inputs["enterdays"], inputs["invalid"]);

            Console.WriteLine("\n" + inputs["sourcemat"] + this.Source);
            Console.WriteLine(inputs["entersource"]);
            string source = Console.ReadLine();

            using (var db = new Learning_Diary_ELContext())
            {
                Models.Topic topic = db.Topics.Find(this.Id);
                topic.Title = title;
                topic.Description = description;
                topic.EstimatedTimeToMaster = estimatedtimetomaster;
                topic.Source = source;
                db.SaveChanges();
            }

            Console.WriteLine("\n" + inputs["topiceditsuccess"]);
            Console.WriteLine(inputs["pressanykey"]);
            Console.ReadKey();
        }
        
        public void PrintShortTasks(Dictionary<string, string> inputs)
        {
            Console.WriteLine();

            using (var db = new Learning_Diary_ELContext())
            {
                IQueryable<Models.Task> tasks = db.Tasks.Where(x => x.Topic == this.Id);
                foreach (Models.Task task in tasks)
                {
                    Console.WriteLine(task.Id + ": " + task.Title);
                }
            }
            Console.WriteLine("\n" + inputs["pressanykey"]);
            Console.ReadKey();
        }

        public static void EditTask(Models.Task taskToEdit, Dictionary<string, string> inputs)
        {
            // this is nested loop to edit a task

            bool taskEditRunning = true;

            while (taskEditRunning)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                UserUI.PrintBanner(inputs["tasktitle"]);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(taskToEdit.ToString(inputs) + "\n");

                // "1 - edit task information" + "\n" + "2 - print notes" + "\n" + "3 - add note" + "\n" + "4 - mark task as complete" + "\n" + "0 - go back." + "\n" + "Enter number to continue: "
                int taskLoopChoice = UserUI.GetInt(inputs["taskmenu"], inputs["invalid"]); 

                switch (taskLoopChoice)
                {
                    case 0:
                        taskEditRunning = false;
                        break;

                    case 1:
                        taskToEdit.EditTaskInfo(inputs);
                        break;

                    case 2:
                        taskToEdit.PrintNotes(inputs);
                        Console.WriteLine();
                        break;

                    case 3:
                        Console.WriteLine(inputs["inputnote"]);
                        string noteToAdd = Console.ReadLine();
                        taskToEdit.AddNote(noteToAdd);
                        Console.WriteLine(inputs["noteaddsuccess"] + "\n" + inputs["pressanykey"]);
                        Console.ReadKey();
                        break;

                    case 4:
                        taskToEdit.CompleteTask();
                        Console.WriteLine(inputs["taskmarkcomplete"] + "\n" + inputs["pressanykey"]);
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine(inputs["invalid"] + "\n" + inputs["pressanykey"]);
                        Console.ReadKey();
                        break;
                }
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
            int printSpent = (int)spentTime.TotalDays;

            return string.Format("ID : {0}\n" +
                                 inputs["title"] + "{1}\n" +
                                 inputs["description"] + "{2}\n" +
                                 inputs["daysmaster"] + "{3}\n" +
                                 inputs["daysspent"] + "{4}\n" +
                                 inputs["sourcemat"] + "{5}\n" +
                                 inputs["started"] + "{6}\n" +
                                 inputs["inprogress"] + inputs["yes"] + "\n", Id, Title, Description, EstimatedTimeToMaster, String.Format("{0:0.#}", printSpent),
                Source, StartLearningDate.Date.ToShortDateString());
        }
    }
}
