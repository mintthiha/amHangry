
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

public class IngredientController
{
    private Ingredient? _currentIngredient;

    private List<Ingredient>? _ingredients;

    private RecipeContext _context;

    public List<Ingredient> Ingredients
    {

        get
        {
            if (_ingredients != null)
            {
                return _ingredients;
            }
            else
            {
                throw new Exception("The list of ingredients is null!");
            }
        }
        set
        {
            _ingredients = value;
        }
    }
    public Ingredient CurrentIngredient
    {

        get
        {
            if (_currentIngredient != null)
            {
                return _currentIngredient;
            }
            else
            {
                throw new Exception("The current ingredient is null!");
            }
        }
        set
        {
            _currentIngredient = value;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="IngredientController"/> class with a specified context.
    /// </summary>
    /// <param name="recipeContext">The context.</param>
    public IngredientController(RecipeContext recipeContext)
    {
        _context = recipeContext;
    }

    /// <summary>
    /// Creates a new ingredient.
    /// </summary>
    /// <param name="name">The name of the ingredient.</param>
    /// <param name="category">The category of the ingredient.</param>
    /// <param name="protein">The protein content of the ingredient.</param>
    /// <param name="fat">The fat content of the ingredient.</param>
    /// <param name="carbs">The carb content of the ingredient.</param>
    /// <param name="cost">The cost of the ingredient.</param>
    /// <param name="amount">The amount of the ingredient.</param>
    /// <param name="unit">The unit of measurement for the ingredient.</param>
    public void CreateIngredient(string name, string category, int protein, int fat,
                                int carbs, double cost, double amount, UnitEntity unit)
    {
        Ingredient newIngredient = new Ingredient(name, category, protein, fat, carbs, cost, amount, unit);
        Ingredients.Add(newIngredient);
    }
    /// <summary>
    /// Sets the current ingredient.
    /// </summary>
    /// <param name="ingredient">The ingredient to set as the current one.</param>
    public void SetCurrentIngredient(Ingredient ingredient)
    {
        CurrentIngredient = ingredient;
    }
    /// <summary>
    /// Retrieves all ingredients from the database.
    /// </summary>
    /// <returns>A list of all ingredients.</returns>
    public List<Ingredient> GetAllIngredientsDB()
    {
        List<Ingredient> allIngredients = _context.Ingredients
        .Include(i => i.UnitEntity)
        .ToList();
        return allIngredients;
    }

    /// <summary>
    /// Updates the name of the current ingredient.
    /// </summary>
    /// <param name="name">The new name for the ingredient.</param>
    public void UpdateIngredientName(string name)
    {
        CurrentIngredient.Name = name;
        _context.SaveChanges();
    }
    /// <summary>
    /// Updates the protein content of the current ingredient.
    /// </summary>
    /// <param name="protein">The new protein content.</param>
    /// <param name="ingredient">The ingredient to update.</param>
    public void UpdateIngredientProtein(int protein, Ingredient ingredient)
    {
        ingredient.Protein = protein;
        _context.SaveChanges();
    }
    /// <summary>
    /// Updates the fat content of the current ingredient.
    /// </summary>
    /// <param name="fat">The new fat content.</param>
    /// <param name="ingredient">The ingredient to update.</param>
    public void UpdateIngredientFat(int fat, Ingredient ingredient)
    {
        ingredient.Fat = fat;
        _context.SaveChanges();
    }
    /// <summary>
    /// Updates the carb content of the current ingredient.
    /// </summary>
    /// <param name="carbs">The new carb content.</param>
    /// <param name="ingredient">The ingredient to update.</param>            
    public void UpdateIngredientCarbs(int carbs, Ingredient ingredient)
    {
        ingredient.Carbs = carbs;
        _context.SaveChanges();
    }
    /// <summary>
    /// Updates the cost of the current ingredient.
    /// </summary>
    /// <param name="cost">The new cost.</param>
    /// <param name="ingredient">The ingredient to update.</param>
    public void UpdateIngredientCost(double cost, Ingredient ingredient)
    {
        ingredient.Cost = cost;
        _context.SaveChanges();
    }
    /// <summary>
    /// Updates the amount of the current ingredient.
    /// </summary>
    /// <param name="amount">The new amount.</param>
    /// <param name="ingredient">The ingredient to update.</param>
    public void UpdateIngredientAmount(double amount, Ingredient ingredient)
    {
        ingredient.Amount = amount;
         _context.SaveChanges();
    }
    /// <summary>
    /// Updates the category of the current ingredient.
    /// </summary>
    /// <param name="category">The new category.</param>
    /// <param name="ingredient">The ingredient to update.</param>
    public void UpdateIngredientCategory(string category, Ingredient ingredient)
    {
        ingredient.Category = category;
        _context.SaveChanges();
    }
    /// <summary>
    /// Updates the name of the current ingredient if it matches another ingredient's name.
    /// </summary>
    /// <param name="name">The new name for the ingredient.</param>
    /// <param name="ingredient">The ingredient to update.</param>
    public void UpdateCurrentIngredientName(string name, Ingredient ingredient)
    {
        bool isExist = false;
        if(ingredient.Name != name)
        {
            try
            {
                Ingredient ing = SearchByExactName(name);
                isExist = true;
            } 
            catch (ArgumentException)
            {
                isExist = false;
            }
        }

        if(isExist)
        {
            throw new ArgumentException("This Ingredient Name is already used!");
        }
        else
        {
            ingredient.Name = name;
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Updates the unit of measurement for the current ingredient.
    /// </summary>
    /// <param name="unit">The new unit of measurement.</param>
    /// <param name="ingredient">The ingredient to update.</param>
    public void UpdateIngredientUnit(UnitEntity unit, Ingredient ingredient)
    {
        ingredient.UnitEntity = unit;
        _context.SaveChanges();
    }

    /// <summary>
    /// Searches for ingredients based on their amount.
    /// </summary>
    /// <param name="amount">The maximum amount of the ingredient.</param>
    /// <returns>A list of ingredients whose amount is less than or equal to the specified amount.</returns>
    public List<Ingredient> SearchByAmount(double amount)
    {
        var ingredientsList = _context.Ingredients
        .Where(i => i.Amount <= amount)
        .ToList();
        if (ingredientsList == null)
        {
            throw new ArgumentException("No ingredients of that amount");
        }
        return ingredientsList;
    }
    /// <summary>
    /// Searches for ingredients based on their cost.
    /// </summary>
    /// <param name="cost">The maximum cost of the ingredient.</param>
    /// <returns>A list of ingredients whose cost is less than or equal to the specified cost.</returns>
    public List<Ingredient> SearchByCost(int cost)
    {
        var ingredientsList = _context.Ingredients
        .Where(i => i.Cost <= cost)
        .ToList();

        if (ingredientsList == null)
        {
            throw new ArgumentException("No ingredients of that unit");
        }
        return ingredientsList;
    }
    /// <summary>
    /// Searches for ingredients based on their carb content.
    /// </summary>
    /// <param name="carbs">The maximum carb content of the ingredient.</param>
    /// <returns>A list of ingredients whose carb content is less than or equal to the specified carb.</returns>
    public List<Ingredient> SearchByCarbs(int carbs)
    {
        var ingredientsList = _context.Ingredients
        .Where(i => i.Carbs <= carbs)
        .ToList();

        if (ingredientsList == null)
        {
            throw new ArgumentException("No ingredients of that unit");
        }
        return ingredientsList;
    }
    /// <summary>
    /// Searches for ingredients based on their fat content.
    /// </summary>
    /// <param name="fat">The maximum fat content of the ingredient.</param>
    /// <returns>A list of ingredients whose fat content is less than or equal to the specified fat.</returns>
    public List<Ingredient> SearchByFat(int fat)
    {
        var ingredientsList = _context.Ingredients
        .Where(i => i.Fat <= fat)
        .ToList();

        if (ingredientsList == null)
        {
            throw new ArgumentException("No ingredients of that unit");
        }
        return ingredientsList;
    }
    /// <summary>
    /// Searches for ingredients based on their protein content.
    /// </summary>
    /// <param name="protein">The maximum protein content of the ingredient.</param>
    /// <returns>A list of ingredients whose protein content is less than or equal to the specified protein.</returns>
    public List<Ingredient> SearchByProtein(int protein)
    {
        var ingredientsList = _context.Ingredients
        .Where(i => i.Protein <= protein)
        .ToList();

        if (ingredientsList == null)
        {
            throw new ArgumentException("No ingredients of that unit");
        }
        return ingredientsList;
    }
    /// <summary>
    /// Searches for ingredients based on their category.
    /// </summary>
    /// <param name="category">The category of the ingredient.</param>
    /// <returns>A list of ingredients that match the specified category.</returns>
    public List<Ingredient> SearchByCategory(string category)
    {
        var ingredientsList = _context.Ingredients
        .Where(i => i.Category == category)
        .ToList();

        if (ingredientsList == null)
        {
            throw new ArgumentException("No ingredients of that unit");
        }
        return ingredientsList;
    }
    /// <summary>
    /// Searches for ingredients based on their unit of measurement.
    /// </summary>
    /// <param name="unit">The unit of measurement for the ingredient.</param>
    /// <returns>A list of ingredients that match the specified unit of measurement.</returns>
    public List<Ingredient> SearchByUnit(UnitEntity unit)
    {
        var ingredientsList = _context.Ingredients
        .Where(i => i.UnitEntity == unit)
        .ToList();

        if (ingredientsList == null)
        {
            throw new ArgumentException("No ingredients of that unit");
        }
        return ingredientsList;
    }
    /// <summary>
    /// Returns the existing unit entity based on the unit parameter.
    /// </summary>
    /// <param name="unit">The unit to search for.</param>
    /// <returns>The existing unit entity if found; otherwise, throws an exception.</returns>
    public UnitEntity ReturnExistingEntitiyUnit(Unit unit)
    {
        var existingUnit = _context.UnitEntities.Where(u => u.Unit == unit).FirstOrDefault();
        if (existingUnit == null)
        {
            throw new ArgumentException("Unit does not exist");
        }
        return existingUnit;
    }

    /// <summary>
    /// Searches for an ingredient by its exact name.
    /// </summary>
    /// <param name="name">The exact name of the ingredient.</param>
    /// <returns>The ingredient with the specified name if found; otherwise, throws an exception.</returns>
    public Ingredient SearchByExactName(string name)
    {
        var ingredientsWithExactName = _context.Ingredients
            .Where(w => w.Name.ToLower().Equals(name.ToLower()))
            .FirstOrDefault();

        if (ingredientsWithExactName == null)
        {
            throw new ArgumentException(name+" doesn't exist in DB");
        }
        return ingredientsWithExactName;
    }
    /// <summary>
    /// Searches for ingredients by a part of their name.
    /// </summary>
    /// <param name="name">Part of the name of the ingredient.</param>
    /// <returns>A list of ingredients whose names contain the specified part.</returns>
    public List<Ingredient> SearchByName(string name)
    {
        var ingredientsList = _context.Ingredients
            .Where(i => i.Name.Contains(name))
            .ToList();
        if (ingredientsList == null)
        {
            throw new ArgumentException("No ingredients of that name");
        }
        return ingredientsList;
    }
    /// <summary>
    /// Creates a new ingredient in the database.
    /// </summary>
    /// <param name="ingredient">The ingredient to create.</param>
    public void CreateIngredient(Ingredient ingredient)
    {
        var existingIngredient = _context.Ingredients
            .FirstOrDefault(i => i.Name == ingredient.Name);
        Console.WriteLine(existingIngredient);
        if (existingIngredient != null)
        {
            throw new ArgumentException("An ingredient with this name already exists.");
        }

        _context.Ingredients.Add(ingredient);
        _context.SaveChanges();
    }
    /// <summary>
    /// Creates a new unit entity in the database.
    /// </summary>
    /// <param name="unit">The unit entity to create.</param>
    public void CreateUnit(UnitEntity unit)
    {
        var existingUnit = _context.UnitEntities.FirstOrDefault(u => u.Unit == unit.Unit);

        if (existingUnit != null)
        {
            throw new ArgumentException("Unit already exist");
        }

        _context.UnitEntities.Add(unit);
        _context.SaveChanges();
    }


    /// <summary>
    /// Deletes an ingredient from the database.
    /// </summary>
    /// <param name="ingredient">The ingredient to delete.</param>
    public void DeleteIngredient(Ingredient ingredient)
    {
        var existingIngredient = _context.Ingredients
            .FirstOrDefault(i => i.Name == ingredient.Name);

        if (existingIngredient != null)
        {
            _context.Ingredients.Remove(ingredient);
            _context.SaveChanges();

        }
        else
        {
            throw new ArgumentException("An ingredient does not exist.");

        }

    }
}