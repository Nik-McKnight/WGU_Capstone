using C971.Data;
using C971.Models;

namespace C971.Views;

public partial class CourseList : ContentPage
{
    C971Database Database { get; set; }
    int termId;
    int userId;
    Term term;
    bool addCourse = false;

    private Models.User user {  get; set; }
    public CourseList()
    {
        InitializeComponent();
        Database = new C971Database();
        BindingContext = new Models.CourseList(0);
    }

    public CourseList(int termId, int userId=0, List<Course> courses = null)
    {
        InitializeComponent();
        Database = new C971Database();
        this.termId = termId;
        this.userId = userId;
        this.user = Database.GetUser(this.userId);
        this.term = Database.GetTerm(this.termId);
        if (courses != null)
        {
            BindingContext = new Models.CourseList(termId, null, courses);
            Add.IsEnabled = false;
            Add.Text = "";
            Add.IconImageSource = null;
            addCourse = true;
        }
        else
        {
            BindingContext = new Models.CourseList(termId);
        }
    }

    public CourseList(Term term)
    {
        InitializeComponent();
        Database = new C971Database();
        this.termId = term.ID;
        this.userId = term.UserId;
        this.user = Database.GetUser(this.userId);
        BindingContext = new Models.CourseList(termId);
        Profile.IsEnabled=false;
        this.term = term;
    }

    public CourseList(Models.User instructor)
    {
        InitializeComponent();
        Database = new C971Database();
        this.user = instructor;
        this.termId = 0;
        this.userId = instructor.ID;
        BindingContext = new Models.CourseList(instructor);
        Add.IsEnabled=true;
        Add.Text="Add";
        Add.IsEnabled=false;
        Profile.IsEnabled=false;

        if (instructor.IsInstructor)
        {
            Profile.IsEnabled=true;
            Profile.Text="Profile";
            Add.IsEnabled=false;
            Add.Text="";
        }
    }


    private async void Add_Clicked(object sender, EventArgs e)
    {
        if (this.user.IsAdmin)
        {
            await Navigation.PushAsync(new CourseNew(13));
        }
        else
        {
            await Navigation.PushAsync(new UserAddCourse(term.ID, term.Start, term.End, userId));
        }

    }

    private async void coursesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            var course = (Models.Course)e.CurrentSelection[0];

            if (user.IsAdmin)
            {
                await Navigation.PushAsync(new CourseDetailed(course.ID, this.userId, this.userId, true));
            }

            else if (user.IsInstructor)
            {
                await Navigation.PushAsync(new CourseDetailed(course.ID, this.userId, course.InstructorId));
            }


            else
            {
                await Navigation.PushAsync(new CourseDetailedStudent(course.ID, this.userId, course.InstructorId, this.addCourse, this.termId));
            }


            coursesCollection.SelectedItem = null;
        }
    }
    private async void Profile_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new User(this.userId));
    }

}