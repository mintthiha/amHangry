using UserNameSpace;
public class RecentRecipeSearch
{
    public int RecentRecipeSearchId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    public Member Member { get; set; } = null!;

    public int RecipeId { get; set; }
    public int MemberId { get; set; }
}