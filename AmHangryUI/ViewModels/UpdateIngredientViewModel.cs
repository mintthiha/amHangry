using ReactiveUI;

namespace AmHangryUI.ViewModels
{
    /// <summary>
    /// ViewModel for updating an ingredient.
    /// </summary>
    public class UpdateIngredientViewModel : ViewModelBase
    {
        private Ingredient _ingredient = null!;
        /// <summary>
        /// The ingredient to be updated.
        /// </summary>
        public Ingredient Ingredient
        {
            get => _ingredient;
            set => this.RaiseAndSetIfChanged(ref _ingredient, value);
        }

        private string _name = string.Empty;
        /// <summary>
        /// The name of the ingredient.
        /// </summary>
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _category = string.Empty;
        /// <summary>
        /// The category of the ingredient.
        /// </summary>
        public string Category
        {
            get => _category;
            set => this.RaiseAndSetIfChanged(ref _category, value);
        }

        private int _protein;
        /// <summary>
        /// The amount of protein in the ingredient.
        /// </summary>
        public int Protein
        {
            get => _protein;
            set => this.RaiseAndSetIfChanged(ref _protein, value);
        }

        private int _fat;
        /// <summary>
        /// The amount of fat in the ingredient.
        /// </summary>
        public int Fat
        {
            get => _fat;
            set => this.RaiseAndSetIfChanged(ref _fat, value);
        }

        private int _carbs;
        /// <summary>
        /// The amount of carbs in the ingredient.
        /// </summary>
        public int Carbs
        {
            get => _carbs;
            set => this.RaiseAndSetIfChanged(ref _carbs, value);
        }

        private double _cost;
        /// <summary>
        /// The cost of the ingredient.
        /// </summary>
        public double Cost
        {
            get => _cost;
            set => this.RaiseAndSetIfChanged(ref _cost, value);
        }

        private double _amount;
        /// <summary>
        /// The amount of the ingredient.
        /// </summary>
        public double Amount
        {
            get => _amount;
            set => this.RaiseAndSetIfChanged(ref _amount, value);
        }

        private string _ingUnit = null!;
        /// <summary>
        /// The unit of the ingredient.
        /// </summary>
        public string IngUnit
        {
            get => _ingUnit;
            set => this.RaiseAndSetIfChanged(ref _ingUnit, value);
        }

        private string _message = string.Empty;
        /// <summary>
        /// The message to display to the user.
        /// </summary>
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }

        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Confirm { get; }
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Cancel { get; }

        private IngredientController _ingredientController = null!;
        /// <summary>
        /// The IngredientController used to interact with the Ingredient data.
        /// </summary>
        public IngredientController IngredientController
        {
            get => _ingredientController;
            set => _ingredientController = value;
        }

        /// <summary>
        /// Initializes a new instance of the UpdateIngredientViewModel class.
        /// </summary>
        /// <param name="ingredient">The ingredient to be updated.</param>
        /// <param name="ingredientController">The IngredientController used to interact with the Ingredient data.</param>
        public UpdateIngredientViewModel(Ingredient ingredient, IngredientController ingredientController)
        {
            Ingredient = ingredient;
            IngredientController = ingredientController;
            Name = ingredient.Name;
            Category = ingredient.Category;
            Protein = ingredient.Protein;
            Fat = ingredient.Fat;
            Carbs = ingredient.Carbs;
            Cost = ingredient.Cost;
            Amount = ingredient.Amount;
            IngUnit = ingredient.UnitEntity.ToString();

            Confirm = ReactiveCommand.Create(() => { });
            Cancel = ReactiveCommand.Create(() => { });
        }

        /// <summary>
        /// Updates the ingredient in the database.
        /// </summary>
        /// <returns>True if the ingredient was updated successfully, false otherwise.</returns>
        public bool UpdateIngredient()
        {
            try
            {
                UnitEntity unit = new UnitEntity();
                if (IngUnit == "Gram")
                {
                    unit.UnitEntityId = 1;
                    unit.Unit = Unit.Gram;
                }
                else if (IngUnit == "Kilogram")
                {
                    unit.UnitEntityId = 2;
                    unit.Unit = Unit.Kilogram;
                }
                else if (IngUnit == "Milliliter")
                {
                    unit.UnitEntityId = 3;
                    unit.Unit = Unit.Milliliter;
                }
                else if (IngUnit == "Liter")
                {
                    unit.UnitEntityId = 4;
                    unit.Unit = Unit.Liter;
                }
                else
                {
                    throw new ArgumentException("Invalid Unit value, try 'Gram', 'Kilogram', 'Milliliter' or 'Liter'");
                }

                UnitEntity unitToUpdate = IngredientController.ReturnExistingEntitiyUnit(unit.Unit);

                IngredientController.UpdateCurrentIngredientName(Name, Ingredient);
                IngredientController.UpdateIngredientCategory(Category, Ingredient);
                IngredientController.UpdateIngredientProtein(Protein, Ingredient);
                IngredientController.UpdateIngredientFat(Fat, Ingredient);
                IngredientController.UpdateIngredientCarbs(Carbs, Ingredient);
                IngredientController.UpdateIngredientCost(Cost, Ingredient);
                IngredientController.UpdateIngredientAmount(Amount, Ingredient);
                IngredientController.UpdateIngredientUnit(unitToUpdate, Ingredient);

                return true;
            }
            catch (ArgumentException e)
            {
                Message = e.Message;
                return false;
            }
        }
    }
}