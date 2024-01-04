using C971;
using C971.Data;
using C971.Models;
using System.Collections.ObjectModel;

namespace D424Test
{
    [TestClass]
    public class TermListTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            TermList t = new TermList();
            int userId = 0;
            Assert.AreEqual(userId, t.userId);
            Assert.AreEqual(new ObservableCollection<Term>().ToArray(), t.Terms.ToArray());
        }

        [TestMethod]
        public void TestConstructor1()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Term;");
            db.Database.CreateTable<Term>();
            int userId = 0;
            db.InsertTerm(new Term("Title", DateTime.Now, DateTime.Now, 10));
            db.InsertTerm(new Term("Title", DateTime.Now, DateTime.Now, 10));
            db.InsertTerm(new Term("Title", DateTime.Now, DateTime.Now, 11));
            db.InsertTerm(new Term("Title", DateTime.Now, DateTime.Now, 11));
            List<Term> result = db.GetTerms(userId);
            Assert.AreEqual(4, result.Count());

            TermList t = new TermList(userId, db);
            Assert.AreEqual(result.Count, t.Terms.Count);
            Assert.AreEqual(userId, t.userId);
        }

        [TestMethod]
        public void TestConstructor2()
        {
            C971Database db = new C971Database(DbConstants.DatabaseFilename);
            db.Init();
            db.Database.Execute("Drop table if exists Term;");
            db.Database.CreateTable<Term>();
            int userId = 10;
            db.InsertTerm(new Term("Title", DateTime.Now, DateTime.Now, 10));
            db.InsertTerm(new Term("Title", DateTime.Now, DateTime.Now, 10));
            db.InsertTerm(new Term("Title", DateTime.Now, DateTime.Now, 11));
            db.InsertTerm(new Term("Title", DateTime.Now, DateTime.Now, 11));
            List<Term> result = db.GetTerms(userId);
            Assert.AreEqual(2, result.Count());

            TermList t = new TermList(userId, db);
            Assert.AreEqual(result.Count, t.Terms.Count);
            Assert.AreEqual(userId, t.userId);
        }
    }
}