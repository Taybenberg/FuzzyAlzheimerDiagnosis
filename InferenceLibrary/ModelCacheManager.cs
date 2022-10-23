using ModelCache;

namespace InferenceLibrary
{
    public static class ModelCacheManager
    {
        /// <summary>
        /// Initialize model values by calculating Lingustic Terms or by restoring from Cache. 
        /// </summary>
        public static void InitializeModel()
        {
            var cache = CacheManager.ReadCache();

            if (cache is not null) //If cache exists
            {
                Model.OutputLV.XValues = cache.OutputLV.XValues;
                Model.OutputLV.TermValues = cache.OutputLV.TermValues
                    .ToDictionary(x => (Severity)x.Key, x => x.Value);

                for (int i = 0; i < cache.InputLVs.Length; i++)
                {
                    Model.InputLVs[i].XValues = cache.InputLVs[i].XValues;
                    Model.InputLVs[i].TermValues = cache.InputLVs[i].TermValues
                        .ToDictionary(x => (Severity)x.Key, x => x.Value);
                }
            }
            else //Generate values on the go
            {
                Model.OutputLV.XValues = Model.OutputLV.X.Range;
                Model.OutputLV.TermValues = new();
                foreach (var term in Model.OutputLV.Terms)
                {
                    //Calculate membership function value for each x 
                    var termValues = Model.OutputLV.XValues
                        .Select(x => term.Value.Invoke(x))
                        .ToArray();

                    Model.OutputLV.TermValues.Add(term.Key, termValues);
                }

                foreach (var inputLV in Model.InputLVs)
                {
                    inputLV.XValues = inputLV.X.Range;

                    inputLV.TermValues = new();
                    foreach (var term in inputLV.Terms)
                    {
                        var termValues = inputLV.XValues
                            .Select(x => term.Value.Invoke(x))
                            .ToArray();

                        inputLV.TermValues.Add(term.Key, termValues);
                    }
                }

                CacheManager.WriteCache(new Cache
                {
                    OutputLV = new LvCache
                    {
                        XValues = Model.OutputLV.XValues,
                        TermValues = Model.OutputLV.TermValues
                            .ToDictionary(x => (int)x.Key, x => x.Value),
                    },
                    InputLVs = Model.InputLVs.Select(i => new LvCache
                    {
                        XValues = i.XValues,
                        TermValues = i.TermValues
                            .ToDictionary(x => (int)x.Key, x => x.Value),
                    }).ToArray()
                });
            }
        }
    }
}
