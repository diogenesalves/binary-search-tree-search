
using System.Diagnostics;
using System.Text.Json;

namespace BinarySearch.Search
{
    public class Program
    {
        static async Task Main()
        {
            Stopwatch stopWatchBinarySearch = new();
            Stopwatch stopWatchLinearSearch = new();

            Console.WriteLine("This is a simple binary search!");

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"data/numbers.json");

            var numbers = await Util.ReadAsync(filePath);

            int[] keys = numbers.Numbers;

            Console.WriteLine("Use one of these numbers [ 964022, 517953, 144558, 587583, 502631, 956529, 639705 ]");

            Console.WriteLine("Insert the value to search:");
            var searchValue = Console.ReadLine();

            if (!int.TryParse(searchValue, out int key))
                key = -1;

            stopWatchLinearSearch.Start();
            int positionLinearSearch = await Work.LinearSearch(keys, keys.Length, key);
            stopWatchLinearSearch.Stop();

            TimeSpan timeLinearSearch = stopWatchLinearSearch.Elapsed;

            if (positionLinearSearch > -1)
            {
                Console.WriteLine($"Value foud at position {positionLinearSearch}");
                Console.WriteLine($"Linear Search O(n) Worst Case: {timeLinearSearch.TotalMilliseconds} ms");
            }
            else
                Console.WriteLine("Linear Search value not found :(");

            Array.Sort(keys);

            stopWatchBinarySearch.Start();
            int positionBinarySearch = await Work.BinarySearch(keys, keys.Length, key);
            stopWatchBinarySearch.Stop();

            TimeSpan timeBinarySearch = stopWatchBinarySearch.Elapsed;

            if (positionBinarySearch > -1)
            {
                Console.WriteLine($"Value foud at position {positionBinarySearch}");
                Console.WriteLine($"Binary Search O(log n) Average: {timeBinarySearch.TotalMilliseconds} ms");
            }
            else
                Console.WriteLine("Binary Search value not found :(");

        }
    }

    static class Util
    {
        public static async Task<Foo> ReadAsync(string filePath)
        {
            using FileStream stream = File.OpenRead(filePath);
            var result = await JsonSerializer.DeserializeAsync<Foo>(stream);

            if (result == null)
                return await Task.FromResult(new Foo());
            else
                return result;
        }
    }

    class Foo
    {
        public int[] Numbers { get; set; } = Array.Empty<int>();
    }

    class Work
    {
        public static async Task<int> BinarySearch(int[] vector, int logicalLength, int key)
        {
            int low = 0;
            int hight = logicalLength - 1;

            do
            {
                int middle = (low + hight) / 2;

                if (vector[middle] == key)
                    return await Task.FromResult(middle);
                else if (vector[middle] < key)
                    low = middle + 1;
                else
                    hight = middle - 1;

            } while (low <= hight);

            return await Task.FromResult(-1);
        }

        public static async Task<int> LinearSearch(int[] vector, int logicalLength, int key)
        {
            for (int i = 0; i < logicalLength; i++)
            {
                if (vector[i] == key)
                    return await Task.FromResult(i);
            }

            return await Task.FromResult(-1);
        }
    }
}