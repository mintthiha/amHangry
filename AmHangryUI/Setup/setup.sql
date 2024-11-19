Drop TABLE "A2245136"."Members" CASCADE CONSTRAINTS;
Drop TABLE "A2245136"."Admins" CASCADE CONSTRAINTS;
Drop TABLE "A2245136"."Ingredients" CASCADE CONSTRAINTS;
Drop TABLE "A2245136"."Instructions" CASCADE CONSTRAINTS;
Drop TABLE "A2245136"."Rates" CASCADE CONSTRAINTS;
Drop TABLE "A2245136"."RecentRecipeSearches" CASCADE CONSTRAINTS;
Drop TABLE "A2245136"."RecipeIngredients" CASCADE CONSTRAINTS;
Drop TABLE "A2245136"."Recipes" CASCADE CONSTRAINTS;
Drop TABLE "A2245136"."RecipeTag" CASCADE CONSTRAINTS;
Drop TABLE "A2245136"."Tags" CASCADE CONSTRAINTS;
Drop TABLE "A2245136"."FavoriteRecipes" CASCADE CONSTRAINTS;
Drop TABLE "A2245136"."UnitEntities" CASCADE CONSTRAINTS;
Drop TABLE "A2245136"."__EFMigrationsHistory" CASCADE CONSTRAINTS;

-- Change the ID to yours when dropping

-- STEPS TO SETUP!!

-- FIRST drop all tables, I just copy pasted these into sql developer

-- SECCOND go into the amHangry folder (Milestone 3), and then run 'dotnet ef migrations add InitialCreate'

-- NOTE: You may want to delete the folder named 'Migrations' when faced the error "The name 'InitialCreate' is used by an existing migration."

-- THIRD run 'dotnet ef database update'

-- FOURTH go into AmHangryUI, comment out 'Program.cs' and uncomment out 'setup.cs'

-- FIFTH run 'dotnet run', verify if the changes are there

-- FINALLY, comment out setup.cs, uncomment Program.cs and we are now good to go!