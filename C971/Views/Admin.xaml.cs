using C971.Data;
using C971.Models;

namespace C971.Views;


public partial class Admin : ContentPage
{
    Models.User user;

    public Admin()
    {
        InitializeComponent();
    }

    public Admin(Models.User user)
	{
		InitializeComponent();
        this.user = user;
	}

    private async void LoadCourses_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CourseList(user));
    }

    private async void LoadUsers_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UserList(0, false, true));
    }

    private async void CReport_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdminReport());
    }

    private async void Profile_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new User(this.user.ID));
    }
}