public static class ArraySelector
{
    public static void Run()
    {
        var l1 = new[] { 1, 2, 3, 4, 5 };
        var l2 = new[] { 2, 4, 6, 8, 10 };
        var select = new[] { 1, 1, 1, 2, 2, 1, 2, 2, 2, 1 };
        var intResult = ListSelector(l1, l2, select);
        Console.WriteLine("<int[]>{" + string.Join(", ", intResult) + "}"); // <int[]>{1, 2, 3, 2, 4, 4, 6, 8, 10, 5}
    }

    private static int[] ListSelector(int[] list1, int[] list2, int[] select)
    {
        //Problem 2
        int[] result = new int[select.Length];
        int l1index = 0; //starting index is 0
        int l2index = 0; //starting index is 0

        for (int i = 0; i < select.Length; i++)
        {
            int nextValue;

            if (select[i] == 1)
            {
                nextValue = list1[l1index];
                l1index++; //Put after finding value so it will start at 0, then increase by 1 for the next index
            }
            else //Don't need to specify 2 because if it's not pulling from list 1 it will be list 2
            {
                nextValue = list2[l2index];
                l2index++; 
            }

            result[i] = nextValue;
        }
        return result;
    }       
    
}
