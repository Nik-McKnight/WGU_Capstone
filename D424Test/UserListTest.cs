using C971;
using C971.Data;
using C971.Models;
using System.Reflection;

namespace D424Test
{
    [TestClass]
    public class UserListTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            UserList u = new UserList();
            int courseId = 0;
            Assert.AreEqual(courseId, u.CourseId);
            Assert.AreEqual(new List<User>().ToArray(), u.Users.ToArray());
        }

        [TestMethod]
        public void TestConstructor1()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists User;");
            db.Database.CreateTable<User>();
            int courseId = 0;
            db.InsertUser(new User(1, "User1", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User2", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User3", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User4", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User5", "Pass", "E", "1", "First", "Last", true, false));
            db.InsertUser(new User(1, "User6", "Pass", "E", "1", "First", "Last", true, false));
            List<User> result = db.GetUsers();
            Assert.AreEqual(6, result.Count);
            UserList u = new UserList(courseId, db);
            Assert.AreEqual(result.Count, u.Users.Count);
            Assert.AreEqual(courseId, u.CourseId);
        }

        [TestMethod]
        public void TestConstructor2()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists User;");
            db.Database.Execute("Drop table if exists CourseUser;");
            db.Database.CreateTable<User>();
            db.Database.CreateTable<CourseUser>();
            int courseId = 2;
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
            List<User> result = db.GetUsers(courseId);
            Assert.AreEqual(3, result.Count);
            UserList u = new UserList(courseId, db);
            Assert.AreEqual(result.Count, u.Users.Count);
            Assert.AreEqual(courseId, u.CourseId);
        }
    }
}