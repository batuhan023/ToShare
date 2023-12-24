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


    private void CvTopPicks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentselection = e.CurrentSelection.FirstOrDefault() as Post;
        if (currentselection == null) return;
        apply_button_Clicked(currentselection, EventArgs.Empty);
    }

    private async void apply_button_Clicked(object sender, EventArgs e)
    {
        
        // Örneðin, CvTopPicks.SelectedItem ile seçilen ürünü alalým
        if (CvTopPicks.SelectedItem is Post selectedPost)
        {
            // Preferences'tan kullanýcý kimliðini alalým
            int userId = Preferences.Get("userId", -1);

            // PostService'deki ApplyPost metodunu kullanarak baþvuru yapalým
            try
            {
                var result = await _post.ApplyPost(selectedPost.Id, userId);

                // Baþvuru baþarýlý olduysa, istediðiniz iþlemleri yapabilirsiniz
                // Örneðin, kullanýcýya bir mesaj gösterme veya baþka bir iþlem
                DisplayAlert("Baþvuru Baþarýlý", "Baþvurunuz baþarýyla gerçekleþtirildi.", "Tamam");
            }
            catch (Exception ex)
            {
                // Baþvuru sýrasýnda bir hata oluþtuysa, kullanýcýya bilgi verme veya loglama
                DisplayAlert("Hata", $"Baþvuru sýrasýnda bir hata oluþtu: {ex.Message}", "Tamam");
            }
        }
        else
        {
            // CvTopPicks'te seçili bir öðe yoksa kullanýcýya bilgi verme
            DisplayAlert("Uyarý", "Lütfen bir ürün seçin.", "Tamam");
        }
    }

    private void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;
        if(currentSelection== null) return;
        Navigation.PushAsync(new CategoryPage(currentSelection.Id, currentSelection.Name));
        ((CollectionView)sender).SelectedItem = null;
    }
}