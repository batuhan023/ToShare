namespace ToShare.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    async void BtnRegister_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushModalAsync(new LoginPage());

    }

    async void TapLogin_Tapped(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new LoginPage());

    }
}