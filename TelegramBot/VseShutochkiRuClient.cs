using System.Collections.Generic;
using System.Linq;

namespace TelegramBot
{
    public class VseShutochkiRuClient : AnicdotClient
    {
        public VseShutochkiRuClient(string domenUrl, Dictionary<AnicdotType, string[]> anicdotCategories, string paginationXPath, string postXPath) : base (domenUrl, anicdotCategories, paginationXPath, postXPath)
        {
        }

        public override string GetRandomAnecdot(AnicdotType anicdotType)
        {
            var anecdots = GetAnecdotNodesFromRandomPage().Select(n => n.GetDirectInnerText()).ToList();
            return anecdots.GetRandom();
        }
    }
}
