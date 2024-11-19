using ReactiveUI;

namespace AmHangryUI.ViewModels;

/// <summary>
/// ViewModel for the DeleteIngredient View.
/// </summary>
public class DeleteIngredientViewModel : ViewModelBase
{
    private Ingredient _ingredient = null!;
    /// <summary>
    /// The ingredient to be deleted.
    /// </summary>
    public Ingredient Ingredient
    {
        get => _ingredient;
        set => _ingredient = value;
    }

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
    /// The command to confirm the deletion of the ingredient.
    /// </summary>
    public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Confirm { get; }

    /// <summary>
    /// The command to cancel the deletion of the ingredient.
    /// </summary>
    public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Cancel { get; }

    /// <summary>
    /// Initializes a new instance of the DeleteIngredientViewModel class.
    /// </summary>
    /// <param name="ingredient">The ingredient to be deleted.</param>
    /// <param name="ingredientController">The IngredientController used to interact with the Ingredient data.</param>
    public DeleteIngredientViewModel(Ingredient ingredient, IngredientController ingredientController)
    {
        Ingredient = ingredient;
        IngredientController = ingredientController;

        Confirm = ReactiveCommand.Create(() => { });
        Cancel = ReactiveCommand.Create(() => { });
    }

    /// <summary>
    /// Deletes the ingredient from the database.
    /// </summary>
    /// <param name="ingredient">The ingredient to be deleted.</param>
    public void DeleteIngredient(Ingredient ingredient)
    {
        IngredientController.DeleteIngredient(ingredient);
    }
}