using static System.Console;
using MySql.Data.MySqlClient;

public class Faculty : Connection
{
    // Private fields
    private int facultyId;
    private string firstName;
    private string lastName;

    public int FacultyId
    {
        get { return facultyId; }
        set { facultyId = value; }
    }
    public string FirstName
    {
        get { return firstName; }
        set { firstName = value; }
    }
    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }
    
    public void AddFaculty()
    {
        WriteLine("Enter faculty details:");
        Write("Faculty ID: ");
        FacultyId = int.Parse(ReadLine());
        Write("First Name: ");
        FirstName = ReadLine();
        Write("Last Name: ");
        LastName = ReadLine();

        using (MySqlConnection connection = OpenConnection())
        {
            string query = @"INSERT INTO faculty (faculty_id, faculty_fname, faculty_lname) 
                            VALUES (@facultyId, @firstName, @lastName)";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@facultyId", FacultyId);
            cmd.Parameters.AddWithValue("@firstName", FirstName);
            cmd.Parameters.AddWithValue("@lastName", LastName);
            cmd.ExecuteNonQuery();
        }
    }

    public void DisplayFaculties()
    {
        using (MySqlConnection connection = OpenConnection())
        {
            string query = "SELECT * FROM faculty";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                WriteLine("Student List:");
                while (reader.Read())
                {
                    int facultyId = reader.GetInt32("faculty_id");
                    string firstName = reader.GetString("faculty_fname");
                    string lastName = reader.GetString("faculty_lname");

                    WriteLine($"{facultyId}: {firstName} {lastName}");
                }
                WriteLine();
            }
        }
    }

    public void UpdateFaculty()
    {
        DisplayFaculties();

        Write("Enter the ID of the faculty you want to update: ");
        int facultyIdToUpdate = int.Parse(ReadLine());
        WriteLine("OK! Lets update the faculty");
        
        Write("Enter new first name: ");
        string newFirstName = ReadLine();

        Write("Enter new last name: ");
        string newLastName = ReadLine();

        using (MySqlConnection connection = OpenConnection())
        {
            string query = @"UPDATE faculty SET faculty_fname = @newFirstName, faculty_lname = @newLastName
                            WHERE faculty_id = @facultyIdToUpdate";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@newFirstName", newFirstName);
            cmd.Parameters.AddWithValue("@newLastName", newLastName);
            cmd.Parameters.AddWithValue("@facultyIdToUpdate", facultyIdToUpdate);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                WriteLine("Faculty updated successfully.");
            else
                WriteLine("Failed to update faculty.");
            
        }
    }


}