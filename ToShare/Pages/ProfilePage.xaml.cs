using ToShare.Services;

namespace ToShare.Pages;

public partial class ProfilePage : ContentPage
{
	private readonly LoginService login = new LoginService();
	public ProfilePage()
	{
        var uemail = Preferences.Get("useremail", string.Empty);
		InitializeComponent();
		GetProfileDetay(uemail);
	}

    private async void GetProfileDetay(string email)
    {
        var user = await login.GetUserByEmail(email);
       
        lblName.Text = user.UserName;
        lblSurname.Text = user.UserSurname;
        lblEmail.Text = user.UserEmail;
        lblphone.Text = user.UserPhone;
        lblincoming.Text = user.Salary.ToString();
        ImgProperty.Source = user.ProfilePhoto;
    }

    private void Add_button_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new AddPostPage());
    }
}