using ReactiveUI;
using UserNameSpace;

namespace AmHangryUI.ViewModels
{
    public class DeleteRecipeViewModel : ViewModelBase
    {
        /// <summary>
        /// The logged-in user.
        /// </summary>
        public User _loggedUser = null!;

        /// <summary>
        /// Gets or sets the logged-in user.
        /// </summary>
        public User LoggedUser
        {
            get => _loggedUser;
            set => _loggedUser = value;
        }

        /// <summary>
        /// The recipe to be deleted.
        /// </summary>
        public Recipe Recipe { get; set; }

        /// <summary>
        /// The controller manipulating member data
        /// </summary>
        public MemberController MemberController { get; set; }

        /// <summary>
        /// The controller manipulating recipe data
        /// </summary>
        public RecipeController RecipeController { get; set; }

        /// <summary>
        /// A command to confirm the deletion of the recipe.
        /// </summary>
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Confirm { get; }

        /// <summary>
        /// A command to cancel the deletion of the recipe.
        /// </summary>
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Cancel { get; }

        /// <summary>
        /// Initializes a new instance of the deleterecipeviewmodel class.
        /// </summary>
        /// <param name="recipeToDelete">The recipe to delete.</param>
        /// <param name="mc">The member controller.</param>
        /// <param name="user">The logged-in user.</param>
        /// <param name="rc">The recipe controller.</param>
        public DeleteRecipeViewModel(Recipe recipeToDelete, MemberController mc, User user, RecipeController rc)
        {
            MemberController = mc;
            LoggedUser = user;
            Recipe = recipeToDelete;
            RecipeController = rc;

            Confirm = ReactiveCommand.Create(() => { });
            Cancel = ReactiveCommand.Create(() => { });
        }

        /// <summary>
        /// Deletes a recipe.
        /// </summary>
        /// <param name="recipe">The recipe to delete.</param>
        public void DeleteRecipe(Recipe recipe)
        {
            if (LoggedUser is Member)
            {
                Member member = (Member)LoggedUser;
                MemberController.DeleteOwnedRecipe(recipe, member);
                member.OwnedRecipes.Remove(recipe);
            }
            else
            {
                RecipeController.DeleteRecipeDB(Recipe);
            }
        }
    }
}