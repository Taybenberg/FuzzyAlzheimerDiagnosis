namespace InferenceLibrary
{
    public static class MembershipFunctions
    {
        /// <summary>
        /// This function computes fuzzy membership values using a spline-based S-shaped membership function.
        /// </summary>
        public static double Smf(double x, double a, double b)
        {
            if (x <= a)
                return 0;

            var m = (a + b) / 2;

            if (a <= x && x <= m)
                return 2 * Math.Pow((x - a) / (b - a), 2);

            if (m <= x && x <= b)
                return 1 - (2 * Math.Pow((x - a) / (b - a), 2));

            return 1;
        }

        /// <summary>
        /// This function computes fuzzy membership values using a spline-based Z-shaped membership function.
        /// </summary>
        public static double Zmf(double x, double a, double b)
        {
            if (x <= a)
                return 1;

            var m = (a + b) / 2;

            if (a <= x && x <= m)
                return 1 - (2 * Math.Pow((x - a) / (b - a), 2));

            if (m <= x && x <= b)
                return 2 * Math.Pow((x - a) / (b - a), 2);

            return 0;
        }

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
