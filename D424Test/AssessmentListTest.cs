using C971;
using C971.Data;
using C971.Models;
using System.Reflection;

namespace D424Test
{
    [TestClass]
    public class AssessmentListTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            AssessmentList a = new AssessmentList();
            int courseId = 0;
            int studentId = 0;
            Assert.AreEqual(courseId, a.CourseId);
            Assert.AreEqual(studentId, a.StudentId);
            Assert.AreEqual(new List<Assessment>().ToArray(), a.Assessments.ToArray());
        }

        [TestMethod]
        public void TestConstructor1()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Assessment;");
            db.Database.CreateTable<Assessment>();
            int courseId = 2;
            int studentId = 0;
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 3));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 3));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 3, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 3, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 3, Category.Objective, 1, 2));
            AssessmentList a = new AssessmentList(courseId, studentId, db);
            List<Assessment> result = db.GetAssessments(courseId, studentId);
            Assert.AreEqual(3, result.Count);

            Assert.AreEqual(result.Count, a.Assessments.Count);
            Assert.AreEqual(courseId, a.CourseId);
            Assert.AreEqual(studentId, a.StudentId);
        }

        [TestMethod]
        public void TestConstructor2()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Assessment;");
            db.Database.CreateTable<Assessment>();
            int courseId = 2;
            int studentId = 3;
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 3));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 3));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 2, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 3, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 3, Category.Objective, 1, 2));
            db.InsertAssessment(new Assessment(1, "Assessment", DateTime.Now, DateTime.Now, 3, Category.Objective, 1, 2));
            AssessmentList a = new AssessmentList(courseId, studentId, db);
            List<Assessment> result = db.GetAssessments(courseId, studentId);
            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(result.Count, a.Assessments.Count);
            Assert.AreEqual(courseId, a.CourseId);
            Assert.AreEqual(studentId, a.StudentId);
        }
    }
}