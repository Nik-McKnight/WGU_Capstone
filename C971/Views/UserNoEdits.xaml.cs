using C971.Data;
using C971.Models;

namespace C971.Views;

public partial class UserNoEdits : ContentPage
{
    C971Database Database;
    int userId;
    Models.User user;
    public UserNoEdits()
	{
        Database = new C971Database();
		InitializeComponent();
	}

    public UserNoEdits(int id=0)
    {
        Database = new C971Database();
        this.userId = id;
        this.user = Database.GetUser(id);
        InitializeComponent();
        BindingContext = this.user;
    }


}