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
        string category = selectionPicker.SelectedItem as string;
        int categoryýd;
        if (category == "Food")
        {
           categoryýd = 1;
        } else if (category == "Clothes")
        {
            categoryýd = 2;
        }
        else if (category == "Scholl Stuff") 
        {
            categoryýd = 3;
        } else if(category == "Cleaning")
        {
            categoryýd = 4;
        }
        else
        {
            categoryýd= 5;
        }

        int count = int.Parse(Count.Text);	
		var userid = Preferences.Get("UserId", 0);
		var response = await post.AddPost(userid, categoryýd, Name.Text,Adress.Text,count, Description.Text, Image.Text, date);
        if (response != null)
        {
            await DisplayAlert("OK", "Your post have added", "Cancel");
        }
        else
        {
            await DisplayAlert("Error", "Oops something went wrong", "Cancel");
        }
        await Navigation.PushModalAsync(new ProfilePage());
    }
}