namespace Dagboksappen
{
    public class DiaryEntry
    {
        public DateTime Date { get; set; }
        public string Text { get; set; }

        public DiaryEntry(DateTime date, string text)
        {
            Date = date;
            Text = text;
        }

        public override string ToString()
        {
            return $"{Date:yyyy-MM-dd}: {Text}";
        }
    }
}
