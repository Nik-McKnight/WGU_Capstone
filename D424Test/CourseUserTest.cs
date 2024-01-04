using C971;
using C971.Models;

namespace D424Test
{
    [TestClass]
    public class CourseUserTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            CourseUser courseUser = new CourseUser();
            int ID = 0;
            int CourseId = 0;
            int StudentId = 0;

            Assert.AreEqual(ID, courseUser.ID);
            Assert.AreEqual(CourseId, courseUser.CourseId);
            Assert.AreEqual(StudentId, courseUser.StudentId);
        }

        [TestMethod]
        public void TestConstructor1()
        {
            CourseUser courseUser = new CourseUser(1, 2, 3);
            int ID = 1;
            int CourseId = 2;
            int StudentId = 3;

            Assert.AreEqual(ID, courseUser.ID);
            Assert.AreEqual(CourseId, courseUser.CourseId);
            Assert.AreEqual(StudentId, courseUser.StudentId);
        }
    }
}