using ReactiveUI;

namespace AmHangryUI.ViewModels
{
    public class NutrientsDisplayViewModel : ViewModelBase
    {
        private Recipe _recipe = null!;
        public Recipe Recipe
        {
            get => _recipe;
            set => this.RaiseAndSetIfChanged(ref _recipe, value);
        }

        /// <summary>
        /// Command to navigate back.
        /// </summary>
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Back { get; }

        public NutrientsDisplayViewModel(Recipe recipe)
        {
            Recipe = recipe;
            Back = ReactiveCommand.Create(() => { });
        }
    }
}