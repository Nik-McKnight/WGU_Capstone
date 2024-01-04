using SQLite;
using C971.Models;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace C971.Data;

public class C971Database
{
    public SQLiteConnection Database;
    public const string DatabaseFilename = "C971.db3";
    public const SQLite.SQLiteOpenFlags Flags =
    // open the database in read/write mode
    SQLite.SQLiteOpenFlags.ReadWrite |
    // create the database if it doesn't exist
    SQLite.SQLiteOpenFlags.Create |
    // enable multi-threaded database access
    SQLite.SQLiteOpenFlags.SharedCache;

    //public string DatabaseFilename = "C971.db3";
    public string DatabasePath { get; set; }
    //public C971Database(string databaseFilename = "C971.db3")
    //{
    //    DatabaseFilename=databaseFilename;
    //}
    public C971Database(string databaseFilename = "C971.db3")
    {
        string DatabaseFilename = databaseFilename;
        if (DatabaseFilename=="C971.db3")
        {
            try
            {
                DatabasePath=Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
            }
            catch { }
        }
        else
        {
            DatabasePath=DatabaseFilename;
        }
    }

    public void Init()
    {
        if (Database is not null)
            return;


        //Database = new SQLiteConnection("Data Source=" + DatabaseFilename);
        Database = new SQLiteConnection(DatabasePath);
        //var result = Database.CreateTable<Assessment>();
        CreateTables();
    }

    public void Close()
    {
        if (Database is not null)
        {
            Database.Close();
        }
    }

    public int CreateTables()
    {
        try
        {
            Database.CreateTable<User>();
            Database.CreateTable<Term>();
            Database.CreateTable<Course>();
            Database.CreateTable<Assessment>();
            Database.CreateTable<Note>();
            Database.CreateTable<CourseUser>();
            Database.CreateTable<TermCourse>();
            return 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }

    public int ClearTables()
    {
        try
        {

            Database.Execute("Drop table if exists Term;");
            Database.Execute("Drop table if exists Course;");
            Database.Execute("Drop table if exists Assessment;");
            Database.Execute("Drop table if exists Note;");
            Database.Execute("Drop table if exists User;");
            Database.Execute("Drop table if exists CourseUser;");
            Database.Execute("Drop table if exists TermCourse;");
            return 1;
        }
        catch
        {
            return 0;
        }
    }

    public int SeedData()
    {
        Init();
        ClearTables();
        CreateTables();
        try
        { 
            DateTime Start1 = DateTime.Parse("9/1/2023 12:00:00 AM");
            DateTime Start2 = DateTime.Parse("1/1/2024 12:00:00 AM");
            DateTime End1 = DateTime.Parse("12/15/2023 12:00:00 AM");
            DateTime End2 = DateTime.Parse("5/1/2024 12:00:00 AM");
            DateTime EndA11 = DateTime.Parse("11/1/2023 12:00:00 AM");
            DateTime EndA21 = DateTime.Parse("3/1/2024 12:00:00 AM");
            User Instructor = new User(13, "i", "i", "anika.patel@strimeuniversity.edu", "555-123-4567",
                                       "Anika", "Patel", false, true);
            User Admin = new User(14, "a", "a", "admin@strimeuniversity.edu", "555-123-4567", "Ad", "Min", true, false);
            List<User> students = new List<User>();
            for (int i = 1; i< 13; i++)
            {
                string temp = i.ToString();
                string phone = i  + i + i + "-" + i  + i + i + "-" + i  + i + i + i;
                User u = new User(i, temp, temp, temp, phone, temp, temp, false, false);
                students.Add(u);
            }

            List<Course> courses = new List<Course>
            {
                new Course(1, "Gym", Start1, End1, Instructor.ID),
                new Course(2, "Math", Start1, End1, Instructor.ID),
                new Course(3, "Science", Start1, End1, Instructor.ID),
                new Course(4, "Business", Start1, End1, Instructor.ID),
                new Course(5, "Accounting", Start1, End1, Instructor.ID),
                new Course(6, "Underwater Basketweaving", Start1, End1, Instructor.ID),
                new Course(7, "Gym", Start2, End2, Instructor.ID),
                new Course(8, "Math", Start2, End2, Instructor.ID),
                new Course(9, "Science", Start2, End2, Instructor.ID),
                new Course(10, "Business", Start2, End2, Instructor.ID),
                new Course(11, "Accounting", Start2, End2, Instructor.ID),
                new Course(12, "Underwater Basketweaving", Start2, End2, Instructor.ID)
            };

            foreach (Course c in courses)
            {
                InsertCourse(c);
                foreach (User u in students)
                {
                    InsertCourseUser(new CourseUser(0, c.ID, u.ID));
                }
            }

            InsertCourse(new Course(13, "Skydiving", Start1, End1, Instructor.ID));

            foreach (User u in students)
            {
                InsertUser(u);
                InsertTerm(new Term(0, "Fall 2023", Start1, End1, u.ID));
                InsertTerm(new Term(0, "Spring 2024", Start2, End2, u.ID));
                int ID = GetTerms().Count;
                for (int  i = 1; i < 7; i++)
                {
                    InsertTermCourse(new TermCourse(0, i, ID-1));
                    InsertTermCourse(new TermCourse(0, i+6, ID));
                }
            }

            List<CourseUser> CUList = GetCourseUser();

            foreach (CourseUser cu in CUList)
            {
                InsertAssessment(new Assessment(0, "Assessment 1", Start1, EndA11, cu.CourseId, Category.Objective, Instructor.ID, cu.StudentId));
                InsertAssessment(new Assessment(0, "Assessment 2", EndA11, End1, cu.CourseId, Category.Practical, Instructor.ID, cu.StudentId));
                InsertNote(new Note(0, "Test Note"  + cu.CourseId, cu.CourseId, cu.StudentId));
            }

            InsertUser(Instructor);
            InsertUser(Admin);


            return 1;
        }
        catch 
        {
            return 0;
        }
    }

    //===========================================================================================================
    // Term

    public int InsertTerm(Term a)
    {
        Init();
        return Database.Insert(a);
    }

    public List<Term> GetTerms(int UserId = 0)
    {
        Init();
        if (UserId == 0)
        {
            return Database.Table<Term>().ToList();
        }
        else return Database.Table<Term>().Where(i => i.UserId == UserId).ToList();
    }

    public Term GetTerm(int id)
    {
        Init();
        return Database.Table<Term>().Where(i => i.ID == id).FirstOrDefault(); ;
    }

    public int UpdateTerm(Term term)
    {
        Init();
        if (term.ID != 0)
        {
            return Database.Update(term);
        }
        else
        {
            return Database.Insert(term);
        }
    }

    public int DeleteTerm(Term term)
    {
        Init();
        return Database.Delete(term);
    }

    public int DeleteTerm(int termId)
    {
        Init();
        Term t = GetTerm(termId);
        return Database.Delete(t);
    }

    //===========================================================================================================
    // Course
    public int InsertCourse(Course a)
    {
        Init();
        return Database.Insert(a);
    }

    public List<Course> GetCourses(int TermId = 0)
    {
        Init();
        if (TermId == 0)
        {
            return Database.Table<Course>().ToList();
        }
        List<TermCourse> TermCourses = GetTermCoursesByTerm(TermId);
        List<Course> courses = new List<Course>();
        foreach (TermCourse tc in TermCourses)
        {
            Course c = GetCourse(tc.CourseId);
            courses.Add(c);
        }
        return courses;
    }

    public List<Course> GetCourses(User Instructor)
    {
        Init();
        return Database.Table<Course>().Where(i => i.InstructorId == Instructor.ID).ToList();
    }

    public Course GetCourse(int id)
    {
        Init();
        return Database.Table<Course>().Where(i => i.ID == id).FirstOrDefault(); ;
    }

    public int UpdateCourse(Course course)
    {
        Init();
        if (course.ID != 0)
        {
            return Database.Update(course);
        }
        else
        {
            return Database.Insert(course);
        }
    }

    public int DeleteCourse(Course course)
    {
        Init();
        return Database.Delete(course);
    }

    public int DeleteCourse(int courseId)
    {
        Init();
        Course c = GetCourse(courseId);
        return Database.Delete(c);
    }

    public List<Course> SearchCourses(string search = null, DateTime? start = null, DateTime? end = null, int userId = 0)
    {
        Init();
        List<Course> courses = new List<Course>();
        List<Course> result;
        DateTime? Start = start;
        DateTime? End = end;
        if (start == null)
        {
            Start = DateTime.MinValue;
        }
        if (end == null)
        {
            End = DateTime.MaxValue;
        }
        if (search != null && search != "")
        {
            result = Database.Table<Course>().Where(i => i.Title.Contains(search) &&
                                        i.Start >= Start && End >= i.End).ToList();
        }
        else
        {
            result = Database.Table<Course>().Where(i => i.Start >= Start && End >= i.End).ToList();
        }
        if (userId == 0)
        {
            return result;
        }
        else
        {

            foreach(Course c in result)
            {
                CourseUser cu = GetCourseUser(c.ID, userId);
                if (cu == null)
                {
                    c.dateString();
                    courses.Add(c);
                }
            }
            return courses;
        }
    }
    //===========================================================================================================
    // Assessment

    public int InsertAssessment(Assessment a)
    {
        Init();
        return Database.Insert(a);
    }

    public int InsertAssessment(Assessment a, int courseId)
    {
        Init();
        List<CourseUser> cu = GetCourseUsers(courseId);
        try
        {
            foreach (CourseUser c in cu)
            {
                Assessment a1 = a;
                a1.StudentId = c.StudentId;
                int result = Database.Insert(a1);
                if (result == 0) break;
            }
            return 1;
        }
        catch
        {
            return 0;
        }
    }

    public List<Assessment> GetAssessments()
    {
        Init();
        return Database.Table<Assessment>().ToList();
    }

    public List<Assessment> GetAssessments(int courseId, int StudentId = 0)
    {
        Init();
        if (StudentId == 0)
        {
            return Database.Table<Assessment>().Where(i => i.CourseId == courseId).ToList();
        }
        else
        {
            return Database.Table<Assessment>().Where(i => i.CourseId == courseId && i.StudentId == StudentId).ToList();
        }
    }

    public List<Assessment> GetAssessments(CourseUser cu)
    {
        Init();
        if (cu.StudentId == 0 || cu.CourseId == 0)
        {
            return null;
        }
        else
        {
            return Database.Table<Assessment>().Where(i => i.CourseId == cu.CourseId && i.StudentId == cu.StudentId).ToList();
        }
    }

    public Assessment GetAssessment(int id)
    {
        Init();
        return Database.Table<Assessment>().Where(i => i.ID == id).FirstOrDefault(); ;
    }

    public int UpdateAssessment(Assessment assessment)
    {
        Init();
        if (assessment.ID != 0)
        {
            return Database.Update(assessment);
        }
        else
        {
            return Database.Insert(assessment);
        }
    }

    public int DeleteAssessment(Assessment assessment)
    {
        Init();
        return Database.Delete(assessment);
    }
    public int DeleteAssessment(int assessmentId)
    {
        Init();
        Assessment a = GetAssessment(assessmentId);
        return Database.Delete(a);
    }

    //===========================================================================================================
    // Note


    public int InsertNote(Note a)
    {
        Init();
        return Database.Insert(a);
    }

    public  List<Note> GetNotes()
    {
        Init();
        return Database.Table<Note>().ToList();
    }

    public List<Note> GetNotes(int courseId)
    {
        Init();
        return Database.Table<Note>().Where(i => i.CourseId == courseId).ToList();
    }

    public List<Note> GetNotes(int courseId, int studentId)
    {
        Init();
        return Database.Table<Note>().Where(i => i.CourseId == courseId && i.StudentId == studentId).ToList();
    }

    public Note GetNote(int id)
    {
        Init();
        return Database.Table<Note>().Where(i => i.ID == id).FirstOrDefault(); ;
    }

    public int UpdateNote(Note note)
    {
        Init();
        if (note.ID != 0)
        {
            return Database.Update(note);
        }
        else
        {
            return Database.Insert(note);
        }
    }

    public int DeleteNote(Note note)
    {
        Init();
        return Database.Delete(note);
    }

    public int DeleteNote(int noteId)
    {
        Init();
        Note n = GetNote(noteId);
        return Database.Delete(n);
    }
    //===========================================================================================================
    // User
    public int InsertUser(User u)
    {
        Init();
        if (CheckExisting(u.Username))
        {
            return 0;
        }
        return Database.Insert(u);
    }

    public List<User> GetUsers()
    {
        Init();
        List<User> result = Database.Table<User>().ToList();
        foreach (User u in result)
        {
            u.Password = null;
        }
        return result;
    }

    public List<User> GetUsers(int courseId)
    {
        Init();
        List<CourseUser> CourseUsers = GetCourseUsers(courseId);
        List<User> Output = new List<User>();
        foreach (CourseUser courseUser in CourseUsers)
        {
            User student = GetUser(courseUser.StudentId);
            student.Password = null;
            Output.Add(student);
        }
        return Output;
    }

    public User GetUser(int id)
    {
        Init();
        User result = Database.Table<User>().Where(i => i.ID == id).FirstOrDefault();
        result.Password = null;
        return result;
    }

    public User LoginUser(int id, string password)
    {
        Init();
        try
        {
            password = HashPassword(password);
            User result = Database.Table<User>().Where(i => i.ID == id && i.Password == password).FirstOrDefault();
            result.Password = null;
            return result;
        }
        catch
        {
            return null;
        }

    }

    public string HashPassword(string password)
    {
        //HashAlgorithmName alg = HashAlgorithmName.SHA512;
        //var hash = Rfc2898DeriveBytes.Pbkdf2(
        //    Encoding.UTF8.GetBytes(password),
        //    RandomNumberGenerator.GetBytes(32), 1000, alg, 32);
        //return Convert.ToHexString(hash);
        return password;
    }

    public User LoginUser(string username, string password)
    {
        Init();
        try
        {
            User result = Database.Table<User>().Where(i => i.Username == username && i.Password == password).FirstOrDefault();
            result.Password = null;
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public Boolean CheckExisting(string username, int ID = 0)
    {
        User result;
        if (ID ==0)
        {
            result = Database.Table<User>().Where(i => i.Username == username).FirstOrDefault();
        }
        else
        {
            result = Database.Table<User>().Where(i => i.Username == username && i.ID != ID).FirstOrDefault();
        }
        return result != null;
    }

    public int UpdateUser(User user)
    {
        Init();
        if (CheckExisting(user.Username, user.ID))
        {
            return 0;
        }
        if (user.ID != 0)
        {
            return Database.Update(user);
        }
        else
        {
            return Database.Insert(user);
        }
    }

    public int DeleteUser(User user)
    {
        Init();
        try
        {
            List<CourseUser> courseUsers = GetCourseUsers(0, user.ID);
            foreach (CourseUser cu in courseUsers)
            {
                if (Database.Delete(cu) ==0)
                {
                    return 0;
                }
            }
               return Database.Delete(user);
        }
        catch { 
            return 0; 
        }
    }

    public int UserAddCourse(User u, Course c, Term t)
    {
        try
        {
            InsertCourseUser(new CourseUser(0, c.ID, u.ID));
            InsertTermCourse(new TermCourse(0, c.ID, t.ID));
            return 1;
        }
        catch { 
            return 0; 
        }
    }

    //===========================================================================================================
    // CourseUser
    public int InsertCourseUser(CourseUser cu)
    {
        Init();
        return Database.Insert(cu);
    }

    public List<CourseUser> GetCourseUser()
    {
        Init();
        return Database.Table<CourseUser>().ToList();
    }

    public List<CourseUser> GetCourseUsers(int CourseId=0, int StudentId =0)
    {
        Init();
        if (CourseId == 0 && StudentId ==0)
        {
            return Database.Table<CourseUser>().ToList();
        }
        else if (CourseId != 0 && StudentId ==0)
        {
            return Database.Table<CourseUser>().Where(i => i.CourseId == CourseId).ToList();
        }
        else
        {
            return Database.Table<CourseUser>().Where(i => i.StudentId == StudentId).ToList();
        }
    }

    public CourseUser GetCourseUser(int CourseUserId)
    {
        Init();
        return Database.Table<CourseUser>().Where(i => i.ID == CourseUserId).FirstOrDefault();
    }

    public CourseUser GetCourseUser(int CourseId, int StudentId)
    {
        Init();
        return Database.Table<CourseUser>().Where(i => i.CourseId == CourseId && i.StudentId == StudentId).FirstOrDefault();
    }


    public int DeleteCourseUser(CourseUser cu)
    {
        Init();
        return Database.Delete(cu);
    }
    public int DeleteCourseUser(int CourseUserId)
    {
        Init();
        CourseUser cu = GetCourseUser(CourseUserId);
        return Database.Delete(cu);
    }

    public int DeleteCourseUser(int CourseId, int StudentId)
    {
        Init();
        CourseUser cu = GetCourseUser(CourseId, StudentId);
        return Database.Delete(cu);
    }

    //===========================================================================================================
    // TermCourse
    public int InsertTermCourse(TermCourse tc)
    {
        Init();
        return Database.Insert(tc);
    }

    public List<TermCourse> GetTermCourses()
    {
        Init();
        return Database.Table<TermCourse>().ToList();
    }
    public List<TermCourse> GetTermCoursesByTerm(int TermId)
    {
        Init();
        return Database.Table<TermCourse>().Where(i => i.TermId == TermId).ToList();
    }

    public List<TermCourse> GetTermCoursesByCourse(int CourseId)
    {
        Init();
        return Database.Table<TermCourse>().Where(i => i.CourseId == CourseId).ToList();
    }

    public TermCourse GetTermCourse(int TermCourseId)
    {
        Init();
        return Database.Table<TermCourse>().Where(i => i.ID == TermCourseId).FirstOrDefault();
    }

    public TermCourse GetTermCourse(int CourseId, int TermId)
    {
        Init();
        return Database.Table<TermCourse>().Where(i => i.CourseId == CourseId && i.TermId == TermId).FirstOrDefault();
    }

    public int DeleteTermCourse(TermCourse tc)
    {
        Init();
        return Database.Delete(tc);
    }
    public int DeleteTermCourse(int TermCourseId)
    {
        Init();
        TermCourse tc = GetTermCourse(TermCourseId);
        return Database.Delete(tc);
    }

    public int DeleteTermCourse(int CourseId, int TermId)
    {
        Init();
        TermCourse tc = GetTermCourse(CourseId, TermId);
        return Database.Delete(tc);
    }
}