using C971.Data;

namespace C971.Views;

public partial class NoteList : ContentPage
{
    C971Database Database { get; set; }
    int courseId;
    int studentId;

    public NoteList()
    {
        InitializeComponent();
        Database = new C971Database();
        BindingContext = new Models.NoteList(2);
    }

    public NoteList(int courseId, int studentId =0)
    {
        this.courseId = courseId;
        this.studentId = studentId;
        InitializeComponent();
        Database = new C971Database();
        BindingContext = new Models.NoteList(this.courseId, this.studentId);
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NoteNew(this.courseId, this.studentId));
    }

    private async void NotesCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var Note = (Models.Note)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            await Navigation.PushAsync(new Note(Note.ID));
            //await Shell.Current.GoToAsync("MainPage2");

            // Unselect the UI
            NotesCollection.SelectedItem = null;
        }
    }

}