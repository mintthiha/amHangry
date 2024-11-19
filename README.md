PART 1:

Questions:
•	How will we store the data? (Entity Framework)
•	How will we retrieve data from the web API (do we use c#?)
•	What will we work on first?
•	How will we code the UI in C# (Avalonia)
•	How will the user navigate the program? 
•	How much data needs to be retrieved
•	Should certain features be locked behind privileges?
•	Should the user created recipes have validation? (Joke recipes, Single ingredient recipes, etc)
•	How do we keep the pictures of the users.


PART 2:

Teamwork Agreements Answers:

o	What will you do to ensure your code is readable? For example, will you share your code frequently with your partner? Setup Visual Studio Code’s auto formatter consistently across group members.
-   In order to make our code more readable, we will add comments, make sure to have good indentations, implement good variable names and finally having good documentations.

o	What procedure will you put in place to ensure committed code is functional?
-   Before committing, we need to make sure the code runs perfectly and without errors. We will also communicate the changes we have done with methods or classes, so we will not be suprised if functionality changes.

While working on certain branches with others, we will specify and be conscice on which part we are working on so things will be smoother while we commit to branches.

o	How do you plan to test your code?
-   In order to test our code, we will use the debugger and the MS Testing.

o	How do you plan to divide the work?
-   For now, every member is assigned to a different class/part to work on, and then later on, we will give other tasks, for instance the database and the UI.

o	How will you ensure that your application is robust and does not fail due to user errors?
-   To ensure our application is robust, we will perform data validation and exception handling, which can be done with helper methods. THis will also prevent user errors.

o	How will you ensure you have stand alone classes can be tested?
-   We can use "Friend Assembly" so that the test class can see the internals, which will allow us to perform the testings. Also, we can try to design our methods and classes to have public and internal headers.

o	Are there any other guidelines or expectations that you would like to ask of one another?
-   Commiting often, so we don't have to deal with big merge conflicts. We also should put about an hour and a half every week, but we may scale it as we go, depending on the workload. Attending lab times so we can have time to work together.

Extra Features
-   Estimate of portion and how long those will last you.
EX: ( portions of pizza will last you abt a week). The scope is for the recipes only.
-   Allows you to scale amount of servings and ingredients. For instance, I want to add more servings, this will increase the ingredients as well. The scope for this is also only for the recipes.
-   History feature, allows to log recipes used and recipes used most often. For instance, if I am a user, every recipe I use will be logged, and there will be a table where it shows the recipes that I used the most often in the past. The scope is once again for the recipe only.
-   Sorting by X thing. We can show all the recipes sorted by the name, by the category, by time added, etc. The scope of this is the main, or the main class that handles the logic.
-   Money Conversion and ingredient conversion. For example, the user wants to see the weight as kgs rather than lbs, or the volume being in litters or cubic meters. The scope of this is again for the recipes.
-   Having the option to log in as a user or an admin. So this way the admin account may have priviledges and options that the user does not have. The scope will be for the users.


PART 3:

o	An explanation of some of your design decisions inside the project’s README. 
-   We will have a namespace for all the recipe related things, so for recipe, ingredient, etc. The user being an interface so we can potentially make an admin. Using Seperation of concerns, we can have a folder called Logic where all the different operations to fetch data like sorting and searching can be placed. We will have a different folder for the unit testing, another one for the database and finally one for the UI.

-   We'll be using the MVC pattern to build our application. Our models for now are the 'Recipe','User', and 'Ingredient' classes, which will represent the data of our application. We'll have multiple controller classes that will manipulate the models and interact with the view (UI/main) application. Each controller class will use an interface to implements its own methods to retrieve and save specific data from/to the database. As we develop the application, we might add more models or controllers if needed as we encounter issues.