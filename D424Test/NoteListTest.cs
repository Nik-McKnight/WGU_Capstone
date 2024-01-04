using C971;
using C971.Data;
using C971.Models;
using System.Reflection;

namespace D424Test
{
    [TestClass]
    public class NoteListTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.SeedData();
            NoteList n = new NoteList();
            int courseId = 0;
            int studentId = 0;
            Assert.AreEqual(courseId, n.CourseId);
            Assert.AreEqual(studentId, n.StudentId);
            Assert.AreEqual(new List<Note>().ToArray(), n.Notes.ToArray());
        }

        [TestMethod]
        public void TestConstructor1()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Note;");
            db.Database.CreateTable<Note>();
            int courseId = 2;
            int studentId = 1;
            db.InsertNote(new Note(0, "Test",  courseId, studentId));
            NoteList n = new NoteList(courseId, studentId, db);
            Assert.AreEqual(courseId, n.CourseId);
            Assert.AreEqual(studentId, n.StudentId);
            Assert.AreEqual(1, n.Notes.Count);
        }

        [TestMethod]
        public void TestLoadNotes()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Note;");
            db.Database.CreateTable<Note>();
            int courseId = 2;
            int studentId = 1;
            db.InsertNote(new Note("Test", 1, 2));
            db.InsertNote(new Note("Test", 1, 2));
            db.InsertNote(new Note("Test", 2, 2));
            db.InsertNote(new Note("Test", 2, 2));
            db.InsertNote(new Note("Test", 1, 3));
            db.InsertNote(new Note("Test", 1, 3));
            NoteList n = new NoteList(courseId, studentId, db);
            List<Note> notes = db.GetNotes(courseId, studentId);
            CollectionAssert.AreEqual(notes.ToArray(), n.Notes.ToArray());
        }
    }
}