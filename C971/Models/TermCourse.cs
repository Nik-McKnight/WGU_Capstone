using SQLite;


namespace C971.Models
{
    public class TermCourse
    {
    [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int CourseId { get; set; }
        public int TermId {  get; set; }

        public TermCourse()
        {
            ID = 0;
            CourseId = 0;
            TermId = 0;
        }

        public TermCourse(int ID = 0, int CourseId = 0, int TermId = 0)
        {
            this.ID = ID;
            this.CourseId = CourseId;
            this.TermId = TermId;
        }
    }
}
