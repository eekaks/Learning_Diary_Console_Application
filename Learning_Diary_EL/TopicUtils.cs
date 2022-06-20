using System;
using System.Collections.Generic;

namespace Learning_Diary_EL
{
    public class TopicUtils
    {
        public static Topic SearchTopics(Dictionary<int, Topic> topics, Dictionary<string, string> inputs)
        {
            PrintShortTopics(topics, inputs);

            while (true)
            {
                Console.WriteLine("\n" + inputs["search"]);
                string search = Console.ReadLine();
                try
                {
                    int searchId = int.Parse(search);
                    if (searchId == 0)
                    {
                        break;
                    }
                    foreach (int key in topics.Keys)
                    {
                        if (searchId == key)
                        {
                            return topics[key];
                        }
                    }
                    Console.WriteLine(inputs["notopicfound"]);
                    Console.WriteLine(inputs["pressanykey"]);
                    Console.ReadKey();
                    break;
                }
                catch (Exception e)
                {
                    List<Topic> foundTopics = new List<Topic>();
                    foreach (Topic topic in topics.Values)
                    {
                        if(topic.Title.ToLower().Contains(search.ToLower()))
                        {
                            foundTopics.Add(topic);
                        }
                    }

                    if (foundTopics.Count == 0)
                    {
                        Console.WriteLine(inputs["notopicfound"]);
                        Console.WriteLine(inputs["pressanykey"]);
                        Console.ReadKey();
                        break;
                    }
                    else if (foundTopics.Count == 1)
                    {
                        return foundTopics[0];
                    }
                    else if (foundTopics.Count > 1)
                    {
                        while (true)
                        {
                            foreach (Topic topic in foundTopics)
                            {
                                Console.WriteLine(topic.Id + ": " + topic.Title);
                            }
                            int topicId = UserUI.GetInt("\n" + inputs["entertopicid"], inputs["invalid"]);
                            foreach (Topic topic in foundTopics)
                            {
                                if (topicId == topic.Id)
                                {
                                    return topic;
                                }
                            }
                            Console.WriteLine(inputs["topicnotfound"]);
                            Console.WriteLine(inputs["pressanykey"]);
                            Console.ReadKey();
                            break;
                        }
                        break;
                    }
                }
            }
            return new Topic("plop", "plop", 60, "plop", -1);
        }
        public static void PrintShortTopics(Dictionary<int, Topic> topics, Dictionary<string, string> inputs)
        {
            // UserUI.PrintBanner(inputs["topicstitle"]);
            Console.WriteLine();

            foreach (int key in topics.Keys)
            {
                Console.WriteLine(topics[key].Id + ": " + topics[key].Title);
            }

            //Console.WriteLine(inputs["pressanykey"]);
            //Console.ReadKey();
        }
        public static void PrintTopics(Dictionary<int, Topic> topics, Dictionary<string, string> inputs)
        {
            UserUI.PrintBanner(inputs["topicstitle"]);

            foreach (int key in topics.Keys)
            {
                Console.WriteLine(topics[key].ToString(inputs));
            }

            Console.WriteLine(inputs["pressanykey"]);
            Console.ReadKey();
        }

        public static Dictionary<int, Topic> DeleteTopic(Dictionary<int, Topic> topics, Dictionary<string, string> inputs)
        {
            Console.WriteLine();
            foreach (int key in topics.Keys)
            {
                Console.WriteLine(topics[key].Id + ": " + topics[key].Title);
            }
            
            int inputId = UserUI.GetInt("\n" + inputs["entertopicdeleteid"], inputs["invalid"]);

            if (topics.Remove(inputId))
            {
                Console.WriteLine(inputs["topicremovesuccess"]);
            }
            else
            {
                Console.WriteLine(inputs["topicnotfound"]);
            }

            Console.WriteLine(inputs["pressanykey"]);
            Console.ReadKey();
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

            if (topicToEdit.Id == -1)
            {
                return;
            }

            bool topicEditRunning = true;

            while (topicEditRunning)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                UserUI.PrintBanner(inputs["topictitle"]);
                Console.ForegroundColor = ConsoleColor.Gray;

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
                        topicToEdit.PrintShortTasks(inputs);
                        break;

                    case 3:
                        topicToEdit.EditTopicInfo(inputs);
                        break;

                    case 4:
                        Console.WriteLine();
                        foreach (int key in topicToEdit.Tasks.Keys)
                        {
                            Console.WriteLine(topicToEdit.Tasks[key].Id + ": " + topicToEdit.Tasks[key].Title);
                        }

                        int deleteChoice = UserUI.GetInt("\n" + inputs["entertaskdeleteid"], inputs["invalid"]);

                        if (topicToEdit.Tasks.Remove(deleteChoice))
                        {
                            Console.WriteLine(inputs["taskdeletesuccess"]);
                            Console.WriteLine(inputs["pressanykey"]);
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine(inputs["tasknotfound"]);
                            Console.WriteLine(inputs["pressanykey"]);
                            Console.ReadKey();
                        }
                        break;

                    case 5:
                        Console.WriteLine();
                        foreach (int key in topicToEdit.Tasks.Keys)
                        {
                            Console.WriteLine(topicToEdit.Tasks[key].Id + ": " + topicToEdit.Tasks[key].Title);
                        }
                        
                        int taskChoice = UserUI.GetInt(inputs["entertaskeditid"], inputs["invalid"]);

                        if (topicToEdit.Tasks.ContainsKey(taskChoice))
                        {
                            Topic.EditTask(topicToEdit.Tasks[taskChoice], inputs);
                        }
                        else
                        {
                            Console.WriteLine(inputs["tasknotfound"]);
                            Console.WriteLine(inputs["pressanykey"]);
                            Console.ReadKey();
                        }
                        break;

                    case 6:
                        topicToEdit.CompleteTopic();
                        Console.WriteLine(inputs["topiccompleted"]);
                        Console.WriteLine(inputs["pressanykey"]);
                        Console.ReadKey();
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
