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
                Console.WriteLine("1 - add a topic, 2 - list topics, 3 - delete topic, 0 - exit. Choose number to continue.");

                int choice;

                try
                {
                    choice = int.Parse(Console.ReadLine());
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
                else if (choice == 2)
                {
                    Console.WriteLine();
                    foreach (Topic t in topics)
                    {
                        Console.WriteLine(t);
                        Console.WriteLine();
                        //Console.WriteLine(t.Id + ": " + t.Title);
                    }
                    Console.WriteLine();
                }
                else if (choice == 1)
                {
                    topics.Add(AddTopic());
                    Console.WriteLine("Topic added.");
                    Console.WriteLine();
                }
                else if (choice == 3)
                {
                    topics = DeleteTopic(topics);
                }
            }

            string json = JsonSerializer.Serialize(topics);
            
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
            int id;

            while (true)
            {
                Console.WriteLine("Give your topic an id: ");
                try
                {
                    id = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid id. Try again.");
                    continue;
                }

                break;
            }

            Console.WriteLine("Give your topic a title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Give your topic a description: ");
            string description = Console.ReadLine();

            Console.WriteLine("Estimate the time it takes to learn this topic: ");
            double estimatedTimeToMaster = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("What is the time spent so far: ");
            double timeSpent = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("What is the source material? e.g. url/book:");
            string source = Console.ReadLine();

            DateTime startLearningDate = new DateTime();

            while (true)
            {
                try
                {
                    Console.WriteLine("What is the date this topic was started? Give in format: YYYY/MM/DD");
                    string date = Console.ReadLine();
                    string[] dateString = date.Split("/");
                    int days = int.Parse(dateString[2]);
                    int months = int.Parse(dateString[1]);
                    int year = int.Parse(dateString[0]);
                    startLearningDate = new DateTime(year, months, days);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error. Try giving the date again.");
                    continue;
                }
                break;
            }

            bool inProgress = true;
            
            DateTime completionDate = new DateTime();

            while (true)
            {
                try
                {
                    Console.WriteLine("What is the date this topic was finished? Write 'no' if not finished, otherwise give in format: YYYY/MM/DD");
                    string date = Console.ReadLine();

                    if (date == "no")
                    {
                        break;
                    }

                    string[] dateString = date.Split("/");
                    int days = int.Parse(dateString[2]);
                    int months = int.Parse(dateString[1]);
                    int year = int.Parse(dateString[0]);
                    completionDate = new DateTime(year, months, days);
                    inProgress = false;

                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error. Try giving the date again.");
                    continue;
                }
            }

            Topic topicToAdd = new Topic(id, title, description, estimatedTimeToMaster, timeSpent, source,
                startLearningDate, inProgress, completionDate);

            return topicToAdd;
        }
    }
}
