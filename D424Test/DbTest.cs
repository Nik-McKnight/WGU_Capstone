
using C971;
using C971.Data;
using C971.Models;
using System.Diagnostics;
using SQLite;

namespace D424Test
{
    [TestClass]
    public class DbTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            Assert.AreEqual(DbConstants.DatabaseFilename, db.DatabasePath);
        }

    //===========================================================================================================
    // Term

        [TestMethod]
        public void TestInsertTerm() {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            Term term = new Term(101,"Title",DateTime.Now, DateTime.Now, 10);
            int result = db.InsertTerm(term);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestGetAllTerms()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Term;");
            db.Database.CreateTable<Term>();
            Term term = new Term("Title", DateTime.Now, DateTime.Now, 10);
            db.InsertTerm(term);
            db.InsertTerm(term);
            db.InsertTerm(term);
            List<Term> result = db.GetTerms();
            Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void TestGetAllTermsByUser()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Term;");
            db.Database.CreateTable<Term>();
            db.InsertTerm(new Term("Title", DateTime.Now, DateTime.Now, 10));
            db.InsertTerm(new Term("Title", DateTime.Now, DateTime.Now, 10));
            db.InsertTerm(new Term("Title", DateTime.Now, DateTime.Now, 11));
            db.InsertTerm(new Term("Title", DateTime.Now, DateTime.Now, 11));
            List<Term> result = db.GetTerms(11);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void TestGetTermById()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Term;");
            db.Database.CreateTable<Term>();
            Term term = new Term("Title", DateTime.Now, DateTime.Now, 10);
            db.InsertTerm(term);
            int ID = db.GetTerms().Count();
            Term result = db.GetTerm(ID);
            Assert.AreEqual(term.ID, result.ID);
            Assert.AreEqual(term.Title, result.Title);
            Assert.AreEqual(term.UserId, result.UserId);
        }

        [TestMethod]
        public void TestUpdateTerm()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.ClearTables();
            Term term = new Term("Title", DateTime.Now, DateTime.Now, 10);
            db.InsertTerm(term);
            int ID = db.GetTerms().Count();
            term = new Term(ID, "Test", DateTime.Now, DateTime.Now, 11);
            int query = db.UpdateTerm(term);
            Term result = db.GetTerm(ID);
            Assert.AreEqual(1, query);
            Assert.AreEqual(term.ID, result.ID);
            Assert.AreEqual(term.Title, result.Title);
            Assert.AreEqual(term.UserId, result.UserId);
        }

        [TestMethod]
        public void TestDeleteTerm()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.ClearTables();
            Term term = new Term("Title", DateTime.Now, DateTime.Now, 10);
            db.InsertTerm(term);
            int ID = db.GetTerms().Count();
            int result = db.GetTerms().Count();
            Assert.AreEqual(ID, result);
            int query = db.DeleteTerm(term);
            Assert.AreEqual(1, query);
            result = db.GetTerms().Count();
            Assert.AreEqual(ID - 1, result);
        }
    //===========================================================================================================
    // Course

        [TestMethod]
        public void TestInsertCourse() {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Course;");
            db.Database.CreateTable<Course>();
            int[] Students = { 2, 3, 4, 5 };
            Course course = new Course(1, "Course", DateTime.Now, DateTime.Now, 1, db);
            int result = db.InsertCourse(course);
            Assert.AreEqual(1, result);
            db.Close();

        }

        [TestMethod]
        public void TestGetAllCourses() {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Course;");
            db.Database.CreateTable<Course>();
            User ins = new User();
            int[] Students = { 2, 3, 4, 5 };
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            List<Course> result = db.GetCourses();
            Assert.AreEqual(6, result.Count);
            db.Close();
        }

        [TestMethod]
        public void TestSearchCourses()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Course;");
            db.Database.CreateTable<Course>();
            User ins = new User();
            int[] Students = { 2, 3, 4, 5 };
            TimeSpan ts = new TimeSpan(365, 0, 0, 0);
            db.InsertCourse(new Course("11Course11", DateTime.Now - ts - ts - ts, DateTime.Now - ts - ts, 1, db));
            db.InsertCourse(new Course("21Course12", DateTime.Now - ts - ts, DateTime.Now - ts, 1, db));
            db.InsertCourse(new Course("31Course13", DateTime.Now - ts, DateTime.Now, 1, db));
            db.InsertCourse(new Course("4Course4", DateTime.Now , DateTime.Now + ts, 1, db));
            db.InsertCourse(new Course("5Course5", DateTime.Now + ts , DateTime.Now+ ts + ts, 1, db));
            db.InsertCourse(new Course("6Course6", DateTime.Now + ts + ts, DateTime.Now+ ts + ts + ts, 1, db));
            List<Course> result = db.SearchCourses("Course", null, null);
            Assert.AreEqual(6, result.Count);
            result = db.SearchCourses("1", null, null);
            Assert.AreEqual(3, result.Count);
            result = db.SearchCourses("4", null, null);
            Assert.AreEqual(1, result.Count);
            result = db.SearchCourses("Course", DateTime.Now - ts - ts - ts, null);
            Assert.AreEqual(5, result.Count);
            result = db.SearchCourses("Course", null, DateTime.Now+ ts);
            Assert.AreEqual(4, result.Count);
            result = db.SearchCourses("Course", DateTime.Now - ts - ts - ts, DateTime.Now+ ts);
            Assert.AreEqual(3, result.Count);

            db.Close();
        }

        [TestMethod]
        public void TestGetCoursesByTermId()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Course;");
            db.Database.Execute("Drop table if exists TermCourse;");
            db.Database.CreateTable<Course>();
            db.Database.CreateTable<TermCourse>();
            User ins = new User();
            int[] Students = { 2, 3, 4, 5 };
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertTermCourse(new TermCourse(0, 1, 1));
            db.InsertTermCourse(new TermCourse(0, 2, 1));
            db.InsertTermCourse(new TermCourse(0, 3, 1));
            db.InsertTermCourse(new TermCourse(0, 4, 2));
            db.InsertTermCourse(new TermCourse(0, 5, 2));
            db.InsertTermCourse(new TermCourse(0, 6, 2));
            List<Course> result = db.GetCourses(2);
            Assert.AreEqual(3, result.Count);
            db.Close();
        }

        [TestMethod]
        public void TestGetCoursesByInstructor()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Course;");
            db.Database.CreateTable<Course>();
            User ins = new User(1);
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 2, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 2, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 2, db));
            List<Course> result = db.GetCourses(ins);
            Assert.AreEqual(3, result.Count);
            db.Close();
        }


        [TestMethod]
        public void TestGetCourseById() {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Course;");
            db.Database.CreateTable<Course>();
            Course course = new Course("tEST", DateTime.Now, DateTime.Now, 1,db );
            db.InsertCourse(course);
            int ID = db.GetCourses().Count();
            Course result = db.GetCourse(ID);
            Assert.AreEqual(ID, result.ID);
            Assert.AreEqual(course.Title, result.Title);
            Assert.AreEqual(course.InstructorId, result.InstructorId);
            db.Close();
        }


        [TestMethod]
        public void TestUpdateCourse() {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Course;");
            db.Database.CreateTable<Course>();
            Course course = new Course("tEST", DateTime.Now, DateTime.Now, 1, db);
            db.InsertCourse(course);
            int ID = db.GetCourses().Count();
            course = new Course(ID, "Test", DateTime.Now, DateTime.Now, 2, db);
            int query = db.UpdateCourse(course);
            Course result = db.GetCourse(ID);
            Assert.AreEqual(1, query);
            Assert.AreEqual(course.ID, result.ID);
            Assert.AreEqual(course.Title, result.Title);
            Assert.AreEqual(course.Start, result.Start);
            Assert.AreEqual(course.End, result.End);
            Assert.AreEqual(course.InstructorId, result.InstructorId);
            db.Close();
        }

        [TestMethod]
        public void TestDeleteCourse() {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Course;");
            db.Database.CreateTable<Course>();
            Course course = new Course("tEST", DateTime.Now, DateTime.Now, 1, db);
            db.InsertCourse(course);
            int ID = db.GetCourses().Count;
            List<Course> result = db.GetCourses(0);
            Assert.AreEqual(ID, result.Count);
            int query = db.DeleteCourse(ID);
            Assert.AreEqual(1, query);
            result = db.GetCourses(0);
            Assert.AreEqual(ID - 1, result.Count);
            db.Close();
        }

        //===========================================================================================================
        // Assessment

        [TestMethod]
        public void TestInsertAssessment()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Assessment;");
            db.Database.CreateTable<Assessment>();
            Assessment a = new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 2);
            int result = db.InsertAssessment(a);
            Assert.AreEqual(1, result);
            db.Close();
        }

        [TestMethod]
        public void TestInsertAssessment1()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Assessment;");
            db.Database.CreateTable<Assessment>();
            db.Database.Execute("Drop table if exists CourseUser;");
            db.Database.CreateTable<CourseUser>();
            int courseId = 2;
            db.InsertCourseUser(new CourseUser(1, 2, 4));
            db.InsertCourseUser(new CourseUser(1, 2, 5));
            db.InsertCourseUser(new CourseUser(1, 2, 6));
            db.InsertCourseUser(new CourseUser(1, 3, 4));
            db.InsertCourseUser(new CourseUser(1, 3, 5));
            db.InsertCourseUser(new CourseUser(1, 3, 6));
            Assessment a = new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, courseId, Category.Objective, 1, 2);
            int result = db.InsertAssessment(a, courseId);
            Assert.AreEqual(1, result);
            List<Assessment> assessments = db.GetAssessments();
            Assert.AreEqual(3, assessments.Count);
            db.Close();
        }

        [TestMethod]
        public void TestGetAllAssessments()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Assessment;");
            db.Database.CreateTable<Assessment>();
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 3, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 3, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 3, Category.Objective, 1, 2));
            List<Assessment> result = db.GetAssessments();
            Assert.AreEqual(6, result.Count);
            db.Close();
        }

        [TestMethod]
        public void TestGetAssessmentByCourseId()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Assessment;");
            db.Database.CreateTable<Assessment>();
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 3, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 3, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 3, Category.Objective, 1, 2));
            List<Assessment> result = db.GetAssessments(2);
            Assert.AreEqual(3, result.Count);
            db.Close();
        }

        [TestMethod]
        public void TestGetAssessmentByCourseIdAndStudentId()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Assessment;");
            db.Database.CreateTable<Assessment>();
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 3));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 3));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 3, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 3, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 3, Category.Objective, 1, 2));
            List<Assessment> result = db.GetAssessments(2,3);
            Assert.AreEqual(2, result.Count);
            db.Close();
        }


        [TestMethod]
        public void TestGetAssessmentById()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Assessment;");
            db.Database.CreateTable<Assessment>();
            Assessment a = new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 2);
            db.InsertAssessment(a);
            int ID = db.GetAssessments().Count();
            a.ID = ID;
            Assessment result = db.GetAssessment(ID);
            Assert.AreEqual(a.ID, result.ID);
            Assert.AreEqual(a.Title, result.Title);
            Assert.AreEqual(a.Start, result.Start);
            Assert.AreEqual(a.End, result.End);
            Assert.AreEqual(a.CourseId, result.CourseId);
            Assert.AreEqual(a.Category, result.Category);
            Assert.AreEqual(a.InstructorId, result.InstructorId);
            Assert.AreEqual(a.StudentId, result.StudentId);
            db.Close();
        }


        [TestMethod]
        public void TestUpdateAssessment()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.ClearTables();
            Assessment a = new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 2);
            db.InsertAssessment(a);
            int ID = db.GetAssessments().Count();
            a = new Assessment(ID, "Assessment 2", DateTime.Now, DateTime.Now, 3, Category.Practical, 2, 3);
            int query = db.UpdateAssessment(a);
            Assessment result = db.GetAssessment(ID);
            Assert.AreEqual(1, query);
            Assert.AreEqual(a.ID, result.ID);
            Assert.AreEqual(a.Title, result.Title);
            Assert.AreEqual(a.Start, result.Start);
            Assert.AreEqual(a.End, result.End);
            Assert.AreEqual(a.CourseId, result.CourseId);
            Assert.AreEqual(a.Category, result.Category);
            Assert.AreEqual(a.InstructorId, result.InstructorId);
            Assert.AreEqual(a.StudentId, result.StudentId);
            db.Close();
        }

        [TestMethod]
        public void TestDeleteAssessment()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.ClearTables();
            Assessment a = new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 2);
            db.InsertAssessment(a);
            int ID = db.GetAssessments().Count;
            List<Assessment> result = db.GetAssessments();
            Assert.AreEqual(ID, result.Count);
            int query = db.DeleteAssessment(ID);
            Assert.AreEqual(1, query);
            result = db.GetAssessments();
            Assert.AreEqual(ID - 1, result.Count);
            db.Close();
        }

        //===========================================================================================================
        // Note

        [TestMethod]
        public void TestInsertNote()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Note;");
            db.Database.CreateTable<Note>();
            Note note = new Note(1,"New Note", 2, 3);
            int result = db.InsertNote(note);
            Assert.AreEqual(1, result);
            db.Close();
        }

        [TestMethod]
        public void TestGetAllNotes()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Note;");
            db.Database.CreateTable<Note>();
            db.InsertNote(new Note(1, "New Note", 2, 5));
            db.InsertNote(new Note(1, "New Note", 2, 5));
            db.InsertNote(new Note(1, "New Note", 2, 3));
            db.InsertNote(new Note(1, "New Note", 2, 3));
            db.InsertNote(new Note(1, "New Note", 4, 5));
            db.InsertNote(new Note(1, "New Note", 4, 5));
            List<Note> result = db.GetNotes();
            Assert.AreEqual(6, result.Count);
            db.Close();
        }

        [TestMethod]
        public void TestGetAllNotesByCourseId()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Note;");
            db.Database.CreateTable<Note>();
            db.InsertNote(new Note(1, "New Note", 2, 5));
            db.InsertNote(new Note(1, "New Note", 2, 5));
            db.InsertNote(new Note(1, "New Note", 2, 3));
            db.InsertNote(new Note(1, "New Note", 2, 3));
            db.InsertNote(new Note(1, "New Note", 4, 5));
            db.InsertNote(new Note(1, "New Note", 4, 5));
            List<Note> result = db.GetNotes(2);
            Assert.AreEqual(4, result.Count);
            db.Close();
        }

        [TestMethod]
        public void TestGetAllNotesByCourseIdAndStudentId()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Note;");
            db.Database.CreateTable<Note>();
            db.InsertNote(new Note(1, "New Note", 2, 5));
            db.InsertNote(new Note(1, "New Note", 2, 5));
            db.InsertNote(new Note(1, "New Note", 2, 3));
            db.InsertNote(new Note(1, "New Note", 2, 3));
            db.InsertNote(new Note(1, "New Note", 4, 5));
            db.InsertNote(new Note(1, "New Note", 4, 5));
            List<Note> result = db.GetNotes(2,5);
            Assert.AreEqual(2, result.Count);
            db.Close();
        }


        [TestMethod]
        public void TestGetNoteById()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Note;");
            db.Database.CreateTable<Note>();
            Note note = new Note(1, "New Note", 2, 3);
            db.InsertNote(note);
            int ID = db.GetNotes().Count();
            note.ID = ID;
            Note result = db.GetNote(ID);
            Assert.AreEqual(note.ID, result.ID);
            Assert.AreEqual(note.Text, result.Text);
            Assert.AreEqual(note.CourseId, result.CourseId);
            Assert.AreEqual(note.StudentId, result.StudentId);
            db.Close();
        }


        [TestMethod]
        public void TestUpdateNote()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Note;");
            db.Database.CreateTable<Note>();
            Note note = new Note(1, "New Note", 2, 3);
            db.InsertNote(note);
            int ID = db.GetNotes().Count();
            note = new Note(ID, "Newer Note", 4, 5);
            int query = db.UpdateNote(note);
            Note result = db.GetNote(ID);
            Assert.AreEqual(1, query);
            Assert.AreEqual(note.ID, result.ID);
            Assert.AreEqual(note.Text, result.Text);
            Assert.AreEqual(note.CourseId, result.CourseId);
            Assert.AreEqual(note.StudentId, result.StudentId);
            db.Close();
        }

        [TestMethod]
        public void TestDeleteNote()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Note;");
            db.Database.CreateTable<Note>();
            Note note = new Note(1, "New Note", 2, 3);
            db.InsertNote(note);
            int ID = db.GetNotes().Count();
            List<Note> result = db.GetNotes();
            Assert.AreEqual(ID, result.Count);
            int query = db.DeleteNote(ID);
            Assert.AreEqual(1, query);
            result = db.GetNotes();
            Assert.AreEqual(ID - 1, result.Count);
            db.Close();
        }

        //===========================================================================================================
        // User

        [TestMethod]
        public void TestInsertUser()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists User;");
            db.Database.CreateTable<User>();
            User user = new User(1, "User", "Pass", "E", "1", "First", "Last", true, false);
            int result = db.InsertUser(user);
            Assert.AreEqual(1, result);
            db.Close();
        }

        [TestMethod]
        public void TestUserAddCourse()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists User;");
            db.Database.Execute("Drop table if exists Course;");
            db.Database.Execute("Drop table if exists Term;");
            db.Database.Execute("Drop table if exists CourseUser;");
            db.Database.Execute("Drop table if exists TermCourse;");
            db.Database.CreateTable<User>();
            db.Database.CreateTable<CourseUser>();
            db.Database.CreateTable<TermCourse>();
            db.Database.CreateTable<Course>();
            db.Database.CreateTable<Term>();
            User user = new User();
            Course course = new Course();
            Term term = new Term();
            int cu = db.GetCourseUser().Count();
            int tc = db.GetTermCourses().Count();

            Assert.AreEqual(0, cu);
            Assert.AreEqual(0, tc);

            int result = db.InsertUser(user);
            Assert.AreEqual(1, result);
            result = db.InsertCourse(course);
            Assert.AreEqual(1, result);
            result = db.InsertTerm(term);
            Assert.AreEqual(1, result);
            result = db.UserAddCourse(user, course, term);
            Assert.AreEqual(1, result);

            cu = db.GetCourseUser().Count();
            tc = db.GetTermCourses().Count();

            Assert.AreEqual(1, cu);
            Assert.AreEqual(1, tc);

            db.Close();
        }

        [TestMethod]
        public void TestGetAllUsers()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists User;");
            db.Database.CreateTable<User>();
            db.InsertUser(new User(1, "User1", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User2", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User3", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User4", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User5", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User6", "Pass", "E", "1", "First", "Last", true, false));
            List<User> result = db.GetUsers();
            Assert.AreEqual(6, result.Count);
            db.Close();
        }

        [TestMethod]
        public void TestGetAllUsersByCourseId()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists User;");
            db.Database.Execute("Drop table if exists CourseUser;");
            db.Database.CreateTable<User>();
            db.Database.CreateTable<CourseUser>();
            db.InsertUser(new User(1, "User1", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User2", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User3", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User4", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User5", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User6", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertCourseUser(new CourseUser(0, 1, 1));
            db.InsertCourseUser(new CourseUser(0, 1, 2));
            db.InsertCourseUser(new CourseUser(0, 1, 3));
            db.InsertCourseUser(new CourseUser(0, 2, 4));
            db.InsertCourseUser(new CourseUser(0, 2, 5));
            db.InsertCourseUser(new CourseUser(0, 2, 6));
            List<User> result = db.GetUsers();
            Assert.AreEqual(6, result.Count);
            result = db.GetUsers(1);
            Assert.AreEqual(3, result.Count);
            db.Close();
        }


        [TestMethod]
        public void TestGetUserById()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists User;");
            db.Database.CreateTable<User>();
            User user = new User(1, "User", "Pass", "E", "1", "First", "Last", true, false);
            db.InsertUser(user);
            db.InsertUser(user);
            db.InsertUser(user);
            db.InsertUser(user);
            int ID = db.GetUsers().Count();
            user.ID = ID;
            User result = db.GetUser(ID);
            Assert.AreEqual(user.ID, result.ID);
            Assert.AreEqual(user.Username, result.Username);
            Assert.AreEqual(null, result.Password);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.Phone, result.Phone);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.IsAdmin, result.IsAdmin);
            Assert.AreEqual(user.IsInstructor, result.IsInstructor);
            db.Close();
        }

        [TestMethod]
        public void TestLoginUser()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists User;");
            db.Database.CreateTable<User>();
            User user = new User(1, "Admin", "Admin", "E", "1", "First", "Last", true, false);
            db.InsertUser(user);
            db.InsertUser(new User(1, "User1", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User2", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User3", "Pass", "E", "1", "First", "Last", true, false));
            user.ID = 1;
            User result = db.LoginUser(user.ID, user.Password);
            Assert.AreEqual(user.ID, result.ID);
            Assert.AreEqual(user.Username, result.Username);
            Assert.AreEqual(null, result.Password);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.Phone, result.Phone);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.IsAdmin, result.IsAdmin);
            Assert.AreEqual(user.IsInstructor, result.IsInstructor);
            db.Close();
        }

        [TestMethod]
        public void TestLoginUser1()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists User;");
            db.Database.CreateTable<User>();
            User user = new User(1, "Admin", "Admin", "E", "1", "First", "Last", true, false);
            db.InsertUser(user);
            db.InsertUser(new User(1, "User1", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User2", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User3", "Pass", "E", "1", "First", "Last", true, false));
            int ID = db.GetUsers().Count();
            user.ID = 1;
            User result = db.LoginUser(user.Username, user.Password);
            Assert.AreEqual(user.ID, result.ID);
            Assert.AreEqual(user.Username, result.Username);
            Assert.AreEqual(null, result.Password);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.Phone, result.Phone);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.IsAdmin, result.IsAdmin);
            Assert.AreEqual(user.IsInstructor, result.IsInstructor);
            db.Close();
        }

        [TestMethod]
        public void TestExistingUsername()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists User;");
            db.Database.CreateTable<User>();
            User user = new User(1, "User", "Pass", "E", "1", "First", "Last", true, false);
            Assert.IsFalse(db.CheckExisting(user.Username));
            int result = db.InsertUser(user);
            db.InsertUser(user);
            Assert.AreEqual(1, result);
            Assert.IsTrue(db.CheckExisting(user.Username));
            Assert.IsFalse(db.CheckExisting(user.Username, 1));
            result = db.InsertUser(user);
            Assert.AreEqual(0, result);
            db.Close();
        }

        [TestMethod]
        public void TestUpdateUser()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists User;");
            db.Database.CreateTable<User>();
            User user = new User(1, "User", "Pass", "E", "1", "First", "Last", true, false);
            db.InsertUser(user);
            int ID = db.GetUsers().Count();
            user = new User(ID, "New User", "Pass2", "Em", "11", "FirstN", "LastN", false, true);
            int query = db.UpdateUser(user);
            User result = db.GetUser(ID);
            Assert.AreEqual(1, query);
            Assert.AreEqual(user.ID, result.ID);
            Assert.AreEqual(user.Username, result.Username);
            Assert.AreEqual(null, result.Password);
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.Phone, result.Phone);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.IsAdmin, result.IsAdmin);
            Assert.AreEqual(user.IsInstructor, result.IsInstructor);
            db.Close();
        }

        //[TestMethod]
        //public void TestDeleteUser()
        //{
        //    C971Database db = new C971Database(DbConstants.DatabaseFilename);
        //    db.Init();
        //    db.Database.Execute("Drop table if exists User;");
        //    db.Database.CreateTable<User>();
        //    db.Database.Execute("Drop table if exists CourseUser;");
        //    db.Database.CreateTable<CourseUser>();

        //    User user = new User(1, "User", "Pass", "E", "1", "First", "Last", true, false);
        //    db.InsertCourseUser(new CourseUser(1, 2, 1));
        //    db.InsertCourseUser(new CourseUser(1, 3, 1));
        //    db.InsertCourseUser(new CourseUser(1, 4, 1));
        //    db.InsertCourseUser(new CourseUser(1, 2, 3));
        //    db.InsertCourseUser(new CourseUser(1, 3, 3));
        //    db.InsertCourseUser(new CourseUser(1, 4, 3));
        //    List<CourseUser> cu = db.GetCourseUsers(0, 1);
        //    Assert.AreEqual(3, cu.Count);
        //    db.InsertUser(user);
        //    List<User> result = db.GetUsers();
        //    Assert.AreEqual(1, result.Count);
        //    int query = db.DeleteUser(1);
        //    Assert.AreEqual(1, query);
        //    result = db.GetUsers();
        //    Assert.AreEqual(0, result.Count);
        //    cu = db.GetCourseUsers(0, 1);
        //    Assert.AreEqual(0, cu.Count);
        //    db.Close();
        //}


        [TestMethod]
        public void TestDeleteUser()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists User;");
            db.Database.CreateTable<User>();
            db.Database.Execute("Drop table if exists CourseUser;");
            db.Database.CreateTable<CourseUser>();

            User user = new User(1, "User", "Pass", "E", "1", "First", "Last", true, false);
            db.InsertCourseUser(new CourseUser(1, 2, 1));
            db.InsertCourseUser(new CourseUser(1, 3, 1));
            db.InsertCourseUser(new CourseUser(1, 4, 1));
            db.InsertCourseUser(new CourseUser(1, 2, 3));
            db.InsertCourseUser(new CourseUser(1, 3, 3));
            db.InsertCourseUser(new CourseUser(1, 4, 3));
            List<CourseUser> cu = db.GetCourseUsers(0, 1);
            Assert.AreEqual(3, cu.Count);
            db.InsertUser(user);
            int ID = db.GetUsers().Count();
            List<User> result = db.GetUsers();
            Assert.AreEqual(ID, result.Count);
            int query = db.DeleteUser(user);
            Assert.AreEqual(1, query);
            result = db.GetUsers();
            Assert.AreEqual(ID - 1, result.Count);
            cu = db.GetCourseUsers(0,1);
            Assert.AreEqual(0, cu.Count);
            db.Close();
        }

        //===========================================================================================================
        // CourseUser

        [TestMethod]
        public void TestInsertCourseUser()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists CourseUser;");
            db.Database.CreateTable<CourseUser>();
            CourseUser c = new CourseUser(1, 2, 3);
            int result = db.InsertCourseUser(c);
            Assert.AreEqual(1, result);
            db.Close();
        }

        [TestMethod]
        public void TestGetAllCourseUsers()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists CourseUser;");
            db.Database.CreateTable<CourseUser>();
            db.InsertCourseUser(new CourseUser(1, 2, 4));
            db.InsertCourseUser(new CourseUser(1, 2, 5));
            db.InsertCourseUser(new CourseUser(1, 2, 6));
            db.InsertCourseUser(new CourseUser(1, 3, 4));
            db.InsertCourseUser(new CourseUser(1, 3, 5));
            db.InsertCourseUser(new CourseUser(1, 3, 6));
            List<CourseUser> result = db.GetCourseUser();
            Assert.AreEqual(6, result.Count);
            db.Close();
        }

        [TestMethod]
        public void TestGetCourseUsersByCourseId()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists CourseUser;");
            db.Database.CreateTable<CourseUser>();
            db.InsertCourseUser(new CourseUser(1, 2, 4));
            db.InsertCourseUser(new CourseUser(1, 2, 5));
            db.InsertCourseUser(new CourseUser(1, 2, 6));
            db.InsertCourseUser(new CourseUser(1, 3, 4));
            db.InsertCourseUser(new CourseUser(1, 3, 5));
            db.InsertCourseUser(new CourseUser(1, 3, 6));
            List<CourseUser> result = db.GetCourseUsers();
            Assert.AreEqual(6, result.Count);
            result = db.GetCourseUsers(2);
            Assert.AreEqual(3, result.Count);
            result = db.GetCourseUsers(0,4);
            Assert.AreEqual(2, result.Count);
            db.Close();
        }

        [TestMethod]
        public void TestGetCourseUserById()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists CourseUser;");
            db.Database.CreateTable<CourseUser>();
            CourseUser c = new CourseUser(1, 2, 3);
            db.InsertCourseUser(c);
            int ID = db.GetCourseUser().Count();
            c.ID = ID;
            CourseUser result = db.GetCourseUser(ID);
            Assert.AreEqual(c.ID, result.ID);
            Assert.AreEqual(c.CourseId, result.CourseId);
            Assert.AreEqual(c.StudentId, result.StudentId);
            db.Close();
        }

        [TestMethod]
        public void TestGetCourseUserByCourseIdAndStudentId()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists CourseUser;");
            db.Database.CreateTable<CourseUser>();
            CourseUser c = new CourseUser(1, 2, 3);
            db.InsertCourseUser(c);
            c.ID = db.GetCourseUser().Count();
            db.InsertCourseUser(new CourseUser(1, 3, 6));
            CourseUser result = db.GetCourseUser(2,3);
            Assert.AreEqual(c.ID, result.ID);
            Assert.AreEqual(c.CourseId, result.CourseId);
            Assert.AreEqual(c.StudentId, result.StudentId);
            db.Close();
        }


        [TestMethod]
        public void TestDeleteCourseUserById()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists CourseUser;");
            db.Database.CreateTable<CourseUser>();
            CourseUser c = new CourseUser(1, 2, 3);
            db.InsertCourseUser(c);
            int ID = db.GetCourseUser().Count();
            List<CourseUser> result = db.GetCourseUser();
            Assert.AreEqual(ID, result.Count);
            int query = db.DeleteCourseUser(ID);
            Assert.AreEqual(1, query);
            result = db.GetCourseUser();
            Assert.AreEqual(ID - 1, result.Count);
            db.Close();
        }

        [TestMethod]
        public void TestDeleteCourseUserByCourseIdAndStudentId()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists CourseUser;");
            db.Database.CreateTable<CourseUser>();
            CourseUser c = new CourseUser(1, 2, 3);
            db.InsertCourseUser(c);
            int ID = db.GetCourseUser().Count();
            List<CourseUser> result = db.GetCourseUser();
            Assert.AreEqual(ID, result.Count);
            int query = db.DeleteCourseUser(2,3);
            Assert.AreEqual(1, query);
            result = db.GetCourseUser();
            Assert.AreEqual(ID - 1, result.Count);
            db.Close();
        }
    }
}