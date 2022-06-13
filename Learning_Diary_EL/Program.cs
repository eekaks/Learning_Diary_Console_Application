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

            Dictionary<int, Topic> topics = new Dictionary<int, Topic>();

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
                    topics = JsonSerializer.Deserialize<Dictionary<int, Topic>>(jsoninput);
                }
            }

            Dictionary<string, string> dictionary_EN = new Dictionary<string, string>()
            {
                { "mainmenu", "1 - add a topic" + "\n" + "2 - list topics" + "\n" + "3 - delete topic" + "\n" + "4 - edit topic" + "\n" + "0 - save & exit." + "\n" + "Enter number to continue:" },
                { "invalid", "Invalid input. Try again." },
                { "topicadded", "Topic added."},
                { "entertopiceditid", "Enter topic ID to choose which topic to edit: "},
                { "topicnotfound", "Topic not found."},
                { "entertopicdeleteid", "Which topic do you want to delete? Enter ID: "},
                { "topicremovesuccess", "Topic removed successfully."},
                { "entertopictitle", "Enter your topic a title: "},
                { "entertopicdesc", "Enter your topic a description: "},
                { "enterdays", "Estimate the amount of days it takes to learn this topic at an average rate. Enter number: "},
                { "entersource", "What is the source material? e.g. url/book:"},
                { "topicmenu", "1 - add task to topic" + "\n" + "2 - list tasks" + "\n" + "3 - edit topic information" + "\n" + "4 - delete task" + "\n" + "5 - edit task" + "\n" + "6 - mark topic as complete" + "\n" + "0 - go back." + "\n" + "Enter number to continue: "},
                { "entertaskdeleteid", "Enter ID of task to delete: "},
                { "taskdeletesuccess", "Task deleted successfully."},
                { "tasknotfound", "Task not found."},
                { "entertaskeditid", "Enter ID of task to edit: "},
                { "topiccompleted", "Topic marked as completed."},
                { "taskmenu", "1 - edit task information" + "\n" + "2 - print notes" + "\n" + "3 - add note" + "\n" + "4 - mark task as complete" + "\n" + "0 - go back." + "\n" + "Enter number to continue: "},
                { "inputnote", "Input note to add: "},
                { "entertaskname", "Enter task name: "},
                { "entertaskdesc", "Enter task description: "},
                { "entertaskdl", "What is the deadline for the task? Give in following format: YYYY/MM/DD"},
                { "entertaskprio", "What is the priority of this task? 1 - Low, 2 - Medium, 3 - High"},
                { "enternewtitle", "Enter new title: "},
                { "enternewdesc", "Enter new description: "},
                { "title", "Title: "},
                { "description", "Description: "},
                { "deadline", "Deadline: "},
                { "daysmaster", "Days to master: "},
                { "daysspent", "Days spent: "},
                { "sourcemat", "Source material: "},
                { "started", "Started: "},
                { "inprogress", "In progress: "},
                { "yes", "Yes"},
                { "no", "No"},
                { "completed", "Completed: "},
                { "enternewdl", "Enter new deadline YYYY/MM/DD:"},
                { "taskeditsuccess", "Task edited successfully."},
                { "taskid", "Task ID: "},
                { "prio", "Priority: "},
                { "finished", "Finished: "},
                { "topictitle", "TOPICS"},
                { "topicstitle", "TOPICS"},
                { "tasktitle", "TASK"},
                { "taskstitle", "TASKS"},
                { "notestitle", "NOTES"}
            };

            Dictionary<string, string> dictionary_FI = new Dictionary<string, string>()
            {
                { "mainmenu", "1 - lisää aihe" + "\n" + "2 - listaa aiheet" + "\n" + "3 - poista aihe" + "\n" + "4 - muokkaa aihetta" + "\n" + "0 - tallenna ja poistu." + "\n" + "Syötä numero jatkaaksesi:" },
                { "invalid", "Virheellinen syöte. Yritä uudestaan." },
                { "topicadded", "Aihe lisätty."},
                { "entertopiceditid", "Syötä muokattavan aiheen ID: "},
                { "topicnotfound", "Aihetta ei löytynyt."},
                { "entertopicdeleteid", "Syötä poistettavan aiheen ID: "},
                { "topicremovesuccess", "Aihe poistettu."},
                { "entertopictitle", "Syötä aiheen otsikko: "},
                { "entertopicdesc", "Syötä aiheen kuvaus: "},
                { "enterdays", "Arvioi keskimäärin montako päivää aiheen oppimiseen menee. Syötä numero: "},
                { "entersource", "Mikä on aiheen lähdemateriaali? url/kirja:"},
                { "topicmenu", "1 - lisää tehtävä aiheeseen" + "\n" + "2 - listaa tehtävät" + "\n" + "3 - muokkaa aiheen tietoja" + "\n" + "4 - poista aihe" + "\n" + "5 - muokkaa tehtävää" + "\n" + "6 - merkitse aihe valmiiksi" + "\n" + "0 - mene takaisin." + "\n" + "Syötä numero jatkaaksesi: "},
                { "entertaskdeleteid", "Syötä poistettavan tehtävän ID: "},
                { "taskdeletesuccess", "Tehtävä poistettiin."},
                { "tasknotfound", "Tehtävää ei löytynyt."},
                { "entertaskeditid", "Syötä muokattavan tehtävän ID: "},
                { "topiccompleted", "Aihe merkattu valmiiksi."},
                { "taskmenu", "1 - muokkaa tehtävän tietoja" + "\n" + "2 - listaa muistiinpanot" + "\n" + "3 - lisää muistiinpano" + "\n" + "4 - merkitse tehtävä valmiiksi" + "\n" + "0 - mene takaisin." + "\n" + "Syötä numero jatkaaksesi: "},
                { "inputnote", "Syötä muistiinpano: "},
                { "entertaskname", "Syötä tehtävän nimi: "},
                { "entertaskdesc", "Syötä tehtävän kuvaus: "},
                { "entertaskdl", "Mikä on tehtävän deadline? Anna tässä formaatissa: YYYY/MM/DD"},
                { "entertaskprio", "Kuinka kiireellinen tehtävä on? 1 - Vähän, 2 - Hieman, 3 - Hyvin"},
                { "enternewtitle", "Syötä uusi otsikko: "},
                { "enternewdesc", "Syötä uusi kuvaus: "},
                { "title", "Otsikko: "},
                { "description", "Kuvaus: "},
                { "deadline", "Deadline: "},
                { "daysmaster", "Oppimisaika: "},
                { "daysspent", "Opiskeltu aika: "},
                { "sourcemat", "Lähdemateriaali: "},
                { "started", "Aloitettu: "},
                { "inprogress", "Käynnissä: "},
                { "yes", "Kyllä"},
                { "no", "Ei"},
                { "completed", "Valmistunut: "},
                { "enternewdl", "Syötä uusi deadline YYYY/MM/DD:"},
                { "taskeditsuccess", "Tehtävää muokattiin onnistuneesti."},
                { "taskid", "Tehtävän ID: "},
                { "prio", "Tärkeys: "},
                { "finished", "Valmistunut: "},
                { "topictitle", "AIHE"},
                { "topicstitle", "AIHEET"},
                { "tasktitle", "TEHTÄVÄ"},
                { "taskstitle", "TEHTÄVÄT"},
                { "notestitle", "MUISTIINPANOT"}
            };

            Dictionary<string, string> inputs = new Dictionary<string, string>();

            Console.WriteLine("Choose language, 1 - English, 2 - Suomi: ");
            int languagechoice = int.Parse(Console.ReadLine());
            if (languagechoice == 1)
            {
                inputs = dictionary_EN;
            }
            else if (languagechoice == 2)
            {
                inputs = dictionary_FI;
            }

            PrintBanner();

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

            string json = JsonSerializer.Serialize(topics);

            var myfile2 = File.CreateText("topics.json");
            myfile2.Close();
            File.WriteAllText("topics.json", json);
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
            Console.WriteLine(new string('*', 30));
            Console.WriteLine("*" + new string(' ', 11) + inputs["topicstitle"] + new string(' ', 11) + "*");
            Console.WriteLine(new string('*', 30));
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
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("*" + new string(' ', 11) + inputs["topictitle"] + new string(' ', 12) + "*");
                Console.WriteLine(new string('*', 30));
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
                    Console.WriteLine(new string('*', 30));
                    Console.WriteLine("*" + new string(' ', 11) + inputs["taskstitle"] + new string(' ', 12) + "*");
                    Console.WriteLine(new string('*', 30));
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
                    Console.WriteLine(new string('*', 30));
                    Console.WriteLine("*" + new string(' ', 11) + inputs["taskstitle"] + new string(' ', 12) + "*");
                    Console.WriteLine(new string('*', 30));

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

        public static void EditTask(Topic.Task taskToEdit, Dictionary<string, string> inputs)
        {
            // this is another nested loop to edit a task

            while (true)
            {
                Console.WriteLine(new string('*', 30));
                Console.WriteLine("*" + new string(' ', 12) + inputs["tasktitle"] + new string(' ', 12) + "*");
                Console.WriteLine(new string('*', 30));
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
        public static void PrintBanner()
        {
            Console.WriteLine(@" __         _______     ___      .______     .__   __.  __  .__   __.   _______    _______    __       ___      .______     ____    ____");
            Console.WriteLine(@"|  |      |   ____ |   /   \     |   _  \    |  \ |  | |  | |  \ |  |  /  _____|   |       \ |  |     /   \     |   _  \    \   \  /   /");
            Console.WriteLine(@"|  |      |  | __     /  ^  \    |  | _) |   |   \|  | |  | |   \|  | |  |  __     |  .--.  ||  |    /  ^  \    |  | _) |    \   \/   /");
            Console.WriteLine(@"|  |      |   __ |   /  /_\  \   |      /    |  . `  | |  | |  . `  | |  | | _|    |  |  |  ||  |   /  /_\  \   |      /      \_    _/");
            Console.WriteLine(@"|  `----. |  | ____ /  _____  \  |  |\  \----|  |\   | |  | |  |\   | |  |__| |    |  '--'  ||  |  /  _____  \  |  |\  \----.   |  |");
            Console.WriteLine(@"| _______|| _______/__/     \__\ | _| `._____|__| \__| |__| |__| \__|  \______|    |_______/ |__| /__/     \__\ | _| `._____|   |__|"); 
        }
    }
    
}
