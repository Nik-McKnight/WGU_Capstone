using C971.Data;
using C971.Models;

namespace C971.Views;


public partial class AdminReport : ContentPage
{
    Models.CourseList courseList;
    C971Database Database { get; set; }

    public AdminReport()
    {
        InitializeComponent();
        Database = new C971Database();
        courseList = new Models.CourseList(true);
        NumUsers();
        BindingContext = courseList;
        Time.Text = DateTime.Now.ToString("HH:mm:ss MMM dd, yyyy");

    }

    private void NumUsers()
    {
        foreach (Course c in courseList.Courses)
        {
            c.NumUsers();
        }
    }
}