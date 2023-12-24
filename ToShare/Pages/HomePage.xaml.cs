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
        
        // �rne�in, CvTopPicks.SelectedItem ile se�ilen �r�n� alal�m
        if (CvTopPicks.SelectedItem is Post selectedPost)
        {
            // Preferences'tan kullan�c� kimli�ini alal�m
            int userId = Preferences.Get("userId", -1);

            // PostService'deki ApplyPost metodunu kullanarak ba�vuru yapal�m
            try
            {
                var result = await _post.ApplyPost(selectedPost.Id, userId);

                // Ba�vuru ba�ar�l� olduysa, istedi�iniz i�lemleri yapabilirsiniz
                // �rne�in, kullan�c�ya bir mesaj g�sterme veya ba�ka bir i�lem
                DisplayAlert("Ba�vuru Ba�ar�l�", "Ba�vurunuz ba�ar�yla ger�ekle�tirildi.", "Tamam");
            }
            catch (Exception ex)
            {
                // Ba�vuru s�ras�nda bir hata olu�tuysa, kullan�c�ya bilgi verme veya loglama
                DisplayAlert("Hata", $"Ba�vuru s�ras�nda bir hata olu�tu: {ex.Message}", "Tamam");
            }
        }
        else
        {
            // CvTopPicks'te se�ili bir ��e yoksa kullan�c�ya bilgi verme
            DisplayAlert("Uyar�", "L�tfen bir �r�n se�in.", "Tamam");
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