namespace AmHangryUI.ViewModels;

/// <summary>
/// ViewModel for displaying an ingredient.
/// </summary>
public class IngredientDisplayViewModel : ViewModelBase
{
    private Ingredient _ingredient = null!;

    /// <summary>
    /// The ingredient to be displayed.
    /// </summary>
    public Ingredient Ingredient
    {
        get => _ingredient;
        set => _ingredient = value;
    }

    private string _ingredientStringUnit = null!;

    /// <summary>
    /// The unit of the ingredient as a string.
    /// </summary>
    public string IngredientStringUnit
    {
        get => _ingredientStringUnit;
        set => _ingredientStringUnit = value;
    }

    /// <summary>
    /// Initializes a new instance of the IngredientDisplayViewModel class.
    /// </summary>
    /// <param name="ingredient">The ingredient to be displayed.</param>
    public IngredientDisplayViewModel(Ingredient ingredient)
    {
        _ingredient = ingredient;
        IngredientStringUnit = ingredient.UnitEntity.ToString();
    }
}