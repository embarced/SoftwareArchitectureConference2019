using System;
namespace ScientistSample.ComplicatedCalculation.v2
{
    public class ComplicatedCalc_V2
    {
        public ComplicatedCalc_V2()
        {
        }

        public int GetResult(int context)
        {
            switch (context)
            {
                case 1:
                    return context + context;
                case 2:
                    return context * context;
                case 3:
                    return context + (context + 1 ) * context;
                default:
                    return context;
            }

        }
    }
}
