using C971.Data;
using C971.Models;

namespace C971.Views;
public partial class TermNew : ContentPage
{
    C971Database Database;
    public int userId;

    public TermNew()
    {
        Database = new C971Database();
        InitializeComponent();
        LoadNewTerm();
    }

    public TermNew(int id)
	{
        userId = id;
        Database = new C971Database();
		InitializeComponent();
        LoadNewTerm();
	}

    private void LoadNewTerm()
    {
        Models.Term Term = new Term();
        BindingContext = Term;
    }

    private async void ChangeTerm_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Term term)
            try
            {
                if (string.IsNullOrWhiteSpace(TextEditor.Text) || TextEditor.Text == "You must enter a name.")
                {
                    TextEditor.Text = "You must enter a name.";
                    throw new Exception();
                }

                Database.InsertTerm(new Models.Term(TextEditor.Text, Start.Date, End.Date, this.userId));
                await Shell.Current.GoToAsync("..");
            }
            catch { }

    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}