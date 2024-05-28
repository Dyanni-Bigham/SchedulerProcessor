using System;
using System.IO;
using Newtonsoft.Json;
using Objects;

namespace Utils
{
    class FileReader
    {
        public void ReadFile(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                List<Entry> entries = JsonConvert.DeserializeObject<List<Entry>>(json);

                foreach (Entry entry in entries)
                {
                    Console.WriteLine("Days: " + string.Join(", ", entry.Days ?? new List<string>()));
                    Console.WriteLine("App: " + string.Join(", ", entry.Apps ?? new List<string>()));
                    Console.WriteLine("Interval: " + entry.Interval);

                }
            }
            else
            {
                Console.WriteLine("File Not Found...");
            }
        }
        
    }
}