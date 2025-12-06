using static System.Console;
using MySql.Data.MySqlClient;

public class Semester : Connection
{
    // Private fields
    private int semesterId;
    private string year;
    private string term;

    public int SemesterId
    {
        get { return semesterId; }
        set { semesterId = value; }
    }
    public string Year
    {
        get { return year; }
        set { year = value; }
    }
    public string Term
    {
        get { return term; }
        set { term = value; }
    }

    public void AddSemester()
    {
        WriteLine("Enter semester details:");
        Write("Semester ID: ");
        SemesterId = int.Parse(ReadLine());
        Write("Year: ");
        Year = ReadLine();
        Write("Term: ");
        Term = ReadLine();

        using (MySqlConnection connection = OpenConnection())
        {
            string query = @"INSERT INTO semester (semester_id, year, term)
                            VALUES (@semesterId, @year, @term)";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@semesterId", SemesterId);
            cmd.Parameters.AddWithValue("@year", Year);
            cmd.Parameters.AddWithValue("@term", Term);
            cmd.ExecuteNonQuery();
        }
    }

    public void DisplaySemesters()
    {
        using (MySqlConnection connection = OpenConnection())
        {
            string query = "SELECT * FROM semester";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                WriteLine("Semesters:");
                while (reader.Read())
                {
                    int semesterId = reader.GetInt32("semester_id");
                    int year = reader.GetInt32("year");
                    string term = reader.GetString("term");

                    WriteLine($"{semesterId}: {year} {term}");
                }
                WriteLine();
            }
        }
    }

    public void UpdateSemester()
    {
        DisplaySemesters();

        Write("Enter the ID of the semester you want to update: ");
        int semesterIdToUpdate = int.Parse(ReadLine());
        WriteLine("OK! Lets update the semester");

        Write("Enter new year: ");
        string newYear = ReadLine();

        Write("Enter new term: ");
        string newTerm = ReadLine();

        using (MySqlConnection connection = OpenConnection())
        {
            string query = "UPDATE semester SET year = @newYear, term = @newTerm " +
                           "WHERE semester_id = @semesterIdToUpdate";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@newYear", newYear);
            cmd.Parameters.AddWithValue("@newTerm", newTerm);
            cmd.Parameters.AddWithValue("@semesterIdToUpdate", semesterIdToUpdate);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                WriteLine("Semester updated successfully.");
            else
                WriteLine("Failed to update semester.");

        }
    }


}