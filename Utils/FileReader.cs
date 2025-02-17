using System;
using System.IO;
using Log;
using Newtonsoft.Json;
using Objects;

namespace Utils
{
    public class FileReader
    {
        private static string _filePath;
        private static List<Entry> _entries = new List<Entry>();

        public static string GetFilePath()
        {
            return _filePath;
        }

        public static void SetFilePath(string filePath)
        {
            Logger.Log("Setting the file path");
            string currDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _filePath = Path.Combine(currDirectory, filePath);
            Logger.Log($"I am trying read this file: {_filePath}");
        }

        public static List<Entry> GetEntries()
        {
            return _entries;
        }

        public static void SetEntries(List<Entry> entries)
        {
            _entries = entries;
        }
        public static void ReadFile()
        {
            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                List<Entry> newEntries = JsonConvert.DeserializeObject<List<Entry>>(json);

                foreach (Entry entry in newEntries)
                {

                    Entry newEntry = new Entry(entry.days, entry.apps, entry.interval);
                    _entries.Add(newEntry);

                }
                Logger.Log("entries have been read from file");
            }
            else
            {
                Logger.Log("File Not Found...");
            }
        }
        

        public void DisplayEntries()
        {
            for (int i = 0; i < _entries.Count; i++)
            {
                //Console.WriteLine($"Element: {i}\n");
                //Console.WriteLine($"Entry: {this.entries[i].ToString()}");
            }
        }
    }
}