using static System.Console;
using MySql.Data.MySqlClient;

public class Grading : Enrollment
{
    private int grade;

    public override void DisplayEnrollments()
    {
        using (MySqlConnection connection = OpenConnection())
        {
            string query = @"SELECT first_name, last_name, c.course_code, c.course_title, credits, examName
                            FROM student s
                                INNER JOIN enrollment en ON s.student_id = en.student_id
                                INNER JOIN section sect ON en.sect_id = sect.sect_id
                                INNER JOIN course c ON sect.course_code = c.course_code
                                INNER JOIN exam e ON c.course_code = e.course_code
                                INNER JOIN semester sem ON sect.semester_id = sem.semester_id
                            WHERE s.student_id = @StudentId
                            ORDER BY s.student_id;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@StudentId", StudentId);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                WriteLine("Enrolments: ");
                string currentStudent = null;
                while (reader.Read())
                {
                    string studentName = $"{reader.GetString("first_name")} {reader.GetString("last_name")}";
                    string courseCode = reader.GetString("course_code");
                    string courseTitle = reader.GetString("course_title");
                    int credits = reader.GetInt32("credits");
                    string examName = reader.GetString("examName");


                    if (studentName != currentStudent)
                    {
                        WriteLine();
                        WriteLine(studentName + " enrolled in ");
                        WriteLine($"\t{courseCode}-{courseTitle}({credits})\t{examName}");
                        currentStudent = studentName;
                    }
                    else
                        WriteLine($"\t{courseCode}-{courseTitle}({credits})\t{examName}");
                }
            }
        }
    }
    public void DisplayGrades()
    {
        using (MySqlConnection connection = OpenConnection())
        {
            string query = @"SELECT s.student_id, first_name, last_name, examName, c.course_code, gradeValue
                            FROM student s
                            INNER JOIN grade g ON s.student_id = g.student_id
                            INNER JOIN course c ON g.course_code = c.course_code
                            INNER JOIN exam e ON c.course_code = e.course_code
                            INNER JOIN enrollment en ON s.student_id = en.student_id
                            INNER JOIN section sect ON en.sect_id = sect.sect_id
                            WHERE en.sect_id IS NOT NULL
                            ORDER BY s.student_id ASC";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                WriteLine("Grades: ");
                string currentStudent = null;
                while (reader.Read())
                {
                    string studentName = $"{reader.GetString("first_name")} {reader.GetString("last_name")}";
                    string courseCode = reader.GetString("course_code");
                    string examName = reader.GetString("examName");
                    grade = reader.GetInt32("gradeValue");
                    int studentId = reader.GetInt32("student_id");

                    if (studentName != currentStudent)
                    {
                        WriteLine();
                        WriteLine(studentId + ": " + studentName);
                        WriteLine($"\t{courseCode} ({examName}) - {grade}");
                        currentStudent = studentName;
                    }
                    else
                        WriteLine($"\t{courseCode} ({examName}) - {grade}");
                }
            }
            WriteLine();
        }
    }

    public void AddGrade()
    {
        Clear();
        string namesQuery = @"SELECT  DISTINCT(s.student_id), first_name, last_name
                            FROM student s
                            INNER JOIN enrollment en ON s.student_id = en.student_id
                            WHERE sect_id IS NOT NULL";
        DisplayStudents(namesQuery);

        Write("Enter the ID of the student: ");
        StudentId = int.Parse(ReadLine());

        Clear();
        DisplayEnrollments();

        Write("Enter the course code: ");
        string courseCode = ReadLine();

        Write("Enter the exam name: ");
        string examName = ReadLine();

        Write("Enter the grade: ");
        int gradeValue = int.Parse(ReadLine());

        using (MySqlConnection connection = OpenConnection())
        {
            string gradeQuery = @"INSERT INTO grade (student_id, course_code, gradeValue)
                                VALUES (@studentId, @courseCode, @gradeValue)";
            MySqlCommand gradeCmd = new MySqlCommand(gradeQuery, connection);
            gradeCmd.Parameters.AddWithValue("@studentId", StudentId);
            gradeCmd.Parameters.AddWithValue("@courseCode", courseCode);
            gradeCmd.Parameters.AddWithValue("@gradeValue", gradeValue);

            int gradeRowsAffected = gradeCmd.ExecuteNonQuery();

            string examQuery = @"INSERT INTO exam (examName, course_code)
                               VALUES (@examName, @courseCode)";
            MySqlCommand examCmd = new MySqlCommand(examQuery, connection);
            examCmd.Parameters.AddWithValue("@examName", examName);
            examCmd.Parameters.AddWithValue("@courseCode", courseCode);

            int examRowsAffected = examCmd.ExecuteNonQuery();

            if (gradeRowsAffected > 0 && examRowsAffected > 0)
                WriteLine("Grade and Exam added successfully.");
            else
                WriteLine("Failed to add Grade and Exam.");
        }

    }

    public void UpdateGrade()
    {
        Clear();
        DisplayGrades();

        Write("Enter the ID of the student: ");
        int studentId = int.Parse(ReadLine());

        Write("Enter the course code: ");
        string courseCode = ReadLine();

        Write("Enter the exam name: ");
        string examName = ReadLine();

        Write("Enter the new grade: ");
        int newGradeValue = int.Parse(ReadLine());

        using (MySqlConnection connection = OpenConnection())
        {
            string query = @"UPDATE grade g
                            INNER JOIN course c ON g.course_code = c.course_code
                            INNER JOIN exam e ON c.course_code = e.course_code
                            SET g.gradeValue = @newGradeValue
                            WHERE g.student_id = @studentId AND e.examName = @examName AND c.course_code = @courseCode";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@newGradeValue", newGradeValue);
            cmd.Parameters.AddWithValue("@studentId", studentId);
            cmd.Parameters.AddWithValue("@examName", examName);
            cmd.Parameters.AddWithValue("@courseCode", courseCode);

            Clear();
            Thread.Sleep(1000);
            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
                WriteLine("Grade updated successfully.\n");
            else
                WriteLine("Failed to update grade.\n");
        }

    }

    public void DeleteGrade()
    {
        Clear();
        DisplayGrades();

        Write("Enter the ID of the student: ");
        int studentId = int.Parse(ReadLine());

        Write("Enter the course code: ");
        string courseCode = ReadLine();

        Write("Enter the exam name: ");
        string examName = ReadLine();

        using (MySqlConnection connection = OpenConnection())
        {
            string query = @"DELETE g FROM grade g
                    INNER JOIN course c ON g.course_code = c.course_code
                    INNER JOIN exam e ON c.course_code = e.course_code
                    WHERE g.student_id = @studentId AND e.examName = @examName AND c.course_code = @courseCode";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@studentId", studentId);
            cmd.Parameters.AddWithValue("@examName", examName);
            cmd.Parameters.AddWithValue("@courseCode", courseCode);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
                WriteLine("Grade deleted successfully.");
            else
                WriteLine("Failed to delete grade.");
        }

    }

}
