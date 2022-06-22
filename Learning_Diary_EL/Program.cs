using System;
using System.Collections.Generic;

namespace Learning_Diary_EL
{
    class Program
    {
        static void Main(string[] args)
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
                UserUI.PrintProgramBanner();
                Console.ForegroundColor = ConsoleColor.Gray;

                //"1 - add a topic" + "\n" + "2 - list topics" + "\n" + "3 - delete topic" + "\n" + "4 - edit topic" + "\n" + "0 - save & exit." + "\n" + "Enter number to continue:"
                int choice = UserUI.GetInt(inputs["mainmenu"], inputs["invalid"]);  

                switch (choice)
                {
                    case 0:
                        programRunning = false;
                        break;

                    case 1:
                        TopicUtils.AddTopic(inputs);
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
                        TopicUtils.DeleteTopic(inputs);
                        break;

                    case 4:
                        TopicUtils.EditTopic(TopicUtils.SearchTopics(inputs), inputs);
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
