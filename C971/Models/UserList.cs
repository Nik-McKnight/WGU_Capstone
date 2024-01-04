using C971.Data;

namespace C971.Models
{
    public class UserList
    {
        public List<User> Users { get; set; }
        private C971Database Database;
        public int CourseId { get; set; }
        public UserList()
        {
            Database = new C971Database();
            CourseId=0;
            Users = new List<User>();
        }

        public UserList(int CourseId = 0, C971Database db = null)
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
            Users = new List<User>();
            if (CourseId == 0)
            {
                LoadUsers();
            }
            else
            {
                LoadUsers(CourseId);
            }
        }

        public void LoadUsers()
        {
            try
            {
                this.Users.Clear();
            }
            catch { }
            List<User> users = Database.GetUsers();
            foreach (User user in users)
            {
                Users.Add(user);
            };
        }

        public void LoadUsers(int courseId)
        {
            try
            {
                this.Users.Clear();
            }
            catch { }
            List<User> users = Database.GetUsers(courseId);
            foreach (User user in users)
            {
                Users.Add(user);
            };
        }
    }
}
