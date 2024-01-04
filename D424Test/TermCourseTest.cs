using C971;
using C971.Models;

namespace D424Test
{
    [TestClass]
    public class TermCourseTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            TermCourse termCourse = new TermCourse();
            int ID = 0;
            int CourseId = 0;
            int TermId = 0;

            Assert.AreEqual(ID, termCourse.ID);
            Assert.AreEqual(CourseId, termCourse.CourseId);
            Assert.AreEqual(TermId, termCourse.TermId);
        }

        [TestMethod]
        public void TestConstructor1()
        {
            TermCourse termCourse = new TermCourse(1,2,3);
            int ID = 1;
            int CourseId = 2;
            int TermId = 3;

            Assert.AreEqual(ID, termCourse.ID);
            Assert.AreEqual(CourseId, termCourse.CourseId);
            Assert.AreEqual(TermId, termCourse.TermId);
        }
    }
}