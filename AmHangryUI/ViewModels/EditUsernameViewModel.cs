using ReactiveUI;
using UserNameSpace;

namespace AmHangryUI.ViewModels
{
    public class EditUsernameViewModel : ViewModelBase
    {
        private MemberController _memberController;
        private User _loggedUser ;

        private string _message = string.Empty;

        /// <summary>
        /// Property to get or set the message to be displayed.
        /// </summary>
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }

        private string _username = string.Empty;
        /// <summary>
        /// Property to get or set the username to be displayed.
        /// </summary>
        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }
        /// <summary>
        /// A  reactive command to update the username of the logged user.
        /// </summary>
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> ExcecuteUpdateUsername { get; }
        private string description = string.Empty;

        /// <summary>
        /// get and set the description
        /// </summary>
        public string Description
        {
            get => description;
            set => this.RaiseAndSetIfChanged(ref description, value);
        }

        /// <summary>
        /// Constructor for EditUsernameViewModel
        /// </summary>
        public EditUsernameViewModel(MemberController memberController, User loggedUser)
        {
            _memberController = memberController;
            _loggedUser = loggedUser;

            var isValidObservable = this.WhenAnyValue(x => x.Username, x => !string.IsNullOrEmpty(x));
            ExcecuteUpdateUsername = ReactiveCommand.Create(() => { UpdateUsername(); }, isValidObservable);
        }

        /// <summary>
        /// Updates the username of the logged user
        /// </summary>
        public void UpdateUsername()
        {
            Message = "";
            try
            {
                MemberController.UpdateUserName(Username, (Member)LoggedUser);
                Message = "Username updated";
            }
            catch (ArgumentException e)
            {
                Message = e.Message;
            }
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
        /// Updates the username with a parameter. This is for admin use
        /// </summary>
        public void UpdateUsername(string newUsername)
        {
            MemberController.UpdateUserName(newUsername, (Member)LoggedUser);


        }
    }
}


