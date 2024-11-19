using ReactiveUI;
using UserNameSpace;
using Avalonia.Media.Imaging;
using AvaloniaMiaDev.Helpers;

namespace AmHangryUI.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private MemberController _memberController = null!;
        private User _selectedUser = null!;
        private string _memberData = string.Empty;
        private string _userName = string.Empty;
        private string _profilePicture = string.Empty;
        private string _userDescription = string.Empty;

        public string NewValue = string.Empty;

        private string _myRecipes = string.Empty;
        private ViewModelBase _contentViewModel = null!;

        private Bitmap _bitmapProfilePic = null!;
        /// <summary>
        /// Property to get or set the bitmap profile picture.
        /// </summary>
        public Bitmap BitmapProfilePic
        {
            get => _bitmapProfilePic;
            set => this.RaiseAndSetIfChanged(ref _bitmapProfilePic, value);
        }
        /// <summary>
        /// Constructor for ProfileViewModel
        /// </summary>
        public ProfileViewModel(MemberController memberController, User currentUser)
        {
            SelectedUser = currentUser;
            MemberController = memberController;
            UserName = SelectedUser.Username;
            ProfilePicture = ((Member)SelectedUser).ProfilePicture;
            UserDescription = ((Member)SelectedUser).UserDescription;
            MemberData = ((Member)SelectedUser).ToString();
            
            
            BitmapProfilePic = ImageHelper.LoadFromResource(ProfilePicture);
        }
        /// <summary>
        /// get and set the member controller
        /// </summary>
        public string MyRecipes
        {
            get => _myRecipes;
            set => this.RaiseAndSetIfChanged(ref _myRecipes, value);
        }
        /// <summary>
        /// get and set the content view model
        /// </summary>      
        public ViewModelBase ContentViewModel
        {
            get => _contentViewModel;
            set => this.RaiseAndSetIfChanged(ref _contentViewModel, value);
        }
        /// <summary>
        /// get and set the user description
        /// </summary>
        public string UserDescription
        {
            get => _userDescription;
            set => this.RaiseAndSetIfChanged(ref _userDescription, value);
        }
        /// <summary>
        /// get and set the profile picture
        /// </summary>
        public string ProfilePicture
        {
            get => _profilePicture;
            set => this.RaiseAndSetIfChanged(ref _profilePicture, value);
        }
        /// <summary>
        /// get and set the user name
        /// </summary>
        public string UserName
        {
            get => _userName;
            set => this.RaiseAndSetIfChanged(ref _userName, value);
        }
        /// <summary>
        /// get and set the member data
        /// </summary>
        public string MemberData
        {
            get => _memberData;
            set => this.RaiseAndSetIfChanged(ref _memberData, value);
        }


        /// <summary>
        /// get and set the selected user
        /// </summary>
        public User SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
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
        /// delete a user with db method 
        /// </summary>
        public void DeleteMyAccount()
        {
            MemberController.DeleteMyAccount((Member)_selectedUser);

        }





    }
}
