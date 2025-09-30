using System;
using System.Collections.Generic;

namespace Dagboksappen
{
    public class DiaryManager
    {
        private readonly List<DiaryEntry> entries = new List<DiaryEntry>();

        public void AddEntry(DiaryEntry entry)
        {
            entries.Add(entry);
        }

        public void ListEntries()
        {
            if (entries.Count == 0)
            {
                Console.WriteLine("Inga anteckningar finns.");
                return;
            }

            for (int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {entries[i]}");
            }
        }

        public DiaryEntry? SearchByDate(DateTime date)
        {
            return entries.Find(e => e.Date.Date == date.Date);
        }

        public bool UpdateEntry(int index, string newText)
        {
            if (index < 0 || index >= entries.Count)
                return false;

            entries[index].Text = newText;
            return true;
        }

        public bool RemoveEntry(int index)
        {
            if (index < 0 || index >= entries.Count)
                return false;

            entries.RemoveAt(index);
            return true;
        }

        public List<DiaryEntry> GetEntries()
        {
            return entries;
        }
    }
}
