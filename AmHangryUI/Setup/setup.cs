// using UserNameSpace;

// public class Setup
// {
//     public static void Main(string[] args)
//     {
//         Console.WriteLine("Hello!");
//         RecipeContext rc = new RecipeContext();
//         MemberController memberController = new MemberController(rc);
//         AdminController adminController = new AdminController(rc);
//         IngredientController ingredientController = new IngredientController(rc);
//         RecipeController recipeController = new RecipeController(rc);


//         List<UnitEntity> unitEntities = new List<UnitEntity>{
//             new UnitEntity(Unit.Gram),
//             new UnitEntity(Unit.Kilogram),
//             new UnitEntity(Unit.Milliliter),
//             new UnitEntity(Unit.Liter),
//         };

//         foreach(UnitEntity singleUnit in unitEntities){
//             ingredientController.CreateUnit(singleUnit);
//         }

//         UnitEntity a = new UnitEntity();

//         UnitEntity? gramUnit = rc.UnitEntities 
//         .SingleOrDefault(u => u.Unit == Unit.Gram);

//         UnitEntity? kilogramUnit = rc.UnitEntities 
//         .SingleOrDefault(u => u.Unit == Unit.Kilogram);

//         UnitEntity? milliliterUnit = rc.UnitEntities 
//         .SingleOrDefault(u => u.Unit == Unit.Milliliter);

//         UnitEntity? literUnit = rc.UnitEntities 
//         .SingleOrDefault(u => u.Unit == Unit.Liter);


//         List<Ingredient> dbingredients = new List<Ingredient>{
//             new Ingredient("Flour", "Grains", 13, 1, 95, 2.00, 100, gramUnit),
//             new Ingredient("Egg", "Dairy", 6, 5, 1, 0.30, 50, gramUnit),
//             new Ingredient("Tomato", "Vegetables", 1, 0, 5, 0.50, 100, gramUnit),
//             new Ingredient("Almond", "Nuts", 21, 50, 22, 7.50, 100, gramUnit),
//             new Ingredient("Broccoli", "Vegetables", 3, 0, 7, 2, 500, gramUnit),
//             new Ingredient("Beef", "Meat", 26, 20, 0, 4.00, 300, gramUnit),
//             new Ingredient("Banana", "Fruits", 1, 0, 23, 0.50, 120, gramUnit),
//             new Ingredient("Milk", "Dairy", 4, 4, 5, 5, 1000, milliliterUnit),
//             new Ingredient("Chicken Breast", "Meat", 31, 4, 0, 1.50, 100, gramUnit),
//             new Ingredient("Rice", "Grains", 3, 1, 28, 0.50, 100, gramUnit),
//             new Ingredient("Spinach", "Vegetables", 2, 0, 4, 4.22, 100, gramUnit),
//             new Ingredient("Water", "Beverages", 0, 0, 0, 1.50, 100, literUnit),
//             new Ingredient("Melon", "Fruits", 24, 6, 190, 4.50, 1, kilogramUnit)
//         };
//         foreach(Ingredient ingredient in dbingredients){
//             ingredientController.CreateIngredient(ingredient);
//         }

//         memberController.CreateMember("member1", "passwd1");
//         memberController.CreateMember("member2", "passwd2");
//         Member member1 = memberController.GetMember("member1");
//         Member member2 = memberController.GetMember("member2");
//         adminController.CreateAdmin("admin1", "passwd1");
//         adminController.CreateAdmin("admin2", "passwd2");

//         recipeController.CreateRecipeDB("Rice And Chicken!", 
//                                         member1, 
//                                         "Delicious Meal With Family",
//                                         25,
//                                         30,
//                                         new List<Instruction>{new Instruction("Step 1: Cook Rice"), new Instruction("Step 2: Cook Chicken")},
//                                         new List<Tag>{new Tag("Easy"), new Tag("Delicious")},
//                                         12,
//                                         new List<RecipeIngredient>{new RecipeIngredient{
//                                             Ingredient = ingredientController.SearchByExactName("Rice"),
//                                             Quantity = 200
//                                         }, new RecipeIngredient{
//                                             Ingredient = ingredientController.SearchByExactName("Chicken Breast"),
//                                             Quantity = 200
//                                         }});

//         recipeController.CreateRecipeDB("Tomato Soup",
//                                         member2,
//                                         "Delicious Soup",
//                                         10,
//                                         30,
//                                         new List<Instruction>{new Instruction("Step 1: Cook Tomato"), new Instruction("Step 2: Cook Soup")},
//                                         new List<Tag>{new Tag("Warm"), new Tag("Tasty")},
//                                         2,
//                                         new List<RecipeIngredient>{new RecipeIngredient{
//                                             Ingredient = ingredientController.SearchByExactName("Tomato"),
//                                             Quantity = 250
//                                         }, new RecipeIngredient{
//                                             Ingredient = ingredientController.SearchByExactName("Water"),
//                                             Quantity = 400
//                                         }});

//         recipeController.CreateRecipeDB("Fruit Salad",
//                                         member1,
//                                         "Fruit Salad for everyone!",
//                                         5,
//                                         2,
//                                         new List<Instruction>{new Instruction("Step 1: Cut Fruit"), new Instruction("Step 2: Mix!")},
//                                         new List<Tag>{new Tag("Healthy"), new Tag("Cold")},
//                                         1,
//                                         new List<RecipeIngredient>{new RecipeIngredient{
//                                             Ingredient = ingredientController.SearchByExactName("Melon"),
//                                             Quantity = 6
//                                         }, new RecipeIngredient{
//                                             Ingredient = ingredientController.SearchByExactName("Tomato"),
//                                             Quantity = 375
//                                         }});
//     }
// }
