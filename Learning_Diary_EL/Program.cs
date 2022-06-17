using System;
using System.Collections.Generic;

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
            
            UserUI.PrintProgramBanner();

            // start main program loop
            bool programRunning = true;

            while (programRunning)
            {
                int choice = UserUI.GetInt(inputs["mainmenu"], inputs["invalid"]);  //"1 - add a topic" + "\n" + "2 - list topics" + "\n" + "3 - delete topic" + "\n" + "4 - edit topic" + "\n" + "0 - save & exit." + "\n" + "Enter number to continue:"

                switch (choice)
                {
                    case 0:
                        programRunning = false;
                        break;

                    case 1:
                        Topic topicToAdd = AddTopic(inputs);
                        topics[topicToAdd.Id] = topicToAdd;
                        Console.WriteLine(inputs["topicadded"]);
                        break;

                    case 2:
                        PrintTopics(topics, inputs);
                        break;

                    case 3:
                        topics = DeleteTopic(topics, inputs);
                        break;

                    case 4:
                        Console.WriteLine();
                        foreach (int key in topics.Keys)
                        {
                            Console.WriteLine(topics[key].Id + ": " + topics[key].Title);
                        }
                        Console.WriteLine();

                        int topicChoice = UserUI.GetInt(inputs["entertopiceditid"], inputs["invalid"]);

                        if (topics.ContainsKey(topicChoice))
                        {
                            EditTopic(topics[topicChoice], inputs);
                        }
                        else
                        {
                            Console.WriteLine(inputs["topicnotfound"]);
                        }
                        break;

                    default:
                        Console.WriteLine(inputs["invalid"]);
                        break;
                }
            }
            // when main program loop ends, JSON serialize data and dump into file
            FileIO.WriteFile(topics, path);
        }

        public static Dictionary<int, Topic> DeleteTopic(Dictionary<int, Topic> topics, Dictionary<string, string> inputs)
        {
            int inputId = UserUI.GetInt(inputs["entertopicdeleteid"], inputs["invalid"]);

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
            UserUI.PrintBanner(inputs["topicstitle"]);

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

            double estimatedTimeToMaster = UserUI.GetDouble(inputs["enterdays"], inputs["invalid"]);

            Console.WriteLine(inputs["entersource"]);
            string source = Console.ReadLine();

            Random r = new Random();
            int id = r.Next(1, 1000000);

            return new Topic(title, description, estimatedTimeToMaster, source, id);
        }

        public static void EditTopic(Topic topicToEdit, Dictionary<string, string> inputs)
        {
            //this is a nested loop in the program logic to edit a topic

            bool topicEditRunning = true;

            while (topicEditRunning)
            {
                UserUI.PrintBanner(inputs["topictitle"]);

                Console.WriteLine(topicToEdit.ToString(inputs));

                int editChoice = UserUI.GetInt(inputs["topicmenu"], inputs["invalid"]); // "1 - add task to topic" + "\n" + "2 - list tasks" + "\n" + "3 - edit topic information" + "\n" + "4 - delete task" + "\n" + "5 - edit task" + "\n" + "6 - mark topic as complete" + "\n" + "0 - go back." + "\n" + "Enter number to continue: "


                switch (editChoice)
                {
                    case 0:
                        topicEditRunning = false;
                        break;

                    case 1:
                        topicToEdit.AddTask(inputs);
                        break;

                    case 2:
                        UserUI.PrintBanner(inputs["taskstitle"]);
                        topicToEdit.PrintTasks(inputs);
                        break;

                    case 3:
                        topicToEdit.EditTopicInfo(inputs);
                        break;

                    case 4:
                        topicToEdit.PrintShortTasks();
                        int deleteChoice = UserUI.GetInt(inputs["entertaskdeleteid"], inputs["invalid"]);

                        if (topicToEdit.Tasks.Remove(deleteChoice))
                        {
                            Console.WriteLine(inputs["taskdeletesuccess"]);
                        }
                        else
                        {
                            Console.WriteLine(inputs["tasknotfound"]);
                        }
                        break;

                    case 5:
                        UserUI.PrintBanner(inputs["taskstitle"]);

                        topicToEdit.PrintShortTasks();

                        int taskChoice = UserUI.GetInt(inputs["entertaskeditid"], inputs["invalid"]);

                        if (topicToEdit.Tasks.ContainsKey(taskChoice))
                        {
                            EditTask(topicToEdit.Tasks[taskChoice], inputs);
                        }
                        else
                        {
                            Console.WriteLine(inputs["tasknotfound"]);
                        }
                        break;

                    case 6:
                        topicToEdit.CompleteTopic();
                        Console.WriteLine(inputs["topiccompleted"]);
                        break;

                    default:
                        Console.WriteLine(inputs["invalid"]);
                        break;
                }
            }
        }

        public static void EditTask(Task taskToEdit, Dictionary<string, string> inputs)
        {
            // this is another nested loop to edit a task

            bool taskEditRunning = true;

            while (taskEditRunning)
            {
                UserUI.PrintBanner(inputs["tasktitle"]);
                Console.WriteLine(taskToEdit.ToString(inputs));
                Console.WriteLine();

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
                        break;

                    case 4:
                        taskToEdit.CompleteTask();
                        break;

                    default:
                        Console.WriteLine(inputs["invalid"]);
                        break;
                }
            }
        }
    }
}
