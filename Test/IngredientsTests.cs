using Microsoft.EntityFrameworkCore;
namespace Test;
[TestClass]
public class IngredientsTests
{
    [TestMethod]
    public void TestIngredientConstructor()
    {
        //Test the ingredient constructor
        Ingredient banana = new Ingredient("banana", "Fruit", 20, 133, 10, 10, 50.0, new UnitEntity(Unit.Gram));

    }

    [TestMethod]
    public void UnitConversion_gramToKg()
    {
        //Arrange
        Ingredient chicken = new Ingredient("Chicken Breast", "Meat", 25, 3, 0, 1, 500, new UnitEntity(Unit.Gram));

        //Act
        double kg = chicken.UnitConversion();

        //Assert
        Assert.AreEqual(0.5, kg);

    }

    [TestMethod]
    public void UnitConversion_KgToGram()
    {
        //Arrange
        Ingredient chicken = new Ingredient("Chicken Breast", "Meat", 25, 3, 0, 1, 0.5, new UnitEntity(Unit.Kilogram));

        //Act
        double grams = chicken.UnitConversion();

        //Assert
        Assert.AreEqual(500, grams);

    }

    [TestMethod]
    public void UnitConversion_mlToLiter()
    {
        //Arrange
        Ingredient milk = new Ingredient("Milk", "Diary", 5, 2, 2, 6, 350, new UnitEntity(Unit.Milliliter));

        //Act
        double liter = milk.UnitConversion();

        //Assert
        Assert.AreEqual(0.35, liter);

    }

    [TestMethod]
    public void UnitConversion_LiterToMl()
    {
        //Arrange
        Ingredient milk = new Ingredient("Milk", "Diary", 5, 2, 2, 6, 1.5, new UnitEntity(Unit.Liter));

        //Act
        double ml = milk.UnitConversion();

        //Assert
        Assert.AreEqual(1500, ml);

    }
}