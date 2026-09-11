using Microsoft.Data.Sqlite;
using Spectre.Console;
using System.Globalization;

namespace HabitLogger.elhan3
{
    internal class DatabaseManager
    {
        string connectionString = @"Data Source=habit-tracker.db";
        internal void CreateTable()
        {
            using (var connection = new SqliteConnection(connectionString))
            {

                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS running_logger (
                   Id INTEGER PRIMARY KEY AUTOINCREMENT,
                   Date TEXT,
                   Distance INTEGER
                    )";

                tableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        internal void View()
        {
            List<RunningLog> logs = new();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM running_logger";

                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        logs.Add(new RunningLog
                        {
                            Id = reader.GetInt32(0),
                            Date = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", new CultureInfo("en-US")),
                            Distance = reader.GetInt32(2)
                        }); ;
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Table is empty.[/]");
                }

                connection.Close();

                var table = new Table()
                    .Border(TableBorder.Square)
                    .BorderColor(Color.Green)
                    .AddColumns("[yellow]Id[/]", "[yellow]Date[/]", "[yellow]Distance (in meters) [/]");

                foreach (var log in logs)
                {
                    table.AddRow(log.Id.ToString(), log.Date.ToString(), log.Distance.ToString());
                }

                AnsiConsole.Write(table);


            }
        }

        internal void Insert(string date, int distance)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO running_logger(Date, Distance) VALUES ($date, $distance)";
                command.Parameters.AddWithValue("$date", date);
                command.Parameters.AddWithValue("$distance", distance);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        internal void Update(int id, string date, int distance)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var checkCommand = connection.CreateCommand();
                checkCommand.CommandText = $"SELECT EXISTS (SELECT 1 FROM running_logger WHERE Id = $id)";
                checkCommand.Parameters.AddWithValue("$id", id);

                int checkQuery = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (checkQuery == 0)
                {
                    AnsiConsole.MarkupLine($"[red]Record with id {id} doesn't exist.[/]");
                    connection.Close();
                    return;
                }

                var command = connection.CreateCommand();

                command.CommandText = $"UPDATE running_logger SET Date = $date, Distance = $distance WHERE Id = $id";
                command.Parameters.AddWithValue("$date", date);
                command.Parameters.AddWithValue("$distance", distance);
                command.Parameters.AddWithValue("$id", id);

                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        internal int Delete(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM running_logger WHERE Id = $id";
                command.Parameters.AddWithValue("$id", id);

                int count = command.ExecuteNonQuery();

                connection.Close();
                return count;
            }
        }
    }
}
