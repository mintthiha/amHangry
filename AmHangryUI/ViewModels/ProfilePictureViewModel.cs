using ReactiveUI;
using UserNameSpace;

namespace AmHangryUI.ViewModels
{
    public class ProfilePictureViewModel : ViewModelBase
    {
        private MemberController _memberController;
        private User _loggedUser;

        private string _message = null!;

        /// <summary>
        /// Property to get or set the message to be displayed.
        /// </summary>
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }

 

        /// <summary>
        /// A  reactive command to update the profile picture of the logged user.
        /// </summary>
        public ReactiveCommand<string, System.Reactive.Unit> UpdateProfilePictureCommand { get; }

        /// <summary>
        /// Constructor for ProfilePictureViewModel
        /// </summary>
        public ProfilePictureViewModel(MemberController memberController, User loggedUser)
        {
            _memberController = memberController;
            _loggedUser = loggedUser;
            var profilePicturePath = ((Member)loggedUser).ProfilePicture;
            UpdateProfilePictureCommand = ReactiveCommand.Create<string>(UpdateProfilePicture);
        }

        /// <summary>
        /// get and set the logged user
        /// </summary>0
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
        /// Updates the profile picture of the logged in user
        /// </summary>
        public void UpdateProfilePicture(string picture)
        {
            try
            {
                MemberController.UpdateProfilePicture(picture, (Member)LoggedUser);
                Message = "Profile Picture Updated";
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }
    }
}