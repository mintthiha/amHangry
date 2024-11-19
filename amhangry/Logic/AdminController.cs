using UserNameSpace;

public class AdminController : UserController
{
    private List<Admin> _admins = null!;
    private Admin _currentAdmin = null!;
    private RecipeContext _context = null!;
    public AdminController(List<Admin> admins)
    {
        _admins = admins;
    }

    public AdminController(RecipeContext recipeContext)
    {
        _context = recipeContext;
    }

    /// <summary>
    /// Property of the field _admins
    /// </summary>
    public List<Admin> Admins
    {
        get
        {
            if (_admins != null)
            {
                return _admins;
            }
            else
            {
                throw new NullReferenceException("The admins field is null!");
            }
        }
        set { _admins = value; }
    }

    /// <summary>
    /// Property of the field _currentAdmin
    /// </summary>
    public Admin CurrentAdmin
    {
        get
        {
            if (_currentAdmin != null)
            {
                return _currentAdmin;
            }
            else
            {
                throw new Exception("The current admin is null!");
            }
        }
        set { _currentAdmin = value; }
    }

    /// <summary>
    /// This method creates an Admin.
    /// </summary>
    /// <param name="username">The username of the admin in question</param>
    /// <param name="password">The password of the admin in question</param>
    public void CreateAdmin(string username, string password)
    {
        var admin = new Admin(username, password);
        if (_context != null)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }
        else
        {
            throw new ArgumentNullException("The recipe context is null!");
        }

    }

    /// <summary>
    /// This method deletes this user's account.
    /// </summary>
    public void DeleteMyAccountDB()
    {
        if (_context != null && _currentAdmin != null)
        {
            _context.Admins.Remove(_currentAdmin);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// This method updates this user password.
    /// </summary>
    /// <param name="newPassword">the new password to update</param>
    public void UpdatePassword(string newPassword)
    {
        try
        {
            if (_currentAdmin != null)
            {
                _currentAdmin.Password = newPassword;
                Console.WriteLine("Password changed successfully! Do NOT share with anyone.");
            }
            else
            {
                Console.WriteLine("The current admin is null!");
            }
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Unable to update password");
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// This method verifies the login of the admin.
    /// </summary>
    /// <param name="username">The username of an admin that wants to login</param>
    /// <param name="password">The password of an admin that wants to login</param>
    /// <returns></returns>
    public bool VerifyLogin(string username, string password)
    {
        if (_context != null)
        {
            var admin = _context.Admins
                .AsEnumerable()
                .FirstOrDefault(m => m.Username == username);
            if (admin == null)
            {
                return false;
            }
            else
            {
                if (admin.MatchPasswords(password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            throw new ArgumentNullException("The context field is null!");
        }
    }


    /// <summary>
    /// This method Bans a specified member.
    /// </summary>
    /// <param name="members">The list of members in the DB</param>
    /// <param name="member">the member to be banned</param>
    /// <returns></returns>
    public List<Member> BanMember(List<Member> members, Member member)
    {
        members.Remove(member);
        return members;
    }

    public Admin GetAdmin(string username)
    {
        var admin = _context.Admins
        .FirstOrDefault(m => m.Username == username);
        if (admin == null)
        {
            throw new ArgumentNullException("Admin should not be null");
        }
        else
        {
            return admin;
        }
    }
}