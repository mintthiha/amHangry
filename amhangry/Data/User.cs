using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace UserNameSpace
{


    /// <summary>
    /// Abstract class representing a user, with a username and a password
    /// </summary>
    public abstract class User
    {
        public User() { }

        /// <summary>
        /// The username of the user.
        /// </summary>
        private string _username = string.Empty;
        /// <summary>
        /// The hashed password of the user.
        /// </summary>
        private string _password = string.Empty;
        /// <summary>
        /// The salt used for password hashing.
        /// </summary>
        public byte[]? _salt;

        /// <summary>
        ///   Gets or sets the username of the user.
        /// </summary>
        /// <remarks>
        ///    The username must be more than 3 characters and less than 20 characters.
        /// </remarks>
        public string Username
        {
            get
            {
                if (_username != null)
                {
                    return _username;
                }
                else
                {
                    throw new NullReferenceException("The username field is null!");
                }
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Username must not be empty nor null");
                }
                if (value.Length <= 3)
                {
                    throw new ArgumentException("Username must be more than 3 character");
                }
                else if (value.Length >= 20)
                {
                    throw new ArgumentException("Username must be less than 20 characters");
                }
                //Need to add unique username validation somehow
                _username = value;
            }
        }
        /// <summary>
        /// Gets or sets the salt used for password hashing.
        /// </summary>
        public byte[] Salt
        {
            get
            {
                if (_salt != null)
                {
                    return _salt;
                }
                else
                {
                    throw new Exception("The _salt field is null!");
                }
            }
            set
            {
                _salt = value;
            }
        }
        /// <summary>
        /// Gets or sets the hashed password of the user.
        /// </summary>
        /// <remarks>
        /// The password must be more than 5 characters and less than 20 characters.
        /// </remarks>
        public string Password
        {
            get { return _password; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Invalid password must not be empty nor null");
                }
                if (value.Length <= 5)
                {
                    throw new ArgumentException("Password must be more than 5 characters");
                }
                else if (value.Length >= 20)
                {
                    throw new ArgumentException("Password must be less than 20 characters");
                }
                _password = GetPasswordHash(value);
            }
        }


        /// <summary>
        /// This method is to generate the password of the user instance from an hashing algorithm
        /// Additionally, fills the Salt proprety of the user instance for use by the MatchPasswords
        /// </summary>
        /// <param name="password"> The value to be transformed into an hash</param>
        /// <returns> Hashed password in string </returns>
        private string GetPasswordHash(string password)
        {
            byte[] salt = new byte[16];
            //I put pragma because the class is outdated
            #pragma warning disable SYSLIB0023
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
            
                rng.GetBytes(salt);
                Salt = salt;
            }
            int iterations = 100;
            #pragma warning disable SYSLIB0041
            //I put pragma because the class is outdated
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = key.GetBytes(32);
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// This method is to compare the password given to compared to the password field of this user instance
        /// </summary>
        /// <param name="password"> The value to be transformed into an hash and compared</param>
        /// <returns> true if it is equal to the field hashed, false if it is not the case or the parameter is null </returns>
        public bool MatchPasswords(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }
            byte[] salt = Salt;
            int iterations = 100;
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = key.GetBytes(32);
            return Convert.ToBase64String(hash).Equals(Password);
        }

        /// <summary>
        /// Initializes an instance of the user class with the username and password given
        /// </summary>
        /// <param name="username">Username of user</param>
        /// <param name="password">Password of user</param>
        public User(string username, string password)
        {
            if (username != null)
            {
                Username = username;
            }
            Password = password;
        }
    }

    /// <summary>
    /// Represents a member user with profile information and recipe lists.
    /// </summary>
    public class Member : User
    {
        public int MemberId { get; set; }

        public Member() { }
        /// <summary>
        /// The profile picture URL of the member.
        /// </summary>
        private string _profilePicture =null!;

        /// <summary>
        /// The description of the member.
        /// </summary>
        private string _userDescription =null!;

        /// <summary>
        /// The list of recipes owned by the member.
        /// </summary>
        private List<Recipe> _ownedRecipes =null!;

        /// <summary>
        /// The list of recipes favorited by the member.
        /// </summary>
        private List<FavoriteRecipe> _favoriteRecipes =null!;

        /// <summary>
        /// The list of recipes recently searched by the member.
        /// </summary>
        private List<RecentRecipeSearch> _recentRecipeSearches =null!;

        /// <summary>
        /// Initializes a new instance of the Member class with the username and password given.
        /// </summary>
        /// <param name="username">The username of the member.</param>
        /// <param name="password">The password of the member.</param>
        public Member(string username, string password) : base(username, password)
        {
            _ownedRecipes = new List<Recipe>();
            _favoriteRecipes = new List<FavoriteRecipe>();
            _recentRecipeSearches = new List<RecentRecipeSearch>();
            _profilePicture = "../Assets/Default.jpg";
            _userDescription = "This user's description is empty. Update it to add information!";

        }

        /// <summary>
        /// Gets or sets the profile picture URL of the member.
        /// </summary>
        /// <remarks>
        /// The URL must not be empty or null.
        /// </remarks>
        public string ProfilePicture
        {
            get { return _profilePicture; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Invalid profile picture url");
                }

                _profilePicture = value;
            }
        }
        
        /// <summary>
        /// Gets or sets the description of the member.
        /// </summary>
        /// <remarks>
        /// The description must not be empty or null and must not exceed 100 characters.
        /// </remarks>
        public string UserDescription
        {
            get { return _userDescription; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Invalid user description must not be empty nor null");
                }
                if (value.Length >= 100)
                {
                    throw new ArgumentException("User Description must not be more than 100 characters");
                }
                _userDescription = value;
            }
        }

        /// <summary>
        /// Gets or sets the list of recipes owned by the member.
        /// </summary>
        public List<Recipe> OwnedRecipes
        {
            get
            {
                if (_ownedRecipes != null)
                {
                    return _ownedRecipes;
                }
                else
                {
                    throw new Exception("The _ownedRecipes list field is null!");
                }
            }
            set
            {
                if (value is List<Recipe>)
                {
                    _ownedRecipes = value;
                }
                else
                {
                    throw new ArgumentException("Invalid type of value, must be a list of recipes");
                }
            }
        }
        /// <summary>
        /// Gets or sets the list of recipes favorited by the member.
        /// </summary>
        public List<FavoriteRecipe> FavoriteRecipes
        {
            get
            {
                if (_favoriteRecipes != null)
                {
                    return _favoriteRecipes;
                }
                else
                {
                    throw new NullReferenceException("The favourite Recipe field is null!");
                }
            }
            set
            {
                if (value is List<FavoriteRecipe>)
                {
                    _favoriteRecipes = value;
                }
                else
                {
                    throw new ArgumentException("Invalid type of value, must be a list of recipes");
                }
            }
        }

        /// <summary>
        /// Gets or sets the list of recipes recently searched by the member.
        /// </summary>
        public List<RecentRecipeSearch> RecentRecipeSearches
        {
            get
            {
                if (_recentRecipeSearches != null)
                {
                    return _recentRecipeSearches;
                }
                else
                {
                    throw new NullReferenceException("The recent Recipe searches field is null!");
                }
            }
            set
            {
                if (value is List<RecentRecipeSearch>)
                {
                    _recentRecipeSearches = value;
                }
                else
                {
                    throw new ArgumentException("Invalid type of value, must be a list of recipes");
                }
            }
        }
        /// <summary>
        /// Lists the member's favorite recipes in a formatted string.
        /// </summary>
        /// <returns>A formatted string of the member's favorite recipes.</returns>
        public string? ListFavorite()
        {
            if (_favoriteRecipes == null || _favoriteRecipes.Count == 0)
            {
                Console.WriteLine(Username + " does not have anything in their favorites.");
                return "";
            }
            string list = "";
            int index = 1;
            foreach (FavoriteRecipe favoriteRecipe in _favoriteRecipes)
            {
                list += "------------------\n\n";
                list += String.Format("Recipe", index);
                list += favoriteRecipe.Recipe + "\n";
                index++;
            }
            return list;
        }

        
        /// <summary>
        /// Lists the member's owned recipes in a formatted string.
        /// </summary>
        /// <returns>A formatted string of the member's owned recipes.</returns>
        public string ListOwnedRecipe()
        {
            if (_ownedRecipes == null)
            {
                throw new ArgumentException("Empty Owned Recipe List");
            }
            string builder = "";
            if (OwnedRecipes == null)
            {
                Console.WriteLine("No owned recipes found");
                return builder;
            }
            if (OwnedRecipes.Count == 0)
            {
                builder += "None!";
            }
            else
            {
                List<Recipe> ownedRecipes = OwnedRecipes;
                foreach (Recipe recipe in ownedRecipes)
                {
                    builder += "⚫ " + recipe.Name + "\n";
                }
            }
            return builder;
        }
        /// <summary>
        /// Lists the member's recent recipe searches in a formatted string.
        /// </summary>
        /// <returns>A formatted string of the member's recent recipe searches.</returns>
        public string? ListRecentSearches()
        {
            if (_recentRecipeSearches == null || _recentRecipeSearches.Count == 0)
            {
                return "Empty Recent Searches List!";
            }
            string list = "";
            int index = 1;
            foreach (RecentRecipeSearch recipe in _recentRecipeSearches)
            {
                list += "------------------\n";
                list += String.Format("Recipe {0}\n", index);
                list += recipe.Recipe + "\n";
                index++;
            }
            return list;
        }

        /// <summary>
        /// Adds a recipe to the member's owned recipes list.
        /// </summary>
        /// <param name="recipe">The recipe to add.</param>
        public void addOwnedRecipe(Recipe recipe)
        {
            if (_ownedRecipes != null)
            {
                if (_favoriteRecipes != null)
                {
                    foreach (Recipe ownedRecipe in _ownedRecipes)
                    {
                        if (ownedRecipe.Equals(recipe))
                        {
                            throw new ArgumentNullException("Recipe already exists in owned recipe list");
                        }
                    }
                }
                _ownedRecipes.Add(recipe);
            }
            else
            {
                throw new ArgumentNullException("The _ownedRecipes field cannot be null!");
            }
        }
        /// <summary>
        /// Removes a recipe from the member's owned recipes list by index.
        /// </summary>
        /// <param name="index">The index of the recipe to remove.</param>
        public void DeleteOwnedRecipe(int index)
        {
            if (_ownedRecipes != null)
            {
                if (!_ownedRecipes[index].Equals(null))
                {
                    _ownedRecipes.RemoveAt(index);
                }
                else
                {
                    throw new ArgumentNullException(string.Format("{0} recipe at {1} is null", _ownedRecipes[index], index));
                }
            }
            else
            {
                throw new ArgumentNullException("The _ownedRecipes field is null!");
            }

        }
        /// <summary>
        /// Removes a recipe from the member's favorite recipes list by index.
        /// </summary>
        /// <param name="index">The index of the recipe to remove.</param>
        public void RemoveFavorite(int index)
        {
            if (_favoriteRecipes != null)
            {
                if (!_favoriteRecipes[index].Equals(null))
                {
                    _favoriteRecipes.RemoveAt(index);
                }
                else
                {
                    throw new ArgumentNullException(string.Format("{0} recipe at {1} is null", _favoriteRecipes[index], index));
                }
            }
            else
            {
                throw new Exception("The _facoriteRecipes list field is null!");
            }

        }

        /// <summary>
        /// Returns a string representation of the member's information.
        /// </summary>
        /// <returns>A string containing the member's username, profile picture, description, and owned recipes.</returns>
        public override string ToString()
        {
            string builder = "Username: " + Username + "\n"
            + "Profile Picture: " + ProfilePicture + "\n"
            + "Description: " + UserDescription + "\n"
            + "Owned Recipes: ";
            if (OwnedRecipes == null)
            {
                Console.WriteLine("No owned recipes found");
                return builder;
            }
            if (OwnedRecipes.Count == 0)
            {
                builder += "None!";
            }
            else
            {
                List<Recipe> ownedRecipes = OwnedRecipes;
                foreach (Recipe recipe in ownedRecipes)
                {
                    builder += recipe.Name + ", ";
                }
            }
            return builder;
        }
    }


    public class Admin : User
    {
        //  public int AdmintId { get; set; }
        public int AdminId { get; set; }
        public Admin() { }
        public Admin(string username, string password) : base(username, password)
        {

        }
    }
}
