using C971.Data;

namespace C971.Models
{
    public class NoteList
    {
        // Make list generic object?
        public List<Note> Notes { get; set; } = new List<Note>();
        private C971Database Database;
        public int CourseId;
        public int StudentId;


        public NoteList(int CourseId = 0, int StudentId = 0, C971Database db = null)
        {
            if (db == null) { 
                Database = new C971Database(); 
            }
            else
            {
                Database = db;
            }
            this.CourseId=CourseId;
            this.StudentId=StudentId;
            LoadNotes();
        }
        public NoteList()
        {
            this.CourseId=0;
            this.StudentId=0;
            Notes = new List<Note>();
        }

        public void LoadNotes()
        {
            try
            {
                this.Notes.Clear();
            }
            catch { }
            List<Note> notes = Database.GetNotes(CourseId, StudentId);
            foreach (Note note in notes)
            {
                Notes.Add(note);
            };
        }

        public void LoadNotes(int courseId, int studentId)
        {
            try
            {
                this.Notes.Clear();
            }
            catch { }
            List<Note> notes = Database.GetNotes(CourseId, studentId);
            foreach (Note note in notes)
            {
                Notes.Add(note);
            };
        }
    }
}