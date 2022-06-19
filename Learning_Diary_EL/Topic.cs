using System;
using System.Collections.Generic;

namespace Learning_Diary_EL
{
    public class Topic
    {
        public int Id { get; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public double EstimatedTimeToMaster { get; private set; }
        public double TimeSpent { get; private set; }
        public string Source { get; private set; }
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
            DateTime deadline = UserUI.GetDateTime(inputs["entertaskdl"], inputs["invalid"]);
            int choice = UserUI.GetInt(inputs["entertaskprio"], inputs["invalid"]);

            Random r = new Random();
            int id = r.Next(1, 1000000);

            Task newTask = new Task(id, name, description, deadline, choice);
            Tasks[id] = newTask;
            Console.WriteLine(inputs["taskaddsuccess"]);
            Console.WriteLine(inputs["pressanykey"]);
            Console.ReadKey();
        }

        public void EditTopicInfo(Dictionary<string, string> inputs)
        {
            Console.WriteLine("\n" + inputs["title"] + this.Title);
            Console.WriteLine(inputs["enternewtitle"]);
            this.Title = Console.ReadLine();

            Console.WriteLine("\n" + inputs["description"] + this.Description);
            Console.WriteLine(inputs["enternewdesc"]);
            this.Description = Console.ReadLine();

            Console.WriteLine("\n" + inputs["daysmaster"] + this.EstimatedTimeToMaster);
            this.EstimatedTimeToMaster = UserUI.GetDouble(inputs["enterdays"], inputs["invalid"]);

            Console.WriteLine("\n" + inputs["sourcemat"] + this.Source);
            Console.WriteLine(inputs["entersource"]);
            this.Source = Console.ReadLine();

            Console.WriteLine("\n" + inputs["topiceditsuccess"]);
            Console.WriteLine(inputs["pressanykey"]);
            Console.ReadKey();
        }
        public void PrintTasks(Dictionary<string, string> inputs)
        {
            foreach (int key in Tasks.Keys)
            {
                Console.WriteLine(Tasks[key].ToString(inputs) + "\n");
            }
            Console.WriteLine(inputs["pressanykey"]);
            Console.ReadKey();
        }
        public void PrintShortTasks(Dictionary<string, string> inputs)
        {
            Console.WriteLine();
            foreach (int key in Tasks.Keys)
            {
                Console.WriteLine(Tasks[key].Id + ": " + Tasks[key].Title);
            }
            Console.WriteLine("\n" + inputs["pressanykey"]);
            Console.ReadKey();
        }

        public static void EditTask(Task taskToEdit, Dictionary<string, string> inputs)
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

                int taskLoopChoice = UserUI.GetInt(inputs["taskmenu"], inputs["invalid"]); // "1 - edit task information" + "\n" + "2 - print notes" + "\n" + "3 - add note" + "\n" + "4 - mark task as complete" + "\n" + "0 - go back." + "\n" + "Enter number to continue: "

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
    }
}


