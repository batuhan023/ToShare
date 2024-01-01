using System.Collections.ObjectModel;
using ToShare.Models;
using ToShare.Services;

namespace ToShare.Pages;

public partial class AppliesPage : ContentPage
{
    private readonly PostService _post = new PostService();
    public ObservableCollection<Product> product { get; set; }
    public AppliesPage()
	{
		InitializeComponent();
        GetBookmarkList();
        
    }

    private async void GetBookmarkList()
    {
        var userid = Preferences.Get("UserId", 0);
        var result = await _post.GetApplied(userid);
        CvProperties.ItemsSource = result;
    }

    private void CvProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Post;
        if (currentSelection == null) return;
        Navigation.PushModalAsync(new PostDetailPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }
}