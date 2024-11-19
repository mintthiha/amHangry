using ReactiveUI;
using UserNameSpace;
using System.Collections.ObjectModel;

namespace AmHangryUI.ViewModels
{
    public class CreateRecipeViewModel : ViewModelBase
    {
        public RecipeController recipeController;
        public IngredientController ingredientController;

        public User loggedUser;
        private string _recipeName = null!;
        private string _recipeDescription = null!;
        private int _preparationTime;
        private int _cookingTime;
        private string _instructions = null!;
        private string _tag = null!;
        private int _servings;
        private int _quantity;

        public string RecipeName
        {
            get => _recipeName;
            set => this.RaiseAndSetIfChanged(ref _recipeName, value);
        }

        public string RecipeDescription
        {
            get => _recipeDescription;
            set => this.RaiseAndSetIfChanged(ref _recipeDescription, value);
        }

        public int PreparationTime
        {
            get => _preparationTime;
            set => this.RaiseAndSetIfChanged(ref _preparationTime, value);
        }

        public int CookingTime
        {
            get => _cookingTime;
            set => this.RaiseAndSetIfChanged(ref _cookingTime, value);
        }

        public string Instructions
        {
            get => _instructions;
            set => this.RaiseAndSetIfChanged(ref _instructions, value);
        }

        public string Tag
        {
            get => _tag;
            set => this.RaiseAndSetIfChanged(ref _tag, value);
        }

        public int Servings
        {
            get => _servings;
            set => this.RaiseAndSetIfChanged(ref _servings, value);
        }
        public int Quantity
        {
            get => _quantity;
            set => this.RaiseAndSetIfChanged(ref _quantity, value);
        }

        /// <summary>
        /// Property to get or set the message to be displayed.
        /// </summary>
        public string _message = string.Empty;
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }

        public Ingredient _selectedIngredient = null!;
        public Ingredient SelectedIngredient
        {
            get { return _selectedIngredient; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedIngredient, value);
            }
        }

        // Command for adding a recipe
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> CreateRecipeCommand { get; }

        // Commands for adding and removing recipe components
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> AddInstructionCommand { get; }
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> AddTagCommand { get; }
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> AddRecipeIngredientCommand { get; }

        // Commands for removing recipe components
        public ReactiveCommand<Instruction, System.Reactive.Unit> RemoveInstructionCommand { get; }
        public ReactiveCommand<Tag, System.Reactive.Unit> RemoveTagCommand { get; }
        public ReactiveCommand<RecipeIngredient, System.Reactive.Unit> RemoveRecipeIngredientCommand { get; }

        // Collection for holding instructions
        private ObservableCollection<Instruction> _instructionsItems = null!;
        public ObservableCollection<Instruction> InstructionsItems
        {
            get => _instructionsItems;
            set => this.RaiseAndSetIfChanged(ref _instructionsItems, value);
        }

        // Collection for holding tags
        private ObservableCollection<Tag> _tagsItems = null!;
        public ObservableCollection<Tag> TagsItems
        {
            get => _tagsItems;
            set => this.RaiseAndSetIfChanged(ref _tagsItems, value);
        }

        // Collection for holding recipeIngredients
        private ObservableCollection<RecipeIngredient> _recipeIngredientsItems = null!;
        public ObservableCollection<RecipeIngredient> RecipeIngredientsItems
        {
            get => _recipeIngredientsItems;
            set => this.RaiseAndSetIfChanged(ref _recipeIngredientsItems, value);
        }


        public CreateRecipeViewModel(RecipeController rc, IngredientController ic, User lu)
        {
            recipeController = rc;
            ingredientController = ic;
            loggedUser = lu;

            AddInstructionCommand = ReactiveCommand.Create(AddInstruction);
            AddTagCommand = ReactiveCommand.Create(AddTag);
            AddRecipeIngredientCommand = ReactiveCommand.Create(AddRecipeIngredient);
            RemoveInstructionCommand = ReactiveCommand.Create<Instruction>(RemoveInstruction);
            RemoveTagCommand = ReactiveCommand.Create<Tag>(RemoveTag);
            RemoveRecipeIngredientCommand = ReactiveCommand.Create<RecipeIngredient>(RemoveRecipeIngredient);

            CreateRecipeCommand = ReactiveCommand.Create(CreateRecipe);
            InstructionsItems = new ObservableCollection<Instruction>();
            TagsItems = new ObservableCollection<Tag>();
            RecipeIngredientsItems = new ObservableCollection<RecipeIngredient>();
        }

        /// <summary>
        /// Populates the list of ingredients from the database.
        /// </summary>
        private List<Ingredient> PopulateIngredients()
        {
            List<Ingredient> ingredientsList = ingredientController.GetAllIngredientsDB();
            return ingredientsList;
        }

        public List<Ingredient> Ingredients => PopulateIngredients();

        // <summary>
        /// Adds an instruction to the collection that's holding instructions
        /// </summary>
        private void AddInstruction()
        {
            InstructionsItems.Add(new Instruction(Instructions));
            Instructions = "";
        }

        // <summary>
        /// Remove an instruction from the collection that's holding instructions
        /// </summary>
        /// <param name="instruction">Instruction to be removed.</param>
        private void RemoveInstruction(Instruction instruction)
        {
            if (InstructionsItems.Count > 0)
            {
                InstructionsItems.Remove(instruction);
            }
        }

        // <summary>
        /// Adds a tag to the collection that's holding tags
        /// </summary>
        private void AddTag()
        {
            TagsItems.Add(new Tag(Tag));
            Tag = "";
        }

        // <summary>
        /// Remove a tag from the collection that's holding tags
        /// </summary>
        /// <param name="tag">Tag to be removed.</param>
        private void RemoveTag(Tag tag)
        {
            if (TagsItems.Count > 0)
            {
                TagsItems.Remove(tag);
            }
        }

        // <summary>
        /// Adds a recipeIngredient to the collection that's holding recipeIngredients
        /// </summary>
        public void AddRecipeIngredient()
        {
            RecipeIngredient recipeIngredient = new RecipeIngredient();
            recipeIngredient.Ingredient = SelectedIngredient;
            SelectedIngredient = null!;
            recipeIngredient.Quantity = Quantity;
            Quantity = 0;

            RecipeIngredientsItems.Add(recipeIngredient);
        }

        // <summary>
        /// Removes a recipeIngredient from the collection that's holding recipeIngredients
        /// </summary>
        /// <param name="recipeIngredient">RecipeIngredient to be removed.</param>
        public void RemoveRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            if (RecipeIngredientsItems.Count > 0)
            {
                RecipeIngredientsItems.Remove(recipeIngredient);
            }
        }

        /// <summary>
        /// Creates a new recipe with the specified details.
        /// </summary>
        private void CreateRecipe()
        {
            try
            {
                recipeController.CreateRecipeDB(RecipeName, (Member)loggedUser, RecipeDescription, PreparationTime, CookingTime, InstructionsItems.ToList(), TagsItems.ToList(), Servings, RecipeIngredientsItems.ToList());
                ShowMessage("The Recipe has been created!");
            }
            catch (Exception e)
            {
                ShowMessage(e.Message);
            }
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