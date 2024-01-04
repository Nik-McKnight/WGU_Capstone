using C971;
using C971.Data;
using C971.Models;
using System.Reflection;

namespace D424Test
{
    [TestClass]
    public class CourseListTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            CourseList c = new CourseList();
            int termId = 0;
            Assert.AreEqual(termId, c.TermId);
            Assert.AreEqual(new List<Course>().ToArray(), c.Courses.ToArray());
        }

        [TestMethod]
        public void TestConstructor1()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Course;");
            db.Database.CreateTable<Course>();
            int termId = 0;
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            db.InsertCourse(new Course("Course", DateTime.Now, DateTime.Now, 1, db));
            List<Course> result = db.GetCourses(termId);
            Assert.AreEqual(6, result.Count);
            CourseList c = new CourseList(termId, db);
            Assert.AreEqual(termId, c.TermId);
            Assert.AreEqual(result.Count, c.Courses.Count);
        }

        [TestMethod]
        public void TestConstructor2()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Course;");
            db.Database.Execute("Drop table if exists TermCourse;");
            db.Database.CreateTable<Course>();
            db.Database.CreateTable<TermCourse>();
            int termId = 2;
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
            List<Course> result = db.GetCourses(termId);
            Assert.AreEqual(3, result.Count);
            CourseList c = new CourseList(termId, db);
            Assert.AreEqual(termId, c.TermId);
            Assert.AreEqual(result.Count, c.Courses.Count);
        }
    }
}