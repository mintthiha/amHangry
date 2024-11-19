using ReactiveUI;
using UserNameSpace;
using System.Collections.ObjectModel;

namespace AmHangryUI.ViewModels
{
    public class OwnedRecipesViewModel : ViewModelBase
    {   
        /// <summary>
        /// Backing field for the user currently logged into the application
        /// </summary>
        public User _loggedUser = null!;
        /// <summary>
        /// Property field for the user currently logged into the application
        /// </summary>
        public User LoggedUser
        {
            get => _loggedUser;
            set => _loggedUser =value;
        }
        /// <summary>
        /// Backing field for the message to display status of operations
        /// </summary>
        private string _message = string.Empty;
        /// <summary>
        /// Property field for the message to display status of operations
        /// </summary>
        public string Message
        {
            get=> _message;
            set=> this.RaiseAndSetIfChanged(ref _message, value);
        }
        /// <summary>
        /// Controller for the users, interface as to allow to be used by admins or members
        /// </summary>
        public UserController UserController { get; set; }
        /// <summary>
        /// Controller for the recipes, manipulates all data related to recipes
        /// </summary>
        public RecipeController RecipeController {get; set;}
        /// <summary>
        /// Observable list for the owned recipes from the user
        /// </summary>
        public ObservableCollection<Recipe> OwnedRecipes { get; set; }

        /// <summary>
        /// Sets up the view to display the recipes made by the user or display all the recipes as to allow the admin to modify
        /// </summary>
        /// <param name="uc"> UserController </param>
        /// <param name="user">current user, admin or member</param>
        /// <param name="rc">RecipeController </param>
        public OwnedRecipesViewModel(UserController uc, User user, RecipeController rc)
        {
            UserController = uc;
            LoggedUser = user;
            RecipeController = rc;
            if(LoggedUser is Member)
            {
                MemberController mc = (MemberController) UserController;
                Member member = (Member) LoggedUser;
                if (member.OwnedRecipes != null){
                    OwnedRecipes = new ObservableCollection<Recipe>(mc.GetOwnedRecipes((Member)user));
                }else{
                    OwnedRecipes = new ObservableCollection<Recipe>();
                }
            }
            else
            {
                OwnedRecipes = new ObservableCollection<Recipe>(rc.getAllRecipesDB());
            }
        }
        
        /// <summary>
        /// Sets a success message indicating that a recipe has been successfully deleted.
        /// </summary>
        public void SuccessDeleteMessage()
        {
            Message = "Recipe deleted successfully!";
        }

        /// <summary>
        /// Sets a success message indicating that a recipe has been successfully updated.
        /// </summary>
        public void SuccessUpdateMessage()
        {
            Message = "Recipe updated successfully!";
        }
    }
}