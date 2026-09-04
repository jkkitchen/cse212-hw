public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start

        //Create an array to hold the multiples generated in this function
        double[] multiples = new double[length];

        //Create for loop that will generate the multiples of the number and store them in the array
        for (int i = 0; i < length; i++)
        {
            //store the multiple of the number in the array at index i  
            //multiply the number by (i+1) because the indices start at 0, but the multiples need to start at 1 (i.e. at index 0, multiple will be 1 x number)
            multiples[i] = number * (i + 1);
        }

        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start

        //Create a new list to hold the rotated values with the same number of values as the original list
        List<int> rotatedList = new List<int>(new int[data.Count]);
        //Create a for loop that will iterate the same number of times as the length of the list (determined using .count) to create a new value for each index
        for (int i = 0; i < data.Count; i++)
        {
            //Use if statements to assign new indices to each value in the list based on the amount of rotation
            //Anything at an index that is less than the length of the list minus the amount of rotation will not need to wrap around 
            // and can just have the amount added
            if (i < (data.Count - amount))
            {
                int newIndex = i + amount;
                rotatedList[newIndex] = data[i];
            }
            //Anything at an index that is greater than or equal to the length of the list minus the amount of rotation will need 
            // to wrap around which means adding the amount then subtracting the length of the list
            else
            {
                int newIndex = i - (data.Count - amount);
                rotatedList[newIndex] = data[i];
            }
        }

        //Replace values of the original list with the values of the rotated list
        //this has to be done in a separate loop because the original list is being used to fill the replacement list in the previous loop
        for (int i = 0; i < data.Count; i++)
        {
            data[i] = rotatedList[i]; 
        }
    }
}
