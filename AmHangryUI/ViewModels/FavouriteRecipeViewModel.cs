using ReactiveUI;
using UserNameSpace;
using System.Collections.ObjectModel;

namespace AmHangryUI.ViewModels
{
    public class FavouriteRecipeViewModel : ViewModelBase
    {
        private MemberController _memberController = null!;
        private Member _loggedUser = null!;
        private RecipeController _recipeController = null!;
        /// <summary>
        /// Property to get or set the recipe controller.
        /// </summary>
        public RecipeController RecipeController
        {
            get
            {
                return _recipeController;
            }
            set
            {
                _recipeController = value;
            }
        }
        private ObservableCollection<Recipe> _favoriteRecipe = new ObservableCollection<Recipe>();

        /// <summary>
        /// Property to get or set the favorite recipe to be displayed.
        /// </summary>
        public ObservableCollection<Recipe> FavoriteRecipe
        {
            get => _favoriteRecipe;
            set => this.RaiseAndSetIfChanged(ref _favoriteRecipe, value);
        }
        private ViewModelBase _contentViewModel = null!;
        public ReactiveCommand<Recipe,System.Reactive.Unit> RemoveFavoriteRecipeCommand { get; }

        /// <summary>
        /// Removes a recipe from the user's favorite list
        /// </summary>
          public void RemoveFavoriteRecipe(Recipe recipe)
        {

            MemberController.DeleteAFavRecipe(recipe, _loggedUser);
            FavoriteRecipe.Remove(recipe);

        }   
        /// <summary>
        /// Constructor for FavouriteRecipeViewModel
        /// </summary>

        public FavouriteRecipeViewModel(MemberController memberControler, Member currentUser, RecipeController recipeController)
        {
            LoggedUser = currentUser;
            MemberController = memberControler;
            RecipeController = recipeController;
            RemoveFavoriteRecipeCommand = ReactiveCommand.Create<Recipe>(RemoveFavoriteRecipe);  
              
                foreach (FavoriteRecipe item in currentUser.FavoriteRecipes)
                {
                    FavoriteRecipe.Add(item.Recipe);
                }
            

        }
        /// <summary>
        /// Property to get or set the content view model.
        /// </summary>
        public ViewModelBase ContentViewModel
        {
            get => _contentViewModel;
            set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
        }


        /// <summary>
        /// get and set the logged user
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
        /// get and set the member controller
        /// </summary>
        public MemberController MemberController
        {
            get
            {
                return _memberController;
            }
            set
            {
                _memberController = value;
            }
        }
      

    }
}
