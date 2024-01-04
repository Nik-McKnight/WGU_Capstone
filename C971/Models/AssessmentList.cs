using C971.Data;

namespace C971.Models
{
    public class AssessmentList
    {
        public List<Assessment> Assessments { get; set; } = new List<Assessment>();
        private C971Database Database;
        public int CourseId { get; set; }

        public int StudentId { get; set; }

        public AssessmentList()
        {
            Database = new C971Database();
            CourseId=0;
            StudentId=0;
        }

        public AssessmentList(int CourseId = 0, int StudentId = 0, C971Database db = null)
        {
            if (db == null)
            {
                Database = new C971Database();
            }
            else
            {
                Database = db;
            }
            this.CourseId=CourseId;
            this.StudentId=StudentId;
            LoadAssessments();
        }


        public void LoadAssessments()
        {
            try
            {
                this.Assessments.Clear();
            }
            catch { }

            List<Assessment> assessments = Database.GetAssessments(CourseId, StudentId);

            foreach (Assessment assessment in assessments)
            {
                assessment.dateString();
                Assessments.Add(assessment);
            };
        }

        public void LoadAssessments(int ID)
        {
            try
            {
                this.Assessments.Clear();
            }
            catch { }
            List<Assessment> Assessments = Database.GetAssessments(CourseId);
            foreach (Assessment Assessment in Assessments)
            {
                Assessments.Add(Assessment);
            };
        }
    }
}
