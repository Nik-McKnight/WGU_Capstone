using C971;
using C971.Data;
using C971.Models;

namespace D424Test
{
    [TestClass]
    public class CourseTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Course course = new Course();
            DateTime start = DateTime.MinValue;
            string datestring = "Jan 01, 0001 - Jan 01, 0001";
            int ID = 0;
            string title = "New Course";

            Assert.AreEqual(start, course.Start);
            Assert.AreEqual(start, course.End);
            Assert.AreEqual(datestring, course.DateString);
            Assert.AreEqual(ID, course.ID);
            Assert.AreEqual(title, course.Title);
            Assert.AreEqual(0, course.InstructorId);
        }

        [TestMethod]
        public void TestConstructor1()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Course;");
            db.Database.CreateTable<Course>();
            DateTime start = DateTime.Now;
            string datestring = DateTime.Now.ToString("MMM dd, yyyy") + " - " + DateTime.Now.ToString("MMM dd, yyyy");
            int ID = 1;
            string title = "Course";
            int Instructor = 3;
            List<User> Students = new List<User>();
            Students.Add(new User());
            Course course = new Course(ID, title, start, start, Instructor, db);

            Assert.AreEqual(start, course.Start);
            Assert.AreEqual(start, course.End);
            Assert.AreEqual(datestring, course.DateString);
            Assert.AreEqual(ID, course.ID);
            Assert.AreEqual(title, course.Title);
            Assert.AreEqual(Instructor, course.InstructorId);
        }

        [TestMethod]
        public void TestConstructor2()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Course;");
            db.Database.CreateTable<Course>();
            DateTime start = DateTime.Now;
            string datestring = DateTime.Now.ToString("MMM dd, yyyy") + " - " + DateTime.Now.ToString("MMM dd, yyyy");
            string title = "Course";
            int Instructor = 3;
            List<User> Students = new List<User>();
            Students.Add(new User());
            Course course = new Course(title, start, start, Instructor, db);

            Assert.AreEqual(start, course.Start);
            Assert.AreEqual(start, course.End);
            Assert.AreEqual(datestring, course.DateString);
            Assert.AreEqual(title, course.Title);
            Assert.AreEqual(Instructor, course.InstructorId);
        }

        [TestMethod]
        public void TestSetGetStart()
        {
            Course course = new Course();
            DateTime start = DateTime.Now;
            string datestring = DateTime.Now.ToString("MMM dd, yyyy") + " - Jan 01, 0001";
            course.Start = start;
            course.dateString();
            Assert.AreEqual(start, course.Start);
            Assert.AreNotEqual(start, course.End);
            Assert.AreEqual(datestring, course.DateString);
        }

        [TestMethod]
        public void TestSetGetEnd()
        {
            Course course = new Course();
            DateTime end = DateTime.Now;
            string datestring = "Jan 01, 0001 - " + DateTime.Now.ToString("MMM dd, yyyy");
            course.End = end;
            course.dateString();
            Assert.AreEqual(end, course.End);
            Assert.AreNotEqual(end, course.Start);
            Assert.AreEqual(datestring, course.DateString);
        }

        [TestMethod]
        public void TestSetGetTitle()
        {
            Course course = new Course();
            string newTitle = "New Title";
            course.Title = newTitle;
            Assert.AreEqual(newTitle, course.Title);
        }

        [TestMethod]
        public void TestSetGetID()
        {
            Course course = new Course();
            int newID = 1;
            course.ID = newID;
            Assert.AreEqual(newID, course.ID);
        }

        [TestMethod]
        public void TestSetGetInstructor()
        {
            Course course = new Course();
            Assert.AreEqual(0, course.InstructorId);
            int Instructor = 3;
            course.InstructorId = Instructor;
            Assert.AreEqual(Instructor, course.InstructorId);
        }

    }
}