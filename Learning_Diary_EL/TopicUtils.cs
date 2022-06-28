using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Learning_Diary_EL.Models;
using System.Threading.Tasks;

namespace Learning_Diary_EL
{
    public class TopicUtils
    {
        public static Models.Topic SearchTopics(Dictionary<string, string> inputs)
        {
            PrintShortTopics(inputs);

            while (true)
            {
                Console.WriteLine("\n" + inputs["search"]);
                string search = Console.ReadLine();
                try
                {
                    // try finding an ID first
                    int searchId = int.Parse(search);
                    if (searchId == 0)
                    {
                        break;
                    }

                    using (var db = new Learning_Diary_ELContext())
                    {
                        Models.Topic topic = db.Topics.Find(searchId);
                        if (topic == null)
                        {
                            Console.WriteLine(inputs["notopicfound"]);
                            Console.WriteLine(inputs["pressanykey"]);
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            return topic;
                        }
                    }
                }
                catch (Exception e)
                {
                    //search by keyword next
                    List<Models.Topic> foundTopics = new List<Models.Topic>();

                    using (var db = new Learning_Diary_ELContext())
                    {
                        List<Models.Topic> topics = db.Topics.ToList();
                        foreach (Models.Topic topic in topics)
                        {
                            if (topic.Title.ToLower().Contains(search.ToLower()))
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
                                //if multiple topics found, specify
                                foreach (Models.Topic topic in foundTopics)
                                {
                                    Console.WriteLine(topic.Id + ": " + topic.Title);
                                }
                                int topicId = UserUI.GetInt("\n" + inputs["entertopicid"], inputs["invalid"]);
                                foreach (Models.Topic topic in foundTopics)
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
            }
            return new Models.Topic("plop", "plop", 60, "plop", -1);
        }
        public static void PrintShortTopics(Dictionary<string, string> inputs)
        {
            // UserUI.PrintBanner(inputs["topicstitle"]);
            Console.WriteLine();

            using (var db = new Learning_Diary_ELContext())
            {
                var topics = db.Topics.ToList();

                if (topics.Count == 0)
                {
                    Console.WriteLine(inputs["notopics"]);
                    Console.WriteLine(inputs["pressanykey"]);
                    Console.ReadKey();
                    return;
                }

                foreach (Models.Topic topic in topics)
                {
                    Console.WriteLine(topic.Id + ": " + topic.Title);
                }
            }
        }
        public static async System.Threading.Tasks.Task DeleteTopicAsync(Dictionary<string, string> inputs)
        {
            PrintShortTopics(inputs);
            
            int inputId = UserUI.GetInt("\n" + inputs["entertopicdeleteid"], inputs["invalid"]);

            using (var db = new Learning_Diary_ELContext())
            {
                Models.Topic topic = db.Topics.Find(inputId);

                if (topic == null)
                {
                    Console.WriteLine(inputs["topicnotfound"]);
                }
                else
                {
                    db.Topics.Remove(topic);
                    System.Threading.Tasks.Task plop = db.SaveChangesAsync();
                    Console.WriteLine(inputs["topicremovesuccess"]);
                }
                Console.WriteLine(inputs["pressanykey"]);
                Console.ReadKey();
            }
        }
        public static async System.Threading.Tasks.Task AddTopicAsync(Dictionary<string, string> inputs)
        {
            Console.WriteLine(inputs["entertopictitle"]);
            string title = Console.ReadLine();

            Console.WriteLine(inputs["entertopicdesc"]);
            string description = Console.ReadLine();

            int estimatedTimeToMaster = UserUI.GetInt(inputs["enterdays"], inputs["invalid"]);

            Console.WriteLine(inputs["entersource"]);
            string source = Console.ReadLine();

            using (var db = new Learning_Diary_ELContext())
            {
                int id;
                if (db.Topics.Count() == 0)
                {
                    id = 0;
                }
                else
                {
                    id = db.Topics.Max(topic => topic.Id);
                }
                db.Topics.Add(new Models.Topic(title, description, estimatedTimeToMaster, source, id + 1));
                db.SaveChangesAsync();
            }
        }

        public static async System.Threading.Tasks.Task EditTopicAsync(Models.Topic topicToEdit, Dictionary<string, string> inputs)
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

                // "1 - add task to topic" + "\n" + "2 - list tasks" + "\n" + "3 - edit topic information" + "\n" + "4 - delete task" + "\n" + "5 - edit task" + "\n" + "6 - mark topic as complete" + "\n" + "0 - go back." + "\n" + "Enter number to continue: "
                int editChoice = UserUI.GetInt(inputs["topicmenu"], inputs["invalid"]); 
                
                switch (editChoice)
                {
                    case 0:
                        topicEditRunning = false;
                        break;

                    case 1:
                        System.Threading.Tasks.Task addTask = topicToEdit.AddTaskAsync(inputs);
                        break;

                    case 2:
                        topicToEdit.PrintShortTasks(inputs);
                        break;

                    case 3:
                        System.Threading.Tasks.Task editTopicTask = topicToEdit.EditTopicInfoAsync(inputs);
                        break;

                    case 4:
                        System.Threading.Tasks.Task deleteTask = topicToEdit.DeleteTaskAsync(inputs);
                        break;
                        
                    case 5:
                        using (var db = new Learning_Diary_ELContext())
                        {
                            IQueryable<Models.Task> tasks = db.Tasks.Where(x => x.Topic == topicToEdit.Id);
                            foreach (Models.Task taskFound in tasks)
                            {
                                Console.WriteLine(taskFound.Id + ": " + taskFound.Title);
                            }

                            int taskChoice = UserUI.GetInt(inputs["entertaskeditid"], inputs["invalid"]);

                            Models.Task task = db.Tasks.Find(taskChoice);
                            if (task == null)
                            {
                                Console.WriteLine(inputs["tasknotfound"]);
                                Console.WriteLine(inputs["pressanykey"]);
                                Console.ReadKey();
                            }
                            else
                            {
                                System.Threading.Tasks.Task editTask = Models.Topic.EditTaskAsync(task, inputs);
                            }

                        }
                        break;

                    case 6:
                        System.Threading.Tasks.Task completeTask = topicToEdit.CompleteTopicAsync();
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
