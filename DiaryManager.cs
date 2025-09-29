namespace Dagboksappen
{
    // DiaryManager class
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
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }

        public DiaryEntry? SearchByDate(DateTime date)
        {
            return entries.Find(e => e.Date.Date == date.Date);
        }
    }
}
