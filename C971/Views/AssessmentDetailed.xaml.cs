using C971.Data;
using C971.Models;
using Plugin.LocalNotification;

namespace C971.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class AssessmentDetailed : ContentPage
{
    C971Database Database;
    private int AssessmentId;
    private Category category;
    private int instructorId;
    private Models.User Instructor;
    private int studentId;

    public string ItemId
    {
        set { LoadAssessment(Int32.Parse(value)); }
    }
    public AssessmentDetailed()
    {
        Database = new C971Database();
        InitializeComponent();
    }

    public AssessmentDetailed(int id)
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
        NameEditor.Text = Instructor.FirstName + " " + Instructor.LastName;
        EmailEditor.Text = Instructor.Email;
        PhoneEditor.Text = Instructor.Phone;
        category = assessment.Category;
        BindingContext = assessment;
        Picker.SelectedItem = assessment.Category.ToString();
        this.studentId = assessment.StudentId;
    }

    private async void ChangeAssessment_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Assessment Assessment)
            try
            {
                if (string.IsNullOrWhiteSpace(AssessmentEditor.Text) || AssessmentEditor.Text == "You must enter a name.")
                {
                    AssessmentEditor.Text = "You must enter a name.";
                    throw new Exception();
                }
                if (Picker.SelectedItem == null || Picker.SelectedItem.ToString() == "You must choose a category")
                {
                    Picker.SelectedItem = "You must choose a category";
                    throw new Exception();
                }
                Enum.TryParse<Category>(Picker.SelectedItem.ToString(), out category);
                Database.UpdateAssessment(new Models.Assessment(AssessmentId, AssessmentEditor.Text, Start.Date, End.Date, Assessment.CourseId, category, instructorId, studentId));
                await Shell.Current.GoToAsync("../..");
            }
            catch 
            {
            }
    }
    private async void Delete_Clicked(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync($"{nameof(CourseList)}?{nameof(CourseList.ItemId)}={termId}");
        //await Navigation.PushAsync(new CourseList(this.termId));
        Database.DeleteAssessment(AssessmentId);
        await Shell.Current.GoToAsync("../..");
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