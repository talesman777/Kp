using System;
using System.IO;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;

namespace StoneMass
{
    public class Program
    {
        [Option(ShortName = "input")]
        public string InputFile { get; }

        [Option(ShortName = "output")]
        public string OutputFile { get; }

        private static string CheckFileExisting(bool fileExist)
        {
            return fileExist ? "Exists" : "Not found";
        }

        public static void Main(string[] args)
        {
            CommandLineApplication.Execute<Program>(args);
        }

        private static void FindMassDifference(int iteration, int stonesNumber, int sumFirst, int sumSecond, ref int result, int[] massArray)
        {
            if (iteration == stonesNumber)
            {
                if (Math.Abs(sumFirst - sumSecond) < result)
                {
                    result = Math.Abs(sumFirst - sumSecond);
                }
                return;
            }
            FindMassDifference(iteration + 1, stonesNumber, sumFirst + massArray[iteration], sumSecond, ref result, massArray);
            FindMassDifference(iteration + 1, stonesNumber, sumFirst, sumSecond + massArray[iteration], ref result, massArray);
        }

        private void OnExecute()
        {
            try
            {
                var inputFileExists = File.Exists(InputFile);
                var outputFileExists = File.Exists(OutputFile);

                Console.WriteLine($"Input file {InputFile} {CheckFileExisting(inputFileExists)}");
                Console.WriteLine($"Output file {OutputFile} {CheckFileExisting(outputFileExists)}");

                var inputData = File.ReadLines(InputFile)
                    .Select(x => x.Split(' '))
                    .ToArray();

                if (inputData.Length != 2)
                {
                    throw new ArgumentException("Data in INPUT.txt file does not match criteria");
                }

                var n = Convert.ToInt32(inputData.First().First());

                if (n < 1 || n > 18)
                {
                    throw new ArgumentException("Input data does not match criteria");
                }

                var massArray = inputData.Last().Select(x =>
                {
                    var convertedValue = Convert.ToInt32(x.Trim());
                    if (convertedValue < 1 || convertedValue > 100000)
                    {
                        throw new ArgumentException("Input data for array does not match criteria");
                    }
                    return convertedValue;
                }).ToArray();

                if (massArray.Length != n)
                {
                    throw new ArgumentException("Array length and value n are not equal");
                }

                var result = int.MaxValue;

                FindMassDifference(0, n, 0, 0, ref result, massArray);

                File.WriteAllText(OutputFile, result.ToString());
                Console.WriteLine($"Result file content: `{result.ToString()}`");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }
    }
}
