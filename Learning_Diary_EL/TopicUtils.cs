using System;
using System.Collections.Generic;

namespace Learning_Diary_EL
{
    public class TopicUtils
    {
        public static void PrintTopics(Dictionary<int, Topic> topics, Dictionary<string, string> inputs)
        {
            Console.WriteLine();
            UserUI.PrintBanner(inputs["topicstitle"]);

            foreach (int key in topics.Keys)
            {
                Console.WriteLine(topics[key].ToString(inputs));
            }
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
                            Topic.EditTask(topicToEdit.Tasks[taskChoice], inputs);
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
    }
}
