using System.Collections.ObjectModel;
using ToShare.Models;
using ToShare.Services;

namespace ToShare.Pages;

public partial class CategoryPage : ContentPage
{
    private readonly LoginService _login = new LoginService();
    private readonly PostService _post = new PostService();
    private readonly CategoryService _category = new CategoryService();

    public ObservableCollection<Product> product { get; set; }
    public ObservableCollection<Category> category { get; set; }
    public ObservableCollection<Post> post { get; set; }
    public CategoryPage(int categoryId, string categoryName)
	{
		InitializeComponent();
		Title = categoryName;
		GetCategoryList(categoryId);
	}

    private async void GetCategoryList(int categoryId)
    {
		var category = await _category.GetPostsByCategoryId(categoryId);
        CvCategory.ItemsSource = category;
    }

    private void CvCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Post;
        if (currentSelection == null) return;
        Navigation.PushModalAsync(new PostDetailPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }
}