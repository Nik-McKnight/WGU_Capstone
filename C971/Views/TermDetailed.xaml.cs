using C971.Data;
using C971.Models;

namespace C971.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class TermDetailed : ContentPage
{
    C971Database Database;
    public int termId;
    Term term;
    public string ItemId
    {
        set { LoadTerm(Int32.Parse(value));}
    }

    public TermDetailed()
    {
        Database = new C971Database();
        InitializeComponent();
        LoadTerm(1);
    }

    public TermDetailed(int id)
	{
        termId = id;
        Database = new C971Database();
		InitializeComponent();
        LoadTerm(termId);
	}

    private void LoadTerm(int id)
    {
        term = Database.GetTerm(id);
        term.ID = id;
        BindingContext = term;
    }


    private async void LoadCourses_Clicked(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync($"{nameof(CourseList)}?{nameof(CourseList.ItemId)}={termId}");
        await Navigation.PushAsync(new CourseList(this.term));
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

                Database.UpdateTerm(new Models.Term(termId, TextEditor.Text, Start.Date, End.Date, term.UserId));
                await Shell.Current.GoToAsync("..");
            }
            catch { }
    }

        private async void Delete_Clicked(object sender, EventArgs e)
    {
        Database.DeleteTerm(termId);
        await Shell.Current.GoToAsync("../..");
    }
}