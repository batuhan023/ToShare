using System.Collections.ObjectModel;
using ToShare.Models;
using ToShare.Services;

namespace ToShare.Pages;

public partial class HomePage : ContentPage
{
    private readonly LoginService _login = new LoginService();
	private readonly PostService _post = new PostService();

    public ObservableCollection<Product> product { get; set; }
	public ObservableCollection<Category> category { get; set; }
    public ObservableCollection<Post> post { get; set; }
    public HomePage()
	{

		InitializeComponent();
        LblUserName.Text = "Hi " + Preferences.Get("username", string.Empty);
        GetCategories();
		GetPosts();
		

	}

    private async void GetPosts()
    {
		var posts = await _post.GetActivePost();
        CvTopPicks.ItemsSource = posts;
    }

    private async void GetCategories()
    {
		var categories = await _login.Category();
		CvCategories.ItemsSource = categories;
    }

}