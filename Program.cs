using System;
using System.Collections.Generic;
using System.IO;

namespace Dagboksappen
{
    class Program
    {
        static void Main(string[] args)
        {
            DiaryManager manager = new DiaryManager();
            string filePath = "C:\\Users\\Marik\\source\\repos\\Dagbok\\TextFile\\diary.txt";

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
                    Console.WriteLine("6. Avsluta");
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

                        case "4": // save to file
                            try
                            {
                                using (StreamWriter writer = new StreamWriter(filePath, append: false))
                                {
                                    foreach (var entry in manager.GetEntries())
                                    {
                                        writer.WriteLine($"{entry.Date:yyyy-MM-dd} - {entry.Text}");
                                    }
                                }
                                Console.WriteLine("Alla anteckningar har sparats till fil!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Fel vid sparning av fil: {ex.Message}");
                            }
                            break;

                        case "5":
                            try
                            {
                                if (File.Exists(filePath))
                                {
                                    string[] lines = File.ReadAllLines(filePath);

                                    if (lines.Length > 0)
                                    {
                                        Console.WriteLine("Alla anteckningar från fil:");
                                        foreach (string line in lines)
                                        {
                                            Console.WriteLine(line);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Filen är tom.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ingen dagboksfil hittades.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Fel vid läsning av fil: {ex.Message}");
                            }
                            break;

                        case "6":
                            return;

                        default:
                            Console.WriteLine("Ogiltigt val, försök igen.");
                            break;
                    }

                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ett oväntat fel inträffade: {ex.Message}");
                }
            }
        }
    }
}
