using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;


namespace Learning_Diary_EL
{
    class Program
    {
        static void Main(string[] args)
        {
            // read existing file to list

            List<Topic> topics = new List<Topic>();

            if (!File.Exists("topics.json"))
            {
                var myfile = File.CreateText("topics.json");
                myfile.Close();
            }
            else
            {
                using (StreamReader r = new StreamReader("topics.json"))
                {
                    string jsoninput = r.ReadToEnd();
                    topics = JsonSerializer.Deserialize<List<Topic>>(jsoninput);
                }
            }

            PrintBanner();

            // start main program loop

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1 - add a topic, 2 - list topics, 3 - delete topic, 4 - edit topic, 0 - save & exit. Choose number to continue.");

                int choice;

                try
                {
                    choice = int.Parse(Console.ReadLine());
                    if (choice > 5 || choice < 0)
                    {
                        Console.WriteLine("Invalid input. Try again.");
                        continue;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input. Try again.");
                    continue;
                }

                if (choice == 0)
                {
                    break;
                }
                else if (choice == 1)
                {
                    topics.Add(AddTopic());
                    Console.WriteLine("Topic added.");
                    Console.WriteLine();
                }
                else if (choice == 2)
                {
                    PrintTopics(topics);
                }
                else if (choice == 3)
                {
                    topics = DeleteTopic(topics);
                }
                else if (choice == 4)
                {
                    Console.WriteLine();
                    foreach (Topic t in topics)
                    {
                        Console.WriteLine(t.Id + ": " + t.Title);
                    }
                    Console.WriteLine();

                    Console.WriteLine("Enter topic ID to choose which topic to edit: ");

                    int topicChoice = int.Parse(Console.ReadLine());

                    Topic topicToEdit = new Topic("plop", "plop", 1, "plop", 554455); //placeholder object to be overwritten

                    foreach (Topic t in topics)
                    {
                        if (t.Id == topicChoice)
                        {
                            topicToEdit = t;
                        }
                    }
                    EditTopic(topicToEdit);
                }
            }

            // when main program loop ends, JSON serialize data and dump into file

            string json = JsonSerializer.Serialize(topics);

            var myfile2 = File.CreateText("topics.json");
            myfile2.Close();
            File.WriteAllText("topics.json", json);
        }

        public static List<Topic> DeleteTopic(List<Topic> topics)
        {
            Console.WriteLine("Which topic do you want to delete? Input id: ");
            int inputId = int.Parse(Console.ReadLine());
            return topics.Where(t => t.Id != inputId).ToList();
        }

        public static void PrintTopics(List<Topic> topics)
        {
            Console.WriteLine();
            Console.WriteLine(new string('*', 30));
            Console.WriteLine("*" + new string(' ', 11) + "TOPICS" + new string(' ', 11) + "*");
            Console.WriteLine(new string('*', 30));
            foreach (Topic t in topics)
            {
                Console.WriteLine(t);
                //Console.WriteLine(t.Id + ": " + t.Title);
                //Console.WriteLine();
            }
        }

        public static Topic AddTopic()
        {
            Console.WriteLine("Give your topic a title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Give your topic a description: ");
            string description = Console.ReadLine();

            Console.WriteLine("Estimate the amount of days it takes to learn this topic at an average rate. Enter number: ");
            double estimatedTimeToMaster = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("What is the source material? e.g. url/book:");
            string source = Console.ReadLine();

            Random r = new Random();
            int id = r.Next(1, 1000000);

            return new Topic(title, description, estimatedTimeToMaster, source, id);
        }

        public static void EditTopic(Topic topicToEdit)
        {
            //this is a nested loop in the program logic to edit a topic

            while (true)
            {
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("*" + new string(' ', 11) + "TOPIC" + new string(' ', 12) + "*");
                Console.WriteLine(new string('*', 30));
                Console.WriteLine(topicToEdit);
                
                Console.WriteLine("1 - add task to topic, 2 - list tasks, 3 - mark task as complete, 4 - delete task, 5 - edit task, 0 - go back");

                int editChoice = int.Parse(Console.ReadLine());

                if (editChoice == 0)
                {
                    break;
                }
                else if (editChoice == 1)
                {
                    topicToEdit.AddTask();
                }
                else if (editChoice == 2)
                {
                    Console.WriteLine(new string('*', 30));
                    Console.WriteLine("*" + new string(' ', 11) + "TASKS" + new string(' ', 12) + "*");
                    Console.WriteLine(new string('*', 30));
                    topicToEdit.PrintTasks();
                }
                else if (editChoice == 3)
                {
                    topicToEdit.PrintTasks();

                    Console.WriteLine("Give task ID to mark as complete: ");
                    int taskChoiceId = int.Parse(Console.ReadLine());

                    foreach (Topic.Task t in topicToEdit.Tasks)
                    {
                        if (t.Id == taskChoiceId)
                        {
                            t.CompleteTask();
                        }
                    }

                    Console.WriteLine("Task marked as complete. ");
                }
                else if (editChoice == 4)
                {
                    topicToEdit.PrintTasks();
                    Console.WriteLine("Enter ID of task to delete: ");
                    int deleteChoice = int.Parse(Console.ReadLine());
                    topicToEdit.Tasks = topicToEdit.Tasks.Where(t => t.Id != deleteChoice).ToList();
                    Console.WriteLine("Task deleted. ");
                }
                else if (editChoice == 5)
                {
                    Console.WriteLine(new string('*', 30));
                    Console.WriteLine("*" + new string(' ', 11) + "TASKS" + new string(' ', 12) + "*");
                    Console.WriteLine(new string('*', 30));

                    foreach (Topic.Task t in topicToEdit.Tasks)
                    {
                        Console.WriteLine(t.Id + ": " + t.Title);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Enter ID of task to edit: ");
                    int taskChoice = int.Parse(Console.ReadLine());

                    Topic.Task taskToEdit = new Topic.Task("plop", "plip", DateTime.Now, 1, 554455); //placeholder
                    
                    foreach (Topic.Task t in topicToEdit.Tasks)
                    {
                        if (taskChoice == t.Id)
                        {
                            taskToEdit = t;
                        }
                    }
                    EditTask(taskToEdit);
                }
            }
        }

        public static void EditTask(Topic.Task taskToEdit)
        {
            // this is another nested loop to edit a task

            while (true)
            {
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("*" + new string(' ', 12) + "TASK" + new string(' ', 12) + "*");
                Console.WriteLine(new string('*', 30));
                Console.WriteLine(taskToEdit);
                Console.WriteLine();

                Console.WriteLine("1 - print task, 2 - print notes, 3 - add note, 0 - go back");
                int taskLoopChoice = int.Parse(Console.ReadLine());

                if (taskLoopChoice == 1)
                {
                    Console.WriteLine(taskToEdit);
                    Console.WriteLine();
                }
                else if (taskLoopChoice == 2)
                {
                    taskToEdit.PrintNotes();
                    Console.WriteLine();
                }
                else if (taskLoopChoice == 3)
                {
                    Console.WriteLine("Input note to add: ");
                    string noteToAdd = Console.ReadLine();
                    taskToEdit.AddNote(noteToAdd);
                }
                else if (taskLoopChoice == 0)
                {
                    break;
                }
            }
        }
        public static void PrintBanner()
        {
            Console.WriteLine(@" __         _______     ___      .______     .__   __.  __  .__   __.   _______    _______    __       ___      .______     ____    ____");
            Console.WriteLine(@"|  |      |   ____ |   /   \     |   _  \    |  \ |  | |  | |  \ |  |  /  _____|   |       \ |  |     /   \     |   _  \    \   \  /   /");
            Console.WriteLine(@"|  |      |  | __     /  ^  \    |  | _) |   |   \|  | |  | |   \|  | |  |  __     |  .--.  ||  |    /  ^  \    |  | _) |    \   \/   /");
            Console.WriteLine(@"|  |      |   __ |   /  /_\  \   |      /    |  . `  | |  | |  . `  | |  | | _|    |  |  |  ||  |   /  /_\  \   |      /      \_    _/");
            Console.WriteLine(@"|  `----. |  | ____ /  _____  \  |  |\  \----|  |\   | |  | |  |\   | |  |__| |    |  '--'  ||  |  /  _____  \  |  |\  \----.   |  |");
            Console.WriteLine(@"| _______|| _______/__/     \__\ | _| `._____|__| \__| |__| |__| \__|  \______|    |_______/ |__| /__/     \__\ | _| `._____|   |__|"); 
        }
    }
    
}
