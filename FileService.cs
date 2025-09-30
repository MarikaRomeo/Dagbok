using System;
using System.Collections.Generic;
using System.IO;

namespace Dagboksappen
{
    public class FileService
    {
        private readonly string diaryPath;
        private readonly string errorLogPath;

        public FileService(string diaryPath, string errorLogPath = "error.log")
        {
            this.diaryPath = diaryPath;
            this.errorLogPath = errorLogPath;
        }

        public void SaveEntries(List<DiaryEntry> entries)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(diaryPath, append: false))
                {
                    foreach (var entry in entries)
                    {
                        writer.WriteLine($"{entry.Date:yyyy-MM-dd} - {entry.Text}");
                    }
                }
            }
            catch (Exception ex)
            {
                LogError($"Fel vid sparning: {ex.Message}");
                throw;
            }
        }

        public List<string> LoadEntries()
        {
            try
            {
                if (File.Exists(diaryPath))
                {
                    return new List<string>(File.ReadAllLines(diaryPath));
                }
                return new List<string>();
            }
            catch (Exception ex)
            {
                LogError($"Fel vid läsning: {ex.Message}");
                throw;
            }
        }

        public void LogError(string message)
        {
            try
            {
                File.AppendAllText(errorLogPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}");
            }
            catch
            {
                Console.WriteLine(message);
            }
        }
    }
}
