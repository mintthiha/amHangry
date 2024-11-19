using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using UserNameSpace;

public class RecipeContext : DbContext
{
    private static RecipeContext instance = null!;

    //parameterless constructor is left Public for Moq unit tests to work. 
    //Enable singleton behavior by uncomment the private parameterless constructor.
    
    //private RecipeContext() { }

    public virtual DbSet<Recipe> Recipes { get; set; } = null!;
    public virtual DbSet<Ingredient> Ingredients { get; set; } = null!;
    public virtual DbSet<Member> Members { get; set; } = null!;
    public virtual DbSet<Admin> Admins { get; set; } = null!;
    public virtual DbSet<Instruction> Instructions { get; set; } = null!;
    public virtual DbSet<Tag> Tags { get; set; } = null!;
    public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; } = null!;
    public virtual DbSet<Rate> Rates { get; set; } = null!;
    public virtual DbSet<FavoriteRecipe> FavoriteRecipes { get; set; } = null!;
    public virtual DbSet<RecentRecipeSearch> RecentRecipeSearches { get; set; } = null!;
    public virtual DbSet<UnitEntity> UnitEntities { get; set; } = null!;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? oracleUser = Environment.GetEnvironmentVariable("ORA_USER");
        string? oraclePassword = Environment.GetEnvironmentVariable("ORA_PASSWD");

        if (string.IsNullOrEmpty(oracleUser) || string.IsNullOrEmpty(oraclePassword))
        {
            throw new InvalidOperationException("Oracle user or password environment variables are not set.");
        }

        //Login with your information here
        optionsBuilder.UseOracle($"User Id={oracleUser};Password={oraclePassword};Data Source=198.168.52.211:1521/pdbora19c.dawsoncollege.qc.ca;");
    }

    public static RecipeContext GetInstance()
    {
        if (instance == null)
        {
            instance = new RecipeContext();
        }

        return instance;
    }

    internal Recipe? FirstOrDefault(Func<object, bool> value)
    {
        throw new NotImplementedException();
    }
}