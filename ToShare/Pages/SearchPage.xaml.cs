using System.Collections.ObjectModel;
using ToShare.Models;
using ToShare.Services;

namespace ToShare.Pages;

public partial class SearchPage : ContentPage
{
    private readonly PostService _post = new PostService();
    public ObservableCollection<Post> post { get; set; }
    public SearchPage()
	{
		InitializeComponent();
	}

    private void ýmgBackButton_Clicked(object sender, EventArgs e)
    {
		Navigation.PopModalAsync();
    }

    async void Searcbar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue == null) return;
        var result = await _post.GetPostsByLetter(e.NewTextValue);
        CvSearch.ItemsSource = result;  
    }

    private void CvSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Post;
        if (currentSelection == null) return;
        Navigation.PushModalAsync(new PostDetailPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }
}