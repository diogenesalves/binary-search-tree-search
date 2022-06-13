
var randomGenerator = new Random();

Console.WriteLine("This is a simple binary search!");
Console.WriteLine("Insert the max length of array.");
Console.WriteLine("Input just int values, if inserted value in string or empty, the default value is 100.");
var maxValueOfArray = Console.ReadLine();

if (!int.TryParse(maxValueOfArray, out int max))
    max = 100;

int[] keys = new int[max];

for (int i = 0; i < max; i++)
{
    int num = randomGenerator.Next(1, max * 10);

    while (Array.Exists(keys, key => key == num))
    {
        num = randomGenerator.Next(1, max * 10);
    }

    keys[i] = num;
}

Array.Sort(keys);

Console.WriteLine("The keys of array is [" + String.Join(",", keys) + "]");

Console.WriteLine("Insert the value to search:");
var searchValue = Console.ReadLine();

if (!int.TryParse(searchValue, out int key))
    key = -1;

int position = Work.BinarySearch(keys, keys.Length, key);

if (position > -1)
    Console.WriteLine("Value foud :) at position " + position);
else
    Console.WriteLine("Value not found :(");

class Work
{
    /// <summary>
    /// Simple binary search
    /// </summary>
    /// <param name="vector">Array with values</param>
    /// <param name="logicalLength">Logical length of array</param>
    /// <param name="key">Value to search in array</param>
    /// <returns></returns>
    public static int BinarySearch(int[] vector, int logicalLength, int key)
    {
        int low = 0;
        int hight = logicalLength - 1;

        do
        {
            int middle = (low + hight) / 2;

            if (vector[middle] == key)
                return middle;
            else if (vector[middle] < key)
                low = middle + 1;
            else
                hight = middle - 1;

        } while (low <= hight);

        return -1;
    }
}