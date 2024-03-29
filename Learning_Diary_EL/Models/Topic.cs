﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Learning_Diary_EL.Models
{
    public class Topic
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
            using (var db = new Learning_Diary_ConsoleAppContext())
            {
                Topic topic = db.Topic.Find(this.Id);
                topic.InProgress = false;
                topic.CompletionDate = DateTime.Now;
                TimeSpan spentTime = CompletionDate - topic.StartLearningDate;
                topic.TimeSpent = (int)spentTime.TotalDays;

                db.SaveChanges();
            }
            
        }
        public async System.Threading.Tasks.Task AddTaskAsync(Dictionary<string, string> inputs)
        {
            Console.WriteLine(inputs["entertaskname"]);
            string name = Console.ReadLine();
            Console.WriteLine(inputs["entertaskdesc"]);
            string description = Console.ReadLine();
            DateTime deadline = ConsoleAppUi.GetDateTime(inputs["entertaskdl"], inputs["invalid"]);
            int choice = ConsoleAppUi.GetInt(inputs["entertaskprio"], inputs["invalid"]);

            using (var db = new Learning_Diary_ConsoleAppContext())
            {
                int taskId;
                if (db.Task.Count() == 0)
                {
                    taskId = 0;
                }
                else
                {
                    taskId = db.Task.Max(task => task.Id);
                }
                db.Task.Add(new Models.Task(taskId + 1, this.Id, name, description, deadline, choice));
                db.SaveChangesAsync();
            }
            Console.WriteLine(inputs["taskaddsuccess"]);
            Console.WriteLine(inputs["pressanykey"]);
            Console.ReadKey();
        }

        public async System.Threading.Tasks.Task EditTopicInfoAsync(Dictionary<string, string> inputs)
        {
            Console.WriteLine("\n" + inputs["title"] + this.Title);
            Console.WriteLine(inputs["enternewtitle"]);
            string title = Console.ReadLine();

            Console.WriteLine("\n" + inputs["description"] + this.Description);
            Console.WriteLine(inputs["enternewdesc"]);
            string description = Console.ReadLine();

            Console.WriteLine("\n" + inputs["daysmaster"] + this.EstimatedTimeToMaster);
            int estimatedtimetomaster = ConsoleAppUi.GetInt(inputs["enterdays"], inputs["invalid"]);

            Console.WriteLine("\n" + inputs["sourcemat"] + this.Source);
            Console.WriteLine(inputs["entersource"]);
            string source = Console.ReadLine();

            using (var db = new Learning_Diary_ConsoleAppContext())
            {
                Topic topic = db.Topic.Find(this.Id);
                topic.Title = title;
                topic.Description = description;
                topic.EstimatedTimeToMaster = estimatedtimetomaster;
                topic.Source = source;
                db.SaveChangesAsync();
            }

            Console.WriteLine("\n" + inputs["topiceditsuccess"]);
            Console.WriteLine(inputs["pressanykey"]);
            Console.ReadKey();
        }
        
        public void PrintShortTasks(Dictionary<string, string> inputs)
        {
            Console.WriteLine();

            using (var db = new Learning_Diary_ConsoleAppContext())
            {
                IQueryable<Models.Task> tasks = db.Task.Where(x => x.Topic == this.Id);
                foreach (Models.Task task in tasks)
                {
                    Console.WriteLine(task.Id + ": " + task.Title);
                }
            }
            Console.WriteLine("\n" + inputs["pressanykey"]);
            Console.ReadKey();
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(Dictionary<string, string> inputs)
        {
            Console.WriteLine();

            using (var db = new Learning_Diary_ConsoleAppContext())
            {
                IQueryable<Models.Task> tasks = db.Task.Where(x => x.Topic == this.Id);
                foreach (Models.Task taskFound in tasks)
                {
                    Console.WriteLine(taskFound.Id + ": " + taskFound.Title);
                }
                int deleteChoice = ConsoleAppUi.GetInt("\n" + inputs["entertaskdeleteid"], inputs["invalid"]);

                Models.Task task = db.Task.Find(deleteChoice);
                if (task == null)
                {
                    Console.WriteLine(inputs["tasknotfound"]);
                    Console.WriteLine(inputs["pressanykey"]);
                    Console.ReadKey();
                }
                else
                {
                    db.Task.Remove(task);
                    db.SaveChangesAsync();
                    Console.WriteLine(inputs["taskdeletesuccess"]);
                    Console.WriteLine(inputs["pressanykey"]);
                    Console.ReadKey();
                }

            }
        }

        public static async System.Threading.Tasks.Task EditTaskAsync(Models.Task taskToEdit, Dictionary<string, string> inputs)
        {
            // this is nested loop to edit a task

            bool taskEditRunning = true;

            while (taskEditRunning)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                ConsoleAppUi.PrintBanner(inputs["tasktitle"]);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(taskToEdit.ToString(inputs) + "\n");

                // "1 - edit task information" + "\n" + "2 - print notes" + "\n" + "3 - add note" + "\n" + "4 - mark task as complete" + "\n" + "0 - go back." + "\n" + "Enter number to continue: "
                int taskLoopChoice = ConsoleAppUi.GetInt(inputs["taskmenu"], inputs["invalid"]); 

                switch (taskLoopChoice)
                {
                    case 0:
                        taskEditRunning = false;
                        break;

                    case 1:
                        System.Threading.Tasks.Task editTask = taskToEdit.EditTaskInfo(inputs);
                        break;

                    case 2:
                        taskToEdit.PrintNotes(inputs);
                        Console.WriteLine();
                        break;

                    case 3:
                        Console.WriteLine(inputs["inputnote"]);
                        string noteToAdd = Console.ReadLine();
                        System.Threading.Tasks.Task addTask = taskToEdit.AddNote(noteToAdd);
                        Console.WriteLine(inputs["noteaddsuccess"] + "\n" + inputs["pressanykey"]);
                        Console.ReadKey();
                        break;

                    case 4:
                        System.Threading.Tasks.Task completeTask = taskToEdit.CompleteTask();
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
