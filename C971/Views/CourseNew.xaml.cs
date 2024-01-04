using C971.Data;
using C971.Models;

namespace C971.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class CourseNew : ContentPage
{
    C971Database Database;
    public Models.User Instructor {  get; set; }
    public string ItemId
    {
        set { LoadNewCourse(Int32.Parse(value)); }
    }
    public CourseNew()
	{
        Database = new C971Database();
		InitializeComponent();
	}

    public CourseNew(int InstructorId)
    {
        Database = new C971Database();
        InitializeComponent();
        LoadNewCourse(InstructorId);
    }

    private void LoadNewCourse(int id)
    {
        Models.Course course = new Course();
        course.Start =DateTime.Now;
        course.End =DateTime.Now;
        BindingContext = course;
        course.InstructorId = id;
        GetInstructor(id);
        NameEditor.Text = Instructor.FirstName + " " + Instructor.LastName;
        EmailEditor.Text = Instructor.Email;
        PhoneEditor.Text = Instructor.Phone;

    }

    public void GetInstructor(int instructorId)
    {
        this.Instructor = Database.GetUser(instructorId);
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
                Database.UpdateCourse(new Models.Course(CourseEditor.Text, Start.Date, End.Date, Instructor.ID));
                await Shell.Current.GoToAsync("../..");
            }
            catch
            { 
            }
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}