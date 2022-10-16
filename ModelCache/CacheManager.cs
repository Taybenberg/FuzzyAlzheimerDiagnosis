using System.Text.Json;

namespace ModelCache
{
    public static class CacheManager
    {
        private const string LvCacheFileName = "LiguisticVariablesCache.json";
        public static Cache? ReadCache()
        {
            if (File.Exists(LvCacheFileName))
            {
                var content = File.ReadAllText(LvCacheFileName);
                return JsonSerializer.Deserialize<Cache>(content);
            }

            return null;
        }

        public static void WriteCache(Cache cache)
        {
            var content = JsonSerializer.Serialize(cache);
            File.WriteAllText(LvCacheFileName, content);
        }
    }
}
