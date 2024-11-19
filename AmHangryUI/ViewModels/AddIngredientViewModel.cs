using ReactiveUI;

namespace AmHangryUI.ViewModels;

/// <summary>
/// ViewModel for the AddIngredient View.
/// </summary>
public class AddIngredientViewModel : ViewModelBase
{
    private IngredientController _ingredientController = null!;
    /// <summary>
    /// The IngredientController used to interact with the Ingredient data.
    /// </summary>
    public IngredientController IngredientController
    {
        get => _ingredientController;
        set => _ingredientController = value;
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

    /// <summary>
    /// The command to confirm the addition of the ingredient.
    /// </summary>
    public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Confirm { get; }

    /// <summary>
    /// The command to cancel the addition of the ingredient.
    /// </summary>
    public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Cancel { get; }

    /// <summary>
    /// Initializes a new instance of the AddIngredientViewModel class.
    /// </summary>
    /// <param name="ingredientController">The IngredientController is used to interact with the Ingredient data.</param>
    public AddIngredientViewModel(IngredientController ingredientController)
    {
        IngredientController = ingredientController;
        Confirm = ReactiveCommand.Create(() => { });
        Cancel = ReactiveCommand.Create(() => { });
    }

    /// <summary>
    /// Adds the ingredient to the database.
    /// </summary>
    /// <returns>True if the ingredient was added successfully, false otherwise.</returns>
    public bool AddIngredient()
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

            UnitEntity unitToAdd = IngredientController.ReturnExistingEntitiyUnit(unit.Unit);

            Ingredient ingredientToAdd = new Ingredient(Name, Category, Protein, Fat, Carbs, Cost, Amount, unitToAdd);
            IngredientController.CreateIngredient(ingredientToAdd);
            return true;
        }
        catch (ArgumentException e)
        {
            Message = e.Message;
            return false;
        }
    }
}