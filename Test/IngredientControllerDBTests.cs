using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Moq;
using UserNameSpace;
namespace Test;

[TestClass]
public class IngredientControllerDBTests
{

    //HELPER METHODS

    /// <summary>
    /// Does the setup process of the mockSet based off the QueriedList given
    /// </summary>
    /// <returns>
    /// returns the setup mockSet
    /// </returns>
    private Mock<DbSet<Ingredient>> setUpMockSet(IQueryable<Ingredient> dataSet)
    {
        var mockSet = new Mock<DbSet<Ingredient>>();
        mockSet.As<IQueryable<Ingredient>>().Setup(m => m.Provider).Returns(dataSet.Provider);
        mockSet.As<IQueryable<Ingredient>>().Setup(m => m.Expression).Returns(dataSet.Expression);
        mockSet.As<IQueryable<Ingredient>>().Setup(m => m.ElementType).Returns(dataSet.ElementType);
        mockSet.As<IQueryable<Ingredient>>().Setup(m => m.GetEnumerator()).Returns(dataSet.GetEnumerator());
        return mockSet;
    }
    /// <summary>
    /// Does the setup process of the mockContext based off the mockSet given
    /// </summary>
    /// <returns>
    /// returns the setup mockContext
    /// </returns>
    private Mock<RecipeContext> setUpMockContext(Mock<DbSet<Ingredient>> mockSet)
    {
        var mockContext = new Mock<RecipeContext>();
        mockContext.Setup(m => m.Ingredients).Returns(mockSet.Object);
        return mockContext;
    }

    //Tests
    [TestMethod]
    public void TestCreatingIngredient_AddedToDB_Once_Success()
    {
        //Arrange
        var dataSet = new List<Ingredient>().AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        //Act
        Ingredient ingredient = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram));
        mockService.CreateIngredient(ingredient);

        //Assert
        mockSet.Verify(m => m.Add(It.IsAny<Ingredient>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestCreatingIngredient_FailsWhenIngredientExists()
    {
        // Arrange
        var existingIngredient = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram));
        var dataSet = new List<Ingredient> { existingIngredient }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        // Act
        Ingredient newIngredient = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram));

        mockService.CreateIngredient(newIngredient);
    }

    [TestMethod]
    public void TestGetAllIngredients_ReturnsEmptyList()
    {
        var dataSet = new List<Ingredient> { }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        //Act
        List<Ingredient> ingredients = mockService.GetAllIngredientsDB();

        //Assert
        Assert.IsNotNull(ingredients);
    }

    [TestMethod]
    public void TestGetAllIngredients_ReturnsList()
    {
        var dataSet = new List<Ingredient>{
            new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram)),
            new Ingredient("pear","fruit",2,3,4,4.50,3, new UnitEntity(Unit.Kilogram)),
        }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        //Act
        List<Ingredient> ingredients = mockService.GetAllIngredientsDB();

        //Assert
        Assert.AreEqual("apple", ingredients[0].Name);
        Assert.AreEqual("pear", ingredients[1].Name);
    }
    [TestMethod]
    public void CreateUnitDB_UnitDoesNotExist_ShouldAddUnitToDatabase()
    {
        // Arrange
        var dataSet = new List<UnitEntity>().AsQueryable();
        var mockSet = new Mock<DbSet<UnitEntity>>();
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.Provider).Returns(dataSet.Provider);
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.Expression).Returns(dataSet.Expression);
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.ElementType).Returns(dataSet.ElementType);
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.GetEnumerator()).Returns(dataSet.GetEnumerator());
        var mockContext = new Mock<RecipeContext>();
        mockContext.Setup(m => m.UnitEntities).Returns(mockSet.Object);
        var mockService = new IngredientController(mockContext.Object);


        // Act
        var unitToAdd = new UnitEntity(Unit.Gram);
        mockService.CreateUnit(unitToAdd);

        // Assert
        mockSet.Verify(m => m.Add(It.IsAny<UnitEntity>()), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateUnitDB_UnitExists_ShouldThrowArgumentException()
    {
        // Arrange
        var existingUnit = new UnitEntity(Unit.Gram);
        var dataSet = new List<UnitEntity> { existingUnit }.AsQueryable();
        var mockSet = new Mock<DbSet<UnitEntity>>();
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.Provider).Returns(dataSet.Provider);
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.Expression).Returns(dataSet.Expression);
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.ElementType).Returns(dataSet.ElementType);
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.GetEnumerator()).Returns(dataSet.GetEnumerator());
        var mockContext = new Mock<RecipeContext>();
        mockContext.Setup(m => m.UnitEntities).Returns(mockSet.Object);
        var mockService = new IngredientController(mockContext.Object);

        // Act and Assert
        var unitToAdd = new UnitEntity(Unit.Gram);
        mockService.CreateUnit(unitToAdd);
    }

    [TestMethod]
    public void DeleteIngredientDb_IngredientExists_DeleteIngredientFromDatabase()
    {
        // Arrange
        var existingIngredient = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram));
        var dataSet = new List<Ingredient> { existingIngredient }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);
        // Act
        mockService.DeleteIngredient(existingIngredient);

        // Assert
        mockSet.Verify(m => m.Remove(existingIngredient), Times.Once());
        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void DeleteIngredientDb_IngredientDoesNotExist_ThrowArgumentException()
    {
        // Arrange
        var nonExistingIngredient = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram));
        var dataSet = new List<Ingredient>().AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        // Act and Assert
        mockService.DeleteIngredient(nonExistingIngredient);
    }

    [TestMethod]
    public void TestUpdateIngredientProteinDb_Success()
    {

        //Arrange
        Ingredient ingredientToUpdate = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram));
        var dataSet = new List<Ingredient> { ingredientToUpdate }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);
        //Act
        mockService.UpdateIngredientProtein(10, ingredientToUpdate);

        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Exactly(1));
        var dbIngredient = mockContext.Object.Ingredients.Where(i => i.Protein == 10).FirstOrDefault();
        Assert.IsNotNull(dbIngredient);
    }
    [ExpectedException(typeof(ArgumentException))]
    [TestMethod]
    public void TestUpdateIngredientProteinDb_Failure_NoNegativeNumbers_NotAddedToDB()
    {

        //Arrange
        Ingredient ingredientToUpdate = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram));
        var dataSet = new List<Ingredient> { ingredientToUpdate }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);
        //Act
        mockService.UpdateIngredientProtein(-1, ingredientToUpdate);

        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Exactly(0));
        var dbIngredient = mockContext.Object.Ingredients.Where(i => i.Protein == -1).FirstOrDefault();
        Assert.IsNull(dbIngredient);
    }
    [TestMethod]
    public void TestUpdateIngredientFatDb_Success()
    {

        //Arrange
        Ingredient ingredientToUpdate = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram));
        var dataSet = new List<Ingredient> { ingredientToUpdate }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);
        //Act
        mockService.UpdateIngredientFat(10, ingredientToUpdate);

        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Exactly(1));
        var dbIngredient = mockContext.Object.Ingredients.Where(i => i.Fat == 10).FirstOrDefault();
        Assert.IsNotNull(dbIngredient);
    }
    [ExpectedException(typeof(ArgumentException))]
    [TestMethod]
    public void TestUpdateIngredientFatDb_Fails()
    {

        //Arrange
        Ingredient ingredientToUpdate = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram));
        var dataSet = new List<Ingredient> { ingredientToUpdate }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);
        //Act
        mockService.UpdateIngredientFat(201, ingredientToUpdate);

        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Exactly(0));
        var dbIngredient = mockContext.Object.Ingredients.Where(i => i.Fat == 201).FirstOrDefault();
        Assert.IsNull(dbIngredient);
    }
    [TestMethod]
    public void TestUpdateIngredientCarbsDb_Success()
    {

        //Arrange
        Ingredient ingredientToUpdate = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram));
        var dataSet = new List<Ingredient> { ingredientToUpdate }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);
        //Act
        mockService.UpdateIngredientCarbs(10, ingredientToUpdate);

        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Exactly(1));
        var dbIngredient = mockContext.Object.Ingredients.Where(i => i.Carbs == 10).FirstOrDefault();
        Assert.IsNotNull(dbIngredient);
    }
    [TestMethod]
    public void TestUpdateIngredientCostDb_Success()
    {

        //Arrange
        Ingredient ingredientToUpdate = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram));
        var dataSet = new List<Ingredient> { ingredientToUpdate }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);
        //Act
        mockService.UpdateIngredientCost(10, ingredientToUpdate);

        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Exactly(1));
        var dbIngredient = mockContext.Object.Ingredients.Where(i => i.Cost == 10).FirstOrDefault();
        Assert.IsNotNull(dbIngredient);
    }
    [TestMethod]
    public void TestUpdateIngredientAmountDb_Success()
    {

        //Arrange
        Ingredient ingredientToUpdate = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram));
        var dataSet = new List<Ingredient> { ingredientToUpdate }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);
        //Act
        mockService.UpdateIngredientAmount(10, ingredientToUpdate);

        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Exactly(1));
        var dbIngredient = mockContext.Object.Ingredients.Where(i => i.Amount == 10).FirstOrDefault();
        Assert.IsNotNull(dbIngredient);
    }
    [TestMethod]
    public void TestUpdateIngredientCategoryDb_Success()
    {

        //Arrange
        Ingredient ingredientToUpdate = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram));
        var dataSet = new List<Ingredient> { ingredientToUpdate }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);
        //Act
        mockService.UpdateIngredientCategory("Meat", ingredientToUpdate);

        //Assert

        mockContext.Verify(m => m.SaveChanges(), Times.Exactly(1));
        var dbIngredient = mockContext.Object.Ingredients.Where(i => i.Category.Equals("meat", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        Assert.IsNotNull(dbIngredient);
    }
    [TestMethod]
    public void TestUpdateIngredientNameDb_Success()
    {

        //Arrange
        Ingredient ingredientToUpdate = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram));
        var dataSet = new List<Ingredient> { ingredientToUpdate }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        //Act
        mockService.UpdateCurrentIngredientName("pear", ingredientToUpdate);

        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Exactly(1));
        var dbIngredient = mockContext.Object.Ingredients.Where(i => i.Name.Equals("pear")).FirstOrDefault();
        Assert.IsNotNull(dbIngredient);
    }


    [ExpectedException(typeof(ArgumentException))   ]
    [TestMethod]
    public void TestUpdateIngredientNameDb_Fails()
    {

        //Arrange
        Ingredient ingredientToUpdate = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, new UnitEntity(Unit.Gram));
        var dataSet = new List<Ingredient> { ingredientToUpdate }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        //Act
        mockService.UpdateCurrentIngredientName("pearwithalotofpearitisbluebrownblackandveryveryjuicypear", ingredientToUpdate);

        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Exactly(0));
        var dbIngredient = mockContext.Object.Ingredients.Where(i => i.Name.Equals("pearwithalotofpearitisbluebrownblackandveryveryjuicypear")).FirstOrDefault();
        Assert.IsNull(dbIngredient);
    }
    [TestMethod]
    public void TestUpdateIngredientUnitDb_Success()
    {

        //Arrange
        var dataSetUnit = new List<UnitEntity>{
            new UnitEntity(Unit.Gram),
            new UnitEntity(Unit.Kilogram)
        }.AsQueryable();
        var mockSetUnit = new Mock<DbSet<UnitEntity>>();
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.Provider).Returns(dataSetUnit.Provider);
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.Expression).Returns(dataSetUnit.Expression);
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.ElementType).Returns(dataSetUnit.ElementType);
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.GetEnumerator()).Returns(dataSetUnit.GetEnumerator());

        var mockContextUnit = new Mock<RecipeContext>();
        mockContextUnit.Setup(m => m.UnitEntities).Returns(mockSetUnit.Object);
        var mockServiceUnit = new IngredientController(mockContextUnit.Object);

        Ingredient ingredientToUpdate = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, mockServiceUnit.ReturnExistingEntitiyUnit(Unit.Gram));
        var dataSet = new List<Ingredient> { ingredientToUpdate }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);
        //Act
        mockService.UpdateIngredientUnit(mockServiceUnit.ReturnExistingEntitiyUnit(Unit.Kilogram), ingredientToUpdate);

        //Assert
        mockContext.Verify(m => m.SaveChanges(), Times.Exactly(1));
        var dbIngredient = mockContext.Object.Ingredients.Where(i => i.UnitEntity.Unit == new UnitEntity(Unit.Kilogram).Unit).FirstOrDefault();
        Assert.IsNotNull(dbIngredient);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestUpdateIngredientUnitDb_Fails()
    {

        //Arrange
        var dataSetUnit = new List<UnitEntity>{
            new UnitEntity(Unit.Gram)
        }.AsQueryable();
        var mockSetUnit = new Mock<DbSet<UnitEntity>>();
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.Provider).Returns(dataSetUnit.Provider);
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.Expression).Returns(dataSetUnit.Expression);
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.ElementType).Returns(dataSetUnit.ElementType);
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.GetEnumerator()).Returns(dataSetUnit.GetEnumerator());

        var mockContextUnit = new Mock<RecipeContext>();
        mockContextUnit.Setup(m => m.UnitEntities).Returns(mockSetUnit.Object);
        var mockServiceUnit = new IngredientController(mockContextUnit.Object);

        Ingredient ingredientToUpdate = new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 2, mockServiceUnit.ReturnExistingEntitiyUnit(Unit.Gram));
        var dataSet = new List<Ingredient> { ingredientToUpdate }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);
        //Act
        mockService.UpdateIngredientUnit(mockServiceUnit.ReturnExistingEntitiyUnit(Unit.Kilogram), ingredientToUpdate);
    }
    [TestMethod]
    public void TestSearchbyAmountDb_Returns2Items_UnderOrEqual100()
    {

        //Arrange
        var dataSet = new List<Ingredient>
        {
            new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 50, new UnitEntity(Unit.Gram)),
            new Ingredient("pear", "fruit", 1, 2, 3, 4.50, 100, new UnitEntity(Unit.Gram)),
            new Ingredient("orange", "fruit", 1, 2, 3, 4.50, 200, new UnitEntity(Unit.Gram)),
        }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        //Act
        var result = mockService.SearchByAmount(100);

        //Assert
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.Any(i => i.Name.Equals("apple")));
        Assert.IsTrue(result.Any(i => i.Name.Equals("pear")));
    }
    [TestMethod]
    public void TestSearchbyAmountDb_ReturnsNothing()
    {

        //Arrange
        var dataSet = new List<Ingredient>
        {
            new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 50, new UnitEntity(Unit.Gram)),
            new Ingredient("pear", "fruit", 1, 2, 3, 4.50, 100, new UnitEntity(Unit.Gram)),
            new Ingredient("orange", "fruit", 1, 2, 3, 4.50, 200, new UnitEntity(Unit.Gram)),
        }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        //Act
        var result = mockService.SearchByAmount(30);

        //Assert
        Assert.AreEqual(0, result.Count);
    }

    [TestMethod]
    public void TestSearchByCostDb_Returns2Items_UnderOrEqual10()
    {
        // Arrange
        var dataSet = new List<Ingredient>
        {
            new Ingredient("apple", "fruit", 1, 2, 3, 4.50, 50, new UnitEntity(Unit.Gram)),
            new Ingredient("pear", "fruit", 1, 2, 3, 10, 100, new UnitEntity(Unit.Gram)),
            new Ingredient("orange", "fruit", 1, 2, 3, 20, 200, new UnitEntity(Unit.Gram)),
        }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        // Act
        var result = mockService.SearchByCost(10);

        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.Any(i => i.Name.Equals("apple")));
        Assert.IsTrue(result.Any(i => i.Name.Equals("pear")));
    }
    [TestMethod]
    public void TestSearchByCarbsDb_ReturnsIngredientsWithCarbsUnderOrEqual20()
    {
        // Arrange
        var dataSet = new List<Ingredient>
        {
            new Ingredient("apple", "fruit", 1, 2, 10, 4.50, 10, new UnitEntity(Unit.Gram)),
            new Ingredient("pear", "fruit", 1, 2, 20, 4.50, 10, new UnitEntity(Unit.Gram)),
            new Ingredient("orange", "fruit", 1, 2, 30, 4.50, 10, new UnitEntity(Unit.Gram)),
        }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        // Act
        var result = mockService.SearchByCarbs(20);

        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.Any(i => i.Name.Equals("apple")));
        Assert.IsTrue(result.Any(i => i.Name.Equals("pear")));
    }
    [TestMethod]
    public void TestSearchByFatDb_ReturnsIngredientsWithFatUnderOrEqual10()
    {
        // Arrange
        var dataSet = new List<Ingredient>
        {
            new Ingredient("apple", "fruit", 1, 5, 10, 4.50, 10, new UnitEntity(Unit.Gram)),
            new Ingredient("pear", "fruit", 1, 10, 20, 4.50, 10, new UnitEntity(Unit.Gram)),
            new Ingredient("orange", "fruit", 1, 15, 30, 4.50, 10, new UnitEntity(Unit.Gram)),
        }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        // Act
        var result = mockService.SearchByFat(10);

        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.Any(i => i.Name.Equals("apple")));
        Assert.IsTrue(result.Any(i => i.Name.Equals("pear")));
    }
    [TestMethod]
    public void TestSearchByProteinDb_ReturnsIngredientsWithProteinUnderOrEqual15()
    {
        // Arrange
        var dataSet = new List<Ingredient>
        {
            new Ingredient("apple", "fruit", 10, 2, 10, 4.50, 10, new UnitEntity(Unit.Gram)),
            new Ingredient("pear", "fruit", 15, 2, 20, 4.50, 10, new UnitEntity(Unit.Gram)),
            new Ingredient("orange", "fruit", 20, 2, 30, 4.50, 10, new UnitEntity(Unit.Gram)),
        }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        // Act
        var result = mockService.SearchByProtein(15);

        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.Any(i => i.Name.Equals("apple")));
        Assert.IsTrue(result.Any(i => i.Name.Equals("pear")));
    }

    [TestMethod]
    public void TestSearchByCategoryDb_ReturnsIngredientsByCategoryFruit()
    {
        // Arrange
        var dataSet = new List<Ingredient>
        {
            new Ingredient("apple", "fruit", 1, 2, 10, 4.50, 10, new UnitEntity(Unit.Gram)),
            new Ingredient("pear", "fruit", 1, 2, 20, 4.50, 10, new UnitEntity(Unit.Gram)),
            new Ingredient("orange", "fruit", 1, 2, 30, 4.50, 10, new UnitEntity(Unit.Gram)),
        }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        // Act
        var result = mockService.SearchByCategory("fruit");

        // Assert
        Assert.AreEqual(3, result.Count);
        Assert.IsTrue(result.Any(i => i.Name.Equals("apple")));
        Assert.IsTrue(result.Any(i => i.Name.Equals("pear")));
        Assert.IsTrue(result.Any(i => i.Name.Equals("orange")));
    }
    [TestMethod]
    public void TestReturnExistingEntityUnit_ReturnsGramUnit()
    {

        var dataSet = new List<UnitEntity>{
            new UnitEntity(Unit.Gram)
        }.AsQueryable();
        var mockSet = new Mock<DbSet<UnitEntity>>();
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.Provider).Returns(dataSet.Provider);
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.Expression).Returns(dataSet.Expression);
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.ElementType).Returns(dataSet.ElementType);
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.GetEnumerator()).Returns(dataSet.GetEnumerator());

        var mockContext = new Mock<RecipeContext>();
        mockContext.Setup(m => m.UnitEntities).Returns(mockSet.Object);
        var mockService = new IngredientController(mockContext.Object);

        var unit = mockService.ReturnExistingEntitiyUnit(Unit.Gram);

        Assert.IsNotNull(unit);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestReturnExistingEntityUnit_NoUnit_ThrowsException()
    {

        var dataSet = new List<UnitEntity>().AsQueryable();
        var mockSet = new Mock<DbSet<UnitEntity>>();
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.Provider).Returns(dataSet.Provider);
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.Expression).Returns(dataSet.Expression);
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.ElementType).Returns(dataSet.ElementType);
        mockSet.As<IQueryable<UnitEntity>>().Setup(m => m.GetEnumerator()).Returns(dataSet.GetEnumerator());

        var mockContext = new Mock<RecipeContext>();
        mockContext.Setup(m => m.UnitEntities).Returns(mockSet.Object);
        var mockService = new IngredientController(mockContext.Object);

        var unit = mockService.ReturnExistingEntitiyUnit(Unit.Gram);
    }
    [TestMethod]
    public void TestSearchByUnitDb_ReturnsIngredientsByUnitGram()
    {
        // Arrange
        //First, set up the method to get the same unit entity
        var dataSetUnit = new List<UnitEntity>{
            new UnitEntity(Unit.Gram),
            new UnitEntity(Unit.Kilogram)
        }.AsQueryable();
        var mockSetUnit = new Mock<DbSet<UnitEntity>>();
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.Provider).Returns(dataSetUnit.Provider);
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.Expression).Returns(dataSetUnit.Expression);
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.ElementType).Returns(dataSetUnit.ElementType);
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.GetEnumerator()).Returns(dataSetUnit.GetEnumerator());

        var mockContextUnit = new Mock<RecipeContext>();
        mockContextUnit.Setup(m => m.UnitEntities).Returns(mockSetUnit.Object);
        var mockServiceUnit = new IngredientController(mockContextUnit.Object);

        //Then use that unit to link the ingredients
        var dataSet = new List<Ingredient>
        {
            new Ingredient("apple", "fruit", 1, 2, 10, 4.50, 10, mockServiceUnit.ReturnExistingEntitiyUnit(Unit.Gram)),
            new Ingredient("pear", "fruit", 1, 2, 20, 4.50, 10, mockServiceUnit.ReturnExistingEntitiyUnit(Unit.Kilogram)),
            new Ingredient("orange", "fruit", 1, 2, 30, 4.50, 10, mockServiceUnit.ReturnExistingEntitiyUnit(Unit.Gram)),
        }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        // Act
        var unit = mockServiceUnit.ReturnExistingEntitiyUnit(Unit.Gram);
        var result = mockService.SearchByUnit(unit);

        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.Any(i => i.Name.Equals("apple")));
        Assert.IsTrue(result.Any(i => i.Name.Equals("orange")));
    }

    [TestMethod]
    public void TestSearchByUnitDb_ReturnsNothing()
    {
        // Arrange
        //First, set up the method to get the same unit entity
        var dataSetUnit = new List<UnitEntity>{
            new UnitEntity(Unit.Gram),
            new UnitEntity(Unit.Kilogram)
        }.AsQueryable();
        var mockSetUnit = new Mock<DbSet<UnitEntity>>();
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.Provider).Returns(dataSetUnit.Provider);
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.Expression).Returns(dataSetUnit.Expression);
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.ElementType).Returns(dataSetUnit.ElementType);
        mockSetUnit.As<IQueryable<UnitEntity>>().Setup(m => m.GetEnumerator()).Returns(dataSetUnit.GetEnumerator());

        var mockContextUnit = new Mock<RecipeContext>();
        mockContextUnit.Setup(m => m.UnitEntities).Returns(mockSetUnit.Object);
        var mockServiceUnit = new IngredientController(mockContextUnit.Object);

        //Then use that unit to link the ingredients
        var dataSet = new List<Ingredient>
        {
            new Ingredient("apple", "fruit", 1, 2, 10, 4.50, 10, mockServiceUnit.ReturnExistingEntitiyUnit(Unit.Gram)),
            new Ingredient("pear", "fruit", 1, 2, 20, 4.50, 10, mockServiceUnit.ReturnExistingEntitiyUnit(Unit.Gram)),
            new Ingredient("orange", "fruit", 1, 2, 30, 4.50, 10, mockServiceUnit.ReturnExistingEntitiyUnit(Unit.Gram)),
        }.AsQueryable();
        var mockSet = setUpMockSet(dataSet);
        var mockContext = setUpMockContext(mockSet);
        var mockService = new IngredientController(mockContext.Object);

        // Act
        var unit = mockServiceUnit.ReturnExistingEntitiyUnit(Unit.Kilogram);
        var result = mockService.SearchByUnit(unit);

        // Assert
        Assert.AreEqual(0, result.Count);
    }
}