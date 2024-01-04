using C971.Data;
using C971.Models;

namespace C971.Views;

//[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class UserNew : ContentPage
{
    C971Database Database;
    //public string ItemId
    //{
    //    set { LoadNewCourse(Int32.Parse(value)); }
    //}
    public UserNew()
	{
        Database = new C971Database();
		InitializeComponent();
	}

    public UserNew(int id)
    {
        Database = new C971Database();
        InitializeComponent();
        //LoadNewCourse(id);
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
                Database.InsertUser(new Models.User(0, UsernameEditor.Text, PasswordEditor.Text, EmailEditor.Text, 
                    PhoneEditor.Text, FirstEditor.Text, LastEditor.Text, false, false));
                await Shell.Current.GoToAsync("../..");
            }
            catch
            { 
            }
    }

}