using System.DirectoryServices.ActiveDirectory;
using UserNameSpace;

public class ProgramDB
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to AmHangry!");

        RecipeContext db = RecipeContext.GetInstance();
        ConsoleWriter writer = new ConsoleWriter();
        MemberController memberService = new MemberController(db);
        IngredientController ingredientService = new IngredientController(db);
        RecipeController recipeService = new RecipeController(db);

        // List<UnitEntity> unitEntities = new List<UnitEntity>{
        //     new UnitEntity(Unit.Gram),
        //     new UnitEntity(Unit.Kilogram),
        //     new UnitEntity(Unit.Milliliter),
        //     new UnitEntity(Unit.Liter),
        // };

        // foreach(UnitEntity singleUnit in unitEntities){
        //     ingredientService.CreateUnit(singleUnit);
        // }

        // UnitEntity? unit = db.UnitEntities 
        //    .SingleOrDefault(u => u.UnitEntityId == 1);

        // List<Ingredient> dbingredients = new List<Ingredient>{
        //     new Ingredient("Flour", "Grains", 5, 2, 0, 20, 200, unit),
        //     new Ingredient("Eggs", "Dairy", 6, 5, 1, 3, 100, unit),
        //     new Ingredient("Tomatoes", "Vegetables", 1, 0, 2, 3, 150, unit),
        //     new Ingredient("Chicken Breast", "Meat", 25, 3, 0, 10, 150, unit),
        //     new Ingredient("Rice", "Grains", 4, 1, 0, 20, 150, unit),
        //     new Ingredient("Spinach", "Vegetables", 2, 0, 1, 2, 100, unit),
        //     new Ingredient("Milk", "Dairy", 3, 2, 4, 5, 200, unit),
        // };
        // foreach(Ingredient ingredient in dbingredients){
        //     ingredientService.CreateIngredient(ingredient);
        // }

        bool exitProgram = false;
        while (!exitProgram)
        {
            string action;
            List<string> userActions;
            userActions = new List<string> { "Create An Account", "Log in", "Exit" };
            ConsoleWriter.DisplayOptions(userActions);
            action = ConsoleReader.getValidStringFromOptions(userActions);
            bool exit;
            Member currentMember = new Member();

            switch (action)
            {
                case "Create An Account":
                    Console.WriteLine("Enter your new username!");
                    string username = ConsoleReader.getValidString();
                    Console.WriteLine("Enter your new password!");
                    string password = ConsoleReader.getValidString();
                    memberService.CreateMember(username, password);
                    currentMember = memberService.GetMember(username);
                    Console.WriteLine("Account created Successfully!");
                    ConsoleWriter.ClearWithEnter();
                    break;
                case "Log in":
                    bool login = false;
                    while (!login)
                    {
                        Console.WriteLine("Enter your username!");
                        string loginUsername = ConsoleReader.getValidString();
                        Console.WriteLine("Enter your password!");
                        string loginPassword = ConsoleReader.getValidString();
                        login = memberService.VerifyLogin(loginUsername, loginPassword);
                        if (login)
                        {
                            Console.WriteLine("You have logged in successfully!");
                            currentMember = memberService.GetMember(loginUsername);
                            ConsoleWriter.ClearWithEnter();
                        }
                        else
                        {
                            Console.WriteLine("Failed to log in! Try again.");
                            ConsoleWriter.ClearWithEnter();
                        }
                    }
                    break;
                case "Exit":
                    exitProgram = true;
                    break;
            }

            if (!exitProgram)
            {
                exit = false;
                do
                {
                    Console.Clear();
                    Console.WriteLine("What would you like to do today?");
                    userActions = new List<string> { "Create Recipe", "Search Recipe", "View My Account", "View My Recent Searches", "My Recipe(s) Modification", "Account Modification", "Favorite Recipe List Actions", "Exit and Log Out" };
                    bool bigAction = false;
                    ConsoleWriter.DisplayOptions(userActions);
                    action = ConsoleReader.getValidStringFromOptions(userActions);
                    switch (action)
                    {
                        case "Create Recipe":
                            Console.WriteLine("Enter the name of the recipe");
                            string recipeName = ConsoleReader.getValidString();

                            Console.WriteLine("Enter a short description of your recipe");
                            string recipeDescription = ConsoleReader.getValidString();

                            Console.WriteLine("Enter the duration of preparation time in mins");
                            double recipePreparationTime = ConsoleReader.getValidInt();

                            Console.WriteLine("Enter the duration of cooking time in mins");
                            double recipeCookingTime = ConsoleReader.getValidInt();

                            Console.WriteLine("How many steps of instruction does your recipe have ?");
                            int steps = ConsoleReader.getValidInt();
                            List<Instruction> recipeInstructions = new List<Instruction>();

                            for (int i = 0; i < steps; i++)
                            {
                                Console.WriteLine("Enter step " + (i + 1) + "'s instruction");
                                string instruction = ConsoleReader.getValidString();
                                recipeInstructions.Add(new Instruction(instruction));
                            }

                            Console.WriteLine("How many tags does your recipe have ?");
                            int qtyTags = ConsoleReader.getValidInt();
                            List<Tag> recipeTags = new List<Tag>();

                            for (int i = 0; i < qtyTags; i++)
                            {
                                Console.WriteLine("Enter tag #" + (i + 1));
                                string tag = ConsoleReader.getValidString();
                                recipeTags.Add(new Tag(tag));
                            }

                            Console.WriteLine("How many ingredients would you like to have?");
                            bool validNum = false;
                            int numIng = 0;
                            while (!validNum)
                            {
                                numIng = ConsoleReader.getValidInt();
                                if (numIng <= 0)
                                {
                                    Console.WriteLine("You need to at least have 1 ingredient to create a recipe. Try again.");
                                }
                                else
                                {
                                    validNum = true;
                                }
                            }
                            List<Ingredient> ingredients = ingredientService.GetAllIngredientsDB();
                            List<string> ingredientNames = new List<string>();
                            List<string> ingredientToSearch = new List<string>();
                            foreach (Ingredient ing in ingredients)
                            {
                                ingredientNames.Add(ing.Name);
                            }
                            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();

                            for (int i = 0; i < numIng; i++)
                            {
                                Console.WriteLine("Ingredient #" + (i + 1) + ":");
                                ConsoleWriter.DisplayOptions(ingredientNames);
                                int pos = ConsoleReader.getValidStringFromOptionsReturnPosition(ingredientNames);
                                Ingredient myIng = ingredients[pos];

                                Console.WriteLine("Enter the quantity in " + myIng.UnitEntity.ToString());
                                int quantity = ConsoleReader.getValidInt();
                                RecipeIngredient recipeIngredient = new RecipeIngredient
                                {
                                    Quantity = quantity,
                                    Ingredient = myIng
                                };
                                recipeIngredients.Add(recipeIngredient);
                            }

                            Console.WriteLine("Enter the amount of servings");
                            int recipeServings = ConsoleReader.getValidInt();
                            try
                            {
                                recipeService.CreateRecipeDB(recipeName, currentMember, recipeDescription, recipePreparationTime, recipeCookingTime, recipeInstructions, recipeTags, recipeServings, recipeIngredients);
                                Console.WriteLine("The new recipe is created!");
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("One of the field contains invalid input! Therefore, the recipe isn't created: " + e);
                            }
                            ConsoleWriter.ClearWithEnter();
                            break;
                        case "Search Recipe":
                            bigAction = false;
                            do
                            {
                                Console.WriteLine("How do you want to search?");
                                userActions = new List<string> { "Recipe name", "Username", "Description", "Time", "Rating", "Serving", "Ingredient", "Tag", "Show All Recipes", "Show All Ingredients", "Exit" };
                                ConsoleWriter.DisplayOptions(userActions);
                                action = ConsoleReader.getValidStringFromOptions(userActions);
                                List<Recipe> recipelist = new List<Recipe>();
                                switch (action)
                                {
                                    case "Recipe name":
                                        Console.WriteLine("Enter the recipe name");
                                        string input = ConsoleReader.getValidString();
                                        recipelist = recipeService.SearchByName(input);
                                        break;
                                    case "Username":
                                        Console.WriteLine("Enter the user's name");
                                        string user = ConsoleReader.getValidString();
                                        recipelist = recipeService.SearchByUser(user);
                                        break;
                                    case "Description":
                                        Console.WriteLine("Enter the description");
                                        string description = ConsoleReader.getValidString();
                                        recipelist = recipeService.SearchByDescription(description);
                                        break;
                                    case "Time":
                                        Console.WriteLine("Enter the maximum time");
                                        int time = ConsoleReader.getValidInt();
                                        recipelist = recipeService.SearchByTime(time);
                                        break;
                                    case "Rating":
                                        Console.WriteLine("Enter the minimum rating (1-5)");
                                        int byRating = ConsoleReader.getValidInt();
                                        recipelist = recipeService.SearchByRating(byRating);
                                        break;
                                    case "Serving":
                                        Console.WriteLine("Enter the minimum servings (min 1)");
                                        int servings = ConsoleReader.getValidInt();
                                        recipelist = recipeService.SearchByServings(servings);
                                        break;
                                    case "Ingredient":
                                        Console.WriteLine("How many ingredients would you like to have?");
                                        validNum = false;
                                        numIng = 0;
                                        while (!validNum)
                                        {
                                            numIng = ConsoleReader.getValidInt();
                                            if (numIng <= 0)
                                            {
                                                Console.WriteLine("You need to at least have 1 ingredient. Try again.");
                                            }
                                            else
                                            {
                                                validNum = true;
                                            }
                                        }
                                        ingredients = ingredientService.GetAllIngredientsDB();
                                        ingredientNames = new List<string>();
                                        ingredientToSearch = new List<string>();
                                        foreach (Ingredient ing in ingredients)
                                        {
                                            ingredientNames.Add(ing.Name);
                                        }
                                        for (int i = 0; i < numIng; i++)
                                        {
                                            Console.WriteLine("Ingredient #" + i + ":");
                                            ConsoleWriter.DisplayOptions(ingredientNames);
                                            int pos = ConsoleReader.getValidStringFromOptionsReturnPosition(ingredientNames);
                                            ingredientToSearch.Add(ingredientNames[pos]);
                                        }
                                        if (numIng == 1)
                                        {
                                            recipelist = recipeService.SearchByExactIngredients(ingredientToSearch);
                                        }
                                        else
                                        {
                                            recipelist = recipeService.SearchByIngredients(ingredientToSearch);
                                        }
                                        break;
                                    case "Tag":
                                        Console.WriteLine("Enter a list of tags, type 'done' when you have finished entering everything");
                                        List<Tag> tags = new List<Tag>();
                                        bool isTagsDone = false;
                                        while (isTagsDone == false)
                                        {
                                            string tag = ConsoleReader.getValidString();
                                            if (tag.Equals("done"))
                                            {
                                                isTagsDone = true;
                                            }
                                            else
                                            {
                                                tags.Add(new Tag(tag));
                                            }
                                        }

                                        recipelist = recipeService.SearchByTag(tags);
                                        break;
                                    case "Show All Recipes":
                                        recipelist = recipeService.getAllRecipesDB();
                                        break;
                                    case "Show All Ingredients":
                                        List<Ingredient> ings = ingredientService.GetAllIngredientsDB();
                                        Console.WriteLine("------------- ALL INGREDIENTS-----------");
                                        foreach (Ingredient ing in ings)
                                        {
                                            Console.WriteLine("----------------------------------------");
                                            Console.WriteLine(ing);
                                        }
                                        break;
                                    case "Exit":
                                        bigAction = true;
                                        break;
                                }

                                if (bigAction == false)
                                {
                                    foreach (Recipe recipe in recipelist)
                                    {
                                        Console.WriteLine("-------------------");
                                        Console.WriteLine(recipe);
                                        memberService.AddSearchRecipe(recipe, currentMember);
                                    }
                                    ConsoleWriter.ClearWithEnter();
                                }
                                else
                                {
                                    ConsoleWriter.ClearWithEnter();
                                }
                            } while (!bigAction);

                            break;
                        case "View My Account":
                            Console.WriteLine(currentMember);
                            ConsoleWriter.ClearWithEnter();
                            break;
                        case "View My Recent Searches":
                            Console.WriteLine(currentMember.ListRecentSearches());
                            ConsoleWriter.ClearWithEnter();
                            break;
                        case "My Recipe(s) Modification":
                            ///----------------------
                            ///UPDATE RECIPE SECTION
                            ///----------------------
                            ///
                            Console.Clear();
                            if (currentMember.OwnedRecipes == null)
                            {
                                throw new ArgumentNullException("The owned recipes of the current member is null!");
                            }
                            List<Recipe> myRecipes = currentMember.OwnedRecipes;
                            List<string> myRecipeNames = new List<string>();
                            foreach (Recipe myRecipe in myRecipes)
                            {
                                myRecipeNames.Add(myRecipe.Name);
                            }
                            if (myRecipeNames.Count == 0)
                            {
                                Console.WriteLine("You currently don't owe any recipes!");
                                ConsoleWriter.ClearWithEnter();
                                break;
                            }
                            ConsoleWriter.DisplayOptions(myRecipeNames);
                            int position = ConsoleReader.getValidStringFromOptionsReturnPosition(myRecipeNames);
                            Recipe recipeToUpdate = myRecipes[position];
                            bool goback = false;
                            do
                            {
                                userActions = new List<string>{"Update the recipe name",
                            "Update the recipe description",
                            "Update the recipe preparation time",
                            "Update the recipe cooking time",
                            "Update the recipe servings",
                            "Update the recipe instructions",
                            "Update the recipe tags",
                            "Update the recipe ingredients",
                            "Go Back"
                            };
                                Console.Clear();
                                ConsoleWriter.DisplayOptions(userActions);
                                action = ConsoleReader.getValidStringFromOptions(userActions);
                                switch (action)
                                {
                                    case "Update the recipe name":
                                        Console.WriteLine("Enter the new recipe name");
                                        string name = ConsoleReader.getValidString();
                                        recipeService.UpdateRecipeNameDB(name, recipeToUpdate);
                                        break;
                                    case "Update the recipe description":
                                        Console.WriteLine("Enter the new recipe description");
                                        string description = ConsoleReader.getValidString();
                                        recipeService.UpdateRecipeDescriptionDB(description, recipeToUpdate);
                                        break;
                                    case "Update the recipe preparation time":
                                        Console.WriteLine("Enter the new recipe preparation time");
                                        double preparationTime = ConsoleReader.getValidInt();
                                        recipeService.UpdatePreparationTimeDB(preparationTime, recipeToUpdate);
                                        break;
                                    case "Update the recipe cooking time":
                                        Console.WriteLine("Enter the new recipe cooking time");
                                        double cookingTime = ConsoleReader.getValidInt();
                                        recipeService.UpdateCookingTimeDB(cookingTime, recipeToUpdate);
                                        break;
                                    case "Update the recipe servings":
                                        Console.WriteLine("Enter the new recipe servings");
                                        int servings = ConsoleReader.getValidInt();
                                        recipeService.UpdateRecipeServingDB(servings, recipeToUpdate);
                                        break;
                                    case "Update the recipe instructions":
                                        Console.WriteLine("How many steps of instruction does your recipe have ?");
                                        steps = ConsoleReader.getValidInt();
                                        recipeInstructions = new List<Instruction>();
                                        for (int i = 0; i < steps; i++)
                                        {
                                            Console.WriteLine("Enter step " + (i + 1) + "'s instruction");
                                            string instruction = ConsoleReader.getValidString();
                                            recipeInstructions.Add(new Instruction(instruction));
                                        }
                                        recipeService.UpdateRecipeInstructionsDB(recipeInstructions, recipeToUpdate);
                                        break;
                                    case "Update the recipe tags":
                                        Console.WriteLine("How many tags does your recipe have ?");
                                        qtyTags = ConsoleReader.getValidInt();
                                        recipeTags = new List<Tag>();
                                        for (int i = 0; i < qtyTags; i++)
                                        {
                                            Console.WriteLine("Enter tag #" + (i + 1));
                                            string tag = ConsoleReader.getValidString();
                                            recipeTags.Add(new Tag(tag));
                                        }
                                        recipeService.UpdateRecipeTagsDB(recipeTags, recipeToUpdate);
                                        break;
                                    case "Update the recipe ingredients":
                                        Console.WriteLine("How many ingredients would you like to have?");
                                        validNum = false;
                                        numIng = 0;
                                        while (!validNum)
                                        {
                                            numIng = ConsoleReader.getValidInt();
                                            if (numIng <= 0)
                                            {
                                                Console.WriteLine("You need to at least have 1 ingredient to create a recipe. Try again.");
                                            }
                                            else
                                            {
                                                validNum = true;
                                            }
                                        }
                                        ingredients = ingredientService.GetAllIngredientsDB();
                                        ingredientNames = new List<string>();
                                        foreach (Ingredient ing in ingredients)
                                        {
                                            ingredientNames.Add(ing.Name);
                                        }
                                        recipeIngredients = new List<RecipeIngredient>();

                                        for (int i = 0; i < numIng; i++)
                                        {
                                            Console.WriteLine("Ingredient #" + (i + 1) + ":");
                                            ConsoleWriter.DisplayOptions(ingredientNames);
                                            int pos = ConsoleReader.getValidStringFromOptionsReturnPosition(ingredientNames);
                                            Ingredient myIng = ingredients[pos];

                                            Console.WriteLine("Enter the quantity in " + myIng.UnitEntity.ToString());
                                            int quantity = ConsoleReader.getValidInt();
                                            RecipeIngredient recipeIngredient = new RecipeIngredient
                                            {
                                                Quantity = quantity,
                                                Ingredient = myIng
                                            };
                                            recipeIngredients.Add(recipeIngredient);
                                        }
                                        recipeService.UpdateRecipeIngredientsDB(recipeIngredients, recipeToUpdate);
                                        break;
                                    case "Go Back":
                                        goback = true;
                                        break;
                                }
                                ConsoleWriter.ClearWithEnter();
                            } while (!goback);
                            break;
                        case "Account Modification":
                            userActions = new List<string> { "Change Password", "Delete My Account", "Exit" };
                            while (!bigAction)
                            {
                                ConsoleWriter.DisplayOptions(userActions);
                                action = ConsoleReader.getValidStringFromOptions(userActions);
                                switch (action)
                                {
                                    case "Change Password":
                                        Console.WriteLine("Enter the new password!");
                                        string newPassword = ConsoleReader.getValidString();
                                        memberService.UpdatePassword(newPassword, currentMember);
                                        ConsoleWriter.ClearWithEnter();
                                        break;
                                    case "Delete My Account":
                                        Console.WriteLine("Are you sure? This cannot be undone.");
                                        userActions = new List<string> { "Yes", "No" };
                                        ConsoleWriter.DisplayOptions(userActions);
                                        action = ConsoleReader.getValidStringFromOptions(userActions);
                                        switch (action)
                                        {
                                            case "Yes":
                                                memberService.DeleteMyAccount(currentMember);
                                                ConsoleWriter.ClearWithEnter();
                                                bigAction = true;
                                                exit = true;
                                                break;
                                            case "No":
                                                Console.WriteLine("You cancelled your account deletion!");
                                                ConsoleWriter.ClearWithEnter();
                                                break;
                                        }
                                        break;
                                    case "Exit":
                                        bigAction = true;
                                        break;
                                }
                            }
                            break;
                        case "Favorite Recipe List Actions":
                            ///----------------------
                            ///MODIFY/VIEW YOUR FAVORITE RECIPE LIST SECTION
                            ///----------------------
                            ///
                            bigAction = false;
                            while (!bigAction)
                            {
                                Console.Clear();
                                userActions = new List<string> { "Add a recipe to your favorite list", "Delete a recipe from your favorite list", "Show your favorite list", "View Another Member's Favourite list", "View Members Who Favorite'd A Specific Recipe", "Go Back" };
                                ConsoleWriter.DisplayOptions(userActions);
                                action = ConsoleReader.getValidStringFromOptions(userActions);
                                switch (action)
                                {
                                    case "Add a recipe to your favorite list":
                                        List<Recipe> listRecipes = recipeService.getAllRecipesDB();
                                        List<string> listRecipeNames = new List<string>();
                                        foreach (Recipe recipe in listRecipes)
                                        {
                                            listRecipeNames.Add(recipe.Name);
                                        }
                                        ConsoleWriter.DisplayOptions(listRecipeNames);
                                        Console.WriteLine("Enter which recipe you would like to add to your favourites!");
                                        string nameSelected = ConsoleReader.getValidStringFromOptions(listRecipeNames);
                                        foreach (Recipe recipe in listRecipes)
                                        {
                                            if (nameSelected == recipe.Name)
                                            {
                                                try
                                                {
                                                    memberService.AddFavRecipe(recipe, currentMember);
                                                    Console.WriteLine("You have added a favourite recipe successfully!");
                                                }
                                                catch (ArgumentException)
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("This recipe is already in the favorite list!");
                                                }
                                            }
                                        }
                                        ConsoleWriter.ClearWithEnter();
                                        break;
                                    case "Delete a recipe from your favorite list":
                                        if (currentMember.FavoriteRecipes != null)
                                        {
                                            List<FavoriteRecipe> favoriteRecipes = currentMember.FavoriteRecipes;
                                            List<string> favoriteRecipesNames = new List<string>();
                                            foreach (FavoriteRecipe favRecipe in favoriteRecipes)
                                            {
                                                if (favRecipe.Recipe != null)
                                                {
                                                    favoriteRecipesNames.Add(favRecipe.Recipe.Name);
                                                }
                                            }
                                            ConsoleWriter.DisplayOptions(favoriteRecipesNames);
                                            int recipePosition = ConsoleReader.getValidStringFromOptionsReturnPosition(favoriteRecipesNames);
                                            Console.WriteLine("------------------------");
                                            Console.WriteLine("Enter which recipe you would like to delete!");
                                            currentMember.RemoveFavorite(recipePosition);
                                            Console.Clear();
                                            Console.WriteLine("You have succesfully removed the recipe from your list!");
                                        }
                                        else
                                        {
                                            throw new ArgumentNullException("The currentMember favoriteRecipes field cannot be null!");
                                        }
                                        ConsoleWriter.ClearWithEnter();
                                        break;
                                    case "Show your favorite list":
                                        memberService.ListFavoriteRecipes(currentMember);
                                        ConsoleWriter.ClearWithEnter();
                                        break;
                                    case "View Another Member's Favourite list":
                                        List<Member> members = memberService.GetAllMembersDB();
                                        List<string> memberUsernames = new List<string>();
                                        foreach (Member m in members)
                                        {
                                            if (m.Username != null)
                                            {
                                                memberUsernames.Add(m.Username);
                                            }
                                        }
                                        ConsoleWriter.DisplayOptions(memberUsernames);
                                        int pos = ConsoleReader.getValidStringFromOptionsReturnPosition(memberUsernames);
                                        Member memberToSpy = members[pos];
                                        Console.WriteLine(memberToSpy.ListFavorite());
                                        ConsoleWriter.ClearWithEnter();
                                        break;
                                    case "View Members Who Favorite'd A Specific Recipe":
                                        Console.Clear();
                                        List<Recipe> recipes = recipeService.getAllRecipesDB();
                                        List<string> recipeNames = new List<string>();
                                        foreach (Recipe r in recipes)
                                        {
                                            recipeNames.Add(r.Name);
                                        }
                                        ConsoleWriter.DisplayOptions(recipeNames);
                                        pos = ConsoleReader.getValidStringFromOptionsReturnPosition(recipeNames);
                                        Recipe recipeToSearch = recipes[pos];
                                        Console.WriteLine("--- Members who favorite'd - " + recipeToSearch.Name + "- ---");
                                        List<Member> membersWhoFav = recipeService.GetMembersWhoFavorited(recipeToSearch);
                                        int memberNum = 1;
                                        foreach (Member m in membersWhoFav)
                                        {
                                            Console.WriteLine("------- Member #" + memberNum + "--------");
                                            memberNum++;
                                            Console.WriteLine(m);
                                        }
                                        ConsoleWriter.ClearWithEnter();
                                        break;
                                    case "Go Back":
                                        bigAction = true;
                                        break;
                                }
                            }
                            break;
                        case "Exit and Log Out":
                            exit = true;
                            break;
                    }
                } while (exit != true);
                Console.WriteLine("You logged out successfully!");
                ConsoleWriter.ClearWithEnter();
            }
        }

        Console.WriteLine("Thank you for using AmHangry!");
    }
}







