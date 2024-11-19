using UserNameSpace;
public class FavoriteRecipe
{
    public int FavoriteRecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    public Member Member { get; set; } = null!;

    public int RecipeId { get; set; }
    public int MemberId { get; set; }
}