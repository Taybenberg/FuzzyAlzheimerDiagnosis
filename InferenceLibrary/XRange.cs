namespace InferenceLibrary
{
    public class XRange
    {
        private readonly int _from, _to;
        private readonly double _step;

        public XRange(int from, int to, double step)
        {
            _from = from;
            _to = to;
            _step = step;
        }

        /// <summary>
        /// X axis range.
        /// </summary>
        public double[] Range => Enumerable
            .Range(_from, (int)(_to / _step))  //C# generates only int range
            .Select(i => i * _step)
            .ToArray();
    }
}
