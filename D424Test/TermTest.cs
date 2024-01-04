using C971.Data;
using C971.Models;
using System.Reflection;

namespace D424Test
{
    [TestClass]
    public class TermTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Term term = new Term();
            DateTime start = DateTime.MinValue;
            string datestring = "Jan 01, 0001 - Jan 01, 0001";
            int ID = 0;
            string title = "New Term";

            Assert.AreEqual(start, term.Start);
            Assert.AreEqual(start, term.End);
            Assert.AreEqual(datestring, term.DateString);
            Assert.AreEqual(ID, term.ID);
            Assert.AreEqual(title, term.Title);
            Assert.AreEqual(ID, term.UserId);
        }

        [TestMethod]
        public void TestConstructor1()
        {
            DateTime start = DateTime.Now;
            string datestring = DateTime.Now.ToString("MMM dd, yyyy") + " - " + DateTime.Now.ToString("MMM dd, yyyy");
            int ID = 1;
            int UserId = 2;
            string title = "Term";
            Term term = new Term(ID, title, start, start, UserId);

            Assert.AreEqual(start, term.Start);
            Assert.AreEqual(start, term.End);
            Assert.AreEqual(datestring, term.DateString);
            Assert.AreEqual(ID, term.ID);
            Assert.AreEqual(title, term.Title);
            Assert.AreEqual(UserId, term.UserId);

        }

        [TestMethod]
        public void TestConstructor2()
        {
            DateTime start = DateTime.Now;
            string datestring = DateTime.Now.ToString("MMM dd, yyyy") + " - " + DateTime.Now.ToString("MMM dd, yyyy");
            string title = "Term";
            int UserId = 2;
            Term term = new Term(title, start, start, UserId);

            Assert.AreEqual(start, term.Start);
            Assert.AreEqual(start, term.End);
            Assert.AreEqual(datestring, term.DateString);
            Assert.AreEqual(title, term.Title);
            Assert.AreEqual(UserId, term.UserId);
        }

        [TestMethod]
        public void TestSetGetStart()
        {
            Term term = new Term();
            DateTime start = DateTime.Now;
            string datestring = DateTime.Now.ToString("MMM dd, yyyy") + " - Jan 01, 0001";
            term.Start = start;
            term.dateString();
            Assert.AreEqual(start, term.Start);
            Assert.AreNotEqual(start, term.End);
            Assert.AreEqual(datestring, term.DateString);
        }

        [TestMethod]
        public void TestSetGetEnd()
        {
            Term term = new Term();
            DateTime end = DateTime.Now;
            string datestring = "Jan 01, 0001 - " + DateTime.Now.ToString("MMM dd, yyyy");
            term.End = end;
            term.dateString();
            Assert.AreEqual(end, term.End);
            Assert.AreNotEqual(end, term.Start);
            Assert.AreEqual(datestring, term.DateString);
        }

        [TestMethod]
        public void TestSetGetTitle()
        {
            Term term = new Term();
            string newTitle = "New Title";
            term.Title = newTitle;
            Assert.AreEqual(newTitle, term.Title);
        }

        [TestMethod]
        public void TestSetGetID()
        {
            Term term = new Term();
            int newID = 1;
            term.ID = newID;
            Assert.AreEqual(newID, term.ID);
        }

        [TestMethod]
        public void TestSetGetUserID()
        {
            Term term = new Term();
            int newID = 1;
            term.UserId = newID;
            Assert.AreEqual(newID, term.UserId);
        }
    }
}