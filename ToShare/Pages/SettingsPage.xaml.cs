namespace ToShare.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    private void TapLogout_Tapped(object sender, EventArgs e)
    {
		Application.Current.MainPage = new LoginPage();
    }

    private void TapProfile_Tapped(object sender, EventArgs e)
    {

    }
}