using System.Collections.Generic;
using System.Linq;

namespace TelegramBot
{
    public class AnikdotManager
    {
        List<AnicdotClient> _resources;

        public AnikdotManager()
        {
            Init();
        }

        public string GetAnecdot(AnicdotType anicdotResource)
        {
            return SearchResoursesContainAnicdotType(anicdotResource).GetRandom().GetRandomAnecdot(anicdotResource);
        }

        private IEnumerable<AnicdotClient> SearchResoursesContainAnicdotType(AnicdotType anicdotResource)
        {
            return _resources.Where(c => c.AnicdotTypes.Contains(anicdotResource));
        }

        private void Init()
        {
            _resources = new List<AnicdotClient>() {
                new VseShutochkiRuClient(domenUrl: "https://vse-shutochki.ru",
                                  paginationXPath: "//div[@class='pagination']//li[last()]/a",
                                  postXPath: "//tr[@valign='top']/td[2]//div[@class='post']",
                                  anicdotCategories: new Dictionary<AnicdotType, string[]>{
                                      [AnicdotType.Standart] = new string[]{ "anekdoty", "kvn-shutki" }
                                  }),
                new AnicdotClient(domenUrl: "https://nekdo.ru",
                                  paginationXPath: "//div[@class='list']/a[@class='nav'][last()]",
                                  postXPath: "//div[@class='content']/div[@class='text']",
                                  anicdotCategories:new Dictionary<AnicdotType, string[]>{
                                      [AnicdotType.Cenzored] = new string[]{ "censure" }
                                  })
            };
        }

    }

    public enum AnicdotType
    {
        Standart,
        Cenzored
    }
}
