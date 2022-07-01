using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Learning_Diary_EL
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // set slightly larger console window
            Console.SetWindowSize(136, 30);

            // choose localization
            Dictionary<string, string> inputs = Localization.ChooseLanguage();

            // start main program loop
            bool programRunning = true;
            
            while (programRunning)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                ConsoleAppUi.PrintProgramBanner();
                Console.ForegroundColor = ConsoleColor.Gray;

                //"1 - add a topic" + "\n" + "2 - list topics" + "\n" + "3 - delete topic" + "\n" + "4 - edit topic" + "\n" + "0 - save & exit." + "\n" + "Enter number to continue:"
                int choice = ConsoleAppUi.GetInt(inputs["mainmenu"], inputs["invalid"]);  

                switch (choice)
                {
                    case 0:
                        programRunning = false;
                        break;

                    case 1:
                        System.Threading.Tasks.Task addTask = TopicUtils.AddTopicAsync(inputs);
                        Console.WriteLine(inputs["topicadded"]);
                        Console.WriteLine(inputs["pressanykey"]);
                        Console.ReadKey();
                        break;

                    case 2:
                        TopicUtils.PrintShortTopics(inputs);
                        Console.WriteLine("\n" + inputs["pressanykey"]);
                        Console.ReadKey();
                        break;

                    case 3:
                        System.Threading.Tasks.Task deleteTask = TopicUtils.DeleteTopicAsync(inputs);
                        break;

                    case 4:
                        System.Threading.Tasks.Task editTask = TopicUtils.EditTopicAsync(TopicUtils.SearchTopics(inputs), inputs);
                        break;

                    default:
                        Console.WriteLine(inputs["invalid"]);
                        Console.WriteLine(inputs["pressanykey"]);
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
