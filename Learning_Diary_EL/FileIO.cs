using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Learning_Diary_EL.Models;

namespace Learning_Diary_EL
{
    public class FileIO
    {
        public static Dictionary<int, Topic> ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                var myfile = File.CreateText(path);
                myfile.Close();
                return new Dictionary<int, Topic>();
            }
            using (StreamReader r = new StreamReader(path))
            {
                string jsoninput = r.ReadToEnd();
                return JsonSerializer.Deserialize<Dictionary<int, Topic>>(jsoninput);
            }
        }
        public static void WriteFile(Dictionary<int, Topic> topics, string path)
        {
            string json = JsonSerializer.Serialize(topics);

            var myfile2 = File.CreateText(path);
            myfile2.Close();
            File.WriteAllText(path, json);
        }
    }
}
