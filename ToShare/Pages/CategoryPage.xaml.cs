namespace ToShare.Pages;

public partial class CategoryPage : ContentPage
{
	public CategoryPage(int categoryId, string categoryName)
	{
		InitializeComponent();
		Title = categoryName;
	}
}