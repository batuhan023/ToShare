using ToShare.Pages;

namespace ToShare;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new NavigationPage(new RegisterPage());
    }
}
