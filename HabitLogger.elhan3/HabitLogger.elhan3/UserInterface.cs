using Spectre.Console;

namespace HabitLogger.elhan3
{
    internal class UserInterface
    {
        DatabaseManager Database = new DatabaseManager();
        internal void MainMenu()
        {
            Database.CreateTable();

            while (true)
            {
                Console.Clear();
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<MenuOptions>()
                    .Title("What do you want to do next?")
                    .AddChoices(Enum.GetValues<MenuOptions>()));

                string date;
                int distance, id;

                switch (choice)
                {
                    case MenuOptions.Insert:
                        Console.Clear();
                        date = HelperFunctions.GetInputDate();
                        if (date == "0")
                        {
                            continue;
                        }

                        distance = HelperFunctions.GetInputDistance();
                        if (distance == 0)
                        {
                            continue;
                        }
                        Database.Insert(date, distance);
                        AnsiConsole.MarkupLine("Press Any Key to return to the Main Menu.");
                        Console.ReadKey();
                        break;

                    case MenuOptions.View:
                        Console.Clear();

                        Database.View();

                        AnsiConsole.MarkupLine("Press Any Key to return to the Main Menu.");
                        Console.ReadKey();
                        break;

                    case MenuOptions.Update:
                        Console.Clear();
                        id = AnsiConsole.Ask<int>("Enter the [green]ID[/] of activity that you want to update or press 0 if you want to go back to the Main Menu.");
                        if (id == 0)
                        {
                            continue;
                        }
                        date = HelperFunctions.GetInputDate();
                        if (date == "0")
                        {
                            continue;
                        }

                        distance = HelperFunctions.GetInputDistance();
                        if (distance == 0)
                        {
                            continue;
                        }
                        Database.Update(id, date, distance);
                        AnsiConsole.MarkupLine("Press Any Key to return to the Main Menu.");
                        Console.ReadKey();
                        break;

                    case MenuOptions.Delete:
                        Console.Clear();

                        id = AnsiConsole.Ask<int>("Type the Id of the record you want to delete.");
                        int count = Database.Delete(id);
                        if (count == 0)
                        {
                            AnsiConsole.MarkupLine($"[red]Entry with Id {id} doesn't exist.[/]");
                        }
                        AnsiConsole.MarkupLine("Press Any Key to return to the Main Menu.");
                        Console.ReadKey();
                        break;

                    case MenuOptions.Exit:
                        AnsiConsole.MarkupLine("[yellow]Exiting application...[/]");
                        return;
                }
            }
        }
    }
}
