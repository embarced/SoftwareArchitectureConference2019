using System;
using ScientistSample.ComplicatedCalculation;
using ScientistSample.ComplicatedCalculation.v2;
using GitHub;

namespace ScientistSample.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Scientist World!");

            IResultPublisher publisher = new MyResultPublisher();
            Scientist.ResultPublisher = publisher;
            //Scientist.Enabled(() => true);

            for (int i = 0; i < 5; i++)
            {
                int randomInput = new Random().Next(4);

                Console.WriteLine($"Random num is: {randomInput}");

                //int result = GetResult(randomInput);
                int result = GetResultWithScientist(randomInput);

                Console.WriteLine($"Result: {result}");
                Console.WriteLine();
            }

        }

        private static int GetResult(int randomInput)
        {
            return new ComplicatedCalc_V1().GetResult(randomInput);
        }

        private static int GetResultWithScientist(int randomInput)
        {
            int result = Scientist.Science<int>("First experiment", experiment =>
            {
                experiment.AddContext("Random Data", randomInput);

                experiment.Use(() => new ComplicatedCalc_V1().GetResult(randomInput)); // Old Version
                experiment.Try(() => new ComplicatedCalc_V2().GetResult(randomInput)); // New Version
            });

            return result;
        }
    }
}
