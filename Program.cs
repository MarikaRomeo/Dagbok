using System;

namespace Dagboksappen
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\Marik\\source\\repos\\Dagbok\\TextFile\\diary.txt";
            FileService fileService = new FileService(filePath);
            DiaryManager manager = new DiaryManager();

            while (true)
            {
                try
                {
                    Console.WriteLine("-Dagboksapp-");
                    Console.WriteLine("1. Skriv ny anteckning");
                    Console.WriteLine("2. Lista alla anteckningar");
                    Console.WriteLine("3. Sök anteckning på datum");
                    Console.WriteLine("4. Spara till fil");
                    Console.WriteLine("5. Läs från fil");
                    Console.WriteLine("6. Uppdatera anteckning");
                    Console.WriteLine("7. Ta bort anteckning");
                    Console.WriteLine("8. Avsluta");
                    Console.Write("Välj ett alternativ: ");

                    string? choice = Console.ReadLine();
                    Console.WriteLine();

                    switch (choice)
                    {
                        case "1":
                            DateTime date = DateTime.Now;
                            Console.Write("Skriv anteckning: ");
                            string? inputText = Console.ReadLine();

                            if (!string.IsNullOrWhiteSpace(inputText))
                            {
                                manager.AddEntry(new DiaryEntry(date, inputText));
                                Console.WriteLine("Anteckning tillagd!");
                            }
                            else
                            {
                                Console.WriteLine("Tom text kan inte sparas.");
                            }
                            break;

                        case "2":
                            manager.ListEntries();
                            break;

                        case "3":
                            Console.Write("Skriv datum att söka (yyyy-mm-dd): ");
                            string? searchDate = Console.ReadLine();
                            if (DateTime.TryParse(searchDate, out DateTime sDate))
                            {
                                var entry = manager.SearchByDate(sDate);
                                if (entry != null)
                                    Console.WriteLine(entry);
                                else
                                    Console.WriteLine("Ingen anteckning hittades för detta datum.");
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt datum.");
                            }
                            break;

                        case "4":
                            try
                            {
                                fileService.SaveEntries(manager.GetEntries());
                                Console.WriteLine("Alla anteckningar har sparats till fil!");
                            }
                            catch
                            {
                                Console.WriteLine("Kunde inte spara till fil. Se error.log.");
                            }
                            break;

                        case "5":
                            try
                            {
                                var lines = fileService.LoadEntries();
                                if (lines.Count > 0)
                                {
                                    Console.WriteLine("Alla anteckningar från fil:");
                                    foreach (var line in lines)
                                    {
                                        Console.WriteLine(line);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Filen är tom eller saknas.");
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Kunde inte läsa från fil. Se error.log.");
                            }
                            break;

                        case "6":
                            manager.ListEntries();
                            Console.Write("Vilken anteckning vill du uppdatera (ange nummer)? ");
                            if (int.TryParse(Console.ReadLine(), out int updateIndex))
                            {
                                Console.Write("Ny text: ");
                                string? newText = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(newText) && manager.UpdateEntry(updateIndex - 1, newText))
                                {
                                    Console.WriteLine("Anteckning uppdaterad!");
                                }
                                else
                                {
                                    Console.WriteLine("Misslyckades med att uppdatera anteckning.");
                                }
                            }
                            break;

                        case "7":
                            manager.ListEntries();
                            Console.Write("Vilken anteckning vill du ta bort (ange nummer)? ");
                            if (int.TryParse(Console.ReadLine(), out int removeIndex))
                            {
                                if (manager.RemoveEntry(removeIndex - 1))
                                    Console.WriteLine("Anteckning borttagen!");
                                else
                                    Console.WriteLine("Misslyckades med att ta bort anteckning.");
                            }
                            break;

                        case "8":
                            return;

                        default:
                            Console.WriteLine("Ogiltigt val, försök igen.");
                            break;
                    }

                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    fileService.LogError($"Ov. fel: {ex.Message}");
                    Console.WriteLine("Ett oväntat fel inträffade. Se error.log för detaljer.");
                }
            }
        }
    }
}
