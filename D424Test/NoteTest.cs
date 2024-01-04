
using C971.Models;

namespace D424Test
{
    [TestClass]
    public class NoteTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Note note = new Note();
            int ID = 0;
            string text = "New Note";
            Assert.AreEqual(ID, note.ID);
            Assert.AreEqual(text, note.Text);
            Assert.AreEqual(ID, note.CourseId);
            Assert.AreEqual(ID, note.StudentId);
        }

        [TestMethod]
        public void TestConstructor1()
        {
            int ID = 1;
            string text = "New Note";
            int courseId = 2;
            int studentId = 3;
            Note note = new Note(ID, text, courseId, studentId);
            Assert.AreEqual(ID, note.ID);
            Assert.AreEqual(text, note.Text);
            Assert.AreEqual(courseId, note.CourseId);
            Assert.AreEqual(studentId, note.StudentId);
        }

        [TestMethod]
        public void TestConstructor2()
        {
            string text = "New Note";
            int courseId = 2;
            int studentId = 3;
            Note note = new Note(text, courseId, studentId);
            Assert.AreEqual(text, note.Text);
            Assert.AreEqual(courseId, note.CourseId);
            Assert.AreEqual(studentId, note.StudentId);
        }

        [TestMethod]
        public void TestSetGetID()
        {
            Note note = new Note();
            note.ID = 1;
            Assert.AreEqual(1, note.ID);
        }

        [TestMethod]
        public void TestSetGetText()
        {
            Note note = new Note();
            string text = "test";
            note.Text = text;
            Assert.AreEqual(text, note.Text);
        }

        [TestMethod]
        public void TestSetGetCourseID()
        {
            Note note = new Note();
            note.CourseId = 1;
            Assert.AreEqual(1, note.CourseId);
        }

        [TestMethod]
        public void TestSetGetStudentID()
        {
            Note note = new Note();
            note.StudentId = 1;
            Assert.AreEqual(1, note.StudentId);
        }

    }
}