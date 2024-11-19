using ReactiveUI;
using UserNameSpace;


namespace AmHangryUI.ViewModels
{
    public class RecipeRatingViewModel : ViewModelBase
    {
        public RecipeController RecipeController { get; set; }
        public Recipe Recipe { get; set; }
        public Member UserLogged { get; set; }
        private double _userRating;

        /// <summary>
        /// Property to get and set User's rating for the recipe.
        /// </summary>
        public double UserRating
        {
            get => _userRating;
            set => this.RaiseAndSetIfChanged(ref _userRating, value);
        }
        public string _message = string.Empty;

        /// <summary>
        /// Property to get or set the message to be displayed.
        /// </summary>
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }

        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Confirm { get; }
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Delete { get; }
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Cancel { get; }

        public RecipeRatingViewModel(RecipeController rc, Member userLogged, Recipe recipe)
        {
            RecipeController = rc;
            UserLogged = userLogged;
            Recipe = recipe;

            Confirm = ReactiveCommand.Create(() => { });
            Delete = ReactiveCommand.Create(() => { });
            Cancel = ReactiveCommand.Create(() => { });

            List<Rate> ratings = Recipe.Rates;
            foreach (Rate rate in ratings)
            {
                if (rate.RatedBy.Equals(userLogged.Username))
                {
                    UserRating = rate.Value;
                    break;
                }
            }
        }

        /// <summary>
        /// Adds the user's rating for the recipe.
        /// </summary>
        public bool AddRating()
        {
            try
            {
                RecipeController.RateRecipeDB(UserLogged.Username, UserRating, Recipe);
                return true;
            }
            catch (Exception e)
            {
                ShowMessage(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Deletes the user's rating for the recipe.
        /// </summary>
        /// <returns>True if the rating was deleted successfully, otherwise false.</returns>
        public bool DeleteRating()
        {
            try
            {
                RecipeController.DeleteRatingRecipeDB(UserLogged.Username, Recipe);
                return true;
            }
            catch (Exception e)
            {
                ShowMessage(e.Message);
                return false;
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