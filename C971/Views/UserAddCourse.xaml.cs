using C971.Data;
using C971.Models;

namespace C971.Views;


public partial class UserAddCourse : ContentPage
{
    C971Database Database;
    DateTime? Start;
    DateTime? End;
    int userId;
    int termId;
    public UserAddCourse()
	{
        Database = new C971Database();
		InitializeComponent();
	}

    public UserAddCourse(int termId, DateTime? start, DateTime? end, int userId)
    {
        Database = new C971Database();
        Database.Init();
        InitializeComponent();
        this.termId = termId;
        this.End = end;
        this.Start = start;
        this.userId = userId;
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
        try
        {
            List<Course> courses = Database.SearchCourses(SearchEditor.Text, Start, End, userId);
            await Navigation.PushAsync(new CourseList(termId, userId, courses));
        }
        catch
        { 
        }
    }

}