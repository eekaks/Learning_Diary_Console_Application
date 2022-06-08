using System;

namespace Learning_Diary_EL
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double EstimatedTimeToMaster { get; set; }
        public double TimeSpent { get; set; }
        public string Source { get; set; }
        public DateTime StartLearningDate { get; set; }
        public bool InProgress { get; set; }
        public DateTime CompletionDate { get; set; }

        public Topic(int id, string title, string description, double estimatedTimeToMaster, double timeSpent, string source, DateTime startLearningDate, bool inProgress, DateTime completionDate)
        {
            Id = id;
            Title = title;
            Description = description;
            EstimatedTimeToMaster = estimatedTimeToMaster;
            TimeSpent = timeSpent;
            Source = source;
            StartLearningDate = startLearningDate;
            InProgress = inProgress;
            CompletionDate = completionDate;
        }


        public override string ToString()
        {
            return string.Format("ID : {0}\n" +
                                 "Title: {1}\n" +
                                 "Description: {2}\n" +
                                 "Time to master: {3}\n" +
                                 "Time spent: {4}\n" +
                                 "Source material: {5}\n" +
                                 "Started learning: {6}\n" +
                                 "In progress: {7}\n" +
                                 "Estimated completion: {8}", Id, Title, Description, EstimatedTimeToMaster, TimeSpent,
                Source, StartLearningDate.Date.ToShortDateString(), InProgress, CompletionDate.Date.ToShortDateString());
        }
    }
}


