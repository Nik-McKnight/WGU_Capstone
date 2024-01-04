using SQLite;


namespace C971.Models
{
    public class CourseUser
    {
    [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int CourseId { get; set; }
        public int StudentId {  get; set; }

        public CourseUser()
        {
            ID = 0;
            CourseId = 0;
            StudentId = 0;
        }

        public CourseUser(int ID = 0, int CourseId = 0, int StudentId = 0)
        {
            this.ID = ID;
            this.CourseId = CourseId;
            this.StudentId = StudentId;
        }
    }
}
