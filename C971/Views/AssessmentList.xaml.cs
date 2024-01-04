using C971.Data;
using C971.Models;

namespace C971.Views;

public partial class AssessmentList : ContentPage
{
    C971Database Database { get; set; }
    public int courseId;
    public int studentId;
    private Models.User user { get; set; }
    public AssessmentList()
    {
        InitializeComponent();
        Database = new C971Database();
        BindingContext = new Models.AssessmentList(2);
    }

    public AssessmentList(int courseId, int studentId =0)
    {
        InitializeComponent();
        Database = new C971Database();
        this.courseId = courseId;
        this.studentId = studentId;
        try
        {
            this.user = Database.GetUser(this.studentId);
        }
        catch { }
        if (this.user.IsInstructor || this.user.IsAdmin)
        {
            BindingContext = new Models.AssessmentList(courseId);
            if (this.user.IsAdmin) {
                Add.Text = "";
                Add.IconImageSource=null;
                Add.IsEnabled = false;

            }
        }
        else
        {
            BindingContext = new Models.AssessmentList(courseId, studentId);
            Add.Text = "";
            Add.IconImageSource=null;
            Add.IsEnabled = false;

        }
    }


    protected override void OnAppearing()
    {
        ((Models.AssessmentList)BindingContext).LoadAssessments();
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AssessmentNew(courseId, user.ID));
    }

    private async void AssessmentsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            var Assessment = (Models.Assessment)e.CurrentSelection[0];

            if (user.IsInstructor)
            {
                await Navigation.PushAsync(new AssessmentDetailed(Assessment.ID));
            }

            else
            {
                await Navigation.PushAsync(new AssessmentDetailedStudent(Assessment.ID));
            }

            AssessmentsCollection.SelectedItem = null;
        }
    }

}