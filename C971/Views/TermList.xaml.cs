using C971.Data;
using C971.Models;
using Plugin.LocalNotification;

namespace C971.Views;

public partial class TermList : ContentPage
{
    C971Database Database { get; set; }
    int userId;
	public TermList()
	{
        InitializeComponent();
        BindingContext = new Models.TermList();
    }

    public TermList(int userId)
    {
        InitializeComponent();
        this.userId = userId;
        BindingContext = new Models.TermList(this.userId);
    }

    protected override void OnAppearing()
    {
        ((Models.TermList)BindingContext).LoadTerms(userId);
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TermNew(this.userId));
    }

    private void test()
    {
        var request = new NotificationRequest
        {
            NotificationId = 1,
            Title = "Test",
            Subtitle = "Test Again",
            Description = "hgreahreahera",
            BadgeNumber = 10,
            CategoryType = NotificationCategoryType.Alarm,
            Schedule = new NotificationRequestSchedule()
            {
                NotifyTime = DateTime.Now.AddSeconds(3),
            },
        };

        LocalNotificationCenter.Current.Show(request);
    }

    private async void termsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            // Get the note model
            var term = (Models.Term)e.CurrentSelection[0];

            // Should navigate to "NotePage?ItemId=path\on\device\XYZ.notes.txt"
            //await Shell.Current.GoToAsync($"{nameof(TermDetailed)}?{nameof(TermDetailed.ItemId)}={term.ID}");
            await Navigation.PushAsync(new TermDetailed(term.ID));
            //await Shell.Current.GoToAsync("MainPage2");

            // Unselect the UI
            termsCollection.SelectedItem = null;
        }
    }


    private void Notification_Clicked(object sender, EventArgs e)
    {
        var request = new NotificationRequest
        {
            NotificationId = 1,
            Title = "Test",
            Subtitle = "Test Again",
            Description = "hgreahreahera",
            BadgeNumber = 10,
            CategoryType = NotificationCategoryType.Alarm,
            Schedule = new NotificationRequestSchedule()
            {
                NotifyTime = DateTime.Now.AddSeconds(3),
            },
        };

        LocalNotificationCenter.Current.Show(request);
    }

    private async void Profile_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new User(this.userId));
    }
}