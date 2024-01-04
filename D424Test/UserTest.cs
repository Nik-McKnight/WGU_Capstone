using C971;
using C971.Data;
using C971.Models;
using System.Reflection;

namespace D424Test
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void TestConstructor()
        {
            int ID = 0;
            string Username = "Username";
            string Email = "Email";
            string Password = "Password";
            string Phone = "Phone";
            string FirstName = "First Name";
            string LastName = "Last Name";
            Boolean isAdmin = false;
            Boolean isInstructor = false;
            User user = new User();

            Assert.AreEqual(ID, user.ID);
            Assert.AreEqual(Username, user.Username);
            Assert.AreEqual(Password, user.Password);
            Assert.AreEqual(Email, user.Email);
            Assert.AreEqual(Phone, user.Phone);
            Assert.AreEqual(FirstName, user.FirstName);
            Assert.AreEqual(LastName, user.LastName);
            Assert.AreEqual(isAdmin, user.IsAdmin);
            Assert.AreEqual(isInstructor, user.IsInstructor);
        }

        [TestMethod]
        public void TestConstructor1()
        {
            int ID = 1;
            string Username = "TestUser";
            string Password = "TestPass";
            string Email = "TestEmail";
            string Phone = "TestPhone";
            string FirstName = "Test First Name";
            string LastName = "Test Last Name";
            Boolean isAdmin = false;
            Boolean isInstructor = true;
            User user = new User(ID, Username, Password, Email, Phone, FirstName, LastName, isAdmin, isInstructor);

            Assert.AreEqual(ID, user.ID);
            Assert.AreEqual(Username, user.Username);
            Assert.AreEqual(Password, user.Password);
            Assert.AreEqual(Email, user.Email);
            Assert.AreEqual(Phone, user.Phone);
            Assert.AreEqual(FirstName, user.FirstName);
            Assert.AreEqual(LastName, user.LastName);
            Assert.AreEqual(isAdmin, user.IsAdmin);
            Assert.AreEqual(isInstructor, user.IsInstructor);
        }

        [TestMethod]
        public void TestConstructor2()
        {
            string Username = "TestUser";
            string Password = "TestPass";
            string Email = "TestEmail";
            string Phone = "TestPhone";
            string FirstName = "Test First Name";
            string LastName = "Test Last Name";
            Boolean isAdmin = false;
            Boolean isInstructor = true;
            User user = new User(Username, Password, Email, Phone, FirstName, LastName, isAdmin, isInstructor);

            Assert.AreEqual(Username, user.Username);
            Assert.AreEqual(Password, user.Password);
            Assert.AreEqual(Email, user.Email);
            Assert.AreEqual(Phone, user.Phone);
            Assert.AreEqual(FirstName, user.FirstName);
            Assert.AreEqual(LastName, user.LastName);
            Assert.AreEqual(isAdmin, user.IsAdmin);
            Assert.AreEqual(isInstructor, user.IsInstructor);
        }

        [TestMethod]
        public void TestSetGetID()
        {
            User user = new User();
            int id = 1;
            user.ID = id;
            Assert.AreEqual(id, user.ID);
        }


        [TestMethod]
        public void TestSetGetUsername()
        {
            User user = new User();
            string test = "test";
            user.Username = test;
            Assert.AreEqual(test, user.Username);
        }

        [TestMethod]
        public void TestSetGetPassword()
        {
            User user = new User();
            string test = "test";
            user.Password = test;
            Assert.AreEqual(test, user.Password);
        }

        [TestMethod]
        public void TestSetGetEmail()
        {
            User user = new User();
            string test = "test";
            user.Email = test;
            Assert.AreEqual(test, user.Email);
        }

        [TestMethod]
        public void TestSetGetPhone()
        {
            User user = new User();
            string test = "test";
            user.Phone = test;
            Assert.AreEqual(test, user.Phone);
        }

        [TestMethod]
        public void TestSetGetFirstName()
        {
            User user = new User();
            string test = "test";
            user.FirstName = test;
            Assert.AreEqual(test, user.FirstName);
        }

        [TestMethod]
        public void TestSetGetLastName()
        {
            User user = new User();
            string test = "test";
            user.LastName = test;
            Assert.AreEqual(test, user.LastName);
        }

        [TestMethod]
        public void TestSetGetIsAdmin()
        {
            User user = new User();
            user.IsAdmin = true;
            Assert.AreEqual(true, user.IsAdmin);
            Assert.AreNotEqual(true, user.IsInstructor);
        }

        [TestMethod]
        public void TestSetGetIsInstructor()
        {
            User user = new User();
            user.IsInstructor = true;
            Assert.AreEqual(true, user.IsInstructor);
            Assert.AreNotEqual(true, user.IsAdmin);
        }

    }
}