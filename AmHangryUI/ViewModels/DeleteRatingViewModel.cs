using ReactiveUI;

namespace AmHangryUI.ViewModels
{
    public class DeleteRatingViewModel : ViewModelBase
    {
        public Recipe Recipe { get; set; }

        /// <summary>
        /// Property to get or set the message to be displayed.
        /// </summary>
        public string _message = string.Empty;
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }

        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Confirm { get; }
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Cancel { get; }
        
        public DeleteRatingViewModel(Recipe recipe)
        {
            Recipe = recipe;

            Confirm = ReactiveCommand.Create(() => { });
            Cancel = ReactiveCommand.Create(() => { });
        }

        /// <summary>
        /// Deletes all ratings for the recipe.
        /// </summary>        
        public bool DeleteRating()
        {
            try
            {
                Recipe.ClearRatingList();
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