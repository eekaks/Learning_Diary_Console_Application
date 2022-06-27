using System;
using System.Collections.Generic;

namespace Learning_Diary_EL
{
    public class Localization
    {
        public static Dictionary<string, string> ChooseLanguage()
        {
            int languagechoice = UserUI.GetInt("Choose language, 1 - English, 2 - Suomi: ", "Invalid input. Try again.");
            if (languagechoice == 1)
            {
                return dictionary_EN;
            }
            else if (languagechoice == 2)
            {
                return dictionary_FI;
            }
            else
            {
                return dictionary_EN;
            }
        }

        public static Dictionary<string, string> dictionary_EN = new Dictionary<string, string>()
        {
            {
                "mainmenu",
                "\n1 - add a topic" + "\n" + "2 - list topics" + "\n" + "3 - delete topic" + "\n" +
                "4 - edit topic" + "\n" + "0 - exit." + "\n" + "Enter number to continue:"
            },
            { "invalid", "Invalid input. Try again." },
            { "topicadded", "Topic added.\n" },
            { "entertopiceditid", "Enter topic ID to choose which topic to edit: " },
            { "topicnotfound", "Topic not found." },
            { "entertopicdeleteid", "Which topic do you want to delete? Enter ID: " },
            { "topicremovesuccess", "Topic removed successfully." },
            { "entertopictitle", "Enter your topic a title: " },
            { "entertopicdesc", "Enter your topic a description: " },
            {
                "enterdays",
                "Estimate the amount of days it takes to learn this topic at an average rate. Enter number: "
            },
            { "entersource", "What is the source material? e.g. url/book:" },
            {
                "topicmenu",
                "1 - add task to topic" + "\n" + "2 - list tasks" + "\n" + "3 - edit topic information" + "\n" +
                "4 - delete task" + "\n" + "5 - edit task" + "\n" + "6 - mark topic as complete" + "\n" +
                "0 - go back." + "\n" + "Enter number to continue: "
            },
            { "entertaskdeleteid", "Enter ID of task to delete: " },
            { "taskdeletesuccess", "Task deleted successfully." },
            { "tasknotfound", "Task not found." },
            { "entertaskeditid", "\nEnter ID of task to edit: " },
            { "topiccompleted", "Topic marked as completed." },
            {
                "taskmenu",
                "1 - edit task information" + "\n" + "2 - print notes" + "\n" + "3 - add note" + "\n" +
                "4 - mark task as complete" + "\n" + "0 - go back." + "\n" + "Enter number to continue: "
            },
            { "inputnote", "Input note to add: " },
            { "entertaskname", "Enter task name: " },
            { "entertaskdesc", "Enter task description: " },
            { "entertaskdl", "What is the deadline for the task? Give in following format: YYYY/MM/DD" },
            { "entertaskprio", "What is the priority of this task? 1 - Low, 2 - Medium, 3 - High" },
            { "enternewtitle", "Enter new title: " },
            { "enternewdesc", "Enter new description: " },
            { "title", "Title: " },
            { "description", "Description: " },
            { "deadline", "Deadline: " },
            { "daysmaster", "Days to master: " },
            { "daysspent", "Days spent: " },
            { "sourcemat", "Source material: " },
            { "started", "Started: " },
            { "inprogress", "In progress: " },
            { "yes", "Yes" },
            { "no", "No" },
            { "completed", "Completed: " },
            { "enternewdl", "Enter new deadline YYYY/MM/DD:" },
            { "taskeditsuccess", "Task edited successfully." },
            { "taskid", "Task ID: " },
            { "prio", "Priority: " },
            { "finished", "Finished: " },
            { "topictitle", "TOPIC" },
            { "topicstitle", "TOPICS" },
            { "tasktitle", "TASK" },
            { "taskstitle", "TASKS" },
            { "notestitle", "NOTES" },
            { "pressanykey", "Press any key to continue."},
            { "taskaddsuccess", "Task added successfully."},
            { "topiceditsuccess", "Topic edited successfully."},
            { "noteaddsuccess", "Note added."},
            { "taskmarkcomplete", "Task marked as completed."},
            { "search", "Enter ID or keyword to search. Enter 0 to go back."},
            { "notopicfound", "No topic found."},
            { "entertopicid", "Enter ID of topic to continue."},
            { "notopics", "There are no entries in the learning diary."}
        };

        public static Dictionary<string, string> dictionary_FI = new Dictionary<string, string>()
        {
            {
                "mainmenu",
                "\n1 - lisää aihe" + "\n" + "2 - listaa aiheet" + "\n" + "3 - poista aihe" + "\n" +
                "4 - muokkaa aihetta" + "\n" + "0 - poistu." + "\n" + "Syötä numero jatkaaksesi:"
            },
            { "invalid", "Virheellinen syöte. Yritä uudestaan." },
            { "topicadded", "Aihe lisätty.\n" },
            { "entertopiceditid", "Syötä muokattavan aiheen ID: " },
            { "topicnotfound", "Aihetta ei löytynyt." },
            { "entertopicdeleteid", "Syötä poistettavan aiheen ID: " },
            { "topicremovesuccess", "Aihe poistettu." },
            { "entertopictitle", "Syötä aiheen otsikko: " },
            { "entertopicdesc", "Syötä aiheen kuvaus: " },
            { "enterdays", "Arvioi keskimäärin montako päivää aiheen oppimiseen menee. Syötä numero: " },
            { "entersource", "Mikä on aiheen lähdemateriaali? url/kirja:" },
            {
                "topicmenu",
                "1 - lisää tehtävä aiheeseen" + "\n" + "2 - listaa tehtävät" + "\n" +
                "3 - muokkaa aiheen tietoja" + "\n" + "4 - poista aihe" + "\n" + "5 - muokkaa tehtävää" + "\n" +
                "6 - merkitse aihe valmiiksi" + "\n" + "0 - mene takaisin." + "\n" +
                "Syötä numero jatkaaksesi: "
            },
            { "entertaskdeleteid", "Syötä poistettavan tehtävän ID: " },
            { "taskdeletesuccess", "Tehtävä poistettiin." },
            { "tasknotfound", "Tehtävää ei löytynyt." },
            { "entertaskeditid", "\nSyötä muokattavan tehtävän ID: " },
            { "topiccompleted", "Aihe merkattu valmiiksi." },
            {
                "taskmenu",
                "1 - muokkaa tehtävän tietoja" + "\n" + "2 - listaa muistiinpanot" + "\n" +
                "3 - lisää muistiinpano" + "\n" + "4 - merkitse tehtävä valmiiksi" + "\n" +
                "0 - mene takaisin." + "\n" + "Syötä numero jatkaaksesi: "
            },
            { "inputnote", "Syötä muistiinpano: " },
            { "entertaskname", "Syötä tehtävän nimi: " },
            { "entertaskdesc", "Syötä tehtävän kuvaus: " },
            { "entertaskdl", "Mikä on tehtävän deadline? Anna tässä formaatissa: YYYY/MM/DD" },
            { "entertaskprio", "Kuinka kiireellinen tehtävä on? 1 - Vähän, 2 - Hieman, 3 - Hyvin" },
            { "enternewtitle", "Syötä uusi otsikko: " },
            { "enternewdesc", "Syötä uusi kuvaus: " },
            { "title", "Otsikko: " },
            { "description", "Kuvaus: " },
            { "deadline", "Deadline: " },
            { "daysmaster", "Oppimisaika: " },
            { "daysspent", "Opiskeltu aika: " },
            { "sourcemat", "Lähdemateriaali: " },
            { "started", "Aloitettu: " },
            { "inprogress", "Käynnissä: " },
            { "yes", "Kyllä" },
            { "no", "Ei" },
            { "completed", "Valmistunut: " },
            { "enternewdl", "Syötä uusi deadline YYYY/MM/DD:" },
            { "taskeditsuccess", "Tehtävää muokattu." },
            { "taskid", "Tehtävän ID: " },
            { "prio", "Tärkeys: " },
            { "finished", "Valmistunut: " },
            { "topictitle", "AIHE" },
            { "topicstitle", "AIHEET" },
            { "tasktitle", "TEHTÄVÄ" },
            { "taskstitle", "TEHTÄVÄT" },
            { "notestitle", "MUISTIINPANOT" },
            { "pressanykey", "Paina mitä tahansa näppäintä jatkaaksesi."},
            { "taskaddsuccess", "Tehtävä lisätty."},
            { "topiceditsuccess", "Aihetta muokattu."},
            { "noteaddsuccess", "Muistiinpano lisätty."},
            { "taskmarkcomplete", "Tehtävä merkitty valmiiksi."},
            { "search", "Syötä ID tai hakusana. Syötä 0 siirtyäksesi takaisin."},
            { "notopicfound", "Aihetta ei löydetty."},
            { "entertopicid", "Syötä aiheen ID jatkaaksesi."},
            { "notopics", "Oppimispäiväkirjassa ei ole yhtään aihetta."}
        };
    }
}