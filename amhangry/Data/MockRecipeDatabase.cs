using UserNameSpace;
//Mock database for recipes:

public class MockRecipeDatabase : IDataLoader<Recipe>
{
    public List<Recipe> RecipeList { get; set; }

    public List<Recipe> LoadFromDatabase()
    {
        return RecipeList;
    }
    public MockRecipeDatabase()
    {
        RecipeList = new List<Recipe>();
        FillRecipe();
    }
    public void FillRecipe()
    {
        // Create some dummy data for users
        Member user1 = new Member("John", "aaaaaa");
        Member user2 = new Member("Alice", "bbbbbb");
        Member user3 = new Member("Bobby", "cccccc");

        // Create some dummy data for ingredients
        Ingredient ingredient1 = new Ingredient("Flour", "Grains", 5, 2, 0, 20, 200, new UnitEntity(Unit.Gram));
        Ingredient ingredient2 = new Ingredient("Eggs", "Dairy", 6, 5, 1, 3, 100, new UnitEntity(Unit.Gram));
        Ingredient ingredient3 = new Ingredient("Tomatoes", "Vegetables", 1, 0, 2, 3, 150, new UnitEntity(Unit.Gram));
        Ingredient ingredient4 = new Ingredient("Chicken Breast", "Meat", 25, 3, 0, 10, 150, new UnitEntity(Unit.Gram));
        Ingredient ingredient5 = new Ingredient("Rice", "Grains", 4, 1, 0, 20, 150, new UnitEntity(Unit.Gram));
        Ingredient ingredient6 = new Ingredient("Spinach", "Vegetables", 2, 0, 1, 2, 100, new UnitEntity(Unit.Gram));
        Ingredient ingredient7 = new Ingredient("Milk", "Dairy", 3, 2, 4, 5, 200, new UnitEntity(Unit.Gram));

        // Create some dummy data for RecipeIngredients 
        RecipeIngredient recipeIngredient1 = new RecipeIngredient();
        recipeIngredient1.Ingredient = ingredient1;
        recipeIngredient1.Quantity = 200;

        RecipeIngredient recipeIngredient2 = new RecipeIngredient();
        recipeIngredient2.Ingredient = ingredient2;
        recipeIngredient2.Quantity = 100;

        RecipeIngredient recipeIngredient3 = new RecipeIngredient();
        recipeIngredient3.Ingredient = ingredient3;
        recipeIngredient3.Quantity = 150;

        RecipeIngredient recipeIngredient4 = new RecipeIngredient();
        recipeIngredient4.Ingredient = ingredient4;
        recipeIngredient4.Quantity = 150;

        RecipeIngredient recipeIngredient5 = new RecipeIngredient();
        recipeIngredient5.Ingredient = ingredient5;
        recipeIngredient5.Quantity = 150;

        RecipeIngredient recipeIngredient6 = new RecipeIngredient();
        recipeIngredient6.Ingredient = ingredient6;
        recipeIngredient6.Quantity = 100;

        RecipeIngredient recipeIngredient7 = new RecipeIngredient();
        recipeIngredient7.Ingredient = ingredient7;
        recipeIngredient7.Quantity = 200;

        RecipeIngredient recipeIngredient8 = new RecipeIngredient();
        recipeIngredient8.Ingredient = ingredient7;
        recipeIngredient8.Quantity = 200;



        // Create some dummy data for instructions
        List<Instruction> instructions1 = new List<Instruction> { new Instruction("Step 1 flour"), new Instruction("Step 2 grains"), new Instruction("Step 3") };
        List<Instruction> instructions2 = new List<Instruction> { new Instruction("Step 1 flour"), new Instruction("Step 2 grains") };
        List<Instruction> instructions3 = new List<Instruction> { new Instruction("Step 1 cut tomatoes"), new Instruction("Step 2 mix with other ingredients") };
        List<Instruction> instructions4 = new List<Instruction> { new Instruction("Step 1 marinate chicken"), new Instruction("Step 2 grill chicken") };
        List<Instruction> instructions5 = new List<Instruction> { new Instruction("Step 1 cook rice"), new Instruction("Step 2 serve hot") };
        List<Instruction> instructions6 = new List<Instruction> { new Instruction("Step 1 wash spinach"), new Instruction("Step 2 cook spinach") };
        List<Instruction> instructions7 = new List<Instruction> { new Instruction("Step 1 pour milk into a pan"), new Instruction("Step 2 heat until warm") };


        // Create some dummy data for tags
        List<Tag> tags1 = new List<Tag> { new Tag("Breakfast"), new Tag("Easy") };
        List<Tag> tags2 = new List<Tag> { new Tag("Dinner"), new Tag("Healthy") };
        List<Tag> tags3 = new List<Tag> { new Tag("Lunch"), new Tag("Vegetarian") };
        List<Tag> tags4 = new List<Tag> { new Tag("Dinner"), new Tag("Protein") };
        List<Tag> tags5 = new List<Tag> { new Tag("Lunch"), new Tag("Simple") };
        List<Tag> tags6 = new List<Tag> { new Tag("Dinner"), new Tag("Light") };
        List<Tag> tags7 = new List<Tag> { new Tag("Breakfast"), new Tag("Warm") };


        // Add more instances of Recipe
        RecipeList.Add(new Recipe("Pancakes", user1, "Delicious pancakes recipe", 10.0, 20.0, instructions1, tags1, 4, new List<RecipeIngredient> { recipeIngredient1 }));
        RecipeList.Add(new Recipe("Salad", user2, "Healthy salad recipe", 5.0, 0.0, instructions2, tags2, 2, new List<RecipeIngredient> { recipeIngredient2 }));
        RecipeList.Add(new Recipe("Tomato Salad", user3, "Simple tomato chicken salad recipe", 3.0, 0.0, instructions3, tags3, 2, new List<RecipeIngredient> { recipeIngredient3 }));
        RecipeList.Add(new Recipe("Grilled Chicken", user1, "Tasty grilled chicken recipe", 15.0, 30.0, instructions4, tags4, 3, new List<RecipeIngredient> { recipeIngredient4 }));
        RecipeList.Add(new Recipe("Rice Bowl", user2, "Quick rice bowl recipe", 10.0, 15.0, instructions5, tags5, 2, new List<RecipeIngredient> { recipeIngredient5 }));
        RecipeList.Add(new Recipe("Spinach Saute and Rice", user3, "Simple spinach saute recipe", 5.0, 10.0, instructions6, tags6, 2, new List<RecipeIngredient> { recipeIngredient4, recipeIngredient5, recipeIngredient6 }));
        RecipeList.Add(new Recipe("Warm Milk", user1, "Warm milk recipe", 5.0, 0.0, instructions7, tags7, 1, new List<RecipeIngredient> { recipeIngredient7 }));
        RecipeList.Add(new Recipe("Cold Milk", user1, "Cold milk recipe", 5.0, 0.0, instructions7, tags7, 1, new List<RecipeIngredient> { recipeIngredient8 }));
    }

    public void SaveToDatabase()
    {

    }


}
