using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;

namespace Learning_Diary_EL
{
    class Program
    {
        static void Main(string[] args)
        {
            // read existing file to dictionary
            string path = "topics.json";
            Dictionary<int, Topic> topics = FileIO.ReadFile(path);

            // choose localization
            Dictionary<string, string> inputs = Localization.ChooseLanguage();
            
            Banners.PrintProgramBanner();

            // start main program loop

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(inputs["mainmenu"]); //"1 - add a topic" + "\n" + "2 - list topics" + "\n" + "3 - delete topic" + "\n" + "4 - edit topic" + "\n" + "0 - save & exit." + "\n" + "Enter number to continue:"

                int choice;

                try
                {
                    choice = int.Parse(Console.ReadLine());
                    if (choice > 5 || choice < 0)
                    {
                        Console.WriteLine(inputs["invalid"]);
                        continue;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(inputs["invalid"]);
                    continue;
                }

                if (choice == 0)
                {
                    break;
                }
                else if (choice == 1)
                {
                    Topic topicToAdd = AddTopic(inputs);
                    topics[topicToAdd.Id] = topicToAdd;
                    Console.WriteLine(inputs["topicadded"]);
                    Console.WriteLine();
                }
                else if (choice == 2)
                {
                    PrintTopics(topics, inputs);
                }
                else if (choice == 3)
                {
                    topics = DeleteTopic(topics, inputs);
                }
                else if (choice == 4)
                {
                    Console.WriteLine();
                    foreach (int key in topics.Keys)
                    {
                        Console.WriteLine(topics[key].Id + ": " + topics[key].Title);
                    }
                    Console.WriteLine();

                    Console.WriteLine(inputs["entertopiceditid"]);

                    int topicChoice = int.Parse(Console.ReadLine());

                    if (topics.ContainsKey(topicChoice))
                    {
                        EditTopic(topics[topicChoice], inputs);
                    }
                    else
                    {
                        Console.WriteLine(inputs["topicnotfound"]);
                    }
                }
            }
            // when main program loop ends, JSON serialize data and dump into file
            FileIO.WriteFile(topics, path);
        }

        public static Dictionary<int, Topic> DeleteTopic(Dictionary<int, Topic> topics, Dictionary<string, string> inputs)
        {
            Console.WriteLine(inputs["entertopicdeleteid"]);

            int inputId = int.Parse(Console.ReadLine());

            if (topics.Remove(inputId))
            {
                Console.WriteLine(inputs["topicremovesuccess"]);
            }
            else
            {
                Console.WriteLine(inputs["topicnotfound"]);
            }
            return topics;
        }

        public static void PrintTopics(Dictionary<int, Topic> topics, Dictionary<string, string> inputs)
        {
            Console.WriteLine();
            Banners.PrintBanner(inputs["topicstitle"]);

            foreach (int key in topics.Keys)
            {
                Console.WriteLine(topics[key].ToString(inputs));
            }
        }

        public static Topic AddTopic(Dictionary<string, string> inputs)
        {
            Console.WriteLine(inputs["entertopictitle"]);
            string title = Console.ReadLine();

            Console.WriteLine(inputs["entertopicdesc"]);
            string description = Console.ReadLine();

            Console.WriteLine(inputs["enterdays"]);
            double estimatedTimeToMaster = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine(inputs["entersource"]);
            string source = Console.ReadLine();

            Random r = new Random();
            int id = r.Next(1, 1000000);

            return new Topic(title, description, estimatedTimeToMaster, source, id);
        }

        public static void EditTopic(Topic topicToEdit, Dictionary<string, string> inputs)
        {
            //this is a nested loop in the program logic to edit a topic

            while (true)
            {
                Banners.PrintBanner(inputs["topictitle"]);

                Console.WriteLine(topicToEdit.ToString(inputs));
                
                Console.WriteLine(inputs["topicmenu"]); // "1 - add task to topic" + "\n" + "2 - list tasks" + "\n" + "3 - edit topic information" + "\n" + "4 - delete task" + "\n" + "5 - edit task" + "\n" + "6 - mark topic as complete" + "\n" + "0 - go back." + "\n" + "Enter number to continue: "

                int editChoice = int.Parse(Console.ReadLine());

                if (editChoice == 0)
                {
                    break;
                }
                else if (editChoice == 1)
                {
                    topicToEdit.AddTask(inputs);
                }
                else if (editChoice == 2)
                {
                    Banners.PrintBanner(inputs["taskstitle"]);
                    topicToEdit.PrintTasks(inputs);
                }
                else if (editChoice == 3)
                {
                    topicToEdit.EditTopicInfo(inputs);
                }
                else if (editChoice == 4)
                {
                    topicToEdit.PrintShortTasks();
                    Console.WriteLine(inputs["entertaskdeleteid"]);
                    int deleteChoice = int.Parse(Console.ReadLine());

                    if (topicToEdit.Tasks.Remove(deleteChoice))
                    {
                        Console.WriteLine(inputs["taskdeletesuccess"]);
                    }
                    else
                    {
                        Console.WriteLine(inputs["tasknotfound"]);
                    }
                }
                else if (editChoice == 5)
                {
                    Banners.PrintBanner(inputs["taskstitle"]);

                    topicToEdit.PrintShortTasks();

                    Console.WriteLine();
                    Console.WriteLine(inputs["entertaskeditid"]);
                    int taskChoice = int.Parse(Console.ReadLine());

                    if (topicToEdit.Tasks.ContainsKey(taskChoice))
                    {
                        EditTask(topicToEdit.Tasks[taskChoice], inputs);
                    }
                    else
                    {
                        Console.WriteLine(inputs["tasknotfound"]);
                    }
                }
                else if (editChoice == 6)
                {
                    topicToEdit.CompleteTopic();
                    Console.WriteLine(inputs["topiccompleted"]);
                }
            }
        }

        public static void EditTask(Task taskToEdit, Dictionary<string, string> inputs)
        {
            // this is another nested loop to edit a task

            while (true)
            {
                Banners.PrintBanner(inputs["tasktitle"]);

                Console.WriteLine(taskToEdit.ToString(inputs));
                Console.WriteLine();

                Console.WriteLine(inputs["taskmenu"]); // "1 - edit task information" + "\n" + "2 - print notes" + "\n" + "3 - add note" + "\n" + "4 - mark task as complete" + "\n" + "0 - go back." + "\n" + "Enter number to continue: "
                int taskLoopChoice = int.Parse(Console.ReadLine());

                if (taskLoopChoice == 1)
                {
                    taskToEdit.EditTaskInfo(inputs);
                }
                else if (taskLoopChoice == 2)
                {
                    taskToEdit.PrintNotes(inputs);
                    Console.WriteLine();
                }
                else if (taskLoopChoice == 3)
                {
                    Console.WriteLine(inputs["inputnote"]);
                    string noteToAdd = Console.ReadLine();
                    taskToEdit.AddNote(noteToAdd);
                }
                else if (taskLoopChoice == 4)
                {
                    taskToEdit.CompleteTask();
                }
                else if (taskLoopChoice == 0)
                {
                    break;
                }
            }
        }
    }
}
