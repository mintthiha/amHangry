using System.Collections.ObjectModel;
using ReactiveUI;
namespace AmHangryUI.ViewModels
{
    public class EditOwnedRecipeViewModel : ViewModelBase
    {
        private Recipe _selectedRecipe = null!;
        private string _recipeName = null!;
        private string _recipeDescription = null!;
        private double _preparationTime;
        private double _cookingTime;
        private int _servings;
        private string _recipeInstruction = null!;
        private string _recipeTag = null!;
        private int _quantity;
        private string _message = null!;
        private ObservableCollection<Instruction> _recipeInstructions = null!;

        private ObservableCollection<Tag> _recipeTags = null!;
        private ObservableCollection<RecipeIngredient> _recipeIngredients = null!;

        private Ingredient _selectedIngredient = null!;
        public RecipeController RecipeController { get; }
        public IngredientController IngredientController { get; }

        /// <summary>
        /// Gets or sets the selected recipe.
        /// </summary>
        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set => this.RaiseAndSetIfChanged(ref _selectedRecipe, value);
        }

        /// <summary>
        /// Gets or sets the name of the recipe.
        /// </summary>
        public string RecipeName
        {
            get => _recipeName;
            set => this.RaiseAndSetIfChanged(ref _recipeName, value);
        }

        /// <summary>
        /// Gets or sets the description of the recipe.
        /// </summary>
        public string RecipeDescription
        {
            get => _recipeDescription;
            set => this.RaiseAndSetIfChanged(ref _recipeDescription, value);
        }

        /// <summary>
        /// Gets or sets the preparation time of the recipe.
        /// </summary>
        public double RecipePrepTime
        {
            get => _preparationTime;
            set => this.RaiseAndSetIfChanged(ref _preparationTime, value);
        }

        /// <summary>
        /// Gets or sets the cooking time of the recipe.
        /// </summary>
        public double RecipeCookTime
        {
            get => _cookingTime;
            set => this.RaiseAndSetIfChanged(ref _cookingTime, value);
        }

        /// <summary>
        /// Gets or sets the number of servings for the recipe.
        /// </summary>
        public int RecipeServings
        {
            get => _servings;
            set => this.RaiseAndSetIfChanged(ref _servings, value);
        }

        /// <summary>
        /// Gets or sets the instructions for the recipe.
        /// </summary>
        public string RecipeInstruction
        {
            get => _recipeInstruction;
            set => this.RaiseAndSetIfChanged(ref _recipeInstruction, value);
        }

        /// <summary>
        /// Gets or sets the tags associated with the recipe.
        /// </summary>
        public string RecipeTag
        {
            get => _recipeTag;
            set => this.RaiseAndSetIfChanged(ref _recipeTag, value);
        }

        /// <summary>
        /// Gets or sets the quantity of an ingredient.
        /// </summary>
        public int Quantity
        {
            get => _quantity;
            set => this.RaiseAndSetIfChanged(ref _quantity, value);
        }

        /// <summary>
        /// Gets or sets the collection of instructions for the recipe.
        /// </summary>
        public ObservableCollection<Instruction> RecipeInstructions
        {
            get => _recipeInstructions;
            set => this.RaiseAndSetIfChanged(ref _recipeInstructions, value);
        }

        /// <summary>
        /// Gets or sets the collection of tags for the recipe.
        /// </summary>
        public ObservableCollection<Tag> RecipeTags
        {
            get => _recipeTags;
            set => this.RaiseAndSetIfChanged(ref _recipeTags, value);
        }

        /// <summary>
        /// Gets or sets the collection of ingredients for the recipe.
        /// </summary>
        public ObservableCollection<RecipeIngredient> RecipeIngredients
        {
            get => _recipeIngredients;
            set => this.RaiseAndSetIfChanged(ref _recipeIngredients, value);
        }

        /// <summary>
        /// Gets or sets the selected ingredient.
        /// </summary>
        public Ingredient SelectedIngredient
        {
            get => _selectedIngredient;
            set => this.RaiseAndSetIfChanged(ref _selectedIngredient, value);
        }

        /// <summary>
        /// Returns a list filled with all the ingredients in the database
        /// </summary>
        private List<Ingredient> PopulateIngredients()
        {
            List<Ingredient> ingredientsList = IngredientController.GetAllIngredientsDB();
            return ingredientsList;
        }

        /// <summary>
        /// Gets the available ingredients.
        /// </summary>
        public List<Ingredient> IngredientsAvailable => PopulateIngredients();

        /// <summary>
        /// Gets or sets the message to display.
        /// </summary>
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }

        /// <summary>
        /// Command to add an instruction to the recipe.
        /// </summary>
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> AddInstructionCommand { get; }

        /// <summary>
        /// Command to remove an instruction from the recipe.
        /// </summary>
        public ReactiveCommand<Instruction, System.Reactive.Unit> RemoveInstructionCommand { get; }

        /// <summary>
        /// Command to add a tag to the recipe.
        /// </summary>
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> AddTagCommand { get; }

        /// <summary>
        /// Command to remove a tag from the recipe.
        /// </summary>
        public ReactiveCommand<Tag, System.Reactive.Unit> RemoveTagCommand { get; }

        /// <summary>
        /// Command to add an ingredient to the recipe.
        /// </summary>
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> AddIngredientCommand { get; }

        /// <summary>
        /// Command to remove an ingredient from the recipe.
        /// </summary>
        public ReactiveCommand<RecipeIngredient, System.Reactive.Unit> RemoveIngredientCommand { get; }

        /// <summary>
        /// Command to confirm the edit of the recipe.
        /// </summary>
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> ConfirmEditRecipe { get; }

        /// <summary>
        /// Command to cancel the edit of the recipe.
        /// </summary>
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> CancelEditRecipe { get; }

        /// <summary>
        /// Initializes a new instance of the EditOwnedRecipeViewModel class.
        /// </summary>
        /// <param name="recipeToEdit">The recipe to edit.</param>
        /// <param name="recipeController">The recipe controller.</param>
        /// <param name="ingredientController">The ingredient controller.</param>
        public EditOwnedRecipeViewModel(Recipe recipeToEdit, RecipeController recipeController, IngredientController ingredientController)
        {
            RecipeController = recipeController;
            IngredientController = ingredientController;
            SelectedRecipe = recipeToEdit;

            RecipeName = recipeToEdit.Name;
            RecipeDescription = recipeToEdit.Description;
            
            RecipePrepTime = (double)recipeToEdit.PreparationTime;
            RecipeCookTime = (double)recipeToEdit.CookingTime;
            RecipeServings = recipeToEdit.Servings;
            RecipeInstructions = new ObservableCollection<Instruction>(recipeToEdit.Instructions);
            RecipeTags = new ObservableCollection<Tag>(recipeToEdit.Tags);
            RecipeIngredients = new ObservableCollection<RecipeIngredient>(recipeToEdit.RecipeIngredients);

            AddInstructionCommand = ReactiveCommand.Create(AddInstruction);
            RemoveInstructionCommand = ReactiveCommand.Create<Instruction>(RemoveInstruction);

            AddTagCommand = ReactiveCommand.Create(AddTag);
            RemoveTagCommand = ReactiveCommand.Create<Tag>(RemoveTag);

            AddIngredientCommand = ReactiveCommand.Create(AddRecipeIngredient);
            RemoveIngredientCommand = ReactiveCommand.Create<RecipeIngredient>(RemoveRecipeIngredient);
            ConfirmEditRecipe = ReactiveCommand.Create(() => { });
            CancelEditRecipe = ReactiveCommand.Create(() => { });
        }

        /// <summary>
        /// Updates the recipe details in the database.
        /// </summary>
        /// <returns>true if the update was successful, false otherwise.</returns>
        public bool UpdateRecipe()
        {
            try
            {
                if (SelectedRecipe.Description != RecipeDescription)
                    RecipeController.UpdateRecipeDescriptionDB(RecipeDescription, SelectedRecipe);
                if (SelectedRecipe.Name != RecipeName)
                    RecipeController.UpdateRecipeNameDB(RecipeName, SelectedRecipe);
                if (SelectedRecipe.CookingTime != RecipeCookTime)
                    RecipeController.UpdateCookingTimeDB(RecipeCookTime, SelectedRecipe);
                if (SelectedRecipe.PreparationTime != RecipePrepTime)
                    RecipeController.UpdatePreparationTimeDB(RecipePrepTime, SelectedRecipe);
                if (SelectedRecipe.Servings != RecipeServings)
                    RecipeController.UpdateRecipeServingDB(RecipeServings, SelectedRecipe);
                RecipeController.UpdateRecipeInstructionsDB(RecipeInstructions.ToList(), SelectedRecipe);
                RecipeController.UpdateRecipeTagsDB(RecipeTags.ToList(), SelectedRecipe);
                RecipeController.UpdateRecipeIngredientsDB(RecipeIngredients.ToList(), SelectedRecipe);

                return true;
            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }
        }
        /// <summary>
        /// Adds a new instruction to the recipe.
        /// </summary>
        private void AddInstruction()
        {
            RecipeInstructions.Add(new Instruction(RecipeInstruction));
            RecipeInstruction = "";
        }

        /// <summary>
        /// Removes an instruction from the recipe.
        /// </summary>
        /// <param name="instruction">The instruction to remove.</param>
        private void RemoveInstruction(Instruction instruction)
        {
            if (RecipeInstructions.Any())
            {
                RecipeInstructions.Remove(instruction);
            }
        }

        /// <summary>
        /// Adds a new tag to the recipe.
        /// </summary>
        private void AddTag()
        {
            RecipeTags.Add(new Tag(RecipeTag));
            RecipeTag = "";
        }

        /// <summary>
        /// Removes a tag from the recipe.
        /// </summary>
        /// <param name="tag">The tag to remove.</param>
        private void RemoveTag(Tag tag)
        {
            if (RecipeTags.Count > 0)
            {
                RecipeTags.Remove(tag);
            }
        }

        /// <summary>
        /// Adds a new ingredient to the recipe.
        /// </summary>
        public void AddRecipeIngredient()
        {
            RecipeIngredient recipeIngredient = new RecipeIngredient
            {
                Ingredient = SelectedIngredient
            };
            SelectedIngredient = new RecipeIngredient().Ingredient;
            recipeIngredient.Quantity = Quantity;
            Quantity = 0;
            RecipeIngredients.Add(recipeIngredient);
        }

        /// <summary>
        /// Removes an ingredient from the recipe.
        /// </summary>
        /// <param name="recipeIngredient">The ingredient to remove.</param>
        public void RemoveRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            if (RecipeIngredients.Count > 0)
            {
                RecipeIngredients.Remove(recipeIngredient);
            }
        }
    }
}
