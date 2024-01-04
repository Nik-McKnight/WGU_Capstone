using C971.Data;
using C971.Models;

namespace C971.Views;

public partial class UserList : ContentPage
{
    C971Database Database { get; set; }
    int courseId;
    bool isInstructor;
    bool isAdmin;
    public UserList()
    {
        InitializeComponent();
        Database = new C971Database();
        BindingContext = new Models.CourseList(2);
    }

    public UserList(int courseId= 0, bool isInstructor = false, bool isAdmin = false)
    {
        InitializeComponent();
        Database = new C971Database();
        this.courseId = courseId;
        BindingContext = new Models.UserList(this.courseId);
        this.isInstructor=isInstructor;
        this.isAdmin=isAdmin;
        Add.IsEnabled=false;
        if (this.isAdmin)
        {
            Add.IsEnabled=true;
            Add.IconImageSource="{FontImage Glyph='+', Color=White, Size=22}";
            Add.Text="Add";
        }
     }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserNew());
    }

    private async void usersCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            var user = (Models.User)e.CurrentSelection[0];

            if (this.isInstructor && !this.isAdmin)
            {
                await Navigation.PushAsync(new UserNoEdits(user.ID));
            }

            else
            {
                await Navigation.PushAsync(new User(user.ID));
            }


            usersCollection.SelectedItem = null;
        }
    }

}