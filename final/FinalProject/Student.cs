using static System.Console;
using MySql.Data.MySqlClient;

public class Student : Connection
{
    // Private fields
    private int studentId;
    private string firstName;
    private string lastName;
    private string gender;
    private string city;
    private string state;
    private DateTime birthdate;

    public int StudentId
    {
        get { return studentId; }
        set { studentId = value; }
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
    public string Gender
    {
        get { return gender; }
        set { gender = value; }
    }
    public string City
    {
        get { return city; }
        set { city = value; }
    }
    public string State
    {
        get { return state; }
        set { state = value; }
    }
    public DateTime Birthdate
    {
        get { return birthdate; }
        set { birthdate = value; }
    }
    protected void DisplayStudents(string query)
    {
        using (MySqlConnection connection = OpenConnection())
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                WriteLine("List of Students (ID, First Name, Last Name):");
                while (reader.Read())
                {
                    int studentId = reader.GetInt32("student_id");
                    string firstName = reader.GetString("first_name");
                    string lastName = reader.GetString("last_name");
                    WriteLine($"{studentId}: {firstName} {lastName}");
                }
                WriteLine();
            }
        }
    }

    public void AddStudent()
    {
        WriteLine("Enter student details:");
        Write("Student ID: ");
        StudentId = int.Parse(ReadLine());
        Write("First Name: ");
        FirstName = ReadLine();
        Write("Last Name: ");
        LastName = ReadLine();
        Write("Gender (M/F): ");
        Gender = ReadLine().ToUpper();
        Write("City: ");
        City = ReadLine();
        Write("State: ");
        State = ReadLine();
        Write("Birthdate (YYYY-MM-DD): ");
        Birthdate = DateTime.Parse(ReadLine());

        using (MySqlConnection connection = OpenConnection())
        {
            string query = @"INSERT INTO student (student_id, first_name, last_name, gender, city, state, birthdate)
                           VALUES (@studentId, @firstName, @lastName, @gender, @city, @state, @birthdate)";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@studentId", StudentId);
            cmd.Parameters.AddWithValue("@firstName", FirstName);
            cmd.Parameters.AddWithValue("@lastName", LastName);
            cmd.Parameters.AddWithValue("@gender", Gender);
            cmd.Parameters.AddWithValue("@city", City);
            cmd.Parameters.AddWithValue("@state", State);
            cmd.Parameters.AddWithValue("@birthdate", Birthdate);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
                WriteLine("Student added successfully.");
            Clear();
        }
    }

    public void DisplayStudents()
    {
        using (MySqlConnection connection = OpenConnection())
        {
            string query = "SELECT * FROM student";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                WriteLine("Student List:");
                while (reader.Read())
                {
                    int studentId = reader.GetInt32("student_id");
                    string firstName = reader.GetString("first_name");
                    string lastName = reader.GetString("last_name");
                    string gender = reader.GetString("gender");
                    string city = reader.GetString("city");
                    string state = reader.GetString("state");
                    DateTime birthdate = reader.GetDateTime("birthdate");

                    WriteLine($"{studentId}: {firstName} {lastName}");
                    WriteLine($"\tGender: {gender}");
                    WriteLine($"\tCity: {city}");
                    WriteLine($"\tState: {state}");
                    WriteLine($"\tBirthdate: {birthdate.ToShortDateString()}");
                    WriteLine();
                }
                Thread.Sleep(500);
            }
        }
    }

    public void DeleteStudent()
    {
        DisplayStudents();

        Write("Enter the ID of the student you want to delete: ");
        int studentIdToDelete = int.Parse(ReadLine());

        using (MySqlConnection connection = OpenConnection())
        {
            string query = "DELETE FROM student WHERE student_id = @studentIdToDelete";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@studentIdToDelete", studentIdToDelete);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                WriteLine("Student deleted successfully.\n");
            else
                WriteLine("No student found with the provided ID.");
        }
    }

    public void UpdateStudent()
    {
        DisplayStudents();

        Write("Enter the ID of the student you want to update: ");
        int studentIdToUpdate = int.Parse(ReadLine());
        WriteLine("OK! Lets update the student");
        
        Write("Enter new first name: ");
        string newFirstName = ReadLine();

        Write("Enter new last name: ");
        string newLastName = ReadLine();

        Write("Enter new gender (M/F): ");
        string newGender = ReadLine().ToUpper();

        Write("Enter new city: ");
        string newCity = ReadLine();

        Write("Enter new state: ");
        string newState = ReadLine();

        Write("Enter new birthdate (YYYY-MM-DD): ");
        DateTime newBirthdate = DateTime.Parse(ReadLine());

        using (MySqlConnection connection = OpenConnection())
        {
            string query = "UPDATE student SET first_name = @newFirstName, last_name = @newLastName, " +
                           "gender = @newGender, city = @newCity, state = @newState, birthdate = @newBirthdate " +
                           "WHERE student_id = @studentIdToUpdate";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@newFirstName", newFirstName);
            cmd.Parameters.AddWithValue("@newLastName", newLastName);
            cmd.Parameters.AddWithValue("@newGender", newGender);
            cmd.Parameters.AddWithValue("@newCity", newCity);
            cmd.Parameters.AddWithValue("@newState", newState);
            cmd.Parameters.AddWithValue("@newBirthdate", newBirthdate);
            cmd.Parameters.AddWithValue("@studentIdToUpdate", studentIdToUpdate);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                WriteLine("Student updated successfully.\n");
            else
                WriteLine("Failed to update student.");
            
        }
    }

}