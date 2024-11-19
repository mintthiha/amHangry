using System.Reactive.Linq;
using ReactiveUI;
using UserNameSpace;
namespace AmHangryUI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _contentViewModel = null!;
    public RecipeContext RecipeContext { get; set; }
    public MemberController MemberController { get; set; }
    public RecipeController RecipeController { get; set; }
    public IngredientController IngredientController { get; set; }
    public AdminController AdminController { get; set; }
    public User loggedUser = null!;
    public bool _visibleNavigation;
    public bool _visibleAdmin;
    public bool _visibleMember;

    public string _title = null!;

    private string _username = null!;

    public string Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public ViewModelBase ContentViewModel
    {
        get => _contentViewModel;
        set
        {
            if (_contentViewModel != value)
            {
                _contentViewModel = value;
                this.RaisePropertyChanged(nameof(ContentViewModel));
            }
        }
    }

    public bool VisibleNavigation
    {
        get => _visibleNavigation;
        private set => this.RaiseAndSetIfChanged(ref _visibleNavigation, value);
    }

    public bool VisibleAdmin
    {
        get => _visibleAdmin;
        private set => this.RaiseAndSetIfChanged(ref _visibleAdmin, value);
    }

    public bool VisibleMember
    {
        get => _visibleMember;
        private set => this.RaiseAndSetIfChanged(ref _visibleMember, value);
    }

    public ReactiveCommand<Ingredient, System.Reactive.Unit> IngDetails { get; }
    public ReactiveCommand<Ingredient, System.Reactive.Unit> DeleteIng { get; }
    public ReactiveCommand<Ingredient, System.Reactive.Unit> UpdateIngredient { get; }
    public ReactiveCommand<Recipe, System.Reactive.Unit> ShowRecipe { get; }
    public ReactiveCommand<Recipe, System.Reactive.Unit> ShowRecipeFromRecent { get; }
    public ReactiveCommand<Recipe, System.Reactive.Unit> RateRecipe { get; }
    public ReactiveCommand<Recipe, System.Reactive.Unit> DeleteRating { get; }
    public ReactiveCommand<Recipe, System.Reactive.Unit> DeleteRecipe { get; }
    public ReactiveCommand<Recipe, System.Reactive.Unit> EditRecipe { get; }
    public ReactiveCommand<Member, System.Reactive.Unit> ProfileDetails { get; }
    public ReactiveCommand<Recipe, System.Reactive.Unit> ShowNutrients { get; }
    public ReactiveCommand<User, System.Reactive.Unit> EditProfilePictureAdmin { get; }
    public ReactiveCommand<User, System.Reactive.Unit> EditUsernameAdmin { get; }
    public ReactiveCommand<User, System.Reactive.Unit> EditDescriptionAdmin { get; }
    public ReactiveCommand<User, System.Reactive.Unit> EditPasswordAdmin { get; }
    public ReactiveCommand<User, System.Reactive.Unit> DeleteThisAccount { get; }
    public MainWindowViewModel()
    {
        RecipeContext = RecipeContext.GetInstance();
        MemberController = new MemberController(RecipeContext);
        RecipeController = new RecipeController(RecipeContext);
        IngredientController = new IngredientController(RecipeContext);
        AdminController = new AdminController(RecipeContext);

        ShowRecipe = ReactiveCommand.Create<Recipe>(RecipeDetails);
        ShowRecipeFromRecent = ReactiveCommand.Create<Recipe>(RecipeDetailsFromRecent);
        IngDetails = ReactiveCommand.Create<Ingredient>(IngredientDetails);
        DeleteIng = ReactiveCommand.Create<Ingredient>(DeleteIngredientView);
        UpdateIngredient = ReactiveCommand.Create<Ingredient>(UpdateIngredientView);
        RateRecipe = ReactiveCommand.Create<Recipe>(UpdateRatingView);
        DeleteRating = ReactiveCommand.Create<Recipe>(DeleteRatingView);
        DeleteRecipe = ReactiveCommand.Create<Recipe>(DeleteRecipeView);
        EditRecipe = ReactiveCommand.Create<Recipe>(EditRecipeView);
        ProfileDetails = ReactiveCommand.Create<Member>(ProfileViewAdmin);
        ShowNutrients = ReactiveCommand.Create<Recipe>(NutrientsDetails);
        EditProfilePictureAdmin = ReactiveCommand.Create<User>(EditProfilePictureViewAdmin);
        EditDescriptionAdmin = ReactiveCommand.Create<User>(EditDescriptionViewAdmin);
        EditUsernameAdmin = ReactiveCommand.Create<User>(EditUsernameViewAdmin);
        EditPasswordAdmin = ReactiveCommand.Create<User>(EditPasswordViewAdmin);
        DeleteThisAccount = ReactiveCommand.Create<User>(DeleteUserAccount);

        ShowLogin();
    }

    /// <summary>
    /// Displays the login view.
    /// </summary>
    private void ShowLogin()
    {
        VisibleNavigation = false;
        LoginViewModel vm = new(MemberController, AdminController);
        ContentViewModel = vm;
        vm.Register.Subscribe(_ =>
        {
            try
            {
                HomePage(vm.RegisterUser());
            }
            catch (ArgumentException e)
            {
                vm.ShowError(e.Message);
            }
        });
        vm.Login.Subscribe(_ =>
        {
            try
            {
                HomePage(vm.LoginUser());
            }
            catch (ArgumentException e)
            {
                vm.ShowError(e.Message);
                ContentViewModel = vm;
            }
        });
        vm.AdminLogin.Subscribe(_ =>
        {
            try
            {
                HomePage(vm.LoginAdmin());
            }
            catch (ArgumentException e)
            {
                vm.ShowError(e.Message);
                ContentViewModel = vm;
            }

        });
    }

    /// <summary>
    /// Displays the home page view
    /// </summary>
    /// <param name="currentUser">The current logged-in user.</param>
    public void HomePage(User currentUser)
    {
        VisibleNavigation = true;
        if (currentUser is Admin)
        {
            VisibleAdmin = true;
            VisibleMember = false;
        }
        else
        {
            VisibleAdmin = false;
            VisibleMember = true;
        }
        loggedUser = currentUser;
        UserHomeView();
    }

    /// <summary>
    /// Navigates to the user home view.
    /// </summary>
    public void UserHomeView()
    {
        Title = "🔥🔥Home Page🔥🔥";
        ContentViewModel = new UserHomeViewModel(loggedUser);
    }


    /// <summary>
    /// Display the create recipe view.
    /// </summary>
    public void CreateRecipe()
    {
        Title = "🔥🔥Create Recipe🔥🔥";
        var vm = new CreateRecipeViewModel(RecipeController, IngredientController, loggedUser);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the recipe details view.
    /// </summary>
    /// <param name="recipe">The recipe to display.</param>
    public void RecipeDetails(Recipe recipe)
    {
        Title = "🔥🔥Recipe Details🔥🔥";
        if (loggedUser is not Admin)
        {
            MemberController.AddSearchRecipe(recipe, (Member)loggedUser);
            var vm = new RecipeDisplayViewModel(recipe, MemberController, loggedUser);
            ContentViewModel = vm;
        }
        else
        {
            var vm = new RecipeDisplayViewModel(recipe, AdminController, loggedUser);
            ContentViewModel = vm;
        }
    }
    /// <summary>
    /// Displays the recipe details view when clicked from the Recent Tab.
    /// </summary>
    /// <param name="recipe">The recipe to display.</param>
    public void RecipeDetailsFromRecent(Recipe recipe)
    {
        Title = "🔥🔥Recipe Details🔥🔥";
        if (loggedUser is not Admin)
        {
            var vm = new RecipeDisplayViewModel(recipe, MemberController, loggedUser);
            ContentViewModel = vm;
        }
        else
        {
            var vm = new RecipeDisplayViewModel(recipe, AdminController, loggedUser);
            ContentViewModel = vm;
        }
    }

    /// <summary>
    /// Display the current user's owned recipes view.
    /// </summary>
    public void OwnedRecipesView()
    {
        Title = "🔥🔥 My Recipes 🔥🔥";
        if (loggedUser is Member member)
        {
            var vm = new OwnedRecipesViewModel(MemberController, member, RecipeController);
            ContentViewModel = vm;
        }
        else
        {
            Title = "🔥🔥 Recipe Modification 🔥🔥";
            var vm = new OwnedRecipesViewModel(AdminController, loggedUser, RecipeController);
            ContentViewModel = vm;
        }
    }

    /// <summary>
    /// Displays the edit recipe view.
    /// </summary>
    /// <param name="recipe">The recipe to edit.</param>
    public void EditRecipeView(Recipe recipe)
    {
        Title = "🔥🔥 Edit Recipe 🔥🔥";
        var vm = new EditOwnedRecipeViewModel(recipe, RecipeController, IngredientController);
        ContentViewModel = vm;
        vm.ConfirmEditRecipe.Subscribe(_ =>
        {
            if (vm.UpdateRecipe())
            {
                var RecVm = new OwnedRecipesViewModel(MemberController, loggedUser, RecipeController);
                RecVm.SuccessUpdateMessage();
                ContentViewModel = RecVm;
            }
        });
        vm.CancelEditRecipe.Subscribe(_ =>
        {
            var RecVm = new OwnedRecipesViewModel(MemberController, loggedUser, RecipeController);
            ContentViewModel = RecVm;
        });
    }

    /// <summary>
    /// Displays the delete recipe view.
    /// </summary>
    /// <param name="recipe">The recipe to delete.</param>
    public void DeleteRecipeView(Recipe recipe)
    {
        Title = "🔥🔥 Delete Recipe 🔥🔥";
        var vm = new DeleteRecipeViewModel(recipe, MemberController, loggedUser, RecipeController);
        ContentViewModel = vm;
        vm.Confirm.Subscribe(_ =>
        {
            vm.DeleteRecipe(recipe);
            var RecVm = new OwnedRecipesViewModel(MemberController, loggedUser, RecipeController);
            RecVm.SuccessDeleteMessage();
            ContentViewModel = RecVm;
        });
        vm.Cancel.Subscribe(_ =>
        {
            var RecVm = new OwnedRecipesViewModel(MemberController, loggedUser, RecipeController);
            ContentViewModel = RecVm;
        });
    }

    /// <summary>
    /// Displays the ingredient details view.
    /// </summary>
    /// <param name="ingredient">The ingredient to display.</param>
    public void IngredientDetails(Ingredient ingredient)
    {
        Title = "🔥🔥Ingredient Details🔥🔥";
        var vm = new IngredientDisplayViewModel(ingredient);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the search view.
    /// </summary>
    public void SearchView()
    {
        if (VisibleMember)
            Title = "🔥🔥Search View🔥🔥";
        else
        {
            Title = "🔥🔥Search View (Admin)🔥🔥";
        }
        var vm = new SearchViewModel(RecipeController, IngredientController);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the user's profile view.
    /// </summary>
    public void ProfileView()
    {
        Title = "🔥🔥My Profile🔥🔥";
        var vm = new ProfileViewModel(MemberController, loggedUser);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the account modification view for admins.
    /// </summary>
    public void AccountModificationView()
    {
        Title = "🔥🔥Members Modification🔥🔥";
        var vm = new AccountModificationViewModel(MemberController);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the profile view for a specific member (admin functionality).
    /// </summary>
    /// <param name="member">The member whose profile to display.</param>
    public void ProfileViewAdmin(Member member)
    {
        Title = "🔥🔥Modding " + member.Username + " Profile🔥🔥";
        var vm = new ProfileViewModel(MemberController, member);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the edit username view.
    /// </summary>
    public void EditUsernameView()
    {
        Title = "🔥🔥Edit Username🔥🔥";
        var vm = new EditUsernameViewModel(MemberController, loggedUser);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the edit username view for the admin side.
    /// </summary>
    public void EditUsernameViewAdmin(User member)
    {
        Title = "🔥🔥Edit " + member.Username + "'s Username🔥🔥";
        var vm = new EditUsernameViewModel(MemberController, member);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the edit description view.
    /// </summary>
    public void EditDescriptionView()
    {
        Title = "🔥🔥Edit Description🔥🔥";
        var vm = new EditDescriptionViewModel(MemberController, loggedUser);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the edit description view for the admin side.
    /// </summary>
    public void EditDescriptionViewAdmin(User member)
    {
        Title = "🔥🔥Edit " + member.Username + "'s Description🔥🔥";
        var vm = new EditDescriptionViewModel(MemberController, member);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Clears the loggedUser field and goes to Log In View.
    /// </summary>
    public void ExecuteLogOut()
    {
        loggedUser = new Member();
        ShowLogin();
    }

    /// <summary>
    /// Displays the recent recipes view.
    /// </summary>
    public void RecentRecipeView()
    {
        Title = "🔥🔥Recent Recipe View🔥🔥";
        var vm = new RecentRecipeViewModel((Member)loggedUser);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the ingredients view.
    /// </summary>
    public void IngredientsView()
    {
        Title = "🔥🔥Ingredients View🔥🔥";
        var vm = new IngredientsViewModel(IngredientController);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the add ingredient view.
    /// </summary>
    public void AddIngredientView()
    {
        Title = "🔥🔥Add Ingredient View🔥🔥";
        var vm = new AddIngredientViewModel(IngredientController);
        ContentViewModel = vm;
        var ingVm = new IngredientsViewModel(IngredientController);
        vm.Confirm.Subscribe(_ =>
        {
            if (vm.AddIngredient())
            {
                ingVm.SuccessMessage("You have Added this ingredient successfully!");
                ContentViewModel = ingVm;
            }
            else
            {
                ContentViewModel = vm;
            }
        });
        vm.Cancel.Subscribe(_ =>
        {
            ingVm.CancelMessage("You cancelled adding this ingredient!");
            ContentViewModel = ingVm;
        });
    }

    /// <summary>
    /// Displays the delete ingredient view.
    /// </summary>
    /// <param name="ingredient">The ingredient to delete.</param>
    public void DeleteIngredientView(Ingredient ingredient)
    {
        Title = "🔥🔥Delete Ingredient View🔥🔥";
        var vm = new DeleteIngredientViewModel(ingredient, IngredientController);
        ContentViewModel = vm;
        vm.Confirm.Subscribe(_ =>
        {
            vm.DeleteIngredient(ingredient);
            var ingVm = new IngredientsViewModel(IngredientController);
            ingVm.SuccessMessage("You have deleted this ingredient successfully!");
            ContentViewModel = ingVm;
        });
        vm.Cancel.Subscribe(_ =>
        {
            var ingVm = new IngredientsViewModel(IngredientController);
            ingVm.CancelMessage("You have cancelled deleting this ingredient!");
            ContentViewModel = ingVm;
        });
    }

    /// <summary>
    /// Displays the update ingredient view.
    /// </summary>
    /// <param name="ingredient">The ingredient to update.</param>
    public void UpdateIngredientView(Ingredient ingredient)
    {
        Title = "🔥🔥Update Ingredient View🔥🔥";
        var vm = new UpdateIngredientViewModel(ingredient, IngredientController);
        ContentViewModel = vm;
        var ingVm = new IngredientsViewModel(IngredientController);
        vm.Confirm.Subscribe(_ =>
        {
            if (vm.UpdateIngredient())
            {
                ingVm.SuccessMessage("You have updated this ingredient successfully!");
                ContentViewModel = ingVm;
            }
        });
        vm.Cancel.Subscribe(_ =>
        {
            ingVm.CancelMessage("You have cancelled updating this ingredient!");
            ContentViewModel = ingVm;
        });
    }

    /// <summary>
    /// Displays the user's favourite recipes view.
    /// </summary>
    public void FavouriteRecipeView()
    {
        Title = "🔥🔥Favourite Recipe View🔥🔥";
        var vm = new FavouriteRecipeViewModel(MemberController, (Member)loggedUser, RecipeController);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the recipe rating view.
    /// </summary>
    /// <param name="recipe">The recipe to rate.</param>
    public void UpdateRatingView(Recipe recipe)
    {
        Title = "🔥🔥Recipe rating🔥🔥";
        var vm = new RecipeRatingViewModel(RecipeController, (Member)loggedUser, recipe);
        ContentViewModel = vm;

        var vm2 = new RecipeDisplayViewModel(recipe, MemberController, (Member)loggedUser);
        vm.Confirm.Subscribe(_ =>
       {
           if (vm.AddRating())
           {
               vm2.ShowMessage("Your rating has been added and updated successfully!");
               Title = "🔥🔥Recipe Details🔥🔥";
               ContentViewModel = vm2;
           }
       });
        vm.Delete.Subscribe(_ =>
        {
            if (vm.DeleteRating())
            {
                vm2.ShowMessage("Your rating has been deleted!");
                Title = "🔥🔥Recipe Details🔥🔥";
                ContentViewModel = vm2;
            }
        });

        vm.Cancel.Subscribe(_ =>
        {
            vm2.ShowMessage("You have cancelled updating your rating!");
            Title = "🔥🔥Recipe Details🔥🔥";
            ContentViewModel = vm2;
        });
    }


    /// <summary>
    /// Displays the recipe rating reset view for admins.
    /// </summary>
    /// <param name="recipe">The recipe to reset the rating for.</param>
    public void DeleteRatingView(Recipe recipe)
    {
        Title = "🔥🔥Recipe rating reset (Admin)🔥🔥";
        var vm = new DeleteRatingViewModel(recipe);
        ContentViewModel = vm;

        var vm2 = new SearchViewModel(RecipeController, IngredientController);
        vm.Confirm.Subscribe(_ =>
       {
           if (vm.DeleteRating())
           {
               vm2.ShowMessage("The rating has successfully been reset!");
               Title = "🔥🔥Search View (Admin)🔥🔥";
               ContentViewModel = vm2;
           }
       });

        vm.Cancel.Subscribe(_ =>
        {
            vm2.ShowMessage("You have cancelled deleting the ratings");
            Title = "🔥🔥Search View (Admin)🔥🔥";
            ContentViewModel = vm2;
        });
    }

    /// <summary>
    /// Deletes the current logged-in user's account and logs them out.
    /// </summary>
    public void DeleteMyAccount()
    {
        MemberController.DeleteMyAccount((Member)loggedUser);
        ExecuteLogOut();
    }

    /// <summary>
    /// Deletes a specified user's account (admin functionality).
    /// </summary>
    /// <param name="member">The user whose account to delete.</param>
    public void DeleteUserAccount(User member)
    {
        MemberController.DeleteMyAccount((Member)member);
        ContentViewModel = new AccountModificationViewModel(MemberController);
    }

    /// <summary>
    /// Displays the edit password view.
    /// </summary>
    public void EditPasswordView()
    {
        Title = "🔥🔥Edit Password🔥🔥";
        var vm = new EditPasswordViewModel(MemberController, loggedUser);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the edit password view for a specified user (admin functionality).
    /// </summary>
    /// <param name="member">The user whose password to edit.</param>
    public void EditPasswordViewAdmin(User member)
    {
        Title = "🔥🔥Edit " + member.Username + "'s Password🔥🔥";
        var vm = new EditPasswordViewModel(MemberController, member);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the edit profile picture view.
    /// </summary>
    public void EditProfilePictureView()
    {
        Title = "🔥🔥Edit Profile Picture🔥🔥";
        var vm = new ProfilePictureViewModel(MemberController, loggedUser);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the edit profile picture view for a specified user (admin functionality).
    /// </summary>
    /// <param name="member">The user whose profile picture to edit.</param>
    public void EditProfilePictureViewAdmin(User member)
    {
        Title = "🔥🔥Edit " + member.Username + "'s Profile Picture🔥🔥";
        var vm = new ProfilePictureViewModel(MemberController, member);
        ContentViewModel = vm;
    }

    /// <summary>
    /// Displays the nutrients details view for a specified recipe.
    /// </summary>
    /// <param name="recipe">The recipe whose nutrient details to display.</param>
    public void NutrientsDetails(Recipe recipe)
    {
        Title = "🔥🔥Nutrients Details🔥🔥";
        var vm = new NutrientsDisplayViewModel(recipe);
        ContentViewModel = vm;
        var vm2 = new ViewModelBase();
        if (loggedUser is not Admin)
        {
            vm2 = new RecipeDisplayViewModel(recipe, MemberController, (Member)loggedUser);
        }
        else
        {
            vm2 = new RecipeDisplayViewModel(recipe, MemberController, loggedUser);
        }
        vm.Back.Subscribe(_ =>
       {
           Title = "🔥🔥Recipe Details🔥🔥";
           ContentViewModel = vm2;
       });
    }
}
