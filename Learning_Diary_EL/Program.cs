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

            Dictionary<int, Topic> topics = new Dictionary<int, Topic>();

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
                    topics = JsonSerializer.Deserialize<Dictionary<int, Topic>>(jsoninput);
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
                    Topic topicToAdd = AddTopic();
                    topics[topicToAdd.Id] = topicToAdd;
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
                    foreach (int key in topics.Keys)
                    {
                        Console.WriteLine(topics[key].Id + ": " + topics[key].Title);
                    }
                    Console.WriteLine();

                    Console.WriteLine("Enter topic ID to choose which topic to edit: ");

                    int topicChoice = int.Parse(Console.ReadLine());

                    if (topics.ContainsKey(topicChoice))
                    {
                        EditTopic(topics[topicChoice]);
                    }
                    else
                    {
                        Console.WriteLine("Topic not found.");
                    }
                }
            }

            // when main program loop ends, JSON serialize data and dump into file

            string json = JsonSerializer.Serialize(topics);

            var myfile2 = File.CreateText("topics.json");
            myfile2.Close();
            File.WriteAllText("topics.json", json);
        }

        public static Dictionary<int, Topic> DeleteTopic(Dictionary<int, Topic> topics)
        {
            Console.WriteLine("Which topic do you want to delete? Input id: ");

            int inputId = int.Parse(Console.ReadLine());

            if (topics.Remove(inputId))
            {
                Console.WriteLine("Topic removed successfully.");
            }
            else
            {
                Console.WriteLine("Topic not found.");
            }
            return topics;
        }

        public static void PrintTopics(Dictionary<int, Topic> topics)
        {
            Console.WriteLine();
            Console.WriteLine(new string('*', 30));
            Console.WriteLine("*" + new string(' ', 11) + "TOPICS" + new string(' ', 11) + "*");
            Console.WriteLine(new string('*', 30));
            foreach (int key in topics.Keys)
            {
                Console.WriteLine(topics[key]);
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
                    topicToEdit.PrintShortTasks();

                    Console.WriteLine("Give task ID to mark as complete: ");
                    int taskChoiceId = int.Parse(Console.ReadLine());

                    if (topicToEdit.Tasks.ContainsKey(taskChoiceId))
                    {
                        topicToEdit.Tasks[taskChoiceId].CompleteTask();
                        Console.WriteLine("Task marked as complete.");
                    }
                    else
                    {
                        Console.WriteLine("Task not found.");
                    }
                }
                else if (editChoice == 4)
                {
                    topicToEdit.PrintShortTasks();
                    Console.WriteLine("Enter ID of task to delete: ");
                    int deleteChoice = int.Parse(Console.ReadLine());

                    if (topicToEdit.Tasks.Remove(deleteChoice))
                    {
                        Console.WriteLine("Task deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Task not found.");
                    }
                }
                else if (editChoice == 5)
                {
                    Console.WriteLine(new string('*', 30));
                    Console.WriteLine("*" + new string(' ', 11) + "TASKS" + new string(' ', 12) + "*");
                    Console.WriteLine(new string('*', 30));

                    topicToEdit.PrintShortTasks();

                    Console.WriteLine();
                    Console.WriteLine("Enter ID of task to edit: ");
                    int taskChoice = int.Parse(Console.ReadLine());

                    if (topicToEdit.Tasks.ContainsKey(taskChoice))
                    {
                        EditTask(topicToEdit.Tasks[taskChoice]);
                    }
                    else
                    {
                        Console.WriteLine("Task not found.");
                    }
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
