using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using UserNameSpace;


public class MemberController : UserController
{
    private List<Member> _members = null!;
    private Member _currentMember = null!;
    private RecipeContext _context;


    // {
    //     _members = members;
    //     Console = writer;
    // }

    /// <summary>
    /// This method is the controller of the class
    /// </summary>
    /// <param name="members">The members to be added to the database</param>
    // public MemberController(List<Member> members, IConsole writer)
    public MemberController(RecipeContext recipeContext)
    {
        _context = recipeContext;
    }

    /// <summary>
    /// Property of the members field
    /// </summary>
    public List<Member> Members
    {
        get
        {
            if (_members != null)
            {
                return _members;
            }
            else
            {
                throw new Exception("The members list is null!");
            }
        }
        set { _members = value; }
    }

    /// <summary>
    /// Property of the currentMember field
    /// </summary>
    public Member CurrentMember
    {
        get
        {
            if (_currentMember != null)
            {
                return _currentMember;
            }
            else
            {
                throw new Exception("The current member field is null!");
            }

        }
        set { _currentMember = value; }
    }

    public List<Member> GetAllMembersDB()
    {
        List<Member> allMembersDb = new List<Member>();
        allMembersDb = _context.Members
        .Include(m => m.OwnedRecipes)
        .Include(member => member.FavoriteRecipes)
            .ThenInclude(fr => fr.Recipe)
                .ThenInclude(r => r.Tags)
        .Include(member => member.FavoriteRecipes)
            .ThenInclude(fr => fr.Recipe)
                .ThenInclude(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
        .Include(member => member.FavoriteRecipes)
            .ThenInclude(fr => fr.Recipe)
                .ThenInclude(r => r.Instructions)
        .Include(member => member.FavoriteRecipes)
            .ThenInclude(fr => fr.Recipe)
                .ThenInclude(r => r.Rates)
        .Include(m => m.RecentRecipeSearches)
        .ToList();
        return allMembersDb;
    }

    /// <summary>
    /// Display the Favourite Recipe List.
    /// </summary>
    public void ListFavoriteRecipes(Member memberToView)
    {
        // Console.Clear();
        if (memberToView != null)
        {
            Console.WriteLine(String.Format("\n------ Favorite Recipes of {0} ------\n", memberToView.Username));
            Console.WriteLine(memberToView.ListFavorite());
        }
        else
        {
            throw new ArgumentNullException("The current member field cannot be null!");
        }
    }
    //return all recipes that are favorited by the user into recipe list
    public List<Recipe> GetFavoriteRecipes(Member member)
    {
        List<Recipe> favoriteRecipes = _context.Recipes
            .Where(r => member.FavoriteRecipes.Any(fr => fr.MemberId == member.MemberId))
            .ToList();
        return favoriteRecipes;
    }



    /// <summary>
    /// Display the Owned Recipe List.
    /// </summary>
    public void ListOwnedRecipes(Member memberToView)
    {
        Console.Clear();
        if (memberToView != null)
        {
            Console.WriteLine(String.Format("\n------ Owned By {0} ------\n", memberToView.Username));
            Console.WriteLine(memberToView.ListOwnedRecipe());
        }
        else
        {
            throw new ArgumentNullException("The current member field cannot be null!");
        }
    }

    /// <summary>
    /// List the Recent Recipe List.
    /// </summary>
    public void ListRecentSearches(Member memberToView)
    {
        Console.Clear();
        if (memberToView != null)
        {
            Console.WriteLine(String.Format("\n------ Search History of {0} ------\n", memberToView.Username));
            Console.WriteLine("-- Most Recent to Oldest --\n");
            Console.WriteLine(memberToView.ListRecentSearches());
        }
        else
        {
            throw new ArgumentNullException("The current member field cannot be null!");
        }
    }

    /// <summary>
    /// This method is used to check this member account Details.
    /// </summary>
    public void ViewMyAccountDetails(Member memberToView)
    {
        Console.WriteLine(memberToView);
        ConsoleWriter.ClearWithEnter();
    }

    /// <summary>
    /// This method creates a member
    /// </summary>
    /// <param name="username">The username of the potential member</param>
    /// <param name="password">The password of the potential member</param>
    public void CreateMember(string username, string password)
    {
        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentException("Username cannot be null or empty");
        }

        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Password cannot be null or empty");
        }
        if (_context.Members.Any(m => m.Username == username))
        {
            throw new ArgumentException("Username is already taken");
        }
        Member memberToAdd = new Member(username, password);
        _context.Members.Add(memberToAdd);

        _context.SaveChanges();
    }

    /// <summary>
    /// This method verifies the login of the method
    /// </summary>
    /// <param name="username">The username of the member that wants to login</param>
    /// <param name="password">The password of the member that wants to login</param>
    /// <returns></returns>
    public bool VerifyLogin(string username, string password)
    {
        var member = _context.Members
            .AsEnumerable()
            .FirstOrDefault(m => m.Username == username);
        //First or default returns the first element of the sequence 
        //or a default value (NULL in this case) if no element is found. 
        if (member == null)
        {
            return false;
        }
        else
        {
            if (member.MatchPasswords(password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Add a favorite recipe to this user's favorite's list.
    /// </summary>
    /// <param name="recipe">Recipe to be added to the list</param>
    /// <exception cref="ArgumentException">Exception is thrown is the recipe is alrd in the list</exception>
    public void AddFavRecipe(Recipe recipe, Member memberToUpdate)
    {
        if (recipe != null && memberToUpdate != null)
        {
            if (memberToUpdate.FavoriteRecipes != null)
            {
                foreach (FavoriteRecipe favRecipe in memberToUpdate.FavoriteRecipes)
                {
                    if (favRecipe.Recipe == null)
                    {
                        throw new ArgumentNullException("One of the favourite recipe of this member is null!");
                    }
                    if (favRecipe.Recipe.Equals(recipe))
                    {
                        throw new ArgumentException("Recipe already exists in favorite list");
                    }
                }
            }
            if (memberToUpdate.FavoriteRecipes != null)
            {
                FavoriteRecipe favRecipe = new FavoriteRecipe();
                favRecipe.Recipe = recipe;
                favRecipe.MemberId = memberToUpdate.MemberId;
                favRecipe.RecipeId = recipe.RecipeId;
                _context.FavoriteRecipes.Add(favRecipe);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("The current member FavoriteRecipes field cannot be null!");
            }
        }
        else
        {
            throw new Exception("The current member field is null!");
        }
    }

    /// <summary>
    /// Adds a recipe to the member's recent searches list.
    /// </summary>
    /// <param name="recipe">The recipe to add.</param>
    public void AddSearchRecipe(Recipe recipe, Member memberToUpdate)
    {
        if (memberToUpdate != null)
        {
            if (memberToUpdate.RecentRecipeSearches != null)
            {
                memberToUpdate.RecentRecipeSearches.RemoveAll(i => i.Equals(recipe));
                RecentRecipeSearch searchRecipe = new RecentRecipeSearch
                {
                    Recipe = recipe
                };
                memberToUpdate.RecentRecipeSearches.Insert(0, searchRecipe);
                while (memberToUpdate.RecentRecipeSearches.Count > 20)
                {
                    memberToUpdate.RecentRecipeSearches.RemoveAt(memberToUpdate.RecentRecipeSearches.Count - 1);
                }
                _context.SaveChanges();
            }
            else
            {
                throw new NullReferenceException("The recent recipe of this member is null!");
            }
        }
        else
        {
            throw new Exception("The current member field is null!");
        }
    }

    /// <summary>
    /// This method 
    /// </summary>
    /// <param name="recipe"></param>
    /// <exception cref="ArgumentException"></exception>
    public void DeleteAFavRecipe(Recipe recipe, Member memberToUpdate)
    {
        if (memberToUpdate != null && memberToUpdate.FavoriteRecipes != null)
        {
            FavoriteRecipe? removeFavRecipe = memberToUpdate.FavoriteRecipes.FirstOrDefault(fr => fr.Recipe.RecipeId == recipe.RecipeId);
            if (removeFavRecipe != null)
            {
                memberToUpdate.FavoriteRecipes.Remove(removeFavRecipe);
                _context.SaveChanges();
            }
        }
        else
        {
            throw new ArgumentNullException("The current member favoriteRecipes field cannot be null!");
        }
    }
    /// <summary>
    /// Removes a recipe from the member's owned recipes list by index.
    /// </summary>
    /// <param name="recipe">The recipe to remove.</param>
    public void DeleteOwnedRecipe(Recipe recipe, Member memberToUpdate)
    {
        if (memberToUpdate != null && memberToUpdate.OwnedRecipes != null)
        {
            memberToUpdate.OwnedRecipes.Remove(recipe);
            _context.SaveChanges();
        }
        else
        {
            throw new ArgumentNullException("The _ownedRecipes field is null!");
        }

    }

    /// <summary>
    /// Delete this Member Account.
    /// </summary>
    public void DeleteMyAccount(Member memberToUpdate)
    {
        if (memberToUpdate != null)
        {
            _context.Members.Remove(memberToUpdate);
            _context.SaveChanges();
            Console.WriteLine("Deleted your account successfully!");
        }
        else
        {
            throw new ArgumentNullException("The members field or the current member is null!");
        }
    }

    public void RemoveMemberDescription(Member memberToUpdate)
    {
        if (memberToUpdate != null)
        {
            memberToUpdate.UserDescription = "This user's description is empty. Update it to add information!"; //Change if description is silly.
            _context.SaveChanges();
            Console.WriteLine("Removed the description successfully!");
        }
        else
        {
            throw new ArgumentNullException("The current member field cannot be null!");
        }
    }

    /// <summary>
    /// Update the member Profile picture=.
    /// </summary>
    /// <param name="newProfilePicture">New profile picture string</param>
    public void RemoveProfilePicture(Member memberToUpdate)
    {
        if (memberToUpdate != null)
        {
            memberToUpdate.ProfilePicture = "placeholder, this will be a default pfp."; //change this so it sets a default pfp.
            _context.SaveChanges();
            Console.WriteLine("Removed the profile picture successfully!");
        }
        else
        {
            throw new ArgumentNullException("The current member field cannot be null!");
        }
    }

    public void UpdateUserName(string newUsername, Member memberToUpdate)
    {
        if (memberToUpdate == null)
        {
            throw new ArgumentNullException(nameof(memberToUpdate), "The current member field cannot be null!");
        }
        if (String.IsNullOrEmpty(newUsername))
        {
            throw new ArgumentNullException(nameof(newUsername), "The new username cannot be null or empty!");
        }
        if (_context.Members.Any(m => m.Username == newUsername))
        {
            throw new ArgumentException("Username is already taken");
        }

        try
        {
            memberToUpdate.Username = newUsername;
            _context.SaveChanges();
            Console.WriteLine("Username changed successfully!");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Unable to update username");
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// Updates the member password.
    /// </summary>
    /// <param name="newPassword">new password to be changed</param>
    public void UpdatePassword(string newPassword, Member memberToUpdate)
    {
        if (memberToUpdate == null)
        {
            throw new ArgumentNullException(nameof(memberToUpdate), "The current member field cannot be null!");
        }
        if (memberToUpdate.MatchPasswords(newPassword))
        {
            throw new ArgumentException("The new password cannot be the same as the old password!");
        }
        if (String.IsNullOrEmpty(newPassword))
        {
            throw new ArgumentNullException(nameof(newPassword), "The new password cannot be null or empty!");
        }

        memberToUpdate.Password = newPassword;
        _context.SaveChanges();
    }


    public void UpdateProfilePicture(string newProfilePicture, Member memberToUpdate)
    {
        if (memberToUpdate == null || newProfilePicture == null)
        {
            throw new ArgumentNullException("The current member field cannot be null!");
        }
        try
        {
            memberToUpdate.ProfilePicture = newProfilePicture;
            _context.SaveChanges();
            Console.WriteLine("Updated the profile picture successfully!");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Unable to update profile picture");
            Console.WriteLine(e.Message);
        }
    }

    //Change user's personal description
    public void UpdateUserDescription(string newDescription, Member memberToUpdate)
    {
        if (memberToUpdate == null || newDescription == null)
        {
            throw new ArgumentNullException(nameof(memberToUpdate), "The current member field cannot be null!");
        }

        try
        {
            memberToUpdate.UserDescription = newDescription;
            _context.SaveChanges();
            Console.WriteLine("Description updated successfully! Your new description is: ");
            Console.WriteLine("' " + newDescription + " '");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Unable to update user description");
            Console.WriteLine(e.Message);
        }
    }

    public Member GetMember(string username)
    {
        // Retrieve the member from the DbContext
        var member = _context.Members
        .Include(member => member.FavoriteRecipes)
            .ThenInclude(fr => fr.Recipe)
                .ThenInclude(r => r.Tags)
        .Include(member => member.FavoriteRecipes)
            .ThenInclude(fr => fr.Recipe)
                .ThenInclude(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
        .Include(member => member.FavoriteRecipes)
            .ThenInclude(fr => fr.Recipe)
                .ThenInclude(r => r.Instructions)
        .Include(member => member.FavoriteRecipes)
            .ThenInclude(fr => fr.Recipe)
                .ThenInclude(r => r.Rates)
        .Include(member => member.OwnedRecipes)
        .Include(member => member.RecentRecipeSearches)
            .ThenInclude(rrs => rrs.Recipe)
                .ThenInclude(r => r.Tags)
        .Include(member => member.RecentRecipeSearches)
            .ThenInclude(fr => fr.Recipe)
                .ThenInclude(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)
        .Include(member => member.RecentRecipeSearches)
            .ThenInclude(fr => fr.Recipe)
                .ThenInclude(r => r.Instructions)
        .Include(member => member.RecentRecipeSearches)
            .ThenInclude(fr => fr.Recipe)
                .ThenInclude(r => r.Rates)
        .FirstOrDefault(m => m.Username == username);

        if (member == null)
        {
            throw new ArgumentException("A member with this username does not exist.");
        }

        return member;
    }
    /// <summary>
    /// Method to return all recipes made by user and related data
    /// </summary>
    /// <param name="member"></param>
    /// <returns></returns>
    public List<Recipe> GetOwnedRecipes(Member member)
    {
        return _context.Recipes
        .Where(r => r.Owner.Username == member.Username)
        .Include(r => r.Rates)
        .Include(r => r.Instructions)
        .Include(r => r.Tags)
        .Include(r => r.RecipeIngredients)
            .ThenInclude(r => r.Ingredient)
        .ToList();
    }
}

