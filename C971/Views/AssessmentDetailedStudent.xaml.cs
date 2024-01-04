using C971.Data;
using C971.Models;
using Plugin.LocalNotification;

namespace C971.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class AssessmentDetailedStudent : ContentPage
{
    C971Database Database;
    private int AssessmentId;
    private int instructorId;
    private Models.User Instructor;

    public string ItemId
    {
        set { LoadAssessment(Int32.Parse(value)); }
    }
    public AssessmentDetailedStudent()
    {
        Database = new C971Database();
        InitializeComponent();
    }

    public AssessmentDetailedStudent(int id)
    {
        Database = new C971Database();
        InitializeComponent();
        LoadAssessment(id);
    }

    private void LoadAssessment(int id)
    {
        AssessmentId = id;
        Models.Assessment assessment = Database.GetAssessment(id);
        this.instructorId = assessment.InstructorId;
        this.Instructor = Database.GetUser(instructorId);
        Start.Text = assessment.Start.Value.ToString("MMM dd, yyyy");
        End.Text = assessment.End.Value.ToString("MMM dd, yyyy");
        NameEditor.Text = Instructor.FirstName + " " + Instructor.LastName;
        EmailEditor.Text = Instructor.Email;
        PhoneEditor.Text = Instructor.Phone;
        Category.Text = assessment.Category.ToString();
        BindingContext = assessment;
    }

    private void StartNotification_Clicked(object sender, EventArgs e)
    {
        Assessment assessment = Database.GetAssessment(AssessmentId);
        Course course = Database.GetCourse(assessment.CourseId);
        var request = new NotificationRequest
        {
            NotificationId = Int32.Parse("3000" + AssessmentId.ToString()),
            Title = course.Title + " - " + assessment.Title + ": Starts on " + assessment.Start.Value.ToString("MMM dd, yyyy"),
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
        Assessment assessment = Database.GetAssessment(AssessmentId);
        Course course = Database.GetCourse(assessment.CourseId);
        var request = new NotificationRequest
        {
            NotificationId = Int32.Parse("3000" + AssessmentId.ToString()),
            Title = course.Title + " - " + assessment.Title + ": Ends on " + assessment.End.Value.ToString("MMM dd, yyyy"),
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
}