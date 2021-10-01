using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class VseShutochkiRuClient : AnicdotClient
    {
        public VseShutochkiRuClient(string domenUrl, Dictionary<AnicdotType, string[]> anicdotCategories, string paginationXPath, string postXPath) : base (domenUrl, anicdotCategories, paginationXPath, postXPath)
        {
        }

        public async override Task<string> GetRandomAnecdotAsync(AnicdotType anicdotType)
        {
            var nodes = await GetAnecdotNodesFromRandomPageAsync();
            return nodes.Select(n => n.GetDirectInnerText()).GetRandom();
        }
    }
}
