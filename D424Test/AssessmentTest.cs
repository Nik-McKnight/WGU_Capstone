using C971;
using C971.Data;
using C971.Models;
using System.Reflection;

namespace D424Test
{
    [TestClass]
    public class AssessmentTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Assessment a = new Assessment();
            DateTime start = DateTime.MinValue;
            string datestring = "Jan 01, 0001 - Jan 01, 0001";
            int ID = 0;
            string title = "New Assessment";
            int courseId = 0;
            Category category = Category.Objective;

            Assert.AreEqual(start, a.Start);
            Assert.AreEqual(start, a.End);
            Assert.AreEqual(datestring, a.DateString);
            Assert.AreEqual(ID, a.ID);
            Assert.AreEqual(title, a.Title);
            Assert.AreEqual(courseId, a.CourseId);
            Assert.AreEqual(category, a.Category);
        }

        [TestMethod]
        public void TestConstructor1()
        {
            DateTime start = DateTime.Now;
            string datestring = DateTime.Now.ToString("MMM dd, yyyy") + " - " + DateTime.Now.ToString("MMM dd, yyyy");
            int ID = 1;
            string title = "Assessment";
            int courseId = 2;
            Category category = Category.Objective;
            Assessment a = new Assessment(ID, title, start, start, courseId, category);

            Assert.AreEqual(start, a.Start);
            Assert.AreEqual(start, a.End);
            Assert.AreEqual(datestring, a.DateString);
            Assert.AreEqual(ID, a.ID);
            Assert.AreEqual(title, a.Title);
            Assert.AreEqual(courseId, a.CourseId);
            Assert.AreEqual(category, a.Category);
        }

        [TestMethod]
        public void TestConstructor2()
        {
            DateTime start = DateTime.Now;
            string datestring = DateTime.Now.ToString("MMM dd, yyyy") + " - " + DateTime.Now.ToString("MMM dd, yyyy");
            int ID = 1;
            string title = "Assessment";
            int courseId = 2;
            Category category = Category.Objective;
            Assessment a = new Assessment(title, start, start, courseId, category);

            Assert.AreEqual(start, a.Start);
            Assert.AreEqual(start, a.End);
            Assert.AreEqual(datestring, a.DateString);
            Assert.AreEqual(title, a.Title);
            Assert.AreEqual(courseId, a.CourseId);
            Assert.AreEqual(category, a.Category);
        }

        [TestMethod]
        public void TestSetGetStart()
        {
            Assessment a = new Assessment();
            DateTime start = DateTime.Now;
            string datestring = DateTime.Now.ToString("MMM dd, yyyy") + " - Jan 01, 0001";
            a.Start = start;
            a.dateString();
            Assert.AreEqual(start, a.Start);
            Assert.AreNotEqual(start, a.End);
            Assert.AreEqual(datestring, a.DateString);
        }

        [TestMethod]
        public void TestSetGetEnd()
        {
            Assessment a = new Assessment();
            DateTime end = DateTime.Now;
            string datestring = "Jan 01, 0001 - " + DateTime.Now.ToString("MMM dd, yyyy");
            a.End = end;
            a.dateString();
            Assert.AreEqual(end, a.End);
            Assert.AreNotEqual(end, a.Start);
            Assert.AreEqual(datestring, a.DateString);
        }

        [TestMethod]
        public void TestSetGetTitle()
        {
            Assessment a = new Assessment();
            string newTitle = "New Title";
            a.Title = newTitle;
            Assert.AreEqual(newTitle, a.Title);
        }

        [TestMethod]
        public void TestSetGetID()
        {
            Assessment a = new Assessment();
            int newID = 1;
            a.ID = newID;
            Assert.AreEqual(newID, a.ID);
        }

        [TestMethod]
        public void TestSetGetCourseID()
        {
            Assessment a = new Assessment();
            int newID = 1;
            a.CourseId = newID;
            Assert.AreEqual(newID, a.CourseId);
        }

        [TestMethod]
        public void TestSetGetCategory()
        {
            Assessment a = new Assessment();
            Category c = Category.Practical;
            a.Category = c;
            Assert.AreEqual(c, a.Category);
        }
    }
}