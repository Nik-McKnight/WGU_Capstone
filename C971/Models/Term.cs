using C971.Data;
using Microsoft.Maui.Controls.Compatibility;
using SQLite;

namespace C971.Models
{
    // Inheritance
    public class Term : Dates
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }

        // Different Constructors - Polymorphism
        public Term()
        {
            ID= 0;
            UserId=0;
            Title="New Term";
        }

        public Term(int ID = 0, string Title = "New Term", DateTime? Start = null, DateTime? End = null, int userId = 0)
        {
            this.ID= ID;
            this.Title=Title;
            this.Start = Start.HasValue ? Start : DateTime.MinValue;
            this.End = End.HasValue ? End : DateTime.MinValue;
            UserId=userId;
            dateString();
        }

        public Term(string Title = "New Term", DateTime? Start = null, DateTime? End = null, int userId=0)
        {
            this.Title=Title;
            this.Start = Start.HasValue ? Start : DateTime.MinValue;
            this.End = End.HasValue ? End : DateTime.MinValue;
            UserId=userId;
            dateString();
        }
    }
}
