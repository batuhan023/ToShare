namespace ToShare.Pages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
        LblUserName.Text = "Hi " + Preferences.Get("username", string.Empty);

	}
}