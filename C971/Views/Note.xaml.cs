using C971.Data;
using C971.Models;
using static System.Net.Mime.MediaTypeNames;

namespace C971.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class Note : ContentPage
{
    C971Database Database;
    private int NoteId;
    private Models.Note note {  get; set; }
    public string ItemId
    {
        set { LoadNote(Int32.Parse(value)); }
    }
    public Note()
    {
        Database = new C971Database();
        InitializeComponent();
    }

    public Note(int id)
    {
        Database = new C971Database();
        InitializeComponent();
        LoadNote(id);
    }

    private void LoadNote(int id)
    {
        NoteId = id;
        this.note = Database.GetNote(id);
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
                Database.UpdateNote(new Models.Note(Note.ID, NoteEditor.Text, Note.CourseId, note.StudentId));
                await Shell.Current.GoToAsync("../..");
            }
            catch (Exception ex)
            {
                NoteEditor.Text = ex.Message;
            }
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        //await Shell.Current.GoToAsync($"{nameof(CourseList)}?{nameof(CourseList.ItemId)}={termId}");
        //await Navigation.PushAsync(new CourseList(this.termId));
        Database.DeleteNote(NoteId);
        await Shell.Current.GoToAsync("../..");
    }

    private async void ShareNote_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note Note)
            try
            {
                await Share.Default.RequestAsync(new ShareTextRequest
                {
                    Text = NoteEditor.Text,
                    Title = "Shared Note"
                });
            }
            catch
            {
            }
    }
}