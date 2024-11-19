using ReactiveUI;
using UserNameSpace;

namespace AmHangryUI.ViewModels
{
    /// <summary>
    /// ViewModel for the Login view.
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        /// <summary>
        /// The MemberController used to interact with the Member data.
        /// </summary>
        public MemberController memberController;

        /// <summary>
        /// The AdminController used to interact with the Admin data.
        /// </summary>
        public AdminController adminController;

        private string _username = string.Empty;

        /// <summary>
        /// The username of the user.
        /// </summary>
        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        private string _password = string.Empty;

        /// <summary>
        /// The password of the user.
        /// </summary>
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private string _errorMessage = string.Empty;

        /// <summary>
        /// The error message to display to the user.
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
        }

        /// <summary>
        /// The User object.
        /// </summary>
        public User? User { get; private set; }

        /// <summary>
        /// The command to log in the user.
        /// </summary>
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Login { get; }

        /// <summary>
        /// The command to log in the admin.
        /// </summary>
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> AdminLogin { get; }

        /// <summary>
        /// The command to register a new user.
        /// </summary>
        public ReactiveCommand<System.Reactive.Unit, System.Reactive.Unit> Register { get; }

        //Constructor
        public LoginViewModel(MemberController mc, AdminController ac)
        {
            memberController = mc;
            adminController = ac;

            //Return a boolean, True when the username and password isn't empty
            var loginEnabled = this.WhenAnyValue(
                x => x.Username,
                x => x.Password,
                (username, password) => !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password));

            Login = ReactiveCommand.Create(() => { }, loginEnabled);
            Register = ReactiveCommand.Create(() => { }, loginEnabled);
            AdminLogin = ReactiveCommand.Create(() => { }, loginEnabled);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <returns>The User object.</returns>
        public User RegisterUser()
        {
            memberController.CreateMember(Username, Password);
            User = memberController.GetMember(Username);
            return User;
        }

        /// <summary>
        /// Logs in the user.
        /// </summary>
        /// <returns>The User object.</returns>
        public User LoginUser()
        {
            if (memberController.VerifyLogin(Username, Password))
            {
                User = memberController.GetMember(Username);
                return User;
            }
            else
            {
                throw new ArgumentException("Incorrect Username or Password");
            }
        }

        /// <summary>
        /// Logs in the admin.
        /// </summary>
        /// <returns>The User object.</returns>
        public User LoginAdmin()
        {
            if (adminController.VerifyLogin(Username, Password))
            {
                User = adminController.GetAdmin(Username);
                return User;
            }
            else
            {
                throw new ArgumentException("Incorrect Username or Password!");
            }
        }

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="errorMsg">The error message to display.</param>
        public void ShowError(string errorMsg)
        {
            ErrorMessage = errorMsg;
        }
    }
}