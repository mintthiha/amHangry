using System.Drawing;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using NuGet.Frameworks;
using UserNameSpace;

namespace Test;

[TestClass]
public class MemberControllerDBTests
{

    //HELPER METHODS

    /// <summary>
    /// Does the setup process of the mockSet based off the QueriedList given
    /// </summary>
    /// <returns>
    /// returns the setup mockSet
    /// </returns>
    private Mock<DbSet<Member>> setUpMockSetMember(IQueryable<Member> dataSet)
    {
        var mockSet = new Mock<DbSet<Member>>();
        mockSet.As<IQueryable<Member>>().Setup(m => m.Provider).Returns(dataSet.Provider);
        mockSet.As<IQueryable<Member>>().Setup(m => m.Expression).Returns(dataSet.Expression);
        mockSet.As<IQueryable<Member>>().Setup(m => m.ElementType).Returns(dataSet.ElementType);
        mockSet.As<IQueryable<Member>>().Setup(m => m.GetEnumerator()).Returns(dataSet.GetEnumerator());
        return mockSet;
    }
    private Mock<DbSet<Recipe>> setUpMockSetRecipe(IQueryable<Recipe> dataSet)
    {
        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(dataSet.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(dataSet.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(dataSet.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(dataSet.GetEnumerator());
        return mockSet;
    }
    private Mock<DbSet<RecipeIngredient>> setUpMockSetRecipeIngredient(IQueryable<RecipeIngredient> dataSet)
    {
        var mockSet = new Mock<DbSet<RecipeIngredient>>();
        mockSet.As<IQueryable<RecipeIngredient>>().Setup(m => m.Provider).Returns(dataSet.Provider);
        mockSet.As<IQueryable<RecipeIngredient>>().Setup(m => m.Expression).Returns(dataSet.Expression);
        mockSet.As<IQueryable<RecipeIngredient>>().Setup(m => m.ElementType).Returns(dataSet.ElementType);
        mockSet.As<IQueryable<RecipeIngredient>>().Setup(m => m.GetEnumerator()).Returns(dataSet.GetEnumerator());
        return mockSet;
    }
    private Mock<DbSet<FavoriteRecipe>> setUpMockSetFavoriteRecipe(IQueryable<FavoriteRecipe> dataSet)
    {
        var mockSet = new Mock<DbSet<FavoriteRecipe>>();
        mockSet.As<IQueryable<FavoriteRecipe>>().Setup(m => m.Provider).Returns(dataSet.Provider);
        mockSet.As<IQueryable<FavoriteRecipe>>().Setup(m => m.Expression).Returns(dataSet.Expression);
        mockSet.As<IQueryable<FavoriteRecipe>>().Setup(m => m.ElementType).Returns(dataSet.ElementType);
        mockSet.As<IQueryable<FavoriteRecipe>>().Setup(m => m.GetEnumerator()).Returns(dataSet.GetEnumerator());
        return mockSet;
    }
    private Mock<RecipeContext> setUpMockContext(Mock<DbSet<Member>> mockSet)
    {
        var mockContext = new Mock<RecipeContext>();
        mockContext.Setup(m => m.Members).Returns(mockSet.Object);
        return mockContext;
    }


    //TESTS
    [TestMethod]
    public void TestCreatingMember_AddedToDb_Once_Success_Success()
    {
        //Arrange
        var members = new List<Member>();
        var mockSet = setUpMockSetMember(members.AsQueryable());
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mc = new MemberController(mockContext.Object);
        //Act
        mc.CreateMember("Danny", "123456");
        //Assert
        mockSet.Verify(m => m.Add(It.IsAny<Member>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestCreatingMember_ThrowsException_WhenUsernameExists()
    {
        //Arrange
        var existingMembers = new Member("Danny", "123456");
        var dataSet = new List<Member> { existingMembers }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.CreateMember("Danny", "123456");
    }

    [TestMethod]
    public void TestVerifyLogin_ReturnsTrue_WhenUsernameAndPasswordMatch()
    {
        //Arrange
        var existingMembers = new Member("Danny", "123456");
        var dataSet = new List<Member> { existingMembers }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        var mockContext = setUpMockContext(mockSet);
        IConsole mockWrite = new MockWriter();
        var mockService = new MemberController(mockContext.Object);
        //Act
        var result = mockService.VerifyLogin("Danny", "123456");
        //Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void TestAddFavoriteRecipeEmptyMemberException()
    {
        //Arrange
        Instruction instruction = new Instruction("Step 1: Cut the apples");
        List<Instruction> instructions = new List<Instruction>();
        instructions.Add(instruction);
        Tag tag = new Tag("Fruity");
        List<Tag> tags = new List<Tag>();
        tags.Add(tag);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Quantity = 10;
        Ingredient apple = new Ingredient("Apple", "fruit", 10, 10, 10, 1, 1, new UnitEntity(Unit.Gram));
        recipeIngredient.Ingredient = apple;
        List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
        recipeIngredients.Add(recipeIngredient);

        var member = new Member("Danny", "123456");
        var recipe = new Recipe("ApplePie", member, "A hot apple pie", 20, 20, instructions, tags, 5, recipeIngredients);
        var favoriteRecipe = new FavoriteRecipe { Recipe = recipe, Member = member };

        var membersDataset = new List<Member> { member }.AsQueryable();
        var favoriteRecipes = new List<FavoriteRecipe> { favoriteRecipe }.AsQueryable();


        IConsole mockWrite = new MockWriter();
        var mockMemberSet = setUpMockSetMember(membersDataset);
        var mockFavoriteRecipeSet = setUpMockSetFavoriteRecipe(favoriteRecipes);
        var mockContext = new Mock<RecipeContext>();
        mockContext.Setup(c => c.Members).Returns(mockMemberSet.Object);
        mockContext.Setup(c => c.FavoriteRecipes).Returns(mockFavoriteRecipeSet.Object);
        var mockService = new MemberController(mockContext.Object);

        //Act
        mockService.AddFavRecipe(recipe, null);

    }

    [TestMethod]
    public void TestRemoveFavoriteRecipe_RemovesRecipeFromMember_Once_Success()
    {
        //Arrange
        Instruction instruction = new Instruction("Step 1: Cut the apples");
        List<Instruction> instructions = new List<Instruction>();
        instructions.Add(instruction);

        Tag tag = new Tag("Fruity");
        List<Tag> tags = new List<Tag>();
        tags.Add(tag);

        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Quantity = 10;
        Ingredient apple = new Ingredient("Apple", "fruit", 10, 10, 10, 1, 1, new UnitEntity(Unit.Gram));
        recipeIngredient.Ingredient = apple;
        List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
        recipeIngredients.Add(recipeIngredient);
        var existingMembers = new Member("Danny", "123456");
        var existingRecipe = new Recipe("ApplePie", existingMembers, "A hot apple pie", 20, 20, instructions, tags, 5, recipeIngredients);
        var favoriteRecipe = new FavoriteRecipe { Recipe = existingRecipe, Member = existingMembers };
        existingMembers.FavoriteRecipes.Add(favoriteRecipe);
        
        var membersDataset = new List<Member> { existingMembers }.AsQueryable();
        IConsole mockWrite = new MockWriter();
        var mockMemberSet = setUpMockSetMember(membersDataset);
        var mockContext = new Mock<RecipeContext>();
        mockContext.Setup(c => c.Members).Returns(mockMemberSet.Object);
        var mockService = new MemberController(mockContext.Object);

        //Act
        mockService.DeleteAFavRecipe(existingRecipe, existingMembers);
        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestDeleteAFavRecipeMemberIsNull()
    {
        //Arrange
        Instruction instruction = new Instruction("Step 1: Cut the apples");
        List<Instruction> instructions = new List<Instruction>();
        instructions.Add(instruction);
        Tag tag = new Tag("Fruity");
        List<Tag> tags = new List<Tag>();
        tags.Add(tag);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Quantity = 10;
        Ingredient apple = new Ingredient("Apple", "fruit", 10, 10, 10, 1, 1, new UnitEntity(Unit.Gram));
        recipeIngredient.Ingredient = apple;
        List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
        recipeIngredients.Add(recipeIngredient);

        var member = new Member("Danny", "123456");
        var recipe = new Recipe("ApplePie", member, "A hot apple pie", 20, 20, instructions, tags, 5, recipeIngredients);
        var favoriteRecipe = new FavoriteRecipe { Recipe = recipe, Member = member };

        var membersDataset = new List<Member> { member }.AsQueryable();
        var favoriteRecipes = new List<FavoriteRecipe> { favoriteRecipe }.AsQueryable();

        IConsole mockWrite = new MockWriter();
        var mockMemberSet = setUpMockSetMember(membersDataset);
        var mockFavoriteRecipeSet = setUpMockSetFavoriteRecipe(favoriteRecipes);
        var mockContext = new Mock<RecipeContext>();
        mockContext.Setup(c => c.Members).Returns(mockMemberSet.Object);
        mockContext.Setup(c => c.FavoriteRecipes).Returns(mockFavoriteRecipeSet.Object);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.DeleteAFavRecipe(recipe, null);
    }
    [TestMethod]
    public void TestDeleteAccount_RemovesMemberFromDb_Once_Success()
    {
        //Arrange
        var existingMembers = new Member("Danny", "123456");
        var dataSet = new List<Member> { existingMembers }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.DeleteMyAccount(existingMembers);
        //Assert
        mockSet.Verify(m => m.Remove(It.IsAny<Member>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestDeleteMyAccountMemberIsNull()
    {
        //Arrange
        var dataSet = new List<Member> { }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.DeleteMyAccount(null);
    }
    [TestMethod]
    public void TestRemoveUserDescription_RemovesDescriptionFromMember_Once_Success()
    {
        //Arrange
        var existingMembers = new Member("Danny", "123456");
        var dataSet = new List<Member> { existingMembers }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.RemoveMemberDescription(existingMembers);
        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestRemoveMemberDescriptionMemberIsNull()
    {
        //Arrange
        var dataSet = new List<Member> { }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.RemoveMemberDescription(null);
    }
    [TestMethod]
    public void TestRemoveProfilePicture_RemovesProfilePictureFromMember_Once_Success()
    {
        //Arrange
        var existingMembers = new Member("Danny", "123456");
        var dataSet = new List<Member> { existingMembers }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.RemoveProfilePicture(existingMembers);
        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
        var updatedMember = mockSet.Object.FirstOrDefault(m => m.Username == "Danny");
        Assert.AreEqual("placeholder, this will be a default pfp.", updatedMember?.ProfilePicture);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestRemoveProfilePictureMemberIsNull()
    {
        //Arrang
        var dataSet = new List<Member> { }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.RemoveProfilePicture(null);
    }
    [TestMethod]
    public void TestUpdatePassword_UpdatesPassword_Once_Success()
    {
        //Arrange
        var existingMembers = new Member("Danny", "123456");
        var dataSet = new List<Member> { existingMembers }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.UpdatePassword("NewPassword", existingMembers);
        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestUpdatePasswordMemberIsNull()
    {
        //Arrange
        var dataSet = new List<Member> { }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.UpdatePassword("123456", null);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestUpdatePasswordSamePassword()
    {
        //Arrange
        var existingMembers = new Member("Danny  ", "123456");
        var dataSet = new List<Member> { existingMembers }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.UpdatePassword("123456", existingMembers);
    }

    [TestMethod]
    public void UpdateProfilePicture_UpdatesProfilePicture_Once_Success()
    {
        //Arrange
        var existingMembers = new Member("Danny", "123456");
        var dataSet = new List<Member> { existingMembers }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.UpdateProfilePicture("test", existingMembers);
        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestUpdateProfilePictureMemberIsNull()
    {
        //Arrange
        var dataSet = new List<Member> { }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.UpdateProfilePicture("test", null);
    }
    [TestMethod]
    public void TestUpdateUserDescription_UpdatesDescription_Once_Success()
    {
        //Arrange
        var existingMembers = new Member("Danny", "123456");
        var dataSet = new List<Member> { existingMembers }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.UpdateUserDescription("test", existingMembers);
        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestUpdateUserDescriptionMemberIsNull()
    {
        //Arrange
        var dataSet = new List<Member> { }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.UpdateUserDescription("test", null);
    }
    [TestMethod]
    public void TestUpdateUserDescriptionValue()
    {
        //Arrange
        var existingMembers = new Member("Danny", "123456");
        var dataSet = new List<Member> { existingMembers }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.UpdateUserDescription("test", existingMembers);
        //Assert
        Assert.AreEqual("test", existingMembers.UserDescription);
    }
    [TestMethod]
    public void TestUpdateProfilePictureValue()
    {
        //Arrange
        var existingMember = new Member("Danny", "123456");
        var dataSet = new List<Member> { existingMember }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);

        //Act
        mockService.UpdateProfilePicture("test", existingMember);

        //Assert
        var updatedMember = mockSet.Object.FirstOrDefault(m => m.Username == "Danny");
        Assert.AreEqual("test", updatedMember?.ProfilePicture);
    }
    [TestMethod]
    public void TestUpdatePasswordValue()
    {
        //Arrange
        var existingMember = new Member("Danny", "123456");
        var dataSet = new List<Member> { existingMember }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);

        //Act
        mockService.UpdatePassword("newPassword", existingMember);

        //Assert
        var updatedMember = mockSet.Object.FirstOrDefault(m => m.Username == "Danny");
        Assert.IsTrue(updatedMember?.MatchPasswords("newPassword"));
    }
    [TestMethod]
    public void TestUpdateUserNameValue_UpdatesCorrectly()
    {
        //Arrange
        var existingMembers = new Member("Danny", "123456");
        var dataSet = new List<Member> { existingMembers }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);

        //Act
        mockService.UpdateUserName("newName", existingMembers);

        //Assert
        var updatedMember = mockSet.Object.FirstOrDefault(m => m.Username == "newName");
        Assert.AreEqual("newName", updatedMember?.Username);
    }
    [TestMethod]
    public void TestUpdateUserName_UpdatesUserName_Once_Success()
    {
        //Arrange
        var existingMembers = new Member("Danny", "123456   ");
        var dataSet = new List<Member> { existingMembers }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.UpdateUserName("newName", existingMembers);

        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestUpdateUserNameMemberIsNull()
    {
        //Arrange
        //var existingMembers = new Member("Danny", "123456   ");
        var dataSet = new List<Member> { }.AsQueryable();
        var mockSet = setUpMockSetMember(dataSet);
        IConsole mockWrite = new MockWriter();
        var mockContext = setUpMockContext(mockSet);
        var mockService = new MemberController(mockContext.Object);
        //Act
        mockService.UpdateUserName("newName", null);
    }
}