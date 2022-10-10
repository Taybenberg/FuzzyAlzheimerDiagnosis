namespace FuzzyAlzheimer
{
    public static class MembershipFunctions
    {
        /// <summary>
        /// This function computes fuzzy membership values using a sigmoidal membership function.
        /// </summary>
        public static double Sigmf(double x, double a, double c)
        {
            return 1 / (1 + Math.Exp(-a * (x - c)));
        }

        /// <summary>
        /// This function computes fuzzy membership values using a combination of two Gaussian membership functions.
        /// </summary>
        public static double Gauss2mf(double x, double sig1, double c1, double sig2, double c2)
        {
            var f1 = GaussFunc(x, sig1, c1);
            var f2 = GaussFunc(x, sig2, c2);

            var product = f1 * f2;

            if (product > 1)
                return 1;

            return product;
        }

        /// <summary>
        /// Gaussian membership function.
        /// </summary>
        /// <param name="x">Input value</param>
        /// <param name="sig">Standard deviation</param>
        /// <param name="c">Mean</param>
        private static double GaussFunc(double x, double sig, double c)
        {
            return Math.Exp(Math.Pow(-(x - c), 2) / (2 * Math.Pow(sig, 2)));
        }
    }
}
