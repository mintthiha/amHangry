namespace Test;
using UserNameSpace;
[TestClass]
public class RecipeTests
{


    [TestMethod]
    public void TestRatingsAverage()
    {
        //test getter
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 10, 10, 10, 10, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };
        //act
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", 10, 10, instructions, tags, 1, ingredientsList);
        applepie.RateRecipe("Tommy", 5);
        applepie.RateRecipe("Thiha", 1);

        //assert
        Assert.AreEqual(3, applepie.Ratings);
    }

    [TestMethod]
    public void TestRatingsAverage_withUpdatedRating()
    {
        //test getter
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 10, 10, 10, 10, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };

        //act
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", 10, 10, instructions, tags, 1, ingredientsList);
        applepie.RateRecipe("Tommy", 5);
        applepie.RateRecipe("Thiha", 1);
        applepie.RateRecipe("Thiha", 4);

        //assert
        Assert.AreEqual(4.5, applepie.Ratings);
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestRatingsExceptionOverTheRatingLimit()
    {
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 221, 133, 1000, 1000, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };

        //act
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", 10, 10, instructions, tags, 1, ingredientsList);
        applepie.RateRecipe("Tommy", 7);
        //assert
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestRatingsExceptionWithSelfRating()
    {
        //test setter
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 221, 133, 1000, 1000, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };

        //act
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", 10, 10, instructions, tags, 1, ingredientsList);
        applepie.RateRecipe(Danny.Username, 5);
        //assert
    }


    [TestMethod]
    public void TestRecipeConstructor()
    {
        //Test the constructor here.
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 10, 10, 10, 10, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };

        //act
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", 10, 10, instructions, tags, 1, ingredientsList);
    }

    [TestMethod]
    public void ClearRatingList_clear()
    {
        //Arrange
        List<string> instructions = new List<string> { "put in the oven" };
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 10, 10, 10, 10, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> Instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", 10, 10, Instructions, tags, 1, ingredientsList);
        applepie.RateRecipe("Tommy", 4);
        applepie.RateRecipe("Danny", 3);
        applepie.RateRecipe("Thiha", 5);
        applepie.RateRecipe("Youri", 2);

        //act
        applepie.ClearRatingList();

        //Assert
        Assert.AreEqual(1, applepie.Ratings);


    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExceptionNameForRecipe()
    {
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 221, 133, 1000, 1000, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };


        //act
        Recipe applepie = new Recipe("", Danny, "A apple pie", 10, 10, instructions, tags, 1, ingredientsList);

    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExceptionEmptyDescription()
    {
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 221, 133, 1000, 1000, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };


        //act
        Recipe applepie = new Recipe("applepie", Danny, "", 10, 10, instructions, tags, 1, ingredientsList);

    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExceptionInvalidPrepTime()
    {
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 221, 133, 1000, 1000, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };
        //act
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", -1, 10, instructions, tags, 1, ingredientsList);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExceptionInvalidCookingTime()
    {
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 221, 133, 1000, 1000, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };
        //act
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", 10, -1, instructions, tags, 1, ingredientsList);


    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExceptionEmptyInstruction()
    {
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 221, 133, 1000, 1000, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = null;
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };

        //act
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", 10, 10, instructions, tags, 1, ingredientsList);


    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExceptionInstructionTooShort()
    {
        //test setter
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 221, 133, 1000, 1000, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = new List<Instruction> { new Instruction("a") };
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };

        //act
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", 10, 10, instructions, tags, 1, ingredientsList);


    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExceptionTagsInvalid()
    {
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 221, 133, 1000, 1000, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = null;

        //act
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", 10, 10, instructions, tags, 1, ingredientsList);


    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExceptionTagsTooLong()
    {
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 221, 133, 1000, 1000, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = new List<Tag> { new Tag("desertddddddddddddddddddddddddddddddddddddddddddddddddddddddddfdfdsafsdaf"), new Tag("hot") };

        //act
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", 10, 10, instructions, tags, 1, ingredientsList);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TestExceptionEmptyIngredientsList()
    {
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = null;
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 10, 10, 10, 10, 50, unit);

        List<Instruction> instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };

        //act
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", 10, 10, instructions, tags, 1, ingredientsList);


    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExceptionServingsInvalid()
    {
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 221, 133, 1000, 1000, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };

        //act
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", 10, 10, instructions, tags, -1, ingredientsList);


    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExceptionRatingTooLow()
    {
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 221, 133, 1000, 1000, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };

        //act
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", 10, 10, instructions, tags, 1, ingredientsList);
        applepie.RateRecipe("Tommy", -1);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestExceptionRatingByCreator()
    {
        //arrange
        Member Danny = new Member("danny", "no password");
        List<RecipeIngredient> ingredientsList = new List<RecipeIngredient>();
        UnitEntity unit = new UnitEntity(Unit.Gram);
        Ingredient apple = new Ingredient("apple", "Fruit", 221, 133, 1000, 1000, 50, unit);
        RecipeIngredient applerecipe = new RecipeIngredient();
        applerecipe.Ingredient = apple;
        applerecipe.Quantity = 1;
        ingredientsList.Add(applerecipe);
        List<Instruction> instructions = new List<Instruction> { new Instruction("put in the oven") };
        List<Tag> tags = new List<Tag> { new Tag("desert"), new Tag("hot") };

        //act
        Recipe applepie = new Recipe("applepie", Danny, "A apple pie", 10, 10, instructions, tags, 1, ingredientsList);
        applepie.RateRecipe("danny", 1);

    }



}