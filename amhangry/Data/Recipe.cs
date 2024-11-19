using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Printing;
using UserNameSpace;
public class Recipe
{
    private Recipe()
    {
        _ratings = new List<Rate>();
    }
    public int RecipeId { get; set; }
    public int OwnerId { get; set; }

    public List<FavoriteRecipe> FavoriteRecipes { get; } = new List<FavoriteRecipe>();
    public List<RecentRecipeSearch> RecentRecipeSearch { get; } = new List<RecentRecipeSearch>();

    public List<Rate> Rates
    {
        get
        {
            return _ratings;
        }
        set
        {
            _ratings = value;
        }
    }

    //fields
    private string _name = string.Empty;
    private Member _owner =null!;
    private string _description= string.Empty;
    private double _preparationTime = 0;
    private double _cookingTime = 0;
    private List<Instruction> _instructions =null!;
    private List<Tag> _tags =null!;
    private int _servings = 0;
    private List<RecipeIngredient> _recipeIngredients =null!;
    private List<Rate> _ratings =null!;

    public Recipe(string name, Member owner, string description, double preparationTime, double cookingTime, List<Instruction> instructions, List<Tag> tags, int servings, List<RecipeIngredient> recipeIngredients)
    {
        Name = name;
        Owner = owner;
        Description = description;
        PreparationTime = preparationTime;
        CookingTime = cookingTime;
        Instructions = instructions;
        Tags = tags;
        Servings = servings;
        RecipeIngredients = recipeIngredients;

        _ratings = new List<Rate>();

        if (owner.Username == null)
        {
            throw new ArgumentNullException("The owner username field cannot be null!");
        }
        owner.addOwnedRecipe(this);
    }

    //properties with validations

    /// <summary>
    /// Gets or sets the name of the recipe.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the provided name is null, empty, shorter than 4 characters, or longer than 30 characters.</exception>
    public string Name
    {
        get
        {
            if (_name != null)
            {
                return _name;
            }
            else
            {
                throw new ArgumentNullException("The name is null!");
            }
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Name cannot be empty or null!");
            }

            if (value.Length < 4)
            {
                throw new ArgumentException("Name can't be shorter than 4 characters");
            }
            if (value.Length > 30)
            {
                throw new ArgumentException("Name is too long");
            }

            _name = value;
        }
    }

    /// <summary>
    /// Gets or sets the owner of the recipe.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the owner is null or the owner's username is null or empty.</exception>

    public Member Owner
    {
        get
        {
            if (_owner != null)
            {
                return _owner;
            }
            else
            {
                throw new ArgumentNullException("The owner name of a recipe is null!");
            }
        }
        set
        {
            if (value == null)
            {
                throw new ArgumentException("Owner can't be null");
            }

            if (string.IsNullOrEmpty(value.Username))
            {
                throw new ArgumentException("Owner's name can't be null/empty!");
            }
            _owner = value;
        }
    }

    /// <summary>
    /// Gets or sets the description of the recipe.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the description is null, too short (below 4 characters), or too long (above 50 characters).</exception>
    public string Description
    {
        get
        {
            if (_description != null)
            {
                return _description;
            }
            else
            {
                throw new ArgumentNullException("The description cannot be null!");
            };
        }
        set
        {
            if (value == null)
            {
                throw new ArgumentException("Description can't be null");
            }

            if (value == "")
            {
                throw new ArgumentException("The description cannot be empty!");
            }
            if (value.Length < 4)
            {
                throw new ArgumentException("Description is too short");
            }

            if (value.Length > 50)
            {
                throw new ArgumentException("Description is too long!");
            }

            _description = value;
        }
    }

    /// <summary>
    /// Gets or sets the time required for preparing the recipe.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the preparation time is zero or negative, or exceeds 60 minutes.</exception>
    public double PreparationTime
    {
        get { return _preparationTime; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Preparation time cannot be 0 or negative");
            }

            if (value > 60)
            {
                throw new ArgumentException("Preparation time is too long. It exceeds an hour!");
            }
            _preparationTime = value;
        }
    }

    /// <summary>
    /// Gets or sets the time required for cooking the recipe.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the cooking time is negative or exceeds 90 minutes.</exception>
    public double CookingTime
    {
        get { return _cookingTime; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Cooking time can't be negative");
            }

            if (value > 90)
            {
                throw new ArgumentException("Cooking time is way too long! It exceeds 90 mins");
            }

            _cookingTime = value;
        }
    }

    /// <summary>
    /// Gets the total time required for preparation and cooking.
    /// </summary>
    /// <returns>The total time in minutes.</returns>
    public double? TotalTime
    {
        get { return _preparationTime + _cookingTime; }
    }

    /// <summary>
    /// Gets or sets the list of instructions for preparing the recipe.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the instructions list is null, empty, or contains steps that are null, shorter than 4 characters, or longer than 300 characters.</exception>
    public List<Instruction> Instructions
    {
        get { 
            if (_instructions == null){
                return new List<Instruction>();
            }else{
                return _instructions;
            }
         }
        set
        {
            if (value == null || value.Count == 0)
            {
                throw new ArgumentException("Instructions can't be null or empty!");
            }

            //validate that each step isn't null/empty && isn't too short/long
            foreach (Instruction instruction in value)
            {
                if (instruction == null)
                {
                    throw new ArgumentException("A step in the instructions can't be null or empty!");
                }

                if (instruction.Step.Length < 4)
                {
                    throw new ArgumentException("A step in the instructions is too short!");
                }
                //maximum length can be changed 
                if (instruction.Step.Length > 300)
                {
                    throw new ArgumentException("A step in the instructions is too long!");
                }
            }
            _instructions = value;
        }
    }

    /// <summary>
    /// Gets the average rating of the recipe.
    /// </summary>
    /// <remarks>
    /// The average rating is calculated by averaging the ratings provided by users.
    /// </remarks>
    public double Ratings
    {
        get
        {
            double sum = 0;
            int count = _ratings.Count;

            if (count == 0) 
            {
                return 1; //set to 1 as the default rating if there's no ratings
            }

            foreach (Rate rating in _ratings)
            {
                sum += rating.Value;
            }

            return Math.Round(sum / count, 1);
        }
    }


    /// <summary>
    /// Gets or sets the list of tags associated with the recipe.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the list of tags is null, empty, or contains tags that are null, shorter than 3 characters, or longer than 20 characters.</exception>
    public List<Tag> Tags
    {
        get
        {
            if (_tags != null)
            {
                return _tags;
            }
            else
            {
                throw new ArgumentNullException("The tags field is null!");
            }
        }
        set
        {

            if (value == null || value.Count() == 0)
            {
                throw new ArgumentException("The list of Tags can't be null or empty");
            }


            //check if there's a tag in the list that's null/empty && too short/long
            foreach (Tag tag in value)
            {

                if (string.IsNullOrEmpty(tag.Name))
                {
                    throw new ArgumentException("Tag's name can't be null or empty");
                }

                if (tag.Name.Length < 3)
                {
                    throw new ArgumentException("Tag's name is too short");
                }

                if (tag.Name.Length > 20)
                {
                    throw new ArgumentException("Tag's name is too long");
                }
            }

            _tags = value;
        }
    }

    /// <summary>
    /// Gets or sets the number of servings the recipe yields.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the servings value is zero, or negative.</exception>
    public int Servings
    {
        get { return _servings; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("The serving can't be 0 or negative!");
            }
            _servings = value;
        }
    }


    /// <summary>
    /// Gets or sets the list of ingredients and their quantities required for the recipe.
    /// </summary>
    /// <remarks>
    /// Each RecipeIngredient object represents an ingredient along with its quantity required for the recipe.
    /// </remarks>
    /// <exception cref="ArgumentException">Thrown when the list contains a null RecipeIngredient object.</exception>
    public List<RecipeIngredient> RecipeIngredients
    {
        get
        {
            if (_recipeIngredients != null)
            {
                return _recipeIngredients;
            }
            else
            {
                throw new ArgumentNullException("The list of recipe ingredients is null!");
            }

        }
        set
        {
            if (value != null && value.Count > 0)
            {
                foreach (RecipeIngredient ingredient in value)
                {
                    if (ingredient == null)
                    {
                        throw new ArgumentException("The list contains a null ingredient");
                    }
                    if (ingredient.Quantity <= 0)
                    {
                        throw new ArgumentException("One of the ingredient's quantity cannot be below 1!");
                    }
                }
                _recipeIngredients = value;
            }
            else
            {
                throw new ArgumentNullException("The recipe ingredient list must contain an ingredient!");
            }
        }
    }

    public double Calories => GetTotalCalories();
    public double Cost => GetTotalCost();
    public double Fats => GetTotalFats();
    public double Proteins => GetTotalProteins();
    public double Carbs => GetTotalCarbs();
    //Methods

    /// <summary>
    /// Rates the recipe with a specified rating (int) provided by a user.
    /// </summary>
    /// <param name="username">The username of the user providing the rating.</param>
    /// <param name="userRating">The rating provided by the user (Between 1 and 5).</param>
    /// <remarks>
    /// Users cannot rate their own recipes.
    /// If the user has already rated the recipe before, their previous rating will be replaced.
    /// </remarks>
    /// <exception cref="ArgumentException">Thrown when the username is the same as the owner's username, or when the rating is below 1 or above 5.</exception>
    public void RateRecipe(string username, double userRating)
    {
        if (Owner.Username == null)
        {
            throw new ArgumentNullException("The owner username cannot be null!");
        }

        if (Owner.Username.Equals(username))
        {
            throw new ArgumentException("User can't rate their own recipe!");
        }

        if (userRating < 1 || userRating > 5)
        {
            throw new ArgumentException("Rating must be between 1 and 5 star");
        }

        Boolean isRated = false;
        foreach (Rate rate in _ratings)
        {
            if (rate.RatedBy.Equals(username))
            {
                isRated = true;
                break;
            }
        }

        //delete the user's rating if the user has already rated.
        if (isRated)
        {
            DeleteRating(username);
        }
        _ratings.Add(new Rate(username, userRating));
    }

    /// <summary>
    /// Delete the rating of a user
    /// </summary>
    /// <param name="username">The username of the user that's removing their rating.</param>
    /// <exception cref="ArgumentException">Thrown when the user hasn't rated the recipe</exception>
    public void DeleteRating(string username)
    {

        int indexToRemove = -1;
        for (int i = 0; i < _ratings.Count; i++)
        {

            if (_ratings[i].RatedBy.Equals(username))
            {
                indexToRemove = i;
                break;
            }
        }

        if (indexToRemove != -1)
        {
            _ratings.RemoveAt(indexToRemove);
        }
        else
        {
            throw new ArgumentException("This recipe doesn't have a rating from you");
        }
    }

    /// <summary>
    /// Clears all ratings
    /// </summary>
    public void ClearRatingList()
    {
        _ratings.Clear();

    }

    /// <summary>
    /// Calculates and returns the total calories of the recipe.
    /// </summary>
    /// <returns>The total calories of the recipe.</returns>
    public double GetTotalCalories()
    {
        double totalCalories = 0;
        if (RecipeIngredients != null)
        {
            foreach (RecipeIngredient ingredient in RecipeIngredients)
            {
                totalCalories += ingredient.GetCalories();
            }
            return totalCalories;
        }
        else
        {
            throw new ArgumentNullException("The recipeIngredients cannot be null!");
        }
    }

    /// <summary>
    /// Calculates and returns the total proteins of the recipe.
    /// </summary>
    /// <returns>The total proteins of the recipe.</returns>
    public double GetTotalProteins()
    {
        double totalProteins = 0;
        if (RecipeIngredients != null)
        {
            foreach (RecipeIngredient ingredient in RecipeIngredients)
            {
                totalProteins += ingredient.GetTotalProteins();
            }
            return totalProteins;
        }
        else
        {
            throw new ArgumentNullException("The recipeIngredients cannot be null!");
        }
    }

    /// <summary>
    /// Calculates and returns the total fats of the recipe.
    /// </summary>
    /// <returns>The total fats of the recipe.</returns>
    public double GetTotalFats()
    {
        double totalFats = 0;
        if (RecipeIngredients != null)
        {
            foreach (RecipeIngredient ingredient in RecipeIngredients)
            {
                totalFats += ingredient.GetTotalFats();
            }
            return totalFats;
        }
        else
        {
            throw new ArgumentNullException("The recipeIngredients cannot be null!");
        }
    }

    /// <summary>
    /// Calculates and returns the total carbs of the recipe.
    /// </summary>
    /// <returns>The total carbs of the recipe.</returns>
    public double GetTotalCarbs()
    {
        double totalCarbs = 0;
        if (RecipeIngredients != null)
        {
            foreach (RecipeIngredient ingredient in RecipeIngredients)
            {
                totalCarbs += ingredient.GetTotalCarbs();
            }
            return totalCarbs;
        }
        else
        {
            throw new ArgumentNullException("The recipeIngredients cannot be null!");
        }
    }

    /// <summary>
    /// Calculates and returns the total cost of the recipe based on the cost of ingredients.
    /// </summary>
    /// <returns>The total cost of the recipe.</returns>
    public double GetTotalCost()
    {
        double totalCost = 0;
        if (RecipeIngredients != null)
        {
            foreach (RecipeIngredient ingredient in RecipeIngredients)
            {
                totalCost += ingredient.GetTotalPrice();
            }
            return totalCost;
        }
        else
        {
            throw new ArgumentNullException("The recipeIngredients cannot be null!");
        }
    }

    /// <summary>
    /// Check if the specified object is equal to the current recipe object
    /// </summary>
    /// <param name="obj">The object to compare with the current recipe object</param>
    /// <returns>True if the specified object's name is the same as the current recipe's name</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Recipe other = (Recipe)obj;
        return Name == other.Name;
    }

    public override string ToString()
    {
        string tags = "";
        string ingredients = "";
        string instructions = "";
        if (Tags == null)
        {
            throw new ArgumentNullException("The tags field is null!");
        }
        foreach (Tag tag in Tags)
        {
            tags += tag.Name + " ";
        }
        foreach (RecipeIngredient i in RecipeIngredients)
        {
            ingredients += i.Ingredient.Name + " ";
        }
        if (Instructions == null)
        {
            throw new ArgumentNullException("The instructions field is null!");
        }
        foreach (Instruction i in Instructions)
        {
            instructions += i.Step + " | ";
        }
        string builder = "";
        builder += " ------ " + Name + "------\n";
        builder += "Owner: " + Owner.Username + "\n";
        builder += "Description: " + Description + "\n";
        builder += "Time: " + TotalTime + "mins" + "\n";
        builder += "Rating: " + Ratings + "\n";
        builder += "Servings: " + Servings + "\n";
        builder += "Tags: " + tags + "\n";
        builder += "Calories: " + GetTotalCalories() + "\n";
        builder += "Ingredients: " + ingredients + "\n";
        builder += "instructions: " + instructions;
        return builder;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
        //Will have warning if this method isnt put here.
    }

}