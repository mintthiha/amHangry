using ReactiveUI;
using UserNameSpace;
using System.Collections.ObjectModel;

namespace AmHangryUI.ViewModels
{
    /// <summary>
    /// ViewModel for the AccountModification view.
    /// </summary>
    public class AccountModificationViewModel : ViewModelBase
    {
        private MemberController _memberController;

        /// <summary>
        /// The MemberController used to interact with the member database.
        /// </summary>
        public MemberController MemberController
        {
            get => _memberController;
            set => this.RaiseAndSetIfChanged(ref _memberController, value);
        }

        private ObservableCollection<Member> _members = null!;

        /// <summary>
        /// The collection of all members in the database.
        /// </summary>
        public ObservableCollection<Member> Members
        {
            get => _members;
            set => this.RaiseAndSetIfChanged(ref _members, value);
        }

        private string _userName = null!;

        /// <summary>
        /// The username of the currently selected member.
        /// </summary>
        public string UserName
        {
            get => _userName;
            set => this.RaiseAndSetIfChanged(ref _userName, value);
        }

        private string _password = null!;

        /// <summary>
        /// The password of the currently selected member.
        /// </summary>
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private string _userDescription = null!;

        /// <summary>
        /// The user description of the currently selected member.
        /// </summary>
        public string UserDescription
        {
            get => _userDescription;
            set => this.RaiseAndSetIfChanged(ref _userDescription, value);
        }

        /// <summary>
        /// Initializes a new instance of the AccountModificationViewModel class.
        /// </summary>
        /// <param name="memberController">The MemberController object used to interact with the member database.</param>
        public AccountModificationViewModel(MemberController memberController)
        {
            _memberController = memberController;
            Members = new ObservableCollection<Member>(MemberController.GetAllMembersDB());
        }

    }
}