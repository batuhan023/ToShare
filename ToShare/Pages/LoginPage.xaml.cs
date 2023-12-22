
using ToShare.Services;

namespace ToShare.Pages;

public partial class LoginPage : ContentPage
{
    private readonly LoginService _loginService = new LoginService();
	public LoginPage()
	{
		InitializeComponent();
	}

    async void BtnLogin_Clicked(object sender, EventArgs e)
    {

        Application.Current.MainPage = new CustomTabbedPage();

        //var response = await _loginService.Login(EntEmail.Text, EntPassword.Text);
        //if (response != null)
        //{
        //    Application.Current.MainPage = new CustomTabbedPage();
        //}
        //else
        //{
        //    await DisplayAlert("", "Oops something went wrong", "Cancel");
        //}

    }

    async void TapJoinNow_Tapped(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new RegisterPage());
    }
}