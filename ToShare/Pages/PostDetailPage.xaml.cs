using ToShare.Services;

namespace ToShare.Pages;

public partial class PostDetailPage : ContentPage
{
    private readonly PostService _post = new PostService();
    public PostDetailPage(int postId)
	{
		InitializeComponent();
		GetPostDetail(postId);
	}

    private async void GetPostDetail(int postId)
    {
        var post = await _post.GetPostById(postId);
        lblName.Text = post.Name;
        lblCount.Text = post.Count.ToString();
        ImgProperty.Source = post.Image;
        LblDescription.Text = post.Description;

    }
}