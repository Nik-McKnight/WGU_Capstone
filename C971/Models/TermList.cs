using C971.Data;
using System.Collections.ObjectModel;
using SQLite;

namespace C971.Models
{
    public class TermList
    {
        public ObservableCollection<Term> Terms { get; set; } = new ObservableCollection<Term>();
        private C971Database Database;
        public int userId { get; set; }

        public TermList()
        {
            Database = new C971Database();
            userId = 0;
        }

        public TermList(int userId, C971Database db = null)
        {
            if (db == null)
            {
                Database = new C971Database();
            }
            else
            {
                Database = db;
            }
            this.userId = userId;
            LoadTerms(userId);
        }

        public void LoadTerms(int userId = 0)
        {           
            Terms.Clear();
            List<Term> terms = Database.GetTerms(userId);
            foreach (Term term in terms)
            {
                term.dateString();
                Terms.Add(term);
            };
        }
    }
}