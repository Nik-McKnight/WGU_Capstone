using C971.Data;
using C971.Models;

namespace C971.Views;

public partial class NoteNew : ContentPage
{
    C971Database Database;
    private int courseId;
    private int studentId;
    public NoteNew()
    {
        Database = new C971Database();
        InitializeComponent();
    }

    public NoteNew(int courseId, int studentId)
    {
        Database = new C971Database();
        this.courseId = courseId;
        this.studentId = studentId;
        InitializeComponent();
        LoadNewNote();
    }

    private void LoadNewNote()
    {
        Models.Note note = new Models.Note(null, courseId, studentId);
        BindingContext = note;
    }

    private async void ChangeNote_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note Note)
            try
            {
                if (string.IsNullOrWhiteSpace(NoteEditor.Text) || NoteEditor.Text == "You must enter a note.")
                {
                    throw new Exception("You must enter a note.");
                }
                Database.InsertNote(new Models.Note(NoteEditor.Text, courseId, studentId));
                await Shell.Current.GoToAsync("../..");
            }
            catch (Exception ex)
            {
                NoteEditor.Text = ex.Message;
            }
    }
    private async void Delete_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("../..");
    }
}