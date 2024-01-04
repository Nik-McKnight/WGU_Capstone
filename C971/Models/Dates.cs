using SQLite;

namespace C971.Models
{
    public class Dates
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        [Ignore]
        public string DateString { get; set; }

        public Dates()
        {
            Start = DateTime.MinValue;
            End = DateTime.MinValue;
            dateString();
        }

        public void dateString()
        {
            this.DateString = Start.Value.ToString("MMM dd, yyyy") + " - " + End.Value.ToString("MMM dd, yyyy");
        }
    }
}
