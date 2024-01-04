using C971.Data;
using C971.Models;
using Plugin.LocalNotification;

namespace C971.Views;

public partial class CourseDetailed : ContentPage
{
    C971Database Database;
    private int courseId;
    private int userId;
    private int instructorId;
    private bool admin;
    private Models.User Instructor;
    public CourseDetailed()
	{
        Database = new C971Database();
		InitializeComponent();
	}

    public CourseDetailed(int courseId, int userId = 0, int instructorId=0, bool admin=false)
    {
        Database = new C971Database();
        InitializeComponent();
        this.userId=userId;
        this.courseId=courseId;
        this.instructorId=instructorId;
        this.admin = admin;
        LoadCourse(courseId);
    }

    private void LoadCourse(int id)
    {
        courseId = id;
        Models.Course Course = Database.GetCourse(id);
        this.instructorId = Course.InstructorId;
        GetInstructor();
        NameEditor.Text = Instructor.FirstName + " " + Instructor.LastName;
        EmailEditor.Text = Instructor.Email;
        PhoneEditor.Text = Instructor.Phone;
        BindingContext = Course;
        if (!admin)
        {
            Submit.IsEnabled = false;
            Submit.IsVisible = false;
            Delete.IsEnabled = false;
            Delete.IsVisible = false;
            CourseEditor.IsEnabled = false;
            Start.IsEnabled = false;
            End.IsEnabled = false;
        }
    }
    public void GetInstructor()
    {
        this.Instructor = Database.GetUser(this.instructorId);
    }

    private async void ChangeCourse_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Course course)
            try
            {
                if (string.IsNullOrWhiteSpace(CourseEditor.Text) || CourseEditor.Text == "You must enter a name.")
                {
                    CourseEditor.Text = "You must enter a name.";
                    throw new Exception();
                }
                Database.UpdateCourse(new Models.Course(courseId, CourseEditor.Text, Start.Date, End.Date, course.InstructorId));
                await Shell.Current.GoToAsync("../..");
            }
            catch
            { 
            }
    }

    private async void LoadAssessments_Clicked(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new AssessmentList(this.courseId, this.userId));

    }

    private async void LoadStudents_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserList(this.courseId, Instructor.IsInstructor, admin));
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        Database.DeleteCourse(courseId);
        await Shell.Current.GoToAsync("../..");
    }
}