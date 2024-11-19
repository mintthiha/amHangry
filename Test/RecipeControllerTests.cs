namespace Test;
using Microsoft.EntityFrameworkCore;
using Moq;
using UserNameSpace;

[TestClass]
public class RecipeControllerMoq
{
    [TestMethod]
    public void CreateValidRecipe_SavesRecipeUsingContext()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var mockTags = new Mock<DbSet<Tag>>();
        var tagdata = tags.AsQueryable();
        var recipeData = new List<Recipe>().AsQueryable();
        mockTags.As<IQueryable<Tag>>().Setup(r => r.Provider).Returns(tagdata.Provider);
        mockTags.As<IQueryable<Tag>>().Setup(r => r.Expression).Returns(tagdata.Expression);
        mockTags.As<IQueryable<Tag>>().Setup(r => r.ElementType).Returns(tagdata.ElementType);
        mockTags.As<IQueryable<Tag>>().Setup(r => r.GetEnumerator()).Returns(tagdata.GetEnumerator());

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        mockContext.Setup(r => r.Tags).Returns(mockTags.Object);
        var service = new RecipeController(mockContext.Object);

        //Act
        service.CreateRecipeDB("Recipe1", member, "description", 10, 10, instructions, tags, 1, listIngredients);

        //Assert
        mockSet.Verify(r => r.Add(It.IsAny<Recipe>()), Times.Once());
        mockContext.Verify(r => r.SaveChanges(), Times.Once());

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateRecipe_InvalidNameNoSave()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var mockTags = new Mock<DbSet<Tag>>();
        var data = tags.AsQueryable();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();
        mockTags.As<IQueryable<Tag>>().Setup(r => r.Provider).Returns(data.Provider);
        mockTags.As<IQueryable<Tag>>().Setup(r => r.Expression).Returns(data.Expression);
        mockTags.As<IQueryable<Tag>>().Setup(r => r.ElementType).Returns(data.ElementType);
        mockTags.As<IQueryable<Tag>>().Setup(r => r.GetEnumerator()).Returns(data.GetEnumerator());

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        mockContext.Setup(r => r.Tags).Returns(mockTags.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        service.CreateRecipeDB("cake", member, "description", 10, 10, instructions, tags, 1, listIngredients);


    }

    [TestMethod]
    public void RateRecipe_Add_ValidRatingToRecipe()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };


        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());
        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        service.RateRecipeDB("Danny", 5, recipe);

        //Assert
        var updatedRecipe = mockSet.Object.FirstOrDefault();
        Assert.IsTrue(updatedRecipe?.Rates.Any(r => r.RatedBy == "Danny"));
        mockContext.Verify(r => r.SaveChanges(), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void RateRecipe_Add_InvalidRatingToRecipe()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };


        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());
        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        service.RateRecipeDB("Danny", 66, recipe);
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void RateRecipe_OwnerRateOwnRecipe()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };


        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());
        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        service.RateRecipeDB("Tommy", 4, recipe);
    }

    [TestMethod]
    public void DeleteRatingRecipeDB_Delete_Rating()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };


        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());
        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        service.RateRecipeDB("Danny", 5, recipe);

        //Act
        service.DeleteRatingRecipeDB("Danny", recipe);

        //Assert
        var updatedRecipe = mockSet.Object.FirstOrDefault();
        Assert.IsFalse(updatedRecipe?.Rates.Any(r => r.RatedBy == "Danny"));
        mockContext.Verify(r => r.SaveChanges(), Times.Exactly(2));
    }

    [TestMethod]
    public void UpdateRecipeName_SaveNewName()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);

        //Act
        service.UpdateRecipeNameDB("newName", recipe);

        //Assert
        var updatedRecipe = mockSet.Object.FirstOrDefault(r => r.Name == recipe.Name);
        Assert.AreEqual("newName", updatedRecipe?.Name);
        mockContext.Verify(r => r.SaveChanges(), Times.Once());

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateRecipeName_InvalidNewName()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("ExistName", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);

        //Act
        service.UpdateRecipeNameDB("ExistName", recipe);


    }

    [TestMethod]
    public void UpdateRecipeDescription_SaveNewDescription()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);


        //Act
        service.UpdateRecipeDescriptionDB("new description", recipe);

        //Assert
        var updatedRecipe = mockSet.Object.FirstOrDefault();
        Assert.AreEqual("new description", updatedRecipe?.Description);
        mockContext.Verify(r => r.SaveChanges(), Times.Once());

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateRecipeDescription_InvalidNewDescription()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);


        //Act
        service.UpdateRecipeDescriptionDB("", recipe);

        //Assert
        var updatedRecipe = mockSet.Object.FirstOrDefault();
        Assert.AreEqual("cake recipe", updatedRecipe?.Description);
        mockContext.Verify(r => r.SaveChanges(), Times.Never());

    }



    [TestMethod]
    public void UpdateRecipePreparationTime_SaveNewPreparationTime()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);


        //Act
        service.UpdatePreparationTimeDB(40, recipe);

        //Assert
        var updatedRecipe = mockSet.Object.FirstOrDefault();
        Assert.AreEqual(40, updatedRecipe?.PreparationTime);
        mockContext.Verify(r => r.SaveChanges(), Times.Once());

    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateRecipePreparationTime_InvalidNewPreparationTime()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);


        //Act
        service.UpdatePreparationTimeDB(-55, recipe);

    }

    [TestMethod]
    public void UpdateRecipeCookingTime_SaveNewCookingTime()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);


        //Act
        service.UpdateCookingTimeDB(40, recipe);

        //Assert
        var updatedRecipe = mockSet.Object.FirstOrDefault();
        Assert.AreEqual(40, updatedRecipe?.CookingTime);
        mockContext.Verify(r => r.SaveChanges(), Times.Once());

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateRecipeCookingTime_InvalidCookingTime()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);


        //Act
        service.UpdateCookingTimeDB(-55, recipe);
    }


    [TestMethod]
    public void UpdateRecipeInstructions_SaveNewInstructions()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);


        //Act
        List<Instruction> instructions2 = new List<Instruction> { new Instruction("Step 3"), new Instruction("Step 4") };
        service.UpdateRecipeInstructionsDB(instructions2, recipe);

        //Assert
        var updatedRecipe = mockSet.Object.FirstOrDefault();
        CollectionAssert.AreEqual(instructions2, updatedRecipe?.Instructions);
        mockContext.Verify(r => r.SaveChanges(), Times.Once());

    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateRecipeInstructions_InvalidInstructions()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);


        //Act
        List<Instruction> instructions2 = new List<Instruction> { null };
        service.UpdateRecipeInstructionsDB(instructions2, recipe);

    }

    [TestMethod]
    public void UpdateRecipeTags_SaveNewTags()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var mockTags = new Mock<DbSet<Tag>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        var tagData = tags.AsQueryable();
        mockTags.As<IQueryable<Tag>>().Setup(r => r.Provider).Returns(tagData.Provider);
        mockTags.As<IQueryable<Tag>>().Setup(r => r.Expression).Returns(tagData.Expression);
        mockTags.As<IQueryable<Tag>>().Setup(r => r.ElementType).Returns(tagData.ElementType);
        mockTags.As<IQueryable<Tag>>().Setup(r => r.GetEnumerator()).Returns(tagData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        mockContext.Setup(r => r.Tags).Returns(mockTags.Object);
        var service = new RecipeController(mockContext.Object);


        //Act
        List<Tag> newTags = new List<Tag> { new Tag("Fast") };
        service.UpdateRecipeTagsDB(newTags, recipe);

        //Assert
        var updatedRecipe = mockSet.Object.FirstOrDefault();
        CollectionAssert.AreEqual(newTags, updatedRecipe?.Tags);
        mockContext.Verify(r => r.SaveChanges(), Times.Once());
    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateRecipeTags_invalidNewTags()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var mockTags = new Mock<DbSet<Tag>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        var tagData = tags.AsQueryable();
        mockTags.As<IQueryable<Tag>>().Setup(r => r.Provider).Returns(tagData.Provider);
        mockTags.As<IQueryable<Tag>>().Setup(r => r.Expression).Returns(tagData.Expression);
        mockTags.As<IQueryable<Tag>>().Setup(r => r.ElementType).Returns(tagData.ElementType);
        mockTags.As<IQueryable<Tag>>().Setup(r => r.GetEnumerator()).Returns(tagData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        mockContext.Setup(r => r.Tags).Returns(mockTags.Object);
        var service = new RecipeController(mockContext.Object);


        //Act
        List<Tag> newTags = new List<Tag> { new Tag("") };
        service.UpdateRecipeTagsDB(newTags, recipe);
    }

    [TestMethod]
    public void UpdateRecipeServing_SaveNewServing()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);


        //Act
        service.UpdateRecipeServingDB(5, recipe);

        //Assert
        var updatedRecipe = mockSet.Object.FirstOrDefault();
        Assert.AreEqual(5, updatedRecipe?.Servings);
        mockContext.Verify(r => r.SaveChanges(), Times.Once());

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void UpdateRecipeServing_invalid_servings()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);


        //Act
        service.UpdateRecipeServingDB(-5, recipe);

    }

    [TestMethod]
    public void UpdateRecipeIngredients_SaveNewIngredients()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        Ingredient ingredient2 = new Ingredient("egg", "food", 2, 2, 2, 4, 1, unit);
        Ingredient ingredient3 = new Ingredient("fish", "food", 5, 5, 5, 5, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        RecipeIngredient recipeIngredient2 = new RecipeIngredient();
        recipeIngredient2.Ingredient = ingredient2;
        recipeIngredient2.Quantity = 2;
        RecipeIngredient recipeIngredient3 = new RecipeIngredient();
        recipeIngredient3.Ingredient = ingredient3;
        recipeIngredient3.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        List<RecipeIngredient> listIngredients2 = new List<RecipeIngredient> { recipeIngredient2, recipeIngredient3 };

        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);


        //Act
        service.UpdateRecipeIngredientsDB(listIngredients2, recipe);

        //Assert
        var updatedRecipe = mockSet.Object.FirstOrDefault();
        CollectionAssert.AreEqual(listIngredients2, updatedRecipe?.RecipeIngredients);
        mockContext.Verify(r => r.SaveChanges(), Times.Once());

    }

    [TestMethod]
    public void SearchByName_returns2Recipes()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member, "cake2 recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        List<Recipe> recipes = service.SearchByName("cake");

        //Assert
        Assert.AreEqual(2, recipes.Count);
        Assert.AreEqual("cake", recipes[0].Name);
        Assert.AreEqual("cakev2", recipes[1].Name);
    }



    [TestMethod]
    public void SearchByName_returns0Recipe()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member, "cake2 recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        List<Recipe> recipes = service.SearchByName("unknownRecipe");

        //Assert
        Assert.AreEqual(0, recipes.Count);
    }



    [TestMethod]
    public void SearchByExactName_return1Recipe()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member, "cake2 recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        Recipe result = service.SearchByExactName("cakev2");

        //Assert
        Assert.AreEqual("cakev2", result?.Name);
    }


    [TestMethod]
    public void SearchByExactName_return0RecipeNull()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member, "cake2 recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        Recipe result = service.SearchByExactName("unknown");

        //Assert
        Assert.AreEqual(null, result);
    }


    [TestMethod]
    public void SearchByUser_return2Recipes()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake2 recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        List<Recipe> results = service.SearchByUser("Tommy");

        //Assert
        Assert.AreEqual(2, results.Count);
        Assert.AreEqual("cake", results[0].Name);
        Assert.AreEqual("smoothie", results[1].Name);
    }


    [TestMethod]
    public void SearchByUser_return0Recipe_NoUser()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake2 recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        List<Recipe> results = service.SearchByUser("unknown");

        //Assert
        Assert.AreEqual(0, results.Count);
    }


    [TestMethod]
    public void SearchByDescription_return2Recipes()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        List<Recipe> results = service.SearchByDescription("cake");

        //Assert
        Assert.AreEqual(2, results.Count);
        Assert.AreEqual("cake", results[0].Name);
        Assert.AreEqual("cakev2", results[1].Name);
    }


    [TestMethod]
    public void SearchByDescription_return0Recipe_NoMatchDescription()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 10, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        List<Recipe> results = service.SearchByDescription("Fried rice");

        //Assert
        Assert.AreEqual(0, results.Count);
    }



    [TestMethod]
    public void SearchByTime_return1Recipe()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 3, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        List<Recipe> results = service.SearchByTime(20);

        //Assert
        Assert.AreEqual(1, results.Count);
        Assert.AreEqual("cake", results[0].Name);
    }



    [TestMethod]
    public void SearchByTime_return0Recipe_noRecipeUnder5mins()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 3, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        List<Recipe> results = service.SearchByTime(5);

        //Assert
        Assert.AreEqual(0, results.Count);
    }



    [TestMethod]
    public void SearchByTag_return1Recipe()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        List<Tag> tags2 = new List<Tag> { new Tag("fast") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 3, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags2, 3, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        List<Tag> tagToSearch = new List<Tag> { new Tag("fast") };
        List<Recipe> results = service.SearchByTag(tagToSearch);

        //Assert
        Assert.AreEqual(1, results.Count);
        Assert.AreEqual("smoothie", results[0].Name);
    }



    [TestMethod]
    public void SearchByServings_return2Recipes_2servingsOrMore()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 5, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags, 1, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        List<Recipe> results = service.SearchByServings(3);

        //Assert
        Assert.AreEqual(2, results.Count);
        Assert.AreEqual("cake", results[0].Name);
        Assert.AreEqual("cakev2", results[1].Name);

    }


    [TestMethod]
    public void SearchByServings_return0Recipes_6servingsOrMore()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 5, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags, 1, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        List<Recipe> results = service.SearchByServings(6);

        //Assert
        Assert.AreEqual(0, results.Count);

    }


    [TestMethod]
    public void SearchByIngredients_return3Recipes()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        Ingredient ingredient2 = new Ingredient("butter", "food", 2, 2, 2, 2, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        RecipeIngredient recipeIngredient2 = new RecipeIngredient();
        recipeIngredient2.Ingredient = ingredient2;
        recipeIngredient2.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        List<RecipeIngredient> listIngredients2 = new List<RecipeIngredient> { recipeIngredient, recipeIngredient2 };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 5, listIngredients2);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags, 1, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        List<string> ingredientsToSearch = new List<string> { "tomato" };
        List<Recipe> results = service.SearchByIngredients(ingredientsToSearch);

        //Assert
        Assert.AreEqual(3, results.Count);

    }

    [TestMethod]
    public void SearchByIngredients_return0Recipes_notFound()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        Ingredient ingredient2 = new Ingredient("butter", "food", 2, 2, 2, 2, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        RecipeIngredient recipeIngredient2 = new RecipeIngredient();
        recipeIngredient2.Ingredient = ingredient2;
        recipeIngredient2.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        List<RecipeIngredient> listIngredients2 = new List<RecipeIngredient> { recipeIngredient, recipeIngredient2 };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 5, listIngredients2);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags, 1, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        List<string> ingredientsToSearch = new List<string> { "egg" };
        List<Recipe> results = service.SearchByIngredients(ingredientsToSearch);

        //Assert
        Assert.AreEqual(0, results.Count);

    }


    [TestMethod]
    public void SearchByExactIngredients_return1Recipe()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        Ingredient ingredient2 = new Ingredient("butter", "food", 2, 2, 2, 2, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        RecipeIngredient recipeIngredient2 = new RecipeIngredient();
        recipeIngredient2.Ingredient = ingredient2;
        recipeIngredient2.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        List<RecipeIngredient> listIngredients2 = new List<RecipeIngredient> { recipeIngredient, recipeIngredient2 };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 5, listIngredients2);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags, 1, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        List<string> ingredientsToSearch = new List<string> { "butter", "tomato" };
        List<Recipe> results = service.SearchByExactIngredients(ingredientsToSearch);

        //Assert
        Assert.AreEqual(1, results.Count);

    }

    [TestMethod]
    public void SearchByExactIngredients_return0Recipe()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        Ingredient ingredient2 = new Ingredient("butter", "food", 2, 2, 2, 2, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        RecipeIngredient recipeIngredient2 = new RecipeIngredient();
        recipeIngredient2.Ingredient = ingredient2;
        recipeIngredient2.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        List<RecipeIngredient> listIngredients2 = new List<RecipeIngredient> { recipeIngredient, recipeIngredient2 };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients2);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 5, listIngredients2);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags, 1, listIngredients2);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        List<string> ingredientsToSearch = new List<string> { "tomato" };
        //recipes that contain only the exact ingredient only
        List<Recipe> results = service.SearchByExactIngredients(ingredientsToSearch);

        //Assert
        Assert.AreEqual(0, results.Count);

    }


    [TestMethod]
    public void SearchByRating_return1Recipes_4starsOrAbove()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 5, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags, 1, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        service.RateRecipeDB("Thiha", 4, recipe);
        service.RateRecipeDB("Kenny", 2, recipe2);
        service.RateRecipeDB("Youri", 3, recipe3);

        //Act
        List<Recipe> results = service.SearchByRating(4);

        //Assert
        Assert.AreEqual(1, results.Count);
        Assert.AreEqual("cake", results[0].Name);
    }


    [TestMethod]
    public void SearchByRating_return0Recipes_5starsOrAbove()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 5, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags, 1, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        service.RateRecipeDB("Thiha", 4, recipe);
        service.RateRecipeDB("Kenny", 2, recipe2);
        service.RateRecipeDB("Youri", 3, recipe3);

        //Act
        List<Recipe> results = service.SearchByRating(5);

        //Assert
        Assert.AreEqual(0, results.Count);
    }


    [TestMethod]
    public void DeleteRecipeDB_deleteExistingRecipe()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 5, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags, 1, listIngredients);

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { recipe, recipe2, recipe3 }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);

        var service = new RecipeController(mockContext.Object);

        //Act
        service.DeleteRecipeDB(recipe3);

        //Assert
        mockSet.Verify(r => r.Remove(It.IsAny<Recipe>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [TestMethod]
    public void SortByCriteria_testSortByName()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 5, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags, 1, listIngredients);

        List<Recipe> recipes = new List<Recipe> { recipe, recipe3, recipe2 };
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);

        //Act
        List<Recipe> sortedRecipes = service.SortByCriteria(recipes, "name");

        //Assert
        Assert.AreEqual("cake", sortedRecipes[0].Name);
        Assert.AreEqual("cakev2", sortedRecipes[1].Name);
        Assert.AreEqual("smoothie", sortedRecipes[2].Name);

    }

    [TestMethod]
    public void SortByCriteria_testSortByOwner()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 5, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags, 1, listIngredients);

        List<Recipe> recipes = new List<Recipe> { recipe, recipe2, recipe3 };
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);

        //Act
        List<Recipe> sortedRecipes = service.SortByCriteria(recipes, "owner");

        //Assert
        Assert.AreEqual("cakev2", sortedRecipes[0].Name);
        Assert.AreEqual("cake", sortedRecipes[1].Name);
        Assert.AreEqual("smoothie", sortedRecipes[2].Name);

    }


    [TestMethod]
    public void SortByCriteria_testSortByServings()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 5, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags, 1, listIngredients);

        List<Recipe> recipes = new List<Recipe> { recipe, recipe2, recipe3 };
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);

        //Act
        List<Recipe> sortedRecipes = service.SortByCriteria(recipes, "serving");

        //Assert
        Assert.AreEqual("smoothie", sortedRecipes[0].Name);
        Assert.AreEqual("cake", sortedRecipes[1].Name);
        Assert.AreEqual("cakev2", sortedRecipes[2].Name);

    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SortByCriteria_InvalidCriteria()
    {
        //Arrange
        Member member = new Member("Tommy", "123456");
        Member member2 = new Member("Danny", "123456");
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };
        List<Tag> tags = new List<Tag> { new Tag("Healthy"), new Tag("Easy") };
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };
        Recipe recipe = new Recipe("cake", member, "cake recipe", 10, 10, instructions, tags, 3, listIngredients);
        Recipe recipe2 = new Recipe("cakev2", member2, "cake version 2 recipe", 20, 10, instructions, tags, 5, listIngredients);
        Recipe recipe3 = new Recipe("smoothie", member, "smoothie recipe", 25, 10, instructions, tags, 1, listIngredients);

        List<Recipe> recipes = new List<Recipe> { recipe, recipe2, recipe3 };
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);

        //Act
        List<Recipe> sortedRecipes = service.SortByCriteria(recipes, "INVALID CRITERIA");

    }

    //Null recipe tests

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void UpdateRecipeName_NullRecipe()
    {
        //Arrange
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);

        //Act
        service.UpdateRecipeNameDB("no update", null);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void UpdateRecipeDescriptionDB_NullRecipe()
    {
        //Arrange
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);

        //Act
        service.UpdateRecipeDescriptionDB("no update", null);

    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void UpdatePreparationTimeDB_NullRecipe()
    {
        //Arrange
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);

        //Act
        service.UpdatePreparationTimeDB(10, null);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void UpdateRecipeInstructionsDB_NullRecipe()
    {
        //Arrange
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);
        List<Instruction> instructions = new List<Instruction> { new Instruction("Step 1"), new Instruction("Step 2") };

        //Act
        service.UpdateRecipeInstructionsDB(instructions, null);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void UpdateRecipeTagsDB_NullRecipe()
    {
        //Arrange
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);
        List<Tag> tags = new List<Tag> { new Tag("Fresh"), new Tag("Easy") };

        //Act
        service.UpdateRecipeTagsDB(tags, null);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void UpdateRecipeServingDB_NullRecipe()
    {
        //Arrange
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);

        //Act
        service.UpdateRecipeServingDB(5, null);

    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void UpdateRecipeIngredientsDB_NullRecipe()
    {
        //Arrange
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient = new Ingredient("tomato", "food", 33, 33, 33, 10, 1, unit);
        RecipeIngredient recipeIngredient = new RecipeIngredient();
        recipeIngredient.Ingredient = ingredient;
        recipeIngredient.Quantity = 1;
        List<RecipeIngredient> listIngredients = new List<RecipeIngredient> { recipeIngredient };

        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);

        //Act
        service.UpdateRecipeIngredientsDB(listIngredients, null);

    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void DeleteRecipeDB_NullRecipe()
    {
        //Arrange
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);

        //Act
        service.DeleteRecipeDB(null);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void RateRecipeDB_NullRecipe()
    {
        //Arrange
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);

        //Act
        service.RateRecipeDB("Tommy", 3, null);

    }


    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void DeleteRatingRecipeDB_NullRecipe()
    {
        //Arrange
        var mockContext = new Mock<RecipeContext>();
        var mockSet = new Mock<DbSet<Recipe>>();
        var recipeData = new List<Recipe> { }.AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Provider).Returns(recipeData.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.Expression).Returns(recipeData.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.ElementType).Returns(recipeData.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(r => r.GetEnumerator()).Returns(recipeData.GetEnumerator());

        mockContext.Setup(r => r.Recipes).Returns(mockSet.Object);
        var service = new RecipeController(mockContext.Object);

        //Act
        service.DeleteRatingRecipeDB("Tommy", null);

    }

}