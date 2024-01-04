using C971.Data;
using C971.Models;
using Plugin.LocalNotification;

namespace C971.Views;

public partial class CourseDetailedStudent : ContentPage
{
    C971Database Database;
    private int courseId;
    private int userId;
    private int instructorId;
    private Models.User Instructor;
    private bool addCourse;
    private int termId;
    public CourseDetailedStudent()
	{
        Database = new C971Database();
		InitializeComponent();
	}

    public CourseDetailedStudent(int courseId, int userId = 0, int instructorId = 0, bool addCourse = false, int termId=0)
    {
        Database = new C971Database();
        InitializeComponent();
        LoadCourse(courseId);
        this.userId=userId;
        this.courseId=courseId;
        this.instructorId=instructorId;
        this.addCourse=addCourse;
        this.termId=termId;
        if (addCourse )
        {
            Assessments.IsEnabled = false;
            Assessments.IsVisible = false;
            Notes.IsEnabled = false;
            Notes.IsVisible = false;
            StartButton.IsEnabled = false;
            StartButton.IsVisible = false;
            EndButton.IsEnabled = false;
            EndButton.IsVisible = false;
            Add.IsEnabled = true;
            Add.IsVisible = true;
            Drop.IsEnabled = false;
            Drop.IsVisible = false;
        }
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
        Start.Text = Course.Start.Value.ToString("MMM dd, yyyy");
        End.Text = Course.End.Value.ToString("MMM dd, yyyy");
        BindingContext = Course;
    }
    public void GetInstructor()
    {
        this.Instructor = Database.GetUser(this.instructorId);
    }

    private async void LoadAssessments_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AssessmentList(this.courseId, this.userId));
    }

    private async void LoadNotes_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NoteList(this.courseId, this.userId));
    }

    private void StartNotification_Clicked(object sender, EventArgs e)
    {
        Course course = Database.GetCourse(courseId);
        var request = new NotificationRequest
        {
            NotificationId = Int32.Parse("1000" + courseId.ToString()),
            Title = course.Title + ": Starts on " + course.Start.Value.ToString("MMM dd, yyyy"),
            Subtitle = "Start date reminder",
            Description = "You will be reminded every day.",
            BadgeNumber = 10,
            CategoryType = NotificationCategoryType.Alarm,
            Schedule = new NotificationRequestSchedule()
            {
                NotifyTime = DateTime.Now,
                NotifyRepeatInterval = TimeSpan.FromDays(1),
            },
        };

        LocalNotificationCenter.Current.Show(request);
    }

    private void EndNotification_Clicked(object sender, EventArgs e)
    {
        Course course = Database.GetCourse(courseId);
        var request = new NotificationRequest
        {
            NotificationId = Int32.Parse("2000" + courseId.ToString()),
            Title = course.Title + ": Ends on " + course.End.Value.ToString("MMM dd, yyyy"),
            Subtitle = "End date reminder",
            Description = "You will be reminded every day.",
            BadgeNumber = 10,
            CategoryType = NotificationCategoryType.Alarm,
            Schedule = new NotificationRequestSchedule()
            {
                NotifyTime = DateTime.Now,
                NotifyRepeatInterval = TimeSpan.FromDays(1),
            },
        };

        LocalNotificationCenter.Current.Show(request);
    }

    private async void AddCourse_Clicked(object sender, EventArgs e)
    {
        try
        {
            int cu = Database.InsertCourseUser(new CourseUser(0, courseId, userId));
            int tc = Database.InsertTermCourse(new TermCourse(0, courseId, termId));
            if (cu == 1 && tc == 1)
            {
                await Shell.Current.GoToAsync("../../../..");
            }
        }
        catch { }
    }
    private async void DropCourse_Clicked(object sender, EventArgs e)
    {
        try
        {
            CourseUser cu = Database.GetCourseUser(courseId, userId);
            TermCourse tc = Database.GetTermCourse(courseId, termId);
            int r1 = Database.DeleteCourseUser(cu);
            int r2 = Database.DeleteTermCourse(tc);
            if (r1 == 1 && r2 == 1)
            {
                await Shell.Current.GoToAsync("../..");
            }
        }
        catch { }
    }
}