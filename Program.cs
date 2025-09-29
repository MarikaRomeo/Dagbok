using System;
using System.Collections.Generic;

namespace Dagboksappen
{

    class Program
    {
        static void Main(string[] args)
        {
            DiaryManager manager = new DiaryManager();

            while (true)
            {
                Console.WriteLine("-Dagboksapp-");
                Console.WriteLine("1. Skriv ny anteckning");
                Console.WriteLine("2. Lista alla anteckningar");
                Console.WriteLine("3. Sök anteckning på datum");
                Console.WriteLine("4. Avsluta");
                Console.Write("Välj ett alternativ: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Skriv datum (yyyy-mm-dd): ");
                        string? inputDate = Console.ReadLine();
                        Console.Write("Skriv anteckning: ");
                        string? inputText = Console.ReadLine();

                        if (DateTime.TryParse(inputDate, out DateTime date) && !string.IsNullOrWhiteSpace(inputText))
                        {
                            manager.AddEntry(new DiaryEntry(date, inputText));
                            Console.WriteLine("Anteckning tillagd!");
                        }
                        else
                        {
                            Console.WriteLine("Ogiltig datum eller tom text.");
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
                            Console.WriteLine("Ogiltig datum.");
                        }
                        break;

                    case "4":
                        return;

                    default:
                        Console.WriteLine("Ogiltigt val, försök igen.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}
