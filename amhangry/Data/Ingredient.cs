public class Ingredient
{
    //fields 
    private string _name= string.Empty;
    private string _category =string.Empty;
    private int _protein;
    private int _fat;
    private int _carbs;
    private double _cost;
    private double _amount;
    private const int CHARACTER_LIMIT = 50;
    private const int MAX_PROTEIN = 200;
    private const int MAX_FAT = 200;
    private const int MAX_CARBS = 200;

    public int IngredientId { get; set; }

    public int UnitEntityId { get; set; } // Foreign key for UnitEntity

    public UnitEntity UnitEntity { get; set; } // Navigation property for UnitEntity
    public List<RecipeIngredient> RecipeIngredients { get; } = new List<RecipeIngredient>(); //Nav property for RecipeIngredient

    public Ingredient()
    {
        _name = "";
        _category = "";
        UnitEntity = new UnitEntity();
    }

    public Ingredient(string name, string category, int protein, int fat, int carbs, double cost, double amount, UnitEntity unit)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(category) || protein < 0 || fat < 0 || cost < 0 || carbs < 0 || !Enum.IsDefined(typeof(Unit), unit.Unit))
        {
            throw new ArgumentNullException("Error in the ingredient constructor. Either the fields are null or under 0!");
        }

        if (amount <= 0)
        {
            throw new ArgumentException("The Amount field needs to be higher than 0!");
        }
        Name = name;
        Category = category;
        Protein = protein;
        Fat = fat;
        Carbs = carbs;
        Cost = cost;
        Amount = amount;
        UnitEntity = unit;
    }

    /// <summary>
    /// Gets or sets the name of the ingredient.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when the name is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when the name exceeds the character limit.</exception>
    public string Name
    {
        get { return _name; }
        set
        {
            CheckIfNullOrEmpty(value, "name");
            CheckIfPassCharacterLimit(value, "name");
            _name = value;
        }
    }

    /// <summary>
    /// Gets or sets the category of the ingredient.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown when the category is null or empty.</exception>
    /// <exception cref="ArgumentException">Thrown when the category exceeds the character limit.</exception>
    public string Category
    {
        get { return _category; }
        set
        {
            CheckIfNullOrEmpty(value, "category");
            CheckIfPassCharacterLimit(value, "category");
            _category = value;
        }
    }

    /// <summary>
    /// Gets or sets the protein content of the ingredient in grams.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the protein is less than 0 or above MAX_PROTEIN .</exception>
    public int Protein
    {
        get { return _protein; }
        set
        {
            CheckIfNegativeNumberInt(value, "protein");
            if (value > MAX_PROTEIN)
            {
                throw new ArgumentException("The protein must be lower than " + MAX_PROTEIN + " grams for safety concern");
            }
            _protein = value;
        }
    }

    /// <summary>
    /// Gets or sets the fat content of the ingredient in grams.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the fat is less than 0 or above MAX_FAT .</exception>
    public int Fat
    {
        get { return _fat; }
        set
        {
            CheckIfNegativeNumberInt(value, "fat");
            if (value > MAX_FAT)
            {
                throw new ArgumentException("The fat must be lower than " + MAX_FAT + " grams for safety concern");
            }
            _fat = value;
        }
    }

    /// <summary>
    /// Gets or sets the carbohydrate content of the ingredient in grams.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the carbohydrate is less than 0 or above MAX_CARBS .</exception>
    public int Carbs
    {
        get { return _carbs; }
        set
        {
            CheckIfNegativeNumberInt(value, "carbs");
            if (value > MAX_CARBS)
            {
                throw new ArgumentException("The carbs must be lower than " + MAX_CARBS + " grams for safety concern");
            }
            _carbs = value;
        }
    }

    /// <summary>
    /// Gets or sets the amount (grams/kg/ml/L) of the ingredient.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the amount is less than or equal to 0.</exception>
    public double Amount
    {
        get { return _amount; }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("The amount field cannot be 0 or negative!");
            }
            _amount = value;
        }
    }

    /// <summary>
    /// Gets or sets the cost of the ingredient.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the cost is less than 0.</exception>
    public double Cost
    {
        get { return _cost; }
        set
        {
            CheckIfNegativeNumberDouble(value, "cost");
            _cost = value;
        }

    }

    /// <summary>
    /// Gets or sets the unit of measurement for the ingredient's amount.
    /// </summary>
    /// <exception cref="ArgumentException">Thrown when the specified unit is not a valid value of the <see cref="Unit"/> enum.</exception>
    // public UnitEntity IngredientUnit
    // {
    //     get { return _unit; }
    //     set
    //     {

    //         if (Enum.IsDefined(typeof(Unit), value)) // Check if the value is a valid Unit enum value
    //         {
    //             _unit = value;
    //         }
    //         else
    //         {
    //             throw new ArgumentException("Error setting up the unit field");
    //         }

    //     }
    // }

    /// <summary>
    /// Gets the total calories of the ingredient.
    /// </summary>
    public int Calories
    {
        get
        {
            int proteinCalories = Protein * 4;
            int fatCalories = Fat * 9;
            int carbCalories = Carbs * 4;

            return proteinCalories + fatCalories + carbCalories;
        }
    }

    /// <summary>
    /// Calculates and returns the converted amount of the ingredient to a different unit of measurement.
    /// </summary>
    /// <remarks>
    /// - Grams to kilograms (1000 grams = 1000 / 1000 = 1kg)
    /// - Kilograms to grams (1 kg = 1 * 1000 = 1000 grams)
    /// - Milliliters to liters (1000 milliliters = 1000 / 1000 = 1 liter)
    /// - Liters to milliliters (1 liter = 1 * 1000 = 1000 milliliters)
    /// </remarks>
    /// <returns>The converted amount of the ingredient.</returns>
    /// <exception cref="ArgumentException">Thrown when the specified unit is not a valid value of the <see cref="Unit"/> enum.</exception>
    public double UnitConversion()
    {
        switch (UnitEntity.Unit)
        {
            case Unit.Gram:
                return Math.Round(_amount / 1000, 2); // round to 2 decimal

            case Unit.Kilogram:
                return _amount * 1000;

            case Unit.Milliliter:
                return Math.Round(_amount / 1000, 2);

            case Unit.Liter:
                return _amount * 1000;

            default:
                throw new ArgumentException("Invalid unit in database");
        }
    }

    //Helper methods

    /// <summary>
    /// Checks if the specified int value is negative and throws an exception if it is.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="field">The name of the field being checked.</param>
    /// <exception cref="ArgumentException">Thrown when the specified value is negative.</exception>
    private void CheckIfNegativeNumberInt(int value, string field)
    {
        if (value < 0)
        {
            throw new ArgumentException("The " + field + " field cannot be negative!");
        }
    }

    /// <summary>
    /// Checks if the specified double value is negative and throws an exception if it is.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="field">The name of the field being checked.</param>
    /// <exception cref="ArgumentException">Thrown when the specified value is negative.</exception>
    private void CheckIfNegativeNumberDouble(double value, string field)
    {
        if (value < 0)
        {
            throw new ArgumentException("The " + field + " field cannot be negative!");
        }
    }

    /// <summary>
    /// Checks if the specified value is null or empty and throws an exception if it is.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="field">The name of the field being checked.</param>
    /// <exception cref="ArgumentException">Thrown when the specified value is null or empty.</exception>
    private void CheckIfNullOrEmpty(string value, string field)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("The " + field + " of an ingredient cannot be null, nor empty!");
        }
    }

    /// <summary>
    /// Checks if the specified value exceeds the character limit and throws an exception if it does.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="field">The name of the field being checked.</param>
    /// <exception cref="ArgumentException">Thrown when the specified value exceeds the character limit.</exception>
    private void CheckIfPassCharacterLimit(string value, string field)
    {
        if (value.Length > CHARACTER_LIMIT)
        {
            throw new ArgumentException("The " + field + " of an ingredient cannot pass the character limit of : " + CHARACTER_LIMIT);
        }
    }

    public override string ToString()
    {
        string builder = "";
        builder += " ------ " + _name + "------\n";
        builder += "Category: " + _category + "\n";
        builder += "Protein: " + _protein + "\n";
        builder += "Fat: " + _fat + "\n";
        builder += "Carb: " + _carbs + "\n";
        builder += "Calories: " + Calories + "\n";
        builder += "Cost: " + _cost + "$\n";
        builder += "Amount: " + _amount + " " + UnitEntity + "\n";
        return builder;
    }

}