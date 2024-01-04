using SQLite;

namespace C971.Models
{
    // Inheritance
    public class Assessment : Dates
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public Category Category { get; set; }
        public int InstructorId { get; set; }
        public int StudentId { get; set; }

        // Different Constructors - Polymorphism
        public Assessment()
        {
            ID = 0;
            Title = "New Assessment";
            CourseId = 0;
            Category = Category.Objective;
            InstructorId = 0;
            StudentId = 0;
        }

        public Assessment(int ID = 0, string Title = "New Assessment", DateTime? Start = null, DateTime? End = null,
            int CourseId = 0, Category Category = Category.Objective, int InstructorId = 0, int StudentId = 0)
        {
            this.ID = ID;
            this.Title = Title;
            this.Start = Start.HasValue ? Start : DateTime.MinValue;
            this.End = End.HasValue ? End : DateTime.MinValue;
            dateString();
            this.CourseId = CourseId;
            this.Category = Category;
            this.InstructorId = InstructorId;
            this.StudentId = StudentId;
        }

        public Assessment(string Title = "New Assessment", DateTime? Start = null, DateTime? End = null,
    int CourseId = 0, Category Category = Category.Objective, int InstructorId = 0, int StudentId = 0)
        {
            this.Title = Title;
            this.Start = Start.HasValue ? Start : DateTime.MinValue;
            this.End = End.HasValue ? End : DateTime.MinValue;
            dateString();
            this.CourseId = CourseId;
            this.Category = Category;
            this.InstructorId = InstructorId;
            this.StudentId = StudentId;
        }

    }
}
