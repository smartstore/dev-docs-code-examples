using System.Collections.Generic;
using Smartstore.Core.OutputCache;

namespace MyOrg.ExportTutorial
{
    internal sealed class CacheableRoutes : ICacheableRouteProvider
    {
        public int Order => 0;

        public IEnumerable<string> GetCacheableRoutes()
        {
            return new string[]
            {
                "vc:MyOrg.ExportTutorial/ExportTutorialViewComponent"
            };
        }
    }
}