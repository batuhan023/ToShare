using ToShare.Services;

namespace ToShare.Pages;

public partial class RegisterPage : ContentPage
{
    private readonly LoginService login = new LoginService();
    public RegisterPage()
	{
		InitializeComponent();
	}

    async void BtnRegister_Clicked(object sender, EventArgs e)
    {
        int salary = int.Parse(EntIncome.Text);
        var response = await login.Register(EntName.Text,EntSurname.Text,EntEmail.Text,EntPassword.Text,EntPhone.Text,EntProfil.Text,salary);
        if (response != null)
        {
            await DisplayAlert("OK", "Your profile have created", "Cancel");
        }
        else
        {
            await DisplayAlert("Error", "Oops something went wrong", "Cancel");
        }

        await Navigation.PushAsync(new LoginPage());

    }

    async void TapLogin_Tapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());

    }
}