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

            while (true)
            {
                Console.WriteLine("1 - add a topic, 2 - list topics, 3 - delete topic, 4 - edit topic, 0 - exit. Choose number to continue.");

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
                    Console.WriteLine();
                    foreach (Topic t in topics)
                    {
                        Console.WriteLine(t);
                        //Console.WriteLine(t.Id + ": " + t.Title);
                        Console.WriteLine();
                    }
                }
                else if (choice == 3)
                {
                    topics = DeleteTopic(topics);
                }
                else if (choice == 4)
                {
                    foreach (Topic t in topics)
                    {
                        Console.WriteLine(t.Id + ": " + t.Title);
                    }

                    Console.WriteLine("Enter topic ID to choose which topic to edit: ");

                    int topicChoice = int.Parse(Console.ReadLine());

                    Topic topicToEdit = new Topic("plop", "plop", 1, "plop"); //placeholder object to be overwritten

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

        public static Topic AddTopic()
        {
            Console.WriteLine("Give your topic a title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Give your topic a description: ");
            string description = Console.ReadLine();

            Console.WriteLine("Estimate the amount of days it takes to learn this topic at an average rate: ");
            double estimatedTimeToMaster = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("What is the source material? e.g. url/book:");
            string source = Console.ReadLine();

            return new Topic(title, description, estimatedTimeToMaster, source);
        }

        public static void EditTopic(Topic topicToEdit)
        {
            while (true)
            {
                Console.WriteLine(topicToEdit);
                Console.WriteLine();
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
                    topicToEdit.PrintTasks();
                    Console.WriteLine("Enter ID of task to edit: ");
                    int taskChoice = int.Parse(Console.ReadLine());

                    Topic.Task taskToEdit = new Topic.Task("plop", "plip", DateTime.Now, 1); //placeholder

                    foreach (Topic.Task t in topicToEdit.Tasks)
                    {
                        if (taskChoice == t.Id)
                        {
                            taskToEdit = t;
                        }
                    }

                    while (true)
                    {
                        Console.WriteLine("1 - print task, 2 - print notes, 3 - add note, 0 - go back");
                        int taskLoopChoice = int.Parse(Console.ReadLine());

                        if (taskLoopChoice == 1)
                        {
                            Console.WriteLine(taskToEdit);
                        }
                        else if (taskLoopChoice == 2)
                        {
                            taskToEdit.PrintNotes();
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
            }
        }
    }
}
