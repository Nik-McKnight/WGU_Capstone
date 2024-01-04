using C971.Data;

namespace C971.Models
{
    public class CourseList
    {
        public List<Course> Courses { get; set; } = new List<Course>();
        private C971Database Database;
        public int TermId {  get; set; }
        public CourseList()
        {
            Database = new C971Database();
            TermId=0;
        }

        public CourseList(int termId = 0, C971Database db = null, List<Course> courses = null)
        {
            if (db == null)
            {
                Database = new C971Database();
            }
            else
            {
                Database = db;
            }
            TermId=termId;
            if (courses != null)
            {
                this.Courses = courses;
            }
            else
            {
                LoadCourses();
            }
        }

        public CourseList(User Instructor, C971Database db = null)
        {
            if (db == null)
            {
                Database = new C971Database();
            }
            else
            {
                Database = db;
            }
            if (Instructor.IsAdmin)
            {
                LoadCourses(true);
            }
            else {
                LoadCourses(Instructor);
            }

        }

        public CourseList(bool IsAdmin, C971Database db = null)
        {
            if (db == null)
            {
                Database = new C971Database();
            }
            else
            {
                Database = db;
            }
            LoadCourses(IsAdmin);
        }

        public void LoadCourses()
        {
            try
            {
                Courses.Clear();
            }
            catch { }
            List<Course> courses = Database.GetCourses(TermId);
            foreach (Course course in courses)
            {
                course.dateString();
                Courses.Add(course);
            };
        }

        public void LoadCourses(int ID)
        {
            try
            {
                Courses.Clear();
            }
            catch { }
            List<Course> courses = Database.GetCourses();
            foreach (Course course in courses)
            {
                course.dateString();
                Courses.Add(course);
            };
        }

        public void LoadCourses(bool isAdmin)
        {
            try
            {
                Courses.Clear();
            }
            catch { }
            List<Course> courses;
            if (isAdmin) { courses = Database.GetCourses(); }
            else { courses = new List<Course>(); }
            foreach (Course course in courses)
            {
                course.dateString();
                Courses.Add(course);
            };
        }

        public void LoadCourses(User Instructor)
        {
            try
            {
                Courses.Clear();
            }
            catch { }

            List<Course> courses = Database.GetCourses(Instructor);
            foreach (Course course in courses)
            {
                course.dateString();
                Courses.Add(course);
            };
        }
    }
}
