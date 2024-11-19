public class ConsoleReader
{

    /*
    * This method takes in the options the user can choose. it will loop until either the user enters
    * the option, or the number associated to it.
    */
    public static string getValidStringFromOptions(List<string> options)
    {
        string? input = "";
        bool isValidString = false;
        while (!isValidString)
        {
            input = Console.ReadLine();
            if (int.TryParse(input, out int intInput))
            {
                if (intInput < options.Count() && intInput >= 0)
                {
                    input = options[intInput];
                    isValidString = true;
                }
                else
                {
                    Console.WriteLine("You have entered a number that wasn't displayed. Try again!");
                }
            }
            else
            {
                if (input != null && !options.Contains(input) || input == "")
                {
                    Console.WriteLine("You have given an option that wasn't shown. Try again!");

                }
                else
                {
                    isValidString = true;
                }
            }
        }
        if (input != null)
        {
            return input;
        }
        else
        {
            throw new Exception("The user cannot input a null string!");
        }
    }

    /// <summary>
    /// This method will get a list of strings, and return a valid position.
    /// </summary>
    /// <param name="options"> This parameter is the list of options you want to get a position from.</param>
    /// <returns>Returns valid int</returns>
    public static int getValidStringFromOptionsReturnPosition(List<string> options)
    {
        string? input = "";
        int position = -1;
        bool isValidString = false;
        while (!isValidString)
        {
            input = Console.ReadLine();
            if (int.TryParse(input, out int intInput))
            {
                if (intInput < options.Count() && intInput >= 0)
                {
                    position = intInput;
                    input = options[intInput];
                    isValidString = true;
                }
                else
                {
                    Console.WriteLine("You have entered a number that wasn't displayed. Try again!");
                }
            }
            else
            {
                if (input != null && !options.Contains(input) || input == "")
                {
                    Console.WriteLine("You have given an option that wasn't shown. Try again!");

                }
                else
                {
                    isValidString = true;
                }
            }
        }
        return position;
    }

    /*
    * This method will get a valid string from the user. This string does not have many restrictions,
    * other than the fact that it cannot be null, nor empty.
    */
    public static string getValidString()
    {
        string? input = "";
        bool isValidString = false;
        while (!isValidString)
        {
            input = Console.ReadLine();
            if (input != null || input != "")
            {
                isValidString = true;
            }
            else
            {
                Console.WriteLine("You have given a null or empty string. Try again!");
            }
        }
        if (input != null)
        {
            return input;
        }
        else
        {
            throw new Exception("The string being inputed by the user cannot be null!");
        }
    }

    //Get a valid int from the user.
    public static int getValidInt()
    {
        string? input = "";
        int returnedInt = -1;
        bool iSValidInput = false;
        while (!iSValidInput)
        {
            input = Console.ReadLine();
            if (int.TryParse(input, out int intInput))
            {
                if (intInput > 0)
                {
                    returnedInt = intInput;
                    iSValidInput = true;
                }
                else
                {
                    Console.WriteLine("You have entered a number that's not over 0. Try again!");
                }
            }
            else
            {
                Console.WriteLine("You have entered string, when an Integer is needed!");
            }
        }
        return returnedInt;
    }
}