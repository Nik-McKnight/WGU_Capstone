using C971.Data;
using C971.Models;
using Microsoft.Extensions.Logging.Abstractions;

namespace C971.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class AssessmentNew : ContentPage
{
    C971Database Database;
    private Category category;
    private int courseId;
    private int instructorId;
    private Models.User Instructor;

    public string ItemId
    {
        set { LoadNewAssessment(Int32.Parse(value)); }
    }
    public AssessmentNew()
    {
        Database = new C971Database();
        InitializeComponent();
        //LoadNewAssessment();
    }

    public AssessmentNew(int id, int instructorId = 0)
    {
        Database = new C971Database();
        Database.Init();
        InitializeComponent();
        LoadNewAssessment(id, instructorId);
    }


    private void LoadNewAssessment(int courseId, int instructorId=0)
    {
        Models.Assessment assessment = new Assessment(0,null,null,null,courseId, Category.Objective,instructorId);
        assessment.Start =DateTime.Now;
        assessment.End =DateTime.Now;
        this.courseId = courseId;
        BindingContext = assessment;
        this.instructorId = assessment.InstructorId;
        this.Instructor = Database.GetUser(instructorId);
        Picker.SelectedItem = Category.Objective;
        NameEditor.Text = Instructor.FirstName + " " + Instructor.LastName;
        EmailEditor.Text = Instructor.Email;
        PhoneEditor.Text = Instructor.Phone;
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
                Database.InsertAssessment(new Models.Assessment(AssessmentEditor.Text, Start.Date, End.Date, courseId, category), courseId);
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