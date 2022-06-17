﻿using System;
using System.Collections.Generic;

namespace Learning_Diary_EL
{
    public class Task
    {
        public Task(int id, string title, string description, DateTime deadline, int priority)
        {
            Id = id;
            Title = title;
            Description = description;
            Notes = new List<string>();
            Deadline = deadline;
            Done = false;
            Priority = priority;
        }

        public int Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Notes { get; set; }
        public DateTime Deadline { get; private set; }
        public bool Done { get; private set; }
        public int Priority { get; set; }

        public void CompleteTask()
        {
            Done = true;
        }

        public void AddNote(string note)
        {
            Notes.Add(note);
        }

        public void EditTaskInfo(Dictionary<string, string> inputs)
        {
            Console.WriteLine();
            Console.WriteLine(inputs["title"] + this.Title);
            Console.WriteLine(inputs["enternewtitle"]);
            this.Title = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine(inputs["description"] + this.Description);
            Console.WriteLine(inputs["enternewdesc"]);
            this.Description = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine(inputs["deadline"] + this.Deadline.ToShortDateString());
            this.Deadline = UserUI.GetDateTime(inputs["enternewdl"], inputs["invalid"]);

            Console.WriteLine();
            int choice = UserUI.GetInt(inputs["entertaskprio"], inputs["invalid"]);
            this.Priority = choice;

            Console.WriteLine();
            Console.WriteLine(inputs["taskeditsuccess"]);
        }

        public void PrintNotes(Dictionary<string, string> inputs)
        {
            UserUI.PrintBanner(inputs["notestitle"]);
            foreach (string n in Notes)
            {
                Console.WriteLine(n);
            }
        }

        public string ToString(Dictionary<string, string> inputs)
        {
            string stringBool = String.Empty;

            if (Done)
            {
                stringBool = inputs["yes"];
            }
            else
            {
                stringBool = inputs["no"];
            }
            return string.Format(
                inputs["taskid"] +  "{0}\n" + inputs["title"] + "{1}\n" + inputs["description"] + "{2}\nDeadline: {3}\n" + inputs["prio"] + "{4}\n" + inputs["finished"] + "{5}", Id, Title, Description, Deadline.ToShortDateString(), Priority, stringBool);
        }
    }
}