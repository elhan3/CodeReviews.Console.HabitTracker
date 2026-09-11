using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HabitLogger.elhan3
{
    internal class HelperFunctions
    {
        public static string GetInputDate()
        {
            var date = AnsiConsole.Ask<string>("Enter the [green]date[/] of the activity in format [i]dd-mm-yy[/] (c - current date, 0 - Main Menu).");
            while (!DateTime.TryParseExact(date, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _) && date != "0")
            {
                if (date == "c")
                {
                    return DateTime.Now.ToString("dd-MM-yy");
                }
                date = AnsiConsole.Ask<string>("[red]Invalid date.[/]Please insert the [green]date[/] in format [b]dd-MM-yy[/] (c - current date, 0 - Main Menu):");
            }
            return date;
        }

        public static int GetInputDistance()
        {
            int distance = AnsiConsole.Ask<int>("Enter the [green]distance[/] (in meters) or press 0 to return to the Main Menu.");
            while (distance < 0)
            {
                distance = AnsiConsole.Ask<int>("[red]Incorrect distacne.[/] Please insert the [green]distance[/] (in meters) or press 0 to return to the Main Menu.");
            }
            return distance;
        }
    }
}
