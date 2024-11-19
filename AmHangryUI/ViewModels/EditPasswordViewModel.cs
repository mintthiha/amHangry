using ReactiveUI;
using UserNameSpace;

namespace AmHangryUI.ViewModels
{
    public class EditPasswordViewModel : ViewModelBase
    {
        private MemberController _memberController;
        private User _loggedUser;

        private string _message = string.Empty;
        /// <summary>
        /// Property to get or set the message to be displayed.
        /// </summary>
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }
        /// <summary>
        /// Property to get or set the password to be displayed.
        /// </summary>
        private string _password = string.Empty;

        /// <summary>
        /// A  reactive command to update the password of the logged user.
        /// </summary>
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        /// <summary>
        /// A  reactive command to update the password of the logged user.
        /// </summary>
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> ExcecuteUpdatePassword { get; }
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
        /// Constructor for EditPasswordViewModel
        /// </summary>
        public EditPasswordViewModel(MemberController memberController, User loggedUser)
        {
            _memberController = memberController;
            _loggedUser = loggedUser;

            var isValidObservable = this.WhenAnyValue(x => x.Password, x => !string.IsNullOrEmpty(x));
            ExcecuteUpdatePassword = ReactiveCommand.Create(() => { UpdatePassword(); }, isValidObservable);
        }
        /// <summary>
        /// Updates the password of the logged user
        /// </summary>
        public void UpdatePassword()
        {
            Message = "";
            try
            {
                MemberController.UpdatePassword(Password, (Member)LoggedUser);
                Message = "Password updated";
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


    }
}


