using static System.Console;
using MySql.Data.MySqlClient;

public class Course : Connection
{
    string courseCode;
    string courseTitle;
    int credits;
    int department;
    int sectionId;

    public string CourseCode
    {
        get { return courseCode; }
        set { courseCode = value; }
    }
    public string CourseTitle
    {
        get { return courseTitle; }
        set { courseTitle = value; }
    }
    public int Credits
    {
        get { return credits; }
        set { credits = value; }
    }
    public int Department
    {
        get { return department; }
        set { department = value; }
    }
    public int SectionId
    {
        get { return sectionId; }
        set { sectionId = value; }
    }
    public void AddCourse()
    {
        WriteLine("Enter the course details:");
        Write("Course Code: ");
        CourseCode = ReadLine();
        Write("Course Title: ");
        CourseTitle = ReadLine();
        Write("Credits: ");
        Credits = int.Parse(ReadLine());
        WriteLine("Department: ");
        WriteLine("\t1 = Computer Science");
        WriteLine("\t2 = Mechanical Engineering");
        WriteLine("\t3 = English");
        WriteLine("\t4 = Mathematics");
        WriteLine("\t5 = Finance");
        WriteLine("\t6 = Marketing");
        Write("\t> ");
        Department = int.Parse(ReadLine());

        using (MySqlConnection connection = OpenConnection())
        {
            string query = @"INSERT INTO course (course_code, course_title, credits, department_id)
                            VALUES (@courseCode, @courseTitle, @credits, @department)";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@courseCode", CourseCode);
            cmd.Parameters.AddWithValue("@courseTitle", CourseTitle);
            cmd.Parameters.AddWithValue("@credits", Credits);
            cmd.Parameters.AddWithValue("@department", Department);

            cmd.ExecuteNonQuery();
        }
    }

    public void DisplayCourses()
    {
        using (MySqlConnection connection = OpenConnection())
        {
            string query = @"SELECT course_code, course_title, credits, dept_name
                            FROM course c
                            INNER JOIN department d on c.department_id = d.department_id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                WriteLine("Courses List:");
                while (reader.Read())
                {
                    string courseCode = reader.GetString("course_code");
                    string courseTitle = reader.GetString("course_title");
                    int credits = reader.GetInt32("credits");
                    string departmentName = reader.GetString("dept_name");

                    WriteLine(courseTitle);
                    WriteLine($"\tCourse Code: {courseCode}");
                    WriteLine($"\tCredits: {credits}");
                    WriteLine($"\tDepartment: {departmentName}");
                    WriteLine();
                }
            }
        }
    }

    private void DisplayCourseCodeAndName()
    {
        using (MySqlConnection connection = OpenConnection())
        {
            string query = "SELECT course_code, course_title FROM course";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                WriteLine("Courses List:");
                while (reader.Read())
                {
                    string courseCode = reader.GetString("course_code");
                    string courseTitle = reader.GetString("course_title");
                    WriteLine($"{courseCode} {courseTitle}");
                }
                WriteLine();
            }
        }

    }
    public void UpdateCourse()
    {
        DisplayCourseCodeAndName();
        
        Write("Enter the \"code\" of the course you want to update: ");
        string courseToUpdate = ReadLine();
        WriteLine("OK! Lets update the course");

        Write("Enter the new course code: ");
        string newCourseCode = ReadLine();
        Write("Enter the new course title: ");
        string newCourseTitle = ReadLine();
        Write("Enter the new course credits: ");
        int newCourseCredits = int.Parse(ReadLine());
        WriteLine("Enter the ID fo the new course Department: ");
        WriteLine("\t1 = Computer Science");
        WriteLine("\t2 = Mechanical Engineering");
        WriteLine("\t3 = English");
        WriteLine("\t4 = Mathematics");
        WriteLine("\t5 = Finance");
        WriteLine("\t6 = Marketing");
         Write("\t> ");
        int newDepartmentId = int.Parse(ReadLine());

        using (MySqlConnection connection = OpenConnection())
        {
            string query = @"UPDATE course SET course_code = @newCourseCode, course_title = @newCourseTitle,
                            credits = @newCourseCredits, department_id = @newDepartmentId
                            WHERE course_code = @courseToUpdate";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@newCourseCode", newCourseCode);
            cmd.Parameters.AddWithValue("@newCourseTitle", newCourseTitle);
            cmd.Parameters.AddWithValue("@newCourseCredits", newCourseCredits);
            cmd.Parameters.AddWithValue("@newDepartmentId", newDepartmentId);
            cmd.Parameters.AddWithValue("@courseToUpdate", courseToUpdate);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                WriteLine("Course updated successfully.");
            else
                WriteLine("Failed to update course.");
        }
    }

    public void DeleteCourse()
    {
        DisplayCourseCodeAndName();

        Write("Enter the \"code\" of the course you want to delete: ");
        string courseToDelete = ReadLine();

        using (MySqlConnection connection = OpenConnection())
        {
            string query = "DELETE FROM course WHERE course_code = @courseToDelete";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@courseToDelete", courseToDelete);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                WriteLine("Course deleted successfully.");
            else
                WriteLine("No course found with the provided course code.");
        }
    }
}