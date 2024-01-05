using ToShare.Services;
using ToShare.ViewModel;

namespace ToShare.Pages;

public partial class AddPostPage : ContentPage
{
	private readonly PostService post = new PostService();
	public AddPostPage()
	{
		InitializeComponent();
	}

    private async void BtnLogin_Clicked(object sender, EventArgs e)
    {
        //DateViewModel dateViewModel = (DateViewModel)BindingContext;
        //DateTime selectedDate = dateViewModel.SelectedDate;
        DateTime date = datePicker.Date;
		int count = int.Parse(Count.Text);
		int categoryýd = 1;
		var userid = Preferences.Get("UserId", 0);
		var response = await post.AddPost(userid, categoryýd, Name.Text,Adress.Text,count, Description.Text, Image.Text, date);
        if (response != null)
        {
            await DisplayAlert("", "Your post have added", "Cancel");
        }
        else
        {
            await DisplayAlert("", "Oops something went wrong", "Cancel");
        }
    }
}