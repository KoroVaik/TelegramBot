using System.Linq;

namespace TelegramBot
{
    public class FirstAnicdotClient : AnicdotClient
    {
        public FirstAnicdotClient(string url, AnicdotResources anicdotResourceType, string paginationXPath, string postXPath) : base (url, anicdotResourceType, paginationXPath, postXPath)
        {
        }

        public override string GetRandomAnecdot()
        {
            var anecdots = GetRandomAnecdotNodes().Select(n => n.GetDirectInnerText()).ToList();
            return base.GetRandomAnecdot(anecdots);
        }
    }
}
