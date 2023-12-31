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
        Preferences.Set("PostId", post.Id);
        lblName.Text = post.Name;
        lblCount.Text = post.Count.ToString();
        ImgProperty.Source = post.Image;
        LblDescription.Text = post.Description;

    }

    private async void Apply_button_Clicked(object sender, EventArgs e)
    {
        int uid = Preferences.Get("UserId", 0);
        int pid = Preferences.Get("PostId", 0);
        //userid = uid;
        //postid = pid;
        var response = await _post.ApplyPost(uid, pid);
        if (response != null)
        {
            await DisplayAlert("","Your apply has been successfully created.","Cancel");
        }
        else
        {
            await DisplayAlert("", "Oops something went wrong", "Cancel");
        }
    }

    private void ImgbackBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}