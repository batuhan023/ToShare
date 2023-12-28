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

    private void GetPostDetail(int postId)
    {
        _post
    }
}