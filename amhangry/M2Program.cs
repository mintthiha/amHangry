// // using System.Diagnostics;
// // using System.Runtime.CompilerServices;
// // using UserNameSpace;

// public class Program // application (TEST VERSION) for testing purposes 
// {
// //     //----------------//
// //     // HELPER METHODS //
// //     //----------------//

// //     /// <summary>
// //     /// get a valid int from the user, Over 0, under the max.
// //     /// </summary>
// //     /// <param name="max"> Max number the user can input</param>
// //     /// <returns>The int the user enters thats valid</returns>
// //     public static int inputOptionValidator(int max) // to validate the user's choice from a list of options they picked
// //     {
// //         bool valid = false;
// //         int input = 0;
// //         do
// //         {
// //             try
// //             {
// //                 input = Convert.ToInt32(Console.ReadLine());
// //                 if (input <= max && input > 0)
// //                 {
// //                     valid = true;
// //                 }
// //                 else
// //                 {
// //                     valid = false;
// //                     Console.WriteLine("Invalid Selection. Please enter a number between 1 and " + max + ".");
// //                 }
// //             }
// //             catch (FormatException)
// //             {
// //                 Console.WriteLine("Invalid input. Please enter a valid number.");
// //                 valid = false;
// //             }
// //         } while (valid == false);

// //         return input;
// //     }

// //     /// <summary>
// //     /// This gives you back a valid String, checking if it isnt null or empty.
// //     /// </summary>
// //     /// <returns>Returns Valid String</returns>
// //     public static String inputStringValidator()
// //     {
// //         String? input;
// //         do
// //         {
// //             input = Console.ReadLine();
// //             if (string.IsNullOrEmpty(input))
// //             {
// //                 Console.WriteLine("Please enter a valid input!");
// //             }
// //         } while (input == null | input == "");
// //         if (input != null)
// //         {
// //             return input;
// //         }
// //         else
// //         {
// //             throw new Exception("The input cannot be null!");
// //         }
// //     }

// //     /// <summary>
// //     /// This method checks if the int is valid, as long as its not a string. 
// //     /// </summary>
// //     /// <returns>Returns a valid int </returns>
// //     public static int inputIntValidator()
// //     {
// //         int userInput;
// //         bool isValid = false;

// //         while (!isValid)
// //         {
// //             string? input = Console.ReadLine();
// //             isValid = int.TryParse(input, out userInput) && userInput > 0;
// //             if (!isValid)
// //             {
// //                 Console.WriteLine("Invalid input. Please enter a valid integer (above 0).");
// //             }
// //             return userInput;

// //         }

// //         return -1;
// //     }

// //     public static bool checkUniqueName(string username, List<string?> names)
// //     {
// //         if (names.Contains(username))
// //         {
// //             Console.WriteLine("This name already exists, try again");
// //             return false;
// //         }
// //         return true;
// //     }



//     public static void Main(string[] args)
//     {

//     }
// }
// //         ConsoleWriter writer = new ConsoleWriter();
// //         List<Member> members = new List<Member> // Mock
// //         {
// //             new Member("member1", "passwd1"),
// //             new Member("member2", "passwd2"),
// //         };
// //         List<Admin> admins = new List<Admin>{
// //             new Admin("admin1", "passwd1"),
// //             new Admin("admin2", "passwd2")
// //         };

// //         Ingredient ingredient1 = new Ingredient("Test", "Grains", 5, 2, 0, 20, 200, Unit.Milliliter);
// //         RecipeIngredient recipeIngredient1 = new RecipeIngredient();
// //         recipeIngredient1.IngredientProperty = ingredient1;
// //         recipeIngredient1.Quantity = 150;
// //         members[0].OwnedRecipes = new List<Recipe>{
// //             new Recipe("Testing Purposes.",
// //             members[0],
// //             "This is to test the Owned Recipe Field",
// //             10.0,
// //             25.0,
// //             new List<Instruction>{new Instruction("Step 1: test!"),new Instruction("Step 2: More Testing")},
// //             new List<Tag>{new Tag("Easy"), new Tag("Warm")},
// //             6,
// //             new List<RecipeIngredient>{recipeIngredient1}
// //             )
// //         };
// //         List<Ingredient> ingredients = new List<Ingredient>{
// //             new Ingredient("Tomatoes", "Vegetables", 1, 0, 2, 3, 150, Unit.Gram),
// //             new Ingredient("Chicken Breast", "Meat", 25, 3, 0, 10, 150, Unit.Gram),
// //             new Ingredient("Rice", "Grains", 4, 1, 0, 20, 150, Unit.Gram)
// //         };
// //         //IConsole writer = new ConsoleWriter();
// //         MemberController memberController = new MemberController(members, writer);
// //         AdminController adminController = new AdminController(admins);
// //         RecipeController recipeController = new RecipeController(new MockRecipeDatabase());
// //         IngredientController ingredientController = new IngredientController(ingredients);

// //         bool login;
// //         bool isExit;
// //         List<string> userActions = new List<string>();
// //         string input;
// //         bool isExitProgram = false;
// //         do
// //         {
// //             do
// //             {
// //                 login = false;
// //                 isExit = false;
// //                 Console.Clear();
// //                 Console.WriteLine("Welcome! Pick how would you like to login as!");
// //                 List<string> typeOfAccountOptions = new List<string> { "Member", "Admin", "Exit" };
// //                 ConsoleWriter.DisplayOptions(typeOfAccountOptions);
// //                 input = ConsoleReader.getValidStringFromOptions(typeOfAccountOptions);
// //                 userActions = new List<string>();
// //                 bool validLogin = false;
// //                 switch (input)
// //                 {
// //                     case "Member":
// //                         Console.Clear();
// //                         userActions = new List<string> { "General Search (D)", "Recipe Creation (D)",
// //                         "Modify/View Favorite Recipe List (D)",
// //                         "Update My Recipe (D)", "View My Recipe (D)",
// //                         "Modify My Account (D)","Check Recipe Search History (D)",
// //                         "Rate a Recipe (D)",
// //                         "Exit and Log out (D)"}; //Member Options.
// //                         Console.WriteLine("Do you have an account with us?");
// //                         List<string> accountOptions = new List<string> { "Yes", "No", "Exit" };
// //                         ConsoleWriter.DisplayOptions(accountOptions);
// //                         input = ConsoleReader.getValidStringFromOptions(accountOptions);
// //                         string usr;
// //                         string pwd;
// //                         switch (input)
// //                         {
// //                             case "Yes":
// //                                 Console.Clear();
// //                                 Console.WriteLine("Hello Member!");
// //                                 Console.WriteLine("Enter your username");
// //                                 usr = inputStringValidator();
// //                                 Console.WriteLine("Enter your password");
// //                                 pwd = inputStringValidator();
// //                                 validLogin = memberController.VerifyLogin(usr, pwd);
// //                                 break;
// //                             case "No":
// //                                 Console.WriteLine("Time to make an account!");
// //                                 bool isUnique;
// //                                 do
// //                                 {
// //                                     Console.WriteLine("Enter your new username");
// //                                     usr = ConsoleReader.getValidString();
// //                                     isUnique = checkUniqueName(usr, members.Select(member => member.Username).ToList());
// //                                 } while (isUnique != true);
// //                                 Console.WriteLine("Enter your new password");
// //                                 pwd = ConsoleReader.getValidString();
// //                                 memberController.CreateMember(usr, pwd);
// //                                 validLogin = memberController.VerifyLogin(usr, pwd);
// //                                 ConsoleWriter.ClearWithEnter();
// //                                 break;
// //                             case "Exit":
// //                                 login = true;
// //                                 isExit = true;
// //                                 break;
// //                         }
// //                         break;
// //                     case "Admin":
// //                         Console.Clear();
// //                         userActions = new List<string> { "General Search (D)",
// //                         "All Recipe Modifications (D)",
// //                         "All Ingredients Modifications",
// //                         "All Account Modifications (D)",
// //                         "All Rating Modifications (D)",
// //                         "Exit and Log out (D)" }; //Admin options
// //                         writer.WriteLine("Hello Admin.");
// //                         Console.WriteLine("Enter your username");
// //                         usr = inputStringValidator();
// //                         Console.WriteLine("Enter your password");
// //                         pwd = inputStringValidator();
// //                         validLogin = adminController.VerifyLogin(usr, pwd);
// //                         break;
// //                     case "Exit":
// //                         Console.Clear();
// //                         isExit = true;
// //                         login = true;
// //                         isExitProgram = true;
// //                         break;
// //                 }
// //                 if (isExit == false)
// //                 {
// //                     if (validLogin)
// //                     {
// //                         login = true;
// //                     }
// //                     else
// //                     {
// //                         Console.WriteLine("username or password incorrect. Please try again");
// //                         ConsoleWriter.ClearWithEnter();
// //                     }
// //                 }
// //             } while (login == false);

// //             while (isExit == false)
// //             {
// //                 Console.Clear();
// //                 Console.WriteLine("Welcome to AMHANGRY!");
// //                 ConsoleWriter.DisplayOptions(userActions);
// //                 input = ConsoleReader.getValidStringFromOptions(userActions);

// //                 switch (input)
// //                 {

// //                     case "General Search (D)":
// //                         ///----------------------
// //                         ///GENERAL SEARCH SECTION
// //                         ///----------------------

// //                         bool back = false;
// //                         do
// //                         {
// //                             int optionSearch = 0;

// //                             Console.WriteLine("How do you want to search?");
// //                             Console.WriteLine("1. By Recipe name");
// //                             Console.WriteLine("2. By User's name");
// //                             Console.WriteLine("3. By Description");
// //                             Console.WriteLine("4. By Time");
// //                             Console.WriteLine("5. By Rating");
// //                             Console.WriteLine("6. By Servings");
// //                             Console.WriteLine("7. By Ingredients");
// //                             Console.WriteLine("8. By Tags");
// //                             Console.WriteLine("9. Show all recipes");
// //                             Console.WriteLine("10. Show all ingredients");
// //                             Console.WriteLine("11. Go back");

// //                             optionSearch = inputOptionValidator(11);
// //                             List<Recipe> recipelist = new List<Recipe>();

// //                             switch (optionSearch)
// //                             {
// //                                 case 1:
// //                                     Console.WriteLine("Enter the recipe name");
// //                                     input = inputStringValidator();
// //                                     recipelist = recipeController.SearchByName(input);
// //                                     break;
// //                                 case 2:
// //                                     Console.WriteLine("Enter the user's name");
// //                                     string user = inputStringValidator();
// //                                     recipelist = recipeController.SearchByUser(user);
// //                                     break;
// //                                 case 3:
// //                                     Console.WriteLine("Enter the description");
// //                                     string description = inputStringValidator();
// //                                     recipelist = recipeController.SearchByDescription(description);
// //                                     break;
// //                                 case 4:
// //                                     Console.WriteLine("Enter the maximum time");
// //                                     int time = inputIntValidator();
// //                                     recipelist = recipeController.SearchByTime(time);
// //                                     break;
// //                                 case 5:
// //                                     Console.WriteLine("Enter the minimum rating (1-5)");
// //                                     int byRating = inputIntValidator();
// //                                     recipelist = recipeController.SearchByRating(byRating);
// //                                     break;
// //                                 case 6:
// //                                     Console.WriteLine("Enter the minimum servings (min 1)");
// //                                     int servings = inputIntValidator();
// //                                     recipelist = recipeController.SearchByServings(servings);
// //                                     break;
// //                                 case 7:
// //                                     Console.WriteLine("Enter a list of ingredients, type 'done' when you have entered everything");
// //                                     List<string> inputIngredients = new List<string>();
// //                                     bool done = false;
// //                                     while (done == false)
// //                                     {
// //                                         string ingredient = inputStringValidator();
// //                                         if (ingredient.Equals("done"))
// //                                         {
// //                                             done = true;
// //                                         }
// //                                         else
// //                                         {
// //                                             inputIngredients.Add(ingredient);
// //                                         }
// //                                     }

// //                                     string choice = "";
// //                                     do
// //                                     {
// //                                         Console.WriteLine("Do you want to search recipes with exact ingredients only? y/n");
// //                                         choice = inputStringValidator();
// //                                         if (choice.Equals("y"))
// //                                         {
// //                                             recipelist = recipeController.SearchByExactIngredients(inputIngredients);
// //                                         }
// //                                         else if (choice.Equals("n"))
// //                                         {
// //                                             recipelist = recipeController.SearchByIngredients(inputIngredients);
// //                                         }
// //                                     } while (!choice.Equals("y") && !choice.Equals("n"));
// //                                     break;
// //                                 case 8:
// //                                     Console.WriteLine("Enter a list of tags, type 'done' when you have finished entering everything");
// //                                     List<Tag> tags = new List<Tag>();
// //                                     bool isTagsDone = false;
// //                                     while (isTagsDone == false)
// //                                     {
// //                                         string tag = inputStringValidator();
// //                                         if (tag.Equals("done"))
// //                                         {
// //                                             isTagsDone = true;
// //                                         }
// //                                         else
// //                                         {
// //                                             tags.Add(new Tag(tag));
// //                                         }
// //                                     }

// //                                     recipelist = recipeController.SearchByTag(tags);
// //                                     break;
// //                                 case 9:
// //                                     recipelist = recipeController.Recipes;
// //                                     break;
// //                                 case 10:
// //                                     List<Ingredient> ings = ingredientController.Ingredients;
// //                                     Console.WriteLine("------------- ALL INGREDIENTS-----------");
// //                                     foreach (Ingredient ing in ings)
// //                                     {
// //                                         Console.WriteLine("----------------------------------------");
// //                                         Console.WriteLine(ing);
// //                                         back = true;
// //                                     }
// //                                     break;
// //                                 case 11:
// //                                     back = true;
// //                                     break;
// //                             }

// //                             if (back == false)
// //                             {
// //                                 foreach (Recipe recipe in recipelist)
// //                                 {
// //                                     Console.WriteLine("-------------------");
// //                                     Console.WriteLine(recipe);
// //                                     memberController.CurrentMember.addSearchRecipe(recipe);
// //                                 }
// //                                 ConsoleWriter.ClearWithEnter();
// //                             }
// //                             ConsoleWriter.ClearWithEnter();

// //                         } while (back == false);
// //                         break;

// //                     case "Recipe Creation (D)":
// //                         ///----------------------
// //                         ///CREATE RECIPE SECTION 
// //                         ///----------------------
// //                         Console.WriteLine("Enter the name of the recipe");
// //                         string recipeName = inputStringValidator();

// //                         Console.WriteLine("Enter a short description of your recipe");
// //                         string recipeDescription = inputStringValidator();

// //                         Console.WriteLine("Enter the duration of preparation time in mins");
// //                         double recipePreparationTime = inputIntValidator();

// //                         Console.WriteLine("Enter the duration of cooking time in mins");
// //                         double recipeCookingTime = inputIntValidator();

// //                         Console.WriteLine("How many steps of instruction does your recipe have ?");
// //                         int steps = inputIntValidator();
// //                         List<Instruction> recipeInstructions = new List<Instruction>();

// //                         for (int i = 0; i < steps; i++)
// //                         {
// //                             Console.WriteLine("Enter step " + (i + 1) + "'s instruction");
// //                             string instruction = inputStringValidator();
// //                             recipeInstructions.Add(new Instruction(instruction));
// //                         }

// //                         Console.WriteLine("How many tags does your recipe have ?");
// //                         int qtyTags = inputIntValidator();
// //                         List<Tag> recipeTags = new List<Tag>();

// //                         for (int i = 0; i < qtyTags; i++)
// //                         {
// //                             Console.WriteLine("Enter tag #" + (i + 1));
// //                             string tag = inputStringValidator();
// //                             recipeTags.Add(new Tag(tag));
// //                         }

// //                         List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
// //                         Console.WriteLine("How many ingredients does your recipe need ?");
// //                         int qty = inputIntValidator();

// //                         for (int i = 0; i < qty; i++)
// //                         {
// //                             bool valid = false;
// //                             while (!valid)
// //                             {
// //                                 Console.WriteLine("Enter ingredient #" + (i + 1));
// //                                 string ingredient = inputStringValidator();
// //                                 List<Ingredient> ingredientDB = ingredientController.SearchByExactName(ingredient); //
// //                                 if (ingredientDB.Count() != 0)
// //                                 {
// //                                     Console.WriteLine("Enter the quantity in " + ingredientDB[0].IngredientUnit.ToString());
// //                                     int quantity = inputIntValidator();

// //                                     RecipeIngredient recipeIngredient = new RecipeIngredient();
// //                                     recipeIngredient.Quantity = quantity;
// //                                     recipeIngredient.IngredientProperty = ingredientDB[0];

// //                                     recipeIngredients.Add(recipeIngredient);
// //                                     valid = true;
// //                                 }
// //                                 else
// //                                 {
// //                                     Console.WriteLine("This ingredient name doesnt exist in the db");
// //                                 }
// //                             }
// //                         }

// //                         Console.WriteLine("Enter the amount of servings");
// //                         int recipeServings = inputIntValidator();
// //                         try
// //                         {
// //                             recipeController.CreateRecipe(recipeName, memberController.CurrentMember, recipeDescription, recipePreparationTime, recipeCookingTime, recipeInstructions, recipeTags, recipeServings, recipeIngredients);
// //                             Console.WriteLine("The new recipe is created!");
// //                         }
// //                         catch (Exception e)
// //                         {
// //                             Console.WriteLine("One of the field contains invalid input! Therefore, the recipe isn't created: " + e);
// //                         }
// //                         ConsoleWriter.ClearWithEnter();

// //                         break;

// //                     case "Modify/View Favorite Recipe List (D)":
// //                         ///----------------------
// //                         ///MODIFY/VIEW YOUR FAVORITE RECIPE LIST SECTION
// //                         ///----------------------
// //                         ///
// //                         Console.Clear();
// //                         Console.WriteLine("What would you like to do ?");
// //                         Console.WriteLine("1. Add a recipe to your favorite list");
// //                         Console.WriteLine("2. Delete a recipe from your favorite list");
// //                         Console.WriteLine("3. Show your favorite list");
// //                         Console.WriteLine("4. Go back");
// //                         int optionFavorite = inputOptionValidator(4);

// //                         switch (optionFavorite)
// //                         {
// //                             case 1:
// //                                 List<Recipe> listRecipes = recipeController.Recipes;
// //                                 List<string> listRecipeNames = new List<string>();
// //                                 foreach (Recipe recipe in listRecipes)
// //                                 {
// //                                     listRecipeNames.Add(recipe.Name);
// //                                 }
// //                                 ConsoleWriter.DisplayOptions(listRecipeNames);
// //                                 Console.WriteLine("Enter which recipe you would like to add to your favourites!");
// //                                 string nameSelected = ConsoleReader.getValidStringFromOptions(listRecipeNames);

// //                                 foreach (Recipe recipe in listRecipes)
// //                                 {
// //                                     if (nameSelected == recipe.Name)
// //                                     {
// //                                         try
// //                                         {
// //                                             memberController.AddFavRecipe(recipe);
// //                                         }
// //                                         catch (ArgumentException e)
// //                                         {
// //                                             Console.Clear();
// //                                             Console.WriteLine("This recipe is already in the favorite list! : " + e);
// //                                         }
// //                                     }
// //                                 }
// //                                 break;
// //                             case 2:
// //                                 if (memberController.CurrentMember.FavoriteRecipes != null)
// //                                 {
// //                                     List<FavoriteRecipe> favoriteRecipes = memberController.CurrentMember.FavoriteRecipes;
// //                                     List<string> favoriteRecipesNames = new List<string>();
// //                                     foreach (FavoriteRecipe favRecipe in favoriteRecipes)
// //                                     {
// //                                         favoriteRecipesNames.Add(favRecipe.Recipe.Name);
// //                                     }
// //                                     ConsoleWriter.DisplayOptions(favoriteRecipesNames);
// //                                     int recipePosition = ConsoleReader.getValidStringFromOptionsReturnPosition(favoriteRecipesNames);
// //                                     Console.WriteLine("------------------------");
// //                                     Console.WriteLine("Enter which recipe you would like to delete!");
// //                                     memberController.CurrentMember.RemoveFavorite(recipePosition);
// //                                     Console.Clear();
// //                                     Console.WriteLine("You have succesfully removed the recipe from your list!");
// //                                 }
// //                                 else
// //                                 {
// //                                     throw new ArgumentNullException("The currentMember favoriteRecipes field cannot be null!");
// //                                 }
// //                                 break;
// //                             case 3:
// //                                 memberController.ListFavoriteRecipes();
// //                                 break;
// //                         }
// //                         ConsoleWriter.ClearWithEnter();
// //                         break;
// //                     case "Update My Recipe (D)":
// //                         ///----------------------
// //                         ///UPDATE RECIPE SECTION
// //                         ///----------------------
// //                         ///
// //                         Console.Clear();
// //                         if (memberController.CurrentMember.OwnedRecipes == null)
// //                         {
// //                             throw new ArgumentNullException("The owned recipes of the current member is null!");
// //                         }
// //                         List<Recipe> myRecipes = memberController.CurrentMember.OwnedRecipes;
// //                         List<string> myRecipeNames = new List<string>();
// //                         foreach (Recipe myRecipe in myRecipes)
// //                         {
// //                             myRecipeNames.Add(myRecipe.Name);
// //                         }
// //                         ConsoleWriter.DisplayOptions(myRecipeNames);
// //                         int position = ConsoleReader.getValidStringFromOptionsReturnPosition(myRecipeNames);
// //                         Recipe recipeToUpdate = myRecipes[position];
// //                         recipeController.SetCurrentRecipe(recipeToUpdate);
// //                         bool goback = false;
// //                         do
// //                         {
// //                             Console.Clear();
// //                             Console.WriteLine("What would you like to modify?");
// //                             Console.WriteLine("1. Update the recipe name");
// //                             Console.WriteLine("2. Update the recipe description");
// //                             Console.WriteLine("3. Update the recipe preparation time");
// //                             Console.WriteLine("4. Update the recipe cooking time");
// //                             Console.WriteLine("5. Update the recipe servings");
// //                             Console.WriteLine("6. Update the recipe instructions");
// //                             Console.WriteLine("7. Update the recipe tags");
// //                             Console.WriteLine("8. Update the recipe ingredients");
// //                             Console.WriteLine("9. Go back");
// //                             int updateOption = inputOptionValidator(9);

// //                             switch (updateOption)
// //                             {
// //                                 case 1:
// //                                     Console.WriteLine("Enter the new recipe name");
// //                                     string name = inputStringValidator();
// //                                     recipeController.UpdateRecipeName(name);
// //                                     break;
// //                                 case 2:
// //                                     Console.WriteLine("Enter the new recipe description");
// //                                     string description = inputStringValidator();
// //                                     recipeController.UpdateRecipeDescription(description);
// //                                     break;
// //                                 case 3:
// //                                     Console.WriteLine("Enter the new recipe preparation time");
// //                                     double preparationTime = Convert.ToDouble(inputStringValidator());
// //                                     recipeController.UpdatePreparationTime(preparationTime);
// //                                     break;
// //                                 case 4:
// //                                     Console.WriteLine("Enter the new recipe cooking time");
// //                                     double cookingTime = Convert.ToDouble(inputStringValidator());
// //                                     recipeController.UpdateCookingTime(cookingTime);
// //                                     break;
// //                                 case 5:
// //                                     Console.WriteLine("Enter the new recipe servings");
// //                                     int servings = Convert.ToInt32(inputStringValidator());
// //                                     recipeController.UpdateRecipeServing(servings);
// //                                     break;
// //                                 case 6:

// //                                     Console.WriteLine("How many steps of instruction does your recipe have ?");
// //                                     steps = ConsoleReader.getValidInt();
// //                                     recipeInstructions = new List<Instruction>();
// //                                     for (int i = 0; i < steps; i++)
// //                                     {
// //                                         Console.WriteLine("Enter step " + (i + 1) + "'s instruction");
// //                                         string instruction = inputStringValidator();
// //                                         recipeInstructions.Add(new Instruction(instruction));
// //                                     }
// //                                     recipeController.UpdateRecipeInstructions(recipeInstructions);
// //                                     break;
// //                                 case 7:
// //                                     Console.WriteLine("How many tags does your recipe have ?");
// //                                     qtyTags = ConsoleReader.getValidInt();
// //                                     recipeTags = new List<Tag>();
// //                                     for (int i = 0; i < qtyTags; i++)
// //                                     {
// //                                         Console.WriteLine("Enter tag #" + (i + 1));
// //                                         string tag = inputStringValidator();
// //                                         recipeTags.Add(new Tag(tag));
// //                                     }
// //                                     recipeController.UpdateRecipeTags(recipeTags);
// //                                     break;
// //                                 case 8:
// //                                     recipeIngredients = new List<RecipeIngredient>();
// //                                     Console.WriteLine("How many ingredients does your recipe need ?");
// //                                     qty = ConsoleReader.getValidInt();
// //                                     for (int i = 0; i < qty; i++)
// //                                     {
// //                                         bool valid = false;
// //                                         while (!valid)
// //                                         {
// //                                             Console.WriteLine("Enter ingredient #" + (i + 1));
// //                                             string ingredient = ConsoleReader.getValidString();
// //                                             List<Ingredient> ingredientDB = ingredientController.SearchByExactName(ingredient); //
// //                                             if (ingredientDB.Count() != 0)
// //                                             {
// //                                                 Console.WriteLine("Enter the quantity in " + ingredientDB[0].IngredientUnit.ToString());
// //                                                 int quantity = inputIntValidator();

// //                                                 RecipeIngredient recipeIngredient = new RecipeIngredient();
// //                                                 recipeIngredient.Quantity = quantity;
// //                                                 recipeIngredient.IngredientProperty = ingredientDB[0];
// //                                                 recipeIngredients.Add(recipeIngredient);
// //                                                 valid = true;
// //                                             }
// //                                             else
// //                                             {
// //                                                 Console.WriteLine("This ingredient name doesnt exist in the db");
// //                                             }
// //                                         }
// //                                     }
// //                                     ConsoleWriter.ClearWithEnter();
// //                                     recipeController.UpdateRecipeIngredients(recipeIngredients);
// //                                     break;
// //                                 case 9:
// //                                     goback = true;
// //                                     break;
// //                             }
// //                             if (goback != true)
// //                             {
// //                                 Console.WriteLine("You have updated your recipe successfully!");
// //                                 ConsoleWriter.ClearWithEnter();
// //                             }
// //                         } while (goback == false);
// //                         break;

// //                     case "View My Recipe (D)":
// //                         ///----------------------
// //                         ///VIEW OWNER RECIPES SECTION
// //                         ///----------------------
// //                         memberController.ListOwnedRecipes();
// //                         ConsoleWriter.ClearWithEnter();
// //                         break;

// //                     case "Modify My Account (D)":
// //                         ///----------------------
// //                         ///MODIFY ACCOUNT SETTINGS SECTION
// //                         ///----------------------
// //                         ///
// //                         Console.WriteLine("Welcome To Account Modfying Settings! What would yould you like to do today?");
// //                         List<string> userAccountOptions = new List<string> { "View My Account Details", "Update My Password", "Update My Profile Picture", "Update My Description", "Remove My Profile Picture", "Remove My Description", "Delete My Account", "Go back" };
// //                         ConsoleWriter.DisplayOptions(userAccountOptions);
// //                         input = ConsoleReader.getValidStringFromOptions(userAccountOptions);
// //                         switch (input)
// //                         {
// //                             case "View My Account Details":
// //                                 memberController.ViewMyAccountDetails();
// //                                 break;
// //                             case "Update My Password":
// //                                 Console.WriteLine("Enter the new password you would like to have!");
// //                                 string newPassword = ConsoleReader.getValidString();
// //                                 memberController.UpdatePassword(newPassword);
// //                                 ConsoleWriter.ClearWithEnter();
// //                                 break;
// //                             case "Update My Profile Picture":
// //                                 Console.WriteLine("Enter the new profile picture link you would like to have!");
// //                                 string newProfilePicture = ConsoleReader.getValidString();
// //                                 memberController.UpdateProfilePicture(newProfilePicture);
// //                                 ConsoleWriter.ClearWithEnter();
// //                                 break;
// //                             case "Update My Description":
// //                                 Console.WriteLine("Enter the new description you would like to have!");
// //                                 string newDescription = ConsoleReader.getValidString();
// //                                 memberController.UpdateUserDescription(newDescription);
// //                                 ConsoleWriter.ClearWithEnter();
// //                                 break;
// //                             case "Remove My Profile Picture":
// //                                 memberController.RemoveProfilePicture();
// //                                 ConsoleWriter.ClearWithEnter();
// //                                 break;
// //                             case "Remove My Description":
// //                                 memberController.RemoveMemberDescription();
// //                                 ConsoleWriter.ClearWithEnter();
// //                                 break;
// //                             case "Delete My Account":
// //                                 memberController.DeleteMyAccount();
// //                                 isExit = true;
// //                                 ConsoleWriter.ClearWithEnter();
// //                                 break;
// //                             case "Go back":
// //                                 break;
// //                         }
// //                         break;

// //                     case "Check Recipe Search History (D)":
// //                         ///----------------------
// //                         ///CHECK RECIPE SEARCH HISTORY SECTION
// //                         ///----------------------
// //                         memberController.ListRecentSearches();
// //                         ConsoleWriter.ClearWithEnter();
// //                         break;
// //                     case "Rate a Recipe (D)":
// //                         ///----------------------
// //                         ///RATE A RECIPE SECTION
// //                         ///----------------------
// //                         ///
// //                         Console.Clear();
// //                         Console.WriteLine("What's the recipe name that you'd like to rate?");
// //                         List<Recipe> recipes = recipeController.Recipes;
// //                         List<string> recipeNames = new List<string>();
// //                         foreach (Recipe recipe in recipes)
// //                         {
// //                             recipeNames.Add(recipe.Name);
// //                         }
// //                         ConsoleWriter.DisplayOptions(recipeNames);
// //                         position = ConsoleReader.getValidStringFromOptionsReturnPosition(recipeNames);
// //                         Recipe recipeToRate = recipes[position];
// //                         recipeController.SetCurrentRecipe(recipeToRate);
// //                         Console.WriteLine("Enter your rating for the recipe");
// //                         int rating = Convert.ToInt32(inputStringValidator());

// //                         if (memberController.CurrentMember.Username != null)
// //                         {
// //                             recipeController.RateRecipe(memberController.CurrentMember.Username, rating);
// //                         }
// //                         else
// //                         {
// //                             throw new ArgumentNullException("The current member username cannot be null!");
// //                         }


// //                         Console.WriteLine("Your rating is inserted into the recipe");

// //                         ConsoleWriter.ClearWithEnter();
// //                         break;

// //                     case "All Recipe Modifications":
// //                         userAccountOptions = new List<string> { "Update A Recipe", "Delete A Recipe (D)", "Go back" };
// //                         ConsoleWriter.DisplayOptions(userAccountOptions);
// //                         input = ConsoleReader.getValidStringFromOptions(userAccountOptions);
// //                         switch (input)
// //                         {
// //                             case "Update A Recipe":
// //                                 myRecipes = recipeController.Recipes;
// //                                 List<string> mRecipeNames = new List<string>();
// //                                 foreach (Recipe recipe in myRecipes)
// //                                 {
// //                                     mRecipeNames.Add(recipe.Name);
// //                                 }
// //                                 Console.WriteLine("Which recipe would you like to update?");
// //                                 ConsoleWriter.DisplayOptions(mRecipeNames);
// //                                 position = ConsoleReader.getValidStringFromOptionsReturnPosition(mRecipeNames);
// //                                 recipeToUpdate = myRecipes[position];
// //                                 recipeController.CurrentRecipe = recipeToUpdate;
// //                                 Console.Clear();
// //                                 goback = false;
// //                                 do
// //                                 {
// //                                     Console.WriteLine("What would you like to modify?");
// //                                     Console.WriteLine("1. Update the recipe name");
// //                                     Console.WriteLine("2. Update the recipe description");
// //                                     Console.WriteLine("3. Update the recipe preparation time");
// //                                     Console.WriteLine("4. Update the recipe cooking time");
// //                                     Console.WriteLine("5. Update the recipe servings");
// //                                     Console.WriteLine("6. Update the recipe instructions");
// //                                     Console.WriteLine("7. Update the recipe tags");
// //                                     Console.WriteLine("8. Update the recipe ingredients");
// //                                     Console.WriteLine("9. Go back");
// //                                     int updateOption = inputOptionValidator(9);

// //                                     switch (updateOption)
// //                                     {
// //                                         case 1:
// //                                             Console.WriteLine("Enter the new recipe name");
// //                                             string name = inputStringValidator();
// //                                             recipeController.UpdateRecipeName(name);
// //                                             break;
// //                                         case 2:
// //                                             Console.WriteLine("Enter the new recipe description");
// //                                             string description = inputStringValidator();
// //                                             recipeController.UpdateRecipeDescription(description);
// //                                             break;
// //                                         case 3:
// //                                             Console.WriteLine("Enter the new recipe preparation time");
// //                                             double preparationTime = Convert.ToDouble(inputStringValidator());
// //                                             recipeController.UpdatePreparationTime(preparationTime);
// //                                             break;
// //                                         case 4:
// //                                             Console.WriteLine("Enter the new recipe cooking time");
// //                                             double cookingTime = Convert.ToDouble(inputStringValidator());
// //                                             recipeController.UpdateCookingTime(cookingTime);
// //                                             break;
// //                                         case 5:
// //                                             Console.WriteLine("Enter the new recipe servings");
// //                                             int servings = Convert.ToInt32(inputStringValidator());
// //                                             recipeController.UpdateRecipeServing(servings);
// //                                             break;
// //                                         case 6:

// //                                             Console.WriteLine("How many steps of instruction does this recipe have?");
// //                                             steps = ConsoleReader.getValidInt();
// //                                             recipeInstructions = new List<Instruction>();
// //                                             for (int i = 0; i < steps; i++)
// //                                             {
// //                                                 Console.WriteLine("Enter step " + (i + 1) + "'s instruction");
// //                                                 string instruction = inputStringValidator();
// //                                                 recipeInstructions.Add(new Instruction(instruction));
// //                                             }
// //                                             recipeController.UpdateRecipeInstructions(recipeInstructions);
// //                                             break;
// //                                         case 7:
// //                                             Console.WriteLine("How many tags does this recipe have?");
// //                                             qtyTags = ConsoleReader.getValidInt();
// //                                             recipeTags = new List<Tag>();
// //                                             for (int i = 0; i < qtyTags; i++)
// //                                             {
// //                                                 Console.WriteLine("Enter tag #" + (i + 1));
// //                                                 string tag = inputStringValidator();
// //                                                 recipeTags.Add(new Tag(tag));
// //                                             }
// //                                             recipeController.UpdateRecipeTags(recipeTags);
// //                                             break;
// //                                         case 8:
// //                                             recipeIngredients = new List<RecipeIngredient>();
// //                                             Console.WriteLine("How many ingredients does this recipe need?");
// //                                             qty = ConsoleReader.getValidInt();
// //                                             for (int i = 0; i < qty; i++)
// //                                             {
// //                                                 bool valid = false;
// //                                                 while (!valid)
// //                                                 {
// //                                                     Console.WriteLine("Enter ingredient #" + (i + 1));
// //                                                     string ingredient = ConsoleReader.getValidString();
// //                                                     List<Ingredient> ingredientDB = ingredientController.SearchByExactName(ingredient); //
// //                                                     if (ingredientDB.Count() != 0)
// //                                                     {
// //                                                         Console.WriteLine("Enter the quantity in " + ingredientDB[0].IngredientUnit.ToString());
// //                                                         int quantity = inputIntValidator();

// //                                                         RecipeIngredient recipeIngredient = new RecipeIngredient();
// //                                                         recipeIngredient.Quantity = quantity;
// //                                                         recipeIngredient.IngredientProperty = ingredientDB[0];
// //                                                         recipeIngredients.Add(recipeIngredient);
// //                                                         valid = true;
// //                                                     }
// //                                                     else
// //                                                     {
// //                                                         Console.WriteLine("This ingredient name doesnt exist in the db");
// //                                                     }
// //                                                 }
// //                                             }
// //                                             ConsoleWriter.ClearWithEnter();
// //                                             recipeController.UpdateRecipeIngredients(recipeIngredients);
// //                                             break;
// //                                         case 9:
// //                                             goback = true;
// //                                             break;
// //                                     }
// //                                     if (goback != true)
// //                                     {
// //                                         Console.WriteLine("You have updated -- " + recipeController.CurrentRecipe.Name + " -- successfully!");
// //                                         ConsoleWriter.ClearWithEnter();
// //                                     }
// //                                 } while (goback == false);
// //                                 break;
// //                             case "Delete A Recipe (D)":
// //                                 myRecipes = recipeController.Recipes;
// //                                 recipeNames = new List<string>();
// //                                 foreach (Recipe recipe in myRecipes)
// //                                 {
// //                                     recipeNames.Add(recipe.Name);
// //                                 }
// //                                 Console.WriteLine("Which recipe would you like to delete?");
// //                                 ConsoleWriter.DisplayOptions(recipeNames);
// //                                 position = ConsoleReader.getValidStringFromOptionsReturnPosition(recipeNames);
// //                                 Recipe recipeToDelete = myRecipes[position];
// //                                 recipeController.DeleteRecipe(recipeToDelete);
// //                                 Console.WriteLine("You have deleted : - " + recipeToDelete.Name + " - successfully!");
// //                                 ConsoleWriter.ClearWithEnter();
// //                                 break;
// //                             case "Go back":
// //                                 break;
// //                         }
// //                         break;
// //                     case "All Ingredients Modifications":
// //                         do
// //                         {
// //                             userAccountOptions = new List<string> { "Update An Ingredient", "Delete An Ingredient", "Go Back" };
// //                             ConsoleWriter.DisplayOptions(userAccountOptions);
// //                             string userAction = ConsoleReader.getValidStringFromOptions(userAccountOptions);
// //                             List<Ingredient> myIngredients;
// //                             goback = false;
// //                             switch (userAction)
// //                             {
// //                                 case "Update An Ingredient":
// //                                     myIngredients = ingredientController.Ingredients;
// //                                     List<string> mIngredientNames = new List<string>();
// //                                     foreach (Ingredient ingredient in myIngredients)
// //                                     {
// //                                         mIngredientNames.Add(ingredient.Name);
// //                                     }
// //                                     Console.WriteLine("Which ingredient would you like to update?");
// //                                     ConsoleWriter.DisplayOptions(mIngredientNames);
// //                                     position = ConsoleReader.getValidStringFromOptionsReturnPosition(mIngredientNames);
// //                                     Ingredient ingredientToUpdate = myIngredients[position];
// //                                     ingredientController.CurrentIngredient = ingredientToUpdate;
// //                                     Console.Clear();
// //                                     goback = false;
// //                                     do
// //                                     {
// //                                         Console.WriteLine("What would you like to modify?");
// //                                         Console.WriteLine("1. Update the ingredient name");
// //                                         Console.WriteLine("2. Update the ingredient category");
// //                                         Console.WriteLine("3. Update the ingredient protein");
// //                                         Console.WriteLine("4. Update the ingredient fat");
// //                                         Console.WriteLine("5. Update the ingredient carbs");
// //                                         Console.WriteLine("6. Update the ingredient cost");
// //                                         Console.WriteLine("7. Update the ingredient amount");
// //                                         Console.WriteLine("8. Update the ingredient unit");
// //                                         Console.WriteLine("9. Go back");
// //                                         int updateOption = inputOptionValidator(9);

// //                                         switch (updateOption)
// //                                         {
// //                                             case 1:
// //                                                 Console.WriteLine("Enter the new ingredient name");
// //                                                 string name = inputStringValidator();
// //                                                 ingredientController.UpdateIngredientName(name);
// //                                                 break;
// //                                             case 2:
// //                                                 Console.WriteLine("Enter the new ingredient category");
// //                                                 string category = inputStringValidator();
// //                                                 ingredientController.UpdateIngredientCategory(category);
// //                                                 break;
// //                                             case 3:
// //                                                 Console.WriteLine("Enter the new ingredient protein");
// //                                                 int protein = Convert.ToInt32(inputStringValidator());
// //                                                 ingredientController.UpdateIngredientProtein(protein);
// //                                                 break;
// //                                             case 4:
// //                                                 Console.WriteLine("Enter the new ingredient fat");
// //                                                 int fat = Convert.ToInt32(inputStringValidator());
// //                                                 ingredientController.UpdateIngredientFat(fat);
// //                                                 break;
// //                                             case 5:
// //                                                 Console.WriteLine("Enter the new ingredient carbs");
// //                                                 int carbs = Convert.ToInt32(inputStringValidator());
// //                                                 ingredientController.UpdateIngredientCarbs(carbs);
// //                                                 break;
// //                                             case 6:
// //                                                 Console.WriteLine("Enter the new ingredient cost");
// //                                                 double cost = Convert.ToDouble(inputStringValidator());
// //                                                 ingredientController.UpdateIngredientCost(cost);
// //                                                 break;
// //                                             case 7:
// //                                                 Console.WriteLine("Enter the new ingredient amount");
// //                                                 double amount = Convert.ToDouble(inputStringValidator());
// //                                                 ingredientController.UpdateIngredientAmount(amount);
// //                                                 break;
// //                                             case 8:
// //                                                 Console.WriteLine("Enter the new ingredient unit");
// //                                                 List<string> unitOptions = new List<string> { "Grams", "Kilograms", "Milliliters", "Liters" };
// //                                                 ConsoleWriter.DisplayOptions(unitOptions);
// //                                                 userAction = ConsoleReader.getValidStringFromOptions(unitOptions);
// //                                                 //need to put this as default option else compiler freaks
// //                                                 Unit unitChosen = Unit.Gram;
// //                                                 switch (userAction)
// //                                                 {
// //                                                     case "Grams":
// //                                                         unitChosen = Unit.Gram;
// //                                                         break;
// //                                                     case "Kilograms":
// //                                                         unitChosen = Unit.Kilogram;
// //                                                         break;
// //                                                     case "Milliliters":
// //                                                         unitChosen = Unit.Milliliter;
// //                                                         break;
// //                                                     case "Liters":
// //                                                         unitChosen = Unit.Liter;
// //                                                         break;
// //                                                 }
// //                                                 Unit unit = unitChosen;
// //                                                 ingredientController.UpdateIngredientUnit(unit);
// //                                                 break;
// //                                             case 9:
// //                                                 goback = true;
// //                                                 break;
// //                                         }
// //                                         if (goback != true)
// //                                         {
// //                                             Console.WriteLine("You have updated -- " + ingredientController.CurrentIngredient.Name + " -- successfully!");
// //                                             ConsoleWriter.ClearWithEnter();
// //                                         }
// //                                     } while (goback == false);
// //                                     break;
// //                                 case "Delete An Ingredient":
// //                                     myIngredients = ingredientController.Ingredients;
// //                                     List<string> myIngredientsName = new List<string>();
// //                                     foreach (Ingredient ing in myIngredients)
// //                                     {
// //                                         myIngredientsName.Add(ing.Name);
// //                                     }
// //                                     ConsoleWriter.DisplayOptions(myIngredientsName);
// //                                     position = ConsoleReader.getValidStringFromOptionsReturnPosition(myIngredientsName);
// //                                     Ingredient ingredientToDelete = myIngredients[position];
// //                                     ingredientController.DeleteIngredient(ingredientToDelete);
// //                                     Console.WriteLine("Successfully removed -- " + ingredientToDelete.Name + " -- from the database!");
// //                                     break;
// //                                 case "Go Back":
// //                                     goback = true;
// //                                     break;
// //                             }
// //                             if (goback != true)
// //                             {
// //                                 ConsoleWriter.ClearWithEnter();
// //                             }
// //                         } while (goback == false);
// //                         break;
// //                     case "All Account Modifications (D)":
// //                         Console.Clear();
// //                         members = memberController.Members;
// //                         List<string> memberNames = new List<string>();
// //                         foreach (Member member in members)
// //                         {
// //                             if (member.Username != null)
// //                             {
// //                                 memberNames.Add(member.Username);
// //                             }
// //                             else
// //                             {
// //                                 throw new ArgumentNullException("The member username is null!");
// //                             }
// //                         }
// //                         ConsoleWriter.DisplayOptions(memberNames);
// //                         Console.WriteLine("Enter the Username you would like to perform an action on.");
// //                         position = ConsoleReader.getValidStringFromOptionsReturnPosition(memberNames);
// //                         Console.Clear();
// //                         Member memberToUpdate = members[position];
// //                         bool isExitAccountModification = false;
// //                         do
// //                         {
// //                             userAccountOptions = new List<string> { "View All Members", "Ban This Member", "Update This Member Password", "Update This Member Description", "Go back" };
// //                             Console.WriteLine("What do you want to do today?");
// //                             ConsoleWriter.DisplayOptions(userAccountOptions);
// //                             string action = ConsoleReader.getValidStringFromOptions(userAccountOptions);
// //                             switch (action)
// //                             {
// //                                 case "View All Members":
// //                                     Console.Clear();
// //                                     members = memberController.Members;
// //                                     foreach (Member member in members)
// //                                     {
// //                                         Console.WriteLine("------------------");
// //                                         Console.WriteLine(member);
// //                                     }
// //                                     ConsoleWriter.ClearWithEnter();
// //                                     break;
// //                                 case "Ban This Member":
// //                                     List<Member> updatedMembers = adminController.BanMember(members, memberToUpdate);
// //                                     memberController.Members = updatedMembers;
// //                                     recipes = recipeController.Recipes;
// //                                     foreach (Recipe recipe in recipes)
// //                                     {
// //                                         recipeController.CurrentRecipe = recipe;
// //                                         if (memberToUpdate.Username != null)
// //                                         {
// //                                             recipeController.DeleteRatingRecipe(memberToUpdate.Username);
// //                                         }
// //                                         else
// //                                         {
// //                                             throw new ArgumentNullException("The member to update username cannot be null!");
// //                                         }
// //                                         if (recipe.Owner.Username == memberToUpdate.Username)
// //                                         {
// //                                             recipeController.DeleteRecipe(recipe);
// //                                         }
// //                                     }
// //                                     Console.Clear();
// //                                     Console.WriteLine("Account Banned!");
// //                                     ConsoleWriter.ClearWithEnter();
// //                                     isExitAccountModification = true;
// //                                     break;
// //                                 case "Update This Member Password":
// //                                     bool isValidPassword = false;
// //                                     string newPassword = "";
// //                                     while (!isValidPassword)
// //                                     {
// //                                         Console.WriteLine("Enter the new password '" + memberToUpdate.Username + "' will log in with.");
// //                                         newPassword = ConsoleReader.getValidString();
// //                                         if (memberToUpdate.MatchPasswords(newPassword))
// //                                         {
// //                                             Console.WriteLine("You can't set the same password again! Try again.");
// //                                         }
// //                                         else
// //                                         {
// //                                             isValidPassword = true;
// //                                             Console.WriteLine("You successfully updated the password of: " + memberToUpdate.Username);
// //                                         }
// //                                         ConsoleWriter.ClearWithEnter();
// //                                     }
// //                                     memberToUpdate.Password = newPassword;
// //                                     memberController.Members[position] = memberToUpdate;
// //                                     break;
// //                                 case "Update This Member Description":
// //                                     Console.WriteLine("Enter the new description '" + memberToUpdate.Username + "' will have from now on.");
// //                                     string newDescription = ConsoleReader.getValidString();
// //                                     memberToUpdate.UserDescription = newDescription;
// //                                     memberController.Members[position] = memberToUpdate;
// //                                     Console.WriteLine("Description Updated Succesfully!");
// //                                     ConsoleWriter.ClearWithEnter();
// //                                     break;
// //                                 case "Go back":
// //                                     isExitAccountModification = true;
// //                                     break;
// //                             }
// //                         } while (!isExitAccountModification);
// //                         break;
// //                     case "All Rating Modifications (D)":
// //                         do
// //                         {
// //                             userAccountOptions = new List<string> { "Remove All Ratings Of A Recipe", "Go Back" };
// //                             ConsoleWriter.DisplayOptions(userAccountOptions);
// //                             Console.WriteLine("What would you like to do today?");
// //                             string action = ConsoleReader.getValidStringFromOptions(userAccountOptions);
// //                             goback = false;
// //                             switch (action)
// //                             {
// //                                 case "Remove All Ratings Of A Recipe":
// //                                     myRecipes = recipeController.Recipes;
// //                                     List<string> mRecipeNames = new List<string>();
// //                                     foreach (Recipe recipe in myRecipes)
// //                                     {
// //                                         mRecipeNames.Add(recipe.Name);
// //                                     }
// //                                     Console.WriteLine("Which recipe would you like to remove all ratings?");
// //                                     ConsoleWriter.DisplayOptions(mRecipeNames);
// //                                     position = ConsoleReader.getValidStringFromOptionsReturnPosition(mRecipeNames);
// //                                     Recipe recipeToChangee = myRecipes[position];
// //                                     recipeToChangee.ClearRatingList();
// //                                     Console.Clear();
// //                                     break;
// //                                 case "Go Back":
// //                                     goback = true;
// //                                     break;
// //                             }
// //                             if (goback != true)
// //                             {

// //                                 ConsoleWriter.ClearWithEnter();
// //                             }
// //                         } while (goback != true);
// //                         break;
// //                     case "Exit and Log out (D)":
// //                         isExit = true;

// //                         break;
// //                 }
// //             }
// //         } while (!isExitProgram);
// //         Console.Clear();
// //         Console.WriteLine("Thank you for using Amhangry! Have a nice day.");
// //     }
