using System.Collections.ObjectModel;

namespace AmHangryUI.ViewModels;

/// <summary>
/// ViewModel for displaying a list of ingredients.
/// </summary>
public class IngredientsViewModel : ViewModelBase
{
    /// <summary>
    /// The IngredientController used to interact with the Ingredient data.
    /// </summary>
    public IngredientController IngredientService { get; }

    private string _flashMessage = null!;

    /// <summary>
    /// The message to display to the user.
    /// </summary>
    public string FlashMessage
    {
        get => _flashMessage;
        set => _flashMessage = value;
    }

    /// <summary>
    /// Initializes a new instance of the IngredientsViewModel class.
    /// </summary>
    /// <param name="ingredientController">The IngredientController used to interact with the Ingredient data.</param>
    public IngredientsViewModel(IngredientController ingredientController)
    {
        IngredientService = ingredientController;
    }

    /// <summary>
    /// Populates the list of ingredients.
    /// </summary>
    /// <returns>A list of ingredients.</returns>
    private List<Ingredient> PopulateIngredients()
    {
        List<Ingredient> ingredientsList = IngredientService.GetAllIngredientsDB();
        return ingredientsList;
    }

    /// <summary>
    /// The list of ingredients.
    /// </summary>
    public ObservableCollection<Ingredient> IngredientList => new ObservableCollection<Ingredient>(PopulateIngredients());

    /// <summary>
    /// Displays a success message to the user.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public void SuccessMessage(string message)
    {
        FlashMessage = message;
    }

    /// <summary>
    /// Displays a cancel message to the user.
    /// </summary>
    /// <param name="message">The message to display.</param>
    public void CancelMessage(string message)
    {
        FlashMessage = message;
    }
}