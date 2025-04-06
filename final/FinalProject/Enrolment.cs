using static System.Console;
using MySql.Data.MySqlClient;

public class Enrollment : Student
{
    public virtual void DisplayEnrollments()
    {
        using (MySqlConnection connection = OpenConnection())
        {
            string query = @"SELECT first_name, last_name, c.course_code, c.course_title, credits, sect.section_no, sem.term, sem.year
                            FROM student s
                            INNER JOIN enrollment en ON s.student_id = en.student_id
                            INNER JOIN section sect ON en.sect_id = sect.sect_id
                            INNER JOIN course c ON sect.course_code = c.course_code 
                            INNER JOIN semester sem ON sect.semester_id = sem.semester_id
                            ORDER BY s.student_id;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
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
                    int sectionNumber = reader.GetInt32("section_no");
                    string term = reader.GetString("term");
                    int year = reader.GetInt32("year");

                    if (studentName != currentStudent)
                    {
                        WriteLine();
                        WriteLine(studentName + " enrolled in ");
                        WriteLine($"\t{courseCode}-{courseTitle}({credits}): section({sectionNumber}) {term} {year}");
                        currentStudent = studentName;
                    }
                    else
                        WriteLine($"\t{courseCode}-{courseTitle}({credits}): section({sectionNumber}) {term} {year}");
                }
            }
        }
    }

    public  void DisplayCourses()
    {
        using (MySqlConnection connection = OpenConnection())
        {
            string query = "SELECT sect_id, c.course_code, credits, faculty_lname, faculty_fname, term, year " +
                            "FROM section s " +
                                "INNER JOIN semester sem ON s.semester_id = sem.semester_id " +
                                "INNER JOIN faculty f ON s.faculty_id = f.faculty_id " +
                                "INNER JOIN course c ON c.course_code = s.course_code " +
                            "ORDER BY sect_id ASC; ";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                WriteLine("Choose from the following courses: ");
                while (reader.Read())
                {
                    int sectId = reader.GetInt32("sect_id");
                    string courseCode = reader.GetString("course_code");
                    int credits = reader.GetInt32("credits");
                    string facultyName = $"{reader.GetString("faculty_lname")}, {reader.GetString("faculty_fname")}";
                    string term = reader.GetString("term");
                    int year = reader.GetInt32("year");

                    WriteLine($"{sectId}: {courseCode}({credits}) - {facultyName} - {term}, {year}");
                }
            }
        }
    }
    public void EnrollStudent()
    {
        int studentToEnroll;
        int courseToEnroll;
        string query = "SELECT student_id, first_name, last_name FROM student";
        DisplayStudents(query);
        Write("Which student do you want to enroll: ");
        studentToEnroll = int.Parse(ReadLine());

        WriteLine();
        WriteLine("Here is the list of courses; ");
        DisplayCourses();
        WriteLine();
        Write("Type the number of the course you want to enroll the student: ");
        courseToEnroll = int.Parse(ReadLine());

        using (MySqlConnection connection = OpenConnection())
        {
            string checkQuery = "SELECT COUNT(*) FROM enrollment WHERE student_id = @studentId AND sect_id = @sectionId";
            MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection);
            checkCmd.Parameters.AddWithValue("@studentId", studentToEnroll);
            checkCmd.Parameters.AddWithValue("@sectionId", courseToEnroll);
            int existingEnrollmentsCount = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (existingEnrollmentsCount > 0)
            {
                WriteLine("Student is already enrolled in the selected course.");
                return;
            }
            query = "INSERT INTO enrollment (student_id, sect_id) " +
                            "VALUES (@studnetToEnroll, @courseToEnroll)";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@studnetToEnroll", studentToEnroll);
            cmd.Parameters.AddWithValue("@courseToEnroll", courseToEnroll);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
                WriteLine("Student enrolled successfully.");
            else
                WriteLine("Failed to enroll the student.");
        }
    }
    public void WithdrawStudent()
    {
        int studentToWithdraw;
        string query = "SELECT DISTINCT(first_name), last_name " +
                        "FROM student s " +
                        "INNER JOIN enrollment en ON s.student_id = en.student_id " +
                        "WHERE sect_id IS NOT NULL";

        DisplayStudents(query);
        Write("Which student do you want to withdraw: ");
        studentToWithdraw = int.Parse(ReadLine());

        using (MySqlConnection connection = OpenConnection())
        {
            query = "SELECT c.course_code, c.course_title, credits, sect.sect_id " +
                            "FROM student s " +
                                "INNER JOIN enrollment en ON s.student_id = en.student_id " +
                                "INNER JOIN section sect ON en.sect_id = sect.sect_id " +
                                "INNER JOIN course c ON sect.course_code = c.course_code " +
                            "WHERE s.student_id = @studentToWithdraw";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@studentToWithdraw", studentToWithdraw);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                List<Course> courses = new List<Course>();
                WriteLine("The student is enrolled in the following course(s): ");
                int count = 0;
                while (reader.Read())
                {
                    string courseCode = reader.GetString("course_code");
                    string courseTitle = reader.GetString("course_title");
                    int credits = reader.GetInt16("credits");
                    int sectionId = reader.GetInt32("sect_id");

                    Course course = new Course
                    {
                        CourseCode = courseCode,
                        CourseTitle = courseTitle,
                        Credits = credits,
                        SectionId = sectionId
                    };
                    courses.Add(course);
                    WriteLine($"{++count}: {courseCode} - {courseTitle}({credits})");
                }
                WriteLine();
                Write("Enter the index of the course you want to withdraw from (comma-separated for multiple enrollments): ");
                string[] selectedCoursesIndices = ReadLine().Split(',');
                foreach (string index in selectedCoursesIndices)
                {
                    int selectedIndex = int.Parse(index) - 1;
                    Course selectedCourse = courses[selectedIndex];
                    WithdrawStudentFromCourse(studentToWithdraw, selectedCourse.SectionId);
                }
            }
        }
    }
    private void WithdrawStudentFromCourse(int studentId, int sectionId)
    {
        using (MySqlConnection connection = OpenConnection())
        {
            string query = "DELETE FROM enrollment WHERE student_id = @studentId AND sect_id = @sectionId";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@studentId", studentId);
            cmd.Parameters.AddWithValue("@sectionId", sectionId);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
                WriteLine("Student withdrawn from the course successfully.");
            else
                WriteLine("Failed to withdraw student from the course.");
        }
    }
}