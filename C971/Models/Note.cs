using SQLite;

namespace C971.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }

        // Different Constructors - Polymorphism
        public Note()
        {
            ID = 0;
            Text = "New Note";
            CourseId = 0;
            StudentId = 0;
        }

        public Note(int ID = 0, string Text = "New Note", int CourseId = 0, int StudentId = 0)
        {
            this.ID = ID;
            this.Text = Text;
            this.CourseId = CourseId;
            this.StudentId = StudentId;
        }

        public Note(string Text = "New Note", int CourseId = 0, int StudentId = 0)
        {
            this.Text = Text;
            this.CourseId = CourseId;
            this.StudentId = StudentId;
        }
    }
}
