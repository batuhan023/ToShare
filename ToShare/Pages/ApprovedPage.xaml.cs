using ToShare.Models;
using ToShare.Services;

namespace ToShare.Pages;

public partial class ApprovedPage : ContentPage
{
    private readonly PostService _post = new PostService();

    public ApprovedPage()
	{
		InitializeComponent();
		GetApprovedList();

	}

    private async void GetApprovedList()
    {
        var userid = Preferences.Get("UserId", 0);
        var result = await _post.GetApproved(userid);
        CvProperties.ItemsSource = result;
    }

    private void CvProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var currentSelection = e.CurrentSelection.FirstOrDefault() as Post;
        if (currentSelection == null) return;
        Navigation.PushModalAsync(new ApprovedPostDetailPage(currentSelection.Id));
        ((CollectionView)sender).SelectedItem = null;
    }
}