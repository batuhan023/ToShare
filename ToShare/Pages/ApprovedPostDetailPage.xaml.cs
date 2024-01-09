using ToShare.Services;

namespace ToShare.Pages;

public partial class ApprovedPostDetailPage : ContentPage
{
    private readonly PostService _post = new PostService();
    public ApprovedPostDetailPage(int postId)
	{
		InitializeComponent();
        GetPostDetail(postId);
    }
    private async void GetPostDetail(int postId)
    {
        var post = await _post.GetPostById(postId);
        Preferences.Set("PostId", post.Id);
        lblName.Text = post.Name;
        lblUsername.Text = post.User.UserName;
        lblUsersurname.Text = post.User.UserSurname;
        userphone.Text = post.User.UserPhone;
        ImgUser.Source = post.User.ProfilePhoto;
        lblbadres.Text = post.Adres;
        ImgProperty.Source = post.Image;
        LblDescription.Text = post.Description;


    }

    private void ImgbackBtn_Clicked(object sender, EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}