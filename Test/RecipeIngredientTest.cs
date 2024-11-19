namespace Test;
using UserNameSpace;
[TestClass]
public class RecipeIngredientTest
{
    [TestMethod]
    public void GetTotalCalories_doubleQuantity()
    {
        //Act
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient1 = new Ingredient("Flour", "Grains", 5, 2, 0, 20, 200, unit); //200 grams in db
        RecipeIngredient flour = new RecipeIngredient();
        flour.Quantity = 400;
        flour.Ingredient = ingredient1;

        //Arrange

        int calories = flour.GetCalories();

        int expected = ((5 * 4) + (2 * 9) + (0 * 4)) * 2;

        //Assert
        Assert.AreEqual(expected, calories);
    }

    [TestMethod]
    public void GetTotalCalories_1ThirdIngredient()
    {
        //Act
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient1 = new Ingredient("Chicken Breast", "Meat", 25, 3, 0, 0, 150,unit); //150 grams in db
        RecipeIngredient chicken = new RecipeIngredient();
        chicken.Quantity = 50;
        chicken.Ingredient = ingredient1;

        //Arrange

        int calories = chicken.GetCalories();

        int expected = ((25 * 4) + (3 * 9) + (0 * 4)) / 3;

        //Assert
        Assert.AreEqual(expected, calories);
    }

    [TestMethod]
    public void GetTotalCalories_wholeIngredient()
    {
        //Act
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient1 = new Ingredient("Milk", "Dairy", 3, 2, 4, 5, 200, unit); //200ml

        RecipeIngredient chicken = new RecipeIngredient();
        chicken.Quantity = 200;
        chicken.Ingredient = ingredient1;

        //Arrange

        int calories = chicken.GetCalories();

        int expected = (3 * 4) + (2 * 9) + (4 * 4);

        //Assert
        Assert.AreEqual(expected, calories);
    }

    [TestMethod]
    public void GetTotalProtein_doubleQuantity()
    {
        //Act
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient1 = new Ingredient("Chicken Breast", "Meat", 25, 3, 0, 0, 150, unit); //25 proteins

        RecipeIngredient chicken = new RecipeIngredient();
        chicken.Quantity = 300;
        chicken.Ingredient = ingredient1;

        //Arrange

        int calories = chicken.GetTotalProteins();

        int expected = 25 * 2;

        //Assert
        Assert.AreEqual(expected, calories);
    }

    [TestMethod]
    public void GetTotalProtein_SameQuantity()
    {
        //Act
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient1 = new Ingredient("Chicken Breast", "Meat", 25, 3, 0, 0, 150, unit); //25 proteins

        RecipeIngredient chicken = new RecipeIngredient();
        chicken.Quantity = 150;
        chicken.Ingredient = ingredient1;

        //Arrange

        int calories = chicken.GetTotalProteins();

        int expected = 25;

        //Assert
        Assert.AreEqual(expected, calories);
    }
    [TestMethod]
    public void GetTotalMoney()
    {
        //Act
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient ingredient1 = new Ingredient("Chicken Breast", "Meat", 25, 3, 0, 1, 150, unit); //25 proteins

        RecipeIngredient chicken = new RecipeIngredient();
        chicken.Quantity = 300;
        chicken.Ingredient = ingredient1;

        //Arrange

        double totalCost = chicken.GetTotalPrice();

        int expected = 2;

        //Assert
        Assert.AreEqual(expected, totalCost);
    }

}