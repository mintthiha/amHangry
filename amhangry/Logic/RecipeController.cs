using Microsoft.EntityFrameworkCore;
using UserNameSpace;
using System.Linq;

public class RecipeController
{
    private RecipeContext _context;

    //constructor

    public RecipeController(RecipeContext recipeContext)
    {
        _context = recipeContext;
    }

    //methods


    /// <summary>
    /// Gets all recipes from the database
    /// </summary>
    /// <returns>List of recipes from the database</returns>
    public List<Recipe> getAllRecipesDB()
    {
        List<Recipe> searchRecipesList = new List<Recipe>();
        searchRecipesList = _context.Recipes
        .Include(r => r.Owner)
        .Include(r => r.Instructions)
        .Include(r => r.Tags)
        .Include(r => r.RecipeIngredients)
        .ThenInclude(ri => ri.Ingredient)
        .Include(r => r.Rates)
        .ToList();
        return searchRecipesList;
    }

    /// <summary>
    /// Creates a new recipe with the provided details and adds it to the list of recipes.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if the recipe name already exists in the recipes list.</exception>
    public void CreateRecipeDB(string name, Member owner, string description, double preparationTime, double cookingTime,
    List<Instruction> instructions, List<Tag> tags, int servings, List<RecipeIngredient> recipeIngredients)
    {
        var existingRecipe = _context.Recipes.FirstOrDefault(recipe => recipe.Name.Equals(name));
        if (existingRecipe != null)
        {
            throw new ArgumentException("This recipe name already exists in the database!");
        }

        List<Tag> dbTags = _context.Tags.ToList<Tag>();

        List<Tag> tagsToAdd = new List<Tag>();

        //Add existing tags from database to the list
        foreach (Tag tag in dbTags)
        {
            if (tag.Name == null || tag.Name == "")
            {
                throw new ArgumentException("The tag cannot be null or empty!");
            }
            bool dbTagExist = tags.Any(t => t.Name.Equals(tag.Name));

            if (dbTagExist)
            {
                tagsToAdd.Add(tag);
            }
        }

        //Add non-existing tags to the list
        foreach (Tag tag in tags)
        {
            bool dbTagExist = dbTags.Any(t => t.Name.Equals(tag.Name));

            if (!dbTagExist && !tagsToAdd.Any(t => t.Name.Equals(tag.Name)))
            {
                tagsToAdd.Add(tag);
            }
        }

        _context.Recipes.Add(new Recipe(name, owner, description, preparationTime, cookingTime, instructions, tagsToAdd, servings, recipeIngredients));
        _context.SaveChanges();
    }

    /// <summary>
    /// Adds or updates the rating for the provided recipe.
    /// </summary>
    /// <param name="username">The username of the user providing the rating.</param>
    /// <param name="rating">The rating provided by the user.</param>
    /// <exception cref="ArgumentException">Thrown if the rating is invalid/null .</exception>
    public void RateRecipeDB(string username, double rating, Recipe recipeToUpdate)
    {
        if (recipeToUpdate == null)
        {
            throw new ArgumentNullException("Recipe cannot be null");
        }

        recipeToUpdate.RateRecipe(username, rating);
        _context.SaveChanges();

    }

    /// <summary>
    /// Deletes the rating provided by the specified user for the provided recipe.
    /// </summary>
    /// <param name="username">The username of the user whose rating is to be deleted.</param>
    /// <exception cref="ArgumentException">Thrown if the Recipe is null or if the user hasn't rated the recipe .</exception>
    public void DeleteRatingRecipeDB(string username, Recipe recipeToUpdate)
    {
        if (recipeToUpdate == null)
        {
            throw new ArgumentNullException("Recipe cannot be null");
        }

        recipeToUpdate.DeleteRating(username);
        _context.SaveChanges();

    }

    /// <summary>
    /// Updates the name of the provided recipe.
    /// </summary>
    /// <param name="name">The new name for the recipe.</param>
    /// <exception cref="ArgumentException">Thrown if the new name already exists in the database or if the new name doesn't meet the criteria.</exception>
    public void UpdateRecipeNameDB(string name, Recipe recipeToUpdate)
    {
        if (recipeToUpdate == null)
        {
            throw new ArgumentNullException("Recipe to be updated cannot be null");
        }

        bool nameExist = _context.Recipes.Any(recipe => recipe.Name.Equals(name));

        if (nameExist)
        {
            throw new ArgumentException("This new recipe name already exists in the database ");
        }
        recipeToUpdate.Name = name;
        _context.SaveChanges();

    }

    /// <summary>
    /// Updates the description of the provided recipe.
    /// </summary>
    /// <param name="description">The new description for the recipe.</param>
    /// <exception cref="ArgumentException">Thrown if the new description doesn't meet the criteria.</exception>
    public void UpdateRecipeDescriptionDB(string description, Recipe recipeToUpdate)
    {
        if (recipeToUpdate == null)
        {
            throw new ArgumentNullException("Recipe to be updated cannot be null");
        }

        recipeToUpdate.Description = description;
        _context.SaveChanges();

    }


    /// <summary>
    /// Updates the preparation time of the provided recipe.
    /// </summary>
    /// <param name="preparationTime">The new preparation time for the recipe.</param>
    /// <exception cref="ArgumentException">Thrown if the new preparation time doesn't meet the criteria or the recipe is null</exception>
    public void UpdatePreparationTimeDB(double preparationTime, Recipe recipeToUpdate)
    {
        if (recipeToUpdate == null)
        {
            throw new ArgumentNullException("Recipe to be updated cannot be null");
        }

        recipeToUpdate.PreparationTime = preparationTime;
        _context.SaveChanges();

    }

    /// <summary>
    /// Updates the cooking time of the provided recipe.
    /// </summary>
    /// <param name="cookingTime">The new cooking time for the recipe.</param>
    /// <exception cref="ArgumentException">Thrown if the new cooking time doesn't meet the criteria or the recipe is null.</exception>
    public void UpdateCookingTimeDB(double cookingTime, Recipe recipeToUpdate)
    {
        if (recipeToUpdate == null)
        {
            throw new ArgumentNullException("Recipe to be updated cannot be null");
        }

        recipeToUpdate.CookingTime = cookingTime;
        _context.SaveChanges();

    }

    /// <summary>
    /// Updates the instructions of the provided recipe.
    /// </summary>
    /// <param name="instructions">The new list of instructions for the recipe.</param>
    /// <exception cref="ArgumentException">Thrown if the new instructions don't meet the criteria or the recipe is null.</exception>
    public void UpdateRecipeInstructionsDB(List<Instruction> instructions, Recipe recipeToUpdate)
    {
        if (recipeToUpdate == null)
        {
            throw new ArgumentNullException("Recipe to be updated cannot be null");
        }

        recipeToUpdate.Instructions = instructions;
        _context.SaveChanges();
    }

    /// <summary>
    /// Updates the tags of the provided recipe.
    /// </summary>
    /// <param name="tags">The new list of tags for the recipe.</param>
    /// <exception cref="ArgumentException">Thrown if the new tags don't meet the criteria or the recipe is null.</exception>
    public void UpdateRecipeTagsDB(List<Tag> tags, Recipe recipeToUpdate)
    {
        if (recipeToUpdate == null)
        {
            throw new ArgumentNullException("Recipe to be updated cannot be null");
        }


        List<Tag> dbTags = _context.Tags.ToList<Tag>();

        List<Tag> tagsToAdd = new List<Tag>();

        //Add existing tags from database to the list
        foreach (Tag tag in dbTags)
        {
            bool dbTagExist = tags.Any(t => t.Name.Equals(tag.Name));

            if (dbTagExist)
            {
                tagsToAdd.Add(tag);
            }
        }

        //Add non-existing tags to the list
        foreach (Tag tag in tags)
        {
            bool dbTagExist = dbTags.Any(t => t.Name.Equals(tag.Name));

            if (!dbTagExist && !tagsToAdd.Any(t => t.Name.Equals(tag.Name)))
            {
                tagsToAdd.Add(tag);
            }
        }

        recipeToUpdate.Tags = tagsToAdd;
        _context.SaveChanges();

    }

    /// <summary>
    /// Updates the servings of the provided recipe.
    /// </summary>
    /// <param name="servings">The new number of servings for the recipe.</param>
    /// <exception cref="ArgumentException">Thrown if the new serving size doesn't meet the criteria or the recipe is null.</exception>
    public void UpdateRecipeServingDB(int servings, Recipe recipeToUpdate)
    {
        if (recipeToUpdate == null)
        {
            throw new ArgumentNullException("Recipe to be updated cannot be null");
        }

        recipeToUpdate.Servings = servings;
        _context.SaveChanges();

    }

    /// <summary>
    /// Updates the recipe ingredients of the provided recipe.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown if the new ingredients don't meet the criteria or the recipe is null.</exception>
    public void UpdateRecipeIngredientsDB(List<RecipeIngredient> recipeIngredients, Recipe recipeToUpdate)
    {
        if (recipeToUpdate == null)
        {
            throw new ArgumentNullException("Recipe to be updated cannot be null");
        }

        recipeToUpdate.RecipeIngredients = recipeIngredients;
        _context.SaveChanges();

    }

    /// <summary>
    /// Searches for recipes whose name contains the provided keyword.
    /// </summary>
    /// <param name="name">The keyword to search for in the recipe names.</param>
    /// <returns>A list of recipes whose name contains the provided keyword.</returns>
    public List<Recipe> SearchByName(string name)
    {
        List<Recipe> recipesByName = new List<Recipe>();
        recipesByName = _context.Recipes
        .Include(recipe => recipe.Owner)
        .Include(recipe => recipe.Instructions)
        .Include(recipe => recipe.Tags)
        .Include(recipe => recipe.RecipeIngredients)
        .ThenInclude(ri => ri.Ingredient)
        .Include(recipe => recipe.Rates)
        .Where(recipe => recipe.Name.ToLower().Contains(name.ToLower())).ToList<Recipe>();
        return recipesByName;
    }

    /// <summary>
    /// Searches for a recipe with the exact specified name.
    /// </summary>
    /// <param name="name">The exact name of the recipe to search for.</param>
    /// <returns>The recipe with the exact specified name, or null if not found.</returns>
    public Recipe? SearchByExactName(string name)
    {

        Recipe? recipeByExactName;
        recipeByExactName = _context.Recipes
        .Include(recipe => recipe.Owner)
        .Include(recipe => recipe.Instructions)
        .Include(recipe => recipe.Tags)
        .Include(recipe => recipe.RecipeIngredients)
        .ThenInclude(ri => ri.Ingredient)
        .Include(recipe => recipe.Rates)
        .FirstOrDefault(recipe => recipe.Name.ToLower().Equals(name.ToLower()));

        return recipeByExactName;
    }

    /// <summary>
    /// Searches for recipes created by the specified user.
    /// </summary>
    /// <param name="username">The username of the user who created the recipes.</param>
    /// <returns>A list of recipes created by the specified user.</returns>
    public List<Recipe> SearchByUser(string username)
    {

        List<Recipe> searchRecipes = new List<Recipe>();
        searchRecipes = _context.Recipes
        .Include(recipe => recipe.Owner)
        .Include(recipe => recipe.Instructions)
        .Include(recipe => recipe.Tags)
        .Include(recipe => recipe.RecipeIngredients)
        .ThenInclude(ri => ri.Ingredient)
        .Include(recipe => recipe.Rates)
        .Where(recipe => recipe.Owner.Username.ToLower().Equals(username.ToLower())).ToList<Recipe>();
        return searchRecipes;
    }

    /// <summary>
    /// Searches for recipes that contain the provided keyword in their description.
    /// </summary>
    /// <param name="keyword">The keyword to search for in the recipe descriptions.</param>
    /// <returns>A list of recipes containing the provided keyword in their description.</returns>
    public List<Recipe> SearchByDescription(string keyword)
    {
        List<Recipe> searchRecipesList = new List<Recipe>();
        searchRecipesList = _context.Recipes.Include(recipe => recipe.Owner)
        .Include(recipe => recipe.Instructions)
        .Include(recipe => recipe.Tags)
        .Include(recipe => recipe.RecipeIngredients)
        .ThenInclude(ri => ri.Ingredient)
        .Include(recipe => recipe.Rates)
        .Where(recipe => recipe.Description.ToLower().Contains(keyword.ToLower())).ToList<Recipe>();
        return searchRecipesList;
    }

    /// <summary>
    /// Searches for recipes that take at most the specified number of minutes to prepare and cook.
    /// </summary>
    /// <param name="minutes">The maximum number of minutes for preparation and cooking combined.</param>
    /// <returns>A list of recipes that can be prepared and cooked within the specified time limit.</returns>
    public List<Recipe> SearchByTime(int minutes)
    {
        List<Recipe> searchRecipesList = new List<Recipe>();
        searchRecipesList = _context.Recipes
        .Include(recipe => recipe.Owner)
        .Include(recipe => recipe.Instructions)
        .Include(recipe => recipe.Tags)
        .Include(recipe => recipe.RecipeIngredients)
        .ThenInclude(ri => ri.Ingredient)
        .Include(recipe => recipe.Rates)
        .Where(recipe => recipe.PreparationTime + recipe.CookingTime <= minutes).ToList<Recipe>();
        return searchRecipesList;
    }

    /// <summary>
    /// Searches for recipes that contain all of the provided tags.
    /// </summary>
    /// <param name="tags">The list of tags to search for.</param>
    /// <returns>A list of recipes containing all of the provided tags.</returns>
    public List<Recipe> SearchByTag(List<Tag> tags)
    {

        var recipesWithTags = _context.Recipes
        .Include(recipe => recipe.Owner)
        .Include(recipe => recipe.Instructions)
        .Include(recipe => recipe.Tags)
        .Include(recipe => recipe.RecipeIngredients)
        .ThenInclude(ri => ri.Ingredient)
        .Include(recipe => recipe.Rates)
        .ToList();

        var searchRecipesList = recipesWithTags
            .Where(recipe => tags.All(tag => recipe.Tags.Any(recipeTag => recipeTag.Name.Equals(tag.Name, StringComparison.OrdinalIgnoreCase))))
            .ToList();

        return searchRecipesList;
    }

    // <summary>
    /// Searches for recipes that serve at least the specified number of servings.
    /// </summary>
    /// <param name="servings">The minimum number of servings.</param>
    /// <returns>A list of recipes that serve at least the specified number of servings.</returns>
    public List<Recipe> SearchByServings(int servings)
    {
        List<Recipe> searchRecipesList = new List<Recipe>();
        searchRecipesList = _context.Recipes
        .Include(recipe => recipe.Owner)
        .Include(recipe => recipe.Instructions)
        .Include(recipe => recipe.Tags)
        .Include(recipe => recipe.RecipeIngredients)
        .ThenInclude(ri => ri.Ingredient)
        .Include(recipe => recipe.Rates)
        .Where(recipe => recipe.Servings >= servings).ToList<Recipe>();
        return searchRecipesList;
    }

    /// <summary>
    /// Searches for recipes that contain the provided list of ingredients.
    /// </summary>
    /// <param name="ingredients">The list of ingredients to search for.</param>
    /// <returns>A list of recipes containing the provided ingredients.</returns>
    public List<Recipe> SearchByIngredients(List<string> ingredients)
    {
        List<Recipe> searchRecipesList = new List<Recipe>();
        searchRecipesList = _context.Recipes
        .Include(recipe => recipe.Owner)
        .Include(recipe => recipe.Instructions)
        .Include(recipe => recipe.Tags)
        .Include(recipe => recipe.RecipeIngredients)
        .ThenInclude(ri => ri.Ingredient)
        .Include(recipe => recipe.Rates)
        .ToList<Recipe>();


        List<Recipe> recipesWithIngredients =
        searchRecipesList.Where(recipe => RecipesContainsAllIngredients(recipe, ingredients)).ToList<Recipe>();

        return recipesWithIngredients;
    }

    /// <summary>
    /// Searches for recipes that contain ONLY the provided list of ingredients.
    /// </summary>
    /// <param name="ingredients">The list of ingredients to search for.</param>
    /// <returns>A list of recipes containing only the provided ingredients.</returns>
    public List<Recipe> SearchByExactIngredients(List<string> ingredients)
    {
        List<Recipe> searchRecipesList = new List<Recipe>();
        searchRecipesList = _context.Recipes
        .Include(recipe => recipe.Owner)
        .Include(recipe => recipe.Instructions)
        .Include(recipe => recipe.Tags)
        .Include(recipe => recipe.RecipeIngredients)
        .ThenInclude(ri => ri.Ingredient)
        .Include(recipe => recipe.Rates)
        .ToList<Recipe>();

        List<Recipe> recipesWithExactIngredients =
        searchRecipesList.Where(recipe => recipe.RecipeIngredients != null && recipe.RecipeIngredients.Count == ingredients.Count && RecipesContainsAllIngredients(recipe, ingredients)).ToList<Recipe>();

        return recipesWithExactIngredients;
    }

    /// <summary>
    /// Searches for recipes with at least the specified rating.
    /// </summary>
    /// <param name="rating">The minimum rating value to filter the recipes.</param>
    /// <returns>A list of recipes with at least the specified rating.</returns>
    public List<Recipe> SearchByRating(int rating)
    {
        List<Recipe> searchRecipesList = new List<Recipe>();
        searchRecipesList = _context.Recipes
        .Include(recipe => recipe.Owner)
        .Include(recipe => recipe.Instructions)
        .Include(recipe => recipe.Tags)
        .Include(recipe => recipe.RecipeIngredients)
        .ThenInclude(ri => ri.Ingredient)
        .Include(recipe => recipe.Rates)
        .ToList<Recipe>();

        List<Recipe> recipesByRating = searchRecipesList.Where(recipe => recipe.Ratings >= rating).ToList<Recipe>();

        return recipesByRating;
    }

    /// <summary>
    /// Deletes the specified recipe from the list of recipes.
    /// </summary>
    /// <param name="recipe">The recipe to be deleted.</param>
    /// <exception cref="ArgumentException">Thrown if the recipe doesn't exist in the db or is null .</exception>
    public void DeleteRecipeDB(Recipe recipe)
    {
        if (recipe == null)
        {
            throw new ArgumentNullException("Recipe to be deleted cannot be null");
        }

        var existingRecipe = _context.Recipes.FirstOrDefault(r => r.RecipeId == recipe.RecipeId);
        if (existingRecipe != null)
        {
            _context.Recipes.Remove(existingRecipe);
            _context.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Recipe to be deleted isn't in the database");
        }

    }
    /// <summary>
    /// Sorts the list of recipes based on the specified criteria.
    /// </summary>
    /// <param name="recipesList">The list of recipes to be sorted.</param>
    /// <param name="criteria">The sorting criteria </param>
    /// <returns>The sorted list of recipes.</returns>
    public List<Recipe> SortByCriteria(List<Recipe> recipesList, string criteria)
    {
        switch (criteria.ToLower())
        {
            case "name":
                return recipesList.OrderBy(r => r.Name).ToList();
            case "owner":
                return recipesList.OrderBy(r => r.Owner.Username).ToList();
            case "time":
                return recipesList.OrderBy(r => r.TotalTime).ToList();
            case "rating":
                return recipesList.OrderByDescending(r => r.Ratings).ToList();
            case "serving":
                return recipesList.OrderBy(r => r.Servings).ToList();
            case "calories":
                return recipesList.OrderByDescending(r => r.GetTotalCalories()).ToList();
            case "proteins":
                return recipesList.OrderByDescending(r => r.GetTotalProteins()).ToList();
            case "fats":
                return recipesList.OrderByDescending(r => r.GetTotalFats()).ToList();
            case "carbs":
                return recipesList.OrderByDescending(r => r.GetTotalCarbs()).ToList();
            case "cost":
                return recipesList.OrderBy(r => r.GetTotalCost()).ToList();

            default:
                throw new ArgumentException("Invalid criteria");
        }
    }

    //Helper method for the method (SearchByIngredients) and (SearchByExactIngredients)
    /// <summary>
    /// Checks if a recipe contains all the provided ingredients.
    /// </summary>
    /// <param name="recipe">The recipe to check.</param>
    /// <param name="ingredients">The list of ingredients to check against.</param>
    /// <returns>True if the recipe contains all provided ingredients, otherwise false.</returns>
    internal bool RecipesContainsAllIngredients(Recipe recipe, List<String> ingredients)
    {
        if (recipe.RecipeIngredients == null)
        {
            throw new ArgumentNullException("The recipe's recipeIngredients field is null!");
        }
        return ingredients.All(ingredient =>
        recipe.RecipeIngredients.Any(recipeIngredient =>
            recipeIngredient.Ingredient != null &&
            recipeIngredient.Ingredient.Name.Equals(ingredient, StringComparison.OrdinalIgnoreCase)));
    }

    /// <summary>
    /// Get members who favorited a specific recipe
    /// </summary>
    /// <param name="recipe">The recipe to check.</param>
    /// <returns>List of all members that favorited the recipe</returns>
    public List<Member> GetMembersWhoFavorited(Recipe recipe)
    {
        var members = _context.Members
            .Include(m => m.FavoriteRecipes)
                .ThenInclude(fr => fr.Recipe)
            .Where(m => m.FavoriteRecipes.Any(fr => fr.Recipe == recipe))
            .Include(m => m.OwnedRecipes)
            .Include(m => m.RecentRecipeSearches)
            .ToList();
        return members;
    }

    /// <summary>
    /// Gets all favorite recipes from a member
    /// </summary>
    /// <returns>List of favorite recipes of specific member from the database</returns>
    public List<Recipe> GetFavoriteRecipes(Member member)
    {
        List<Recipe> favoriteRecipes = _context.Recipes
            .Where(r => member.FavoriteRecipes.Any(fr => fr.RecipeId == r.RecipeId))
            .ToList();
        return favoriteRecipes;
    }

}
