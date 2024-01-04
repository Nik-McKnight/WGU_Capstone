using C971.Data;
using C971.Models;

namespace C971.Views;

public partial class User : ContentPage
{
    C971Database Database;
    int userId;
    Models.User user;
    public User()
	{
        Database = new C971Database();
		InitializeComponent();
	}

    public User(int id=0)
    {
        Database = new C971Database();
        this.userId = id;
        this.user = Database.GetUser(id);
        InitializeComponent();
        BindingContext = this.user;
    }

    private async void Submit_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.User user)
            try
            {
                if (string.IsNullOrWhiteSpace(UsernameEditor.Text) || UsernameEditor.Text == "You must enter a username.")
                {
                    UsernameEditor.Text = "You must enter a username.";
                    throw new Exception();
                }
                if (string.IsNullOrWhiteSpace(PasswordEditor.Text) || PasswordEditor.Text == "You must enter a password.")
                {
                    PasswordEditor.Text = "You must enter a password.";
                    throw new Exception();
                }
                if (string.IsNullOrWhiteSpace(ConfirmEditor.Text) || ConfirmEditor.Text == "You must enter a matching password." || PasswordEditor.Text != ConfirmEditor.Text)
                {
                    ConfirmEditor.Text = "You must enter a matching password.";
                    throw new Exception();
                }
                if (string.IsNullOrWhiteSpace(EmailEditor.Text) || EmailEditor.Text == "You must enter an email address.")
                {
                    EmailEditor.Text = "You must enter an email address.";
                    throw new Exception();
                }
                if (string.IsNullOrWhiteSpace(PhoneEditor.Text) || PhoneEditor.Text == "You must enter a phone number.")
                {
                    PhoneEditor.Text = "You must enter a phone number.";
                    throw new Exception();
                }
                if (string.IsNullOrWhiteSpace(FirstEditor.Text) || FirstEditor.Text == "You must enter a first name.")
                {
                    FirstEditor.Text = "You must enter a first name.";
                    throw new Exception();
                }
                if (string.IsNullOrWhiteSpace(LastEditor.Text) || LastEditor.Text == "You must enter a last name.")
                {
                    LastEditor.Text = "You must enter a last name.";
                    throw new Exception();
                }
                Database.UpdateUser(new Models.User(this.userId, UsernameEditor.Text, PasswordEditor.Text, EmailEditor.Text, 
                    PhoneEditor.Text, FirstEditor.Text, LastEditor.Text, this.user.IsAdmin, this.user.IsInstructor));
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
        Database.DeleteUser(user);
        await Shell.Current.GoToAsync("../..");
    }

}