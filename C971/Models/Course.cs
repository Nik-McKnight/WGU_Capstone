using C971.Data;
using SQLite;

namespace C971.Models
{
    // Inheritance
    public class Course : Dates
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public int InstructorId { get; set; }

        [Ignore]
        public User Instructor { get; set; }
        private C971Database db;
        public string InsName;
        public int numUsers { get; set; }

        // Different Constructors - Polymorphism
        public Course()
        {
            ID = 0;
            Title = "New Course";
            InstructorId = 0;
        }

        public Course(int ID = 0, string Title = "New Course", DateTime? Start = null, DateTime? End = null,
            int InstructorId = 0, C971Database db = null)
        {
            if (db == null)
            {
                this.db = new C971Database();
            }
            else
            {
                this.db = db;
            }
            this.ID = ID;
            this.Title = Title;
            this.Start = Start.HasValue ? Start : DateTime.MinValue;
            this.End = End.HasValue ? End : DateTime.MinValue;
            this.InstructorId = InstructorId;
            this.numUsers=0;
            dateString();
        }

        public Course(string Title = "New Course", DateTime? Start = null, DateTime? End = null,
        int InstructorId = 0, C971Database db = null)
        {
            if (db == null)
            {
                this.db = new C971Database();
           
            }
            else
            {
                this.db = db;
            }
            this.Title = Title;
            this.Start = Start.HasValue ? Start : DateTime.MinValue;
            this.End = End.HasValue ? End : DateTime.MinValue;
            this.InstructorId = InstructorId;
            this.numUsers=0;
            dateString();
        }

        public void GetInstructor()
        {
            db = new C971Database();
            this.Instructor = db.GetUser(this.InstructorId);
            this.InsName = Instructor.FirstName + " " + Instructor.LastName;
        }

        public void NumUsers()
        {
            db = new C971Database();
            this.numUsers = db.GetUsers(this.ID).Count;
        }
    }
}
