public class RecipeIngredient
{
    public int RecipeIngredientId { get; set; }
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; } = null!;
    public int IngredientId { get; set; }

    //properties

    /// <summary>
    /// Gets or sets the quantity (in grams/kg/ml/L) of the ingredient.
    /// </summary>
    public double Quantity { get; set; }

    /// <summary>
    /// Gets or sets the ingredient object.
    /// </summary>
    public Ingredient Ingredient { get; set; } = null!;

    /// <summary>
    /// Gets Price of the ingredient with specified quantity
    /// </summary>
    public double Price => GetTotalPrice();

    /// <summary>
    /// Gets Proteins of the ingredient with specified quantity
    /// </summary>
    public double Proteins => GetTotalProteins();

    /// <summary>
    /// Gets Fats of the ingredient with specified quantity
    /// </summary>
    public double Fats => GetTotalFats();

    /// <summary>
    /// Gets Carbs of the ingredient with specified quantity
    /// </summary>
    public double Carbs => GetTotalCarbs();

    /// <summary>
    /// Gets Calories of the ingredient with specified quantity
    /// </summary>
    public double Calories => GetCalories();

    /// <summary>
    /// Calculates and returns the total calories contributed by the ingredient based on its quantity.
    /// </summary>
    /// <returns>The total calories contributed by the ingredient.</returns>
    public int GetCalories()
    {
        if (Ingredient == null)
        {
            throw new ArgumentException("Ingredient cannot be null.");
        }
        return (int)Math.Round((Quantity / Ingredient.Amount) * Ingredient.Calories);
    }

    /// <summary>
    /// Calculates and returns the total proteins contributed by the ingredient based on its quantity.
    /// </summary>
    /// <returns>The total proteins contributed by the ingredient.</returns>
    public int GetTotalProteins()
    {
        if (Ingredient == null)
        {
            throw new ArgumentException("Ingredient cannot be null.");
        }
        return (int)Math.Round((Quantity / Ingredient.Amount) * Ingredient.Protein);
    }

    /// <summary>
    /// Calculates and returns the total fats contributed by the ingredient based on its quantity.
    /// </summary>
    /// <returns>The total fats contributed by the ingredient.</returns>
    public int GetTotalFats()
    {
        if (Ingredient == null)
        {
            throw new ArgumentException("Ingredient cannot be null.");
        }
        return (int)Math.Round((Quantity / Ingredient.Amount) * Ingredient.Fat);
    }

    /// <summary>
    /// Calculates and returns the total carbs contributed by the ingredient based on its quantity.
    /// </summary>
    /// <returns>The total carbs contributed by the ingredient.</returns>
    public int GetTotalCarbs()
    {
        if (Ingredient == null)
        {
            throw new ArgumentException("Ingredient cannot be null.");
        }
        return (int)Math.Round((Quantity / Ingredient.Amount) * Ingredient.Carbs);
    }

    /// <summary>
    /// Calculates and returns the total cost of the ingredient based on its quantity.
    /// </summary>
    /// <returns>The total cost of the ingredient.</returns>
    public double GetTotalPrice()
    {
        if (Ingredient == null)
        {
            throw new ArgumentException("Ingredient cannot be null.");
        }
        return Math.Round((Quantity / Ingredient.Amount) * Ingredient.Cost, 2);
    }
}