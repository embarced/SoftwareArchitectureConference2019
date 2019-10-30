using System;
using System.Threading;

namespace ScientistSample.ComplicatedCalculation
{
    public class ComplicatedCalc_V1
    {
        public ComplicatedCalc_V1()
        {
        }

        public int GetResult(int context)
        {
            if (context == 1)
            {
                int result;

                // Do other complicated stuff
                Thread.Sleep(2);

                int sum1 = context;
                int sum2 = context;

                result = sum1 + sum2;

                return result;
            }

            if (context == 2)
            {
                int result;

                // Do other complicated stuff
                Thread.Sleep(3);

                int mult1 = context;
                int mult2 = context;

                result = mult1 * mult2;

                return result;
            }

            if (context == 3)
            {
                int result;

                // Do other complicated stuff
                Thread.Sleep(4);

                int sum1 = context;
                int sum2;
                int mult1 = context;
                int mult2 = context;

                sum2 = mult1 * mult2;

                result = sum1 + sum2;

                return result;
            }

            // Do other complicated stuff
            Thread.Sleep(1);
            return context;
        }
    }
}
