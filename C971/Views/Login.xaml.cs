using C971.Data;
using C971.Models;

namespace C971.Views;

public partial class Login : ContentPage
{
    C971Database Database;
    private bool seeded = false;

    public Login()
	{
        Database = new C971Database();
        if (!seeded)
        {
            Database.SeedData();
            seeded=true;
        }
        InitializeComponent();
	}

    private async void Login_Clicked(object sender, EventArgs e)
    {
        Models.User user = Database.LoginUser(UsernameEditor.Text, PasswordEditor.Text);
        if (user != null )
        {
            if (user.IsAdmin)
            {
                await Navigation.PushAsync(new Admin(user));
            }
            else if (user.IsInstructor)
            {
                await Navigation.PushAsync(new CourseList(user));
            }
            else
            {
                await Navigation.PushAsync(new TermList(user.ID));
            }
        }
    }

    private async void Register_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(UserNew));
    }
}