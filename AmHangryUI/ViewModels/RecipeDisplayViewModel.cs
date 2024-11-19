using ReactiveUI;
using UserNameSpace;

namespace AmHangryUI.ViewModels
{
    public class RecipeDisplayViewModel : ViewModelBase
    {
        public UserController userController { get; set; }
        public User loggedUser;
        private Recipe _recipe = null!;
        public Recipe Recipe
        {
            get => _recipe;
            set => this.RaiseAndSetIfChanged(ref _recipe, value);
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

        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> AddToFavoriteCommand { get; }

        public RecipeDisplayViewModel(Recipe recipe, UserController uc, User user)
        {
            Recipe = recipe;
            userController = uc;
            loggedUser = user;

            AddToFavoriteCommand = ReactiveCommand.Create(AddFavorite);
        }

        /// <summary>
        /// Adds the recipe to the user's favorites.
        /// </summary>
        public void AddFavorite()
        {
            try
            {
                MemberController mc = (MemberController)userController;
                mc.AddFavRecipe(Recipe, (Member)loggedUser);
                ShowMessage("You have successfully added the recipe to your favorite list!");
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