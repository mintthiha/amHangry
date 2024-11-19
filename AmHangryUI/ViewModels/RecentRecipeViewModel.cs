using System.Collections.ObjectModel;
using UserNameSpace;

namespace AmHangryUI.ViewModels;

/// <summary>
/// ViewModel for displaying a list of recent recipes.
/// </summary>
public class RecentRecipeViewModel : ViewModelBase
{
    private Member _loggedUser;

    /// <summary>
    /// The logged in user.
    /// </summary>
    public Member LoggedUser
    {
        get
        {
            return _loggedUser;
        }
        set
        {
            _loggedUser = value;
        }
    }

    /// <summary>
    /// The list of recent recipes.
    /// </summary>
    public ObservableCollection<Recipe> RecentRecipeList { get; set; }

    private string _message = null!;

    /// <summary>
    /// The message to display to the user.
    /// </summary>
    public string Message
    {
        get { return _message; }
        set { _message = value; }
    }

    /// <summary>
    /// Initializes a new instance of the RecentRecipeViewModel class.
    /// </summary>
    /// <param name="loggedUser">The logged in user.</param>
    public RecentRecipeViewModel(Member loggedUser)
    {
        _loggedUser = loggedUser;
        List<Recipe> recipes = new List<Recipe>();
        List<RecentRecipeSearch> rrc = loggedUser.RecentRecipeSearches;
        foreach (RecentRecipeSearch rc in rrc)
        {
            recipes.Add(rc.Recipe);
        }
        RecentRecipeList = new ObservableCollection<Recipe>(recipes);
        if (RecentRecipeList.Count() == 0)
        {
            Message = "Your Recent Recipe List Is Empty! Go Search!";
        }
    }
}