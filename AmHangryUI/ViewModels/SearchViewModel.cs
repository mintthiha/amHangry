using ReactiveUI;
using System.Collections.ObjectModel;

namespace AmHangryUI.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        public RecipeController recipeController;
        public IngredientController ingredientController;
        public string _message = string.Empty;
        /// <summary>
        /// Property to get or set the message to be displayed.
        /// </summary>
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }

        public string SelectedOption { get; set; } = string.Empty;
        /// <summary>
        /// Selected search option from the user.
        /// </summary>
        public string SelectedSortOption { get; set; }  = string.Empty;

        /// <summary>
        /// Input for search criteria.
        /// </summary>
        public string Input { get; set; }  = string.Empty;


        public ObservableCollection<string> SearchOptions { get; set; }
        public ObservableCollection<string> SortOptions { get; set; }
        public ObservableCollection<Recipe> _recipesList =null!;
        public ObservableCollection<Recipe> RecipeList
        {
            get => _recipesList;
            set => this.RaiseAndSetIfChanged(ref _recipesList, value);
        }
        public ObservableCollection<Instruction> _instructionsItems = null!;
        public ObservableCollection<Instruction> InstructionsItems
        {
            get => _instructionsItems;
            set => this.RaiseAndSetIfChanged(ref _instructionsItems, value);
        }

        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> SearchCommand { get; }
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> SortCommand { get; }

        public SearchViewModel(RecipeController rc, IngredientController ic)
        {
            recipeController = rc;
            ingredientController = ic;
            List<string> options = new List<string> { "ALL", "By Name", "By Exact Name", "By User", "By Description", "By Time", "By Tag", "By Servings", "By Exact Ingredients", "By Ingredients", "By 1 Ingredient", "By Ratings" };
            SearchOptions = new ObservableCollection<string>(options);

            List<string> sortOptions = new List<string> { "name", "owner", "time", "rating", "serving", "calories", "proteins", "fats", "carbs", "cost" };
            SortOptions = new ObservableCollection<string>(sortOptions);

            SearchCommand = ReactiveCommand.Create(SearchRecipe);
            SortCommand = ReactiveCommand.Create(SortBy);
            RecipeList = new ObservableCollection<Recipe>();

            SelectedOption = "ALL";
            SearchRecipe();
        }

        /// <summary>
        /// Executes the search based on selected search option.
        /// </summary>
        public void SearchRecipe()
        {
            switch (SelectedOption)
            {
                case "ALL":
                    ShowMessage("");
                    RecipeList = new ObservableCollection<Recipe>(recipeController.getAllRecipesDB());
                    break;

                case "By Name":
                    ShowMessage("");
                    RecipeList = new ObservableCollection<Recipe>(recipeController.SearchByName(Input));
                    break;

                // Search recipes by exact name only
                case "By Exact Name":
                    ShowMessage("");
                    Recipe? resultRecipe = recipeController.SearchByExactName(Input);

                    if (resultRecipe != null)
                    {
                        List<Recipe> recipe = new List<Recipe> { resultRecipe };
                        RecipeList = new ObservableCollection<Recipe>(recipe);
                    }
                    else
                    {
                        RecipeList = new ObservableCollection<Recipe>();
                    }
                    break;

                case "By User":
                    ShowMessage("");
                    RecipeList = new ObservableCollection<Recipe>(recipeController.SearchByUser(Input));
                    break;

                case "By Description":
                    ShowMessage("");
                    RecipeList = new ObservableCollection<Recipe>(recipeController.SearchByDescription(Input));
                    break;

                // Search recipes by time equal or less than the user's input 
                case "By Time":
                    ShowMessage("");
                    if (int.TryParse(Input, out int time))
                    {
                        RecipeList = new ObservableCollection<Recipe>(recipeController.SearchByTime(time));
                    }
                    else
                    {
                        RecipeList = new ObservableCollection<Recipe>();
                        ShowMessage("Enter a valid number for Time");
                    }
                    break;

                case "By Tag":
                    ShowMessage("");
                    string[] strTags = Input.Split(' ');
                    List<Tag> tags = new List<Tag>();
                    foreach (string tag in strTags)
                    {
                        tags.Add(new Tag(tag));
                    }
                    RecipeList = new ObservableCollection<Recipe>(recipeController.SearchByTag(tags));
                    break;

                case "By Servings":
                    ShowMessage("");
                    if (int.TryParse(Input, out int serving))
                    {
                        RecipeList = new ObservableCollection<Recipe>(recipeController.SearchByServings(serving));
                    }
                    else
                    {
                        RecipeList = new ObservableCollection<Recipe>();
                        ShowMessage("Enter a valid number for Servings");
                    }

                    break;

                case "By Ingredients":
                    ShowMessage("");
                    string[] strIngredients = Input.Split(' ');
                    bool validIngredients = true;
                    List<string> ingredients = new List<string>();

                    foreach (string ingredient in strIngredients)
                    {
                        try
                        {
                            Ingredient i = ingredientController.SearchByExactName(ingredient);
                            ingredients.Add(ingredient);
                        }
                        catch (Exception e)
                        {
                            validIngredients = false;
                            ShowMessage(e.Message);
                            break;
                        }
                    }
                    if (validIngredients)
                        RecipeList = new ObservableCollection<Recipe>(recipeController.SearchByIngredients(ingredients));
                    break;

                // Search recipes by exact ingredients only in the recipe
                case "By Exact Ingredients":
                    ShowMessage("");
                    string[] ingredientsArray = Input.Split(' ');
                    bool validIngredientsList = true;
                    List<string> ingredientsList = new List<string>();

                    foreach (string ingredient in ingredientsArray)
                    {
                        try
                        {
                            Ingredient i = ingredientController.SearchByExactName(ingredient);
                            ingredientsList.Add(ingredient);
                        }
                        catch (Exception e)
                        {
                            validIngredientsList = false;
                            ShowMessage(e.Message);
                            RecipeList = new ObservableCollection<Recipe>();
                            break;
                        }
                    }
                    if (validIngredientsList)
                        RecipeList = new ObservableCollection<Recipe>(recipeController.SearchByExactIngredients(ingredientsList));
                    break;

                case "By 1 Ingredient":
                    ShowMessage("");
                    bool validIngredient = true;
                    List<string> oneIngredientList = new List<string>();
                    try
                    {
                        Ingredient i = ingredientController.SearchByExactName(Input);
                        oneIngredientList.Add(Input);
                    }
                    catch (Exception e)
                    {
                        validIngredient = false;
                        ShowMessage(e.Message);
                        RecipeList = new ObservableCollection<Recipe>();
                        break;
                    }
                    if (validIngredient)
                        RecipeList = new ObservableCollection<Recipe>(recipeController.SearchByIngredients(oneIngredientList));
                    break;

                case "By Ratings":
                    ShowMessage("");
                    if (int.TryParse(Input, out int rate))
                    {
                        RecipeList = new ObservableCollection<Recipe>(recipeController.SearchByRating(rate));
                    }
                    else
                    {
                        RecipeList = new ObservableCollection<Recipe>();
                        ShowMessage("Enter a valid number for Servings");
                    }
                    break;
            }
        }

        /// <summary>
        /// Sorts the recipe list based on selected sort option.
        /// </summary>
        public void SortBy()
        {
            if (RecipeList != null && RecipeList.Count > 0 && SelectedSortOption != null)
                RecipeList = new ObservableCollection<Recipe>(recipeController.SortByCriteria(RecipeList.ToList(), SelectedSortOption));
        }

        /// <summary>
        /// Displays a message in the UI.
        /// </summary>
        public void ShowMessage(string msg)
        {
            Message = msg;
        }

    }
}