
using C971.Models;

namespace D424Test
{
    [TestClass]
    public class DatesTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            Dates dates = new Dates();
            DateTime start = DateTime.MinValue;
            string datestring = "Jan 01, 0001 - Jan 01, 0001";
            Assert.AreEqual(start, dates.Start);
            Assert.AreEqual(start, dates.End);
            Assert.AreEqual(datestring, dates.DateString);
        }

        [TestMethod]
        public void TestSetGetStart()
        {
            Dates dates = new Dates();
            DateTime start = DateTime.Now;
            string datestring = DateTime.Now.ToString("MMM dd, yyyy") + " - Jan 01, 0001";
            dates.Start = start;
            dates.dateString();
            Assert.AreEqual(start, dates.Start);
            Assert.AreNotEqual(start, dates.End);
            Assert.AreEqual(datestring, dates.DateString);
        }

        [TestMethod]
        public void TestSetGetEnd()
        {
            Dates dates = new Dates();
            DateTime end = DateTime.Now;
            string datestring = "Jan 01, 0001 - " + DateTime.Now.ToString("MMM dd, yyyy");
            dates.End = end;
            dates.dateString();
            Assert.AreEqual(end, dates.End);
            Assert.AreNotEqual(end, dates.Start);
            Assert.AreEqual(datestring, dates.DateString);
        }
    }
}