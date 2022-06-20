using System;
using System.Collections.Generic;
using System.Runtime.Versioning;

namespace Learning_Diary_EL
{
    class Program
    {
        static void Main(string[] args)
        {
            // read existing file to dictionary
            string path = "topics.json";
            Dictionary<int, Topic> topics = FileIO.ReadFile(path);

            Console.SetWindowSize(136, 30);

            // choose localization
            Dictionary<string, string> inputs = Localization.ChooseLanguage();
            
            // start main program loop
            bool programRunning = true;
            
            while (programRunning)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                UserUI.PrintProgramBanner();
                Console.ForegroundColor = ConsoleColor.Gray;

                int choice = UserUI.GetInt(inputs["mainmenu"], inputs["invalid"]);  //"1 - add a topic" + "\n" + "2 - list topics" + "\n" + "3 - delete topic" + "\n" + "4 - edit topic" + "\n" + "0 - save & exit." + "\n" + "Enter number to continue:"

                switch (choice)
                {
                    case 0:
                        programRunning = false;
                        break;

                    case 1:
                        Topic topicToAdd = TopicUtils.AddTopic(inputs);
                        topics[topicToAdd.Id] = topicToAdd;
                        Console.WriteLine(inputs["topicadded"]);
                        Console.WriteLine(inputs["pressanykey"]);
                        Console.ReadKey();
                        break;

                    case 2:
                        TopicUtils.PrintShortTopics(topics, inputs);
                        Console.WriteLine("\n" + inputs["pressanykey"]);
                        Console.ReadKey();
                        break;

                    case 3:
                        topics = TopicUtils.DeleteTopic(topics, inputs);
                        break;

                    case 4:
                        if (topics.Count == 0)
                        {
                            Console.WriteLine(inputs["notopics"]);
                            Console.WriteLine(inputs["pressanykey"]);
                            Console.ReadKey();
                        }
                        TopicUtils.EditTopic(TopicUtils.SearchTopics(topics, inputs), inputs);
                        //Console.WriteLine();
                        //foreach (int key in topics.Keys)
                        //{
                        //    Console.WriteLine(topics[key].Id + ": " + topics[key].Title);
                        //}
                        //Console.WriteLine();

                        //int topicChoice = UserUI.GetInt(inputs["entertopiceditid"], inputs["invalid"]);

                        //if (topics.ContainsKey(topicChoice))
                        //{
                        //    TopicUtils.EditTopic(topics[topicChoice], inputs);
                        //}
                        //else
                        //{
                        //    Console.WriteLine(inputs["topicnotfound"]);
                        //    Console.WriteLine(inputs["pressanykey"]);
                        //    Console.ReadKey();
                        //}
                        break;

                    default:
                        Console.WriteLine(inputs["invalid"]);
                        Console.WriteLine(inputs["pressanykey"]);
                        Console.ReadKey();
                        break;
                }
            }
            // when main program loop ends, JSON serialize data and dump into file
            FileIO.WriteFile(topics, path);
        }
    }
}
