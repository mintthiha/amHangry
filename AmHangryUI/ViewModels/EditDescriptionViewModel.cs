using ReactiveUI;
using UserNameSpace;

namespace AmHangryUI.ViewModels
{
    public class EditDescriptionViewModel : ViewModelBase
    {
        private MemberController _memberController;
        private User _loggedUser;

        private string _description = string.Empty;

        /// <summary>
        /// A  reactive command to update the description of the logged user. 
        /// </summary>
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> ExcecuteUpdateUsername { get; }

        /// <summary>
        /// get and set the description
        ///  </summary>
        public string Description
        {
            get => _description;
            set => this.RaiseAndSetIfChanged(ref _description, value);
        }

        /// <summary>
        /// Constructor for EditDescriptionViewModel
        /// </summary>
        public EditDescriptionViewModel(MemberController memberController, User loggedUser)
        {
            _memberController = memberController;
            _loggedUser = loggedUser;

            var isValidObservable = this.WhenAnyValue(x => x.Description, x => !string.IsNullOrEmpty(x));
            ExcecuteUpdateUsername = ReactiveCommand.Create(() => { UpdateDescription(); }, isValidObservable);
        }

        /// <summary>
        /// Updates the description of the logged user
        /// </summary>
        public void UpdateDescription()
        {
            MemberController.UpdateUserDescription(Description, (Member)LoggedUser);
        }

        /// <summary>
        /// get and set the logged user
        /// </summary>
        public User LoggedUser
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
        
        /// <summary>
        /// Updates the description with a parameter for admins
        /// </summary>
        public void UpdateUserDescription(string newDescription)
        {
            _memberController.UpdateUserDescription(newDescription, (Member)LoggedUser);
        }
    }
}


