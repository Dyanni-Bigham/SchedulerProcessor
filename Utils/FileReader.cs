using System;
using System.IO;
using Newtonsoft.Json;
using Objects;

namespace Utils
{
    public class FileReader
    {
        private string filePath;
        private List<Entry> entries;
        // Create a constructor where it takes in a filepath to the config file
        public FileReader(string path)
        {
            this.filePath = path;
            this.entries = new List<Entry>();
        }
        public void ReadFile()
        {
            if (File.Exists(this.filePath))
            {
                string json = File.ReadAllText(this.filePath);
                List<Entry> newEntries = JsonConvert.DeserializeObject<List<Entry>>(json);

                foreach (Entry entry in newEntries)
                {

                    Entry newEntry = new Entry(entry.days, entry.apps, entry.interval);
                    this.entries.Add(newEntry);
                    Console.WriteLine("Entry successfully added");

                }
            }
            else
            {
                Console.WriteLine("File Not Found...");
            }
        }
        
        public List<Entry> GetEntries()
        {
            return this.entries;
        }

        public void DisplayEntries()
        {
            for (int i = 0; i < this.entries.Count; i++)
            {
                //Console.WriteLine($"Element: {i}\n");
                Console.WriteLine($"Entry: {this.entries[i].ToString()}");
            }
        }
    }
}