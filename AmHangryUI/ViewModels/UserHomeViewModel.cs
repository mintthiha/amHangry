using UserNameSpace;

namespace AmHangryUI.ViewModels;

/// <summary>
/// ViewModel for the UserHome view.
/// </summary>
public class UserHomeViewModel : ViewModelBase
{
    private string _username = null!;

    /// <summary>
    /// The username of the user.
    /// </summary>
    public string Username
    {
        get => _username;
        set => _username = value;
    }

    /// <summary>
    /// Initializes a new instance of the UserHomeViewModel class.
    /// </summary>
    /// <param name="user">The user object.</param>
    public UserHomeViewModel(User user)
    {
        Username = user.Username;
    }
}