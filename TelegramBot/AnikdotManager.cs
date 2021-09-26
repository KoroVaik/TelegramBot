using System.Collections.Generic;

namespace TelegramBot
{
    public class AnikdotManager
    {
        List<AnicdotClient> _resources;

        public AnikdotManager()
        {
            Init();
        }

        public string GetAnecdot(AnicdotResources anicdotResource)
        {
            return _resources.Find(c => c.AnicdotResourceType == anicdotResource).GetRandomAnecdot();
        }

        private void Init()
        {
            _resources = new List<AnicdotClient>() {
                new FirstAnicdotClient(url: "https://vse-shutochki.ru/anekdoty",
                                  anicdotResourceType: AnicdotResources.Standart,
                                  paginationXPath: "//div[@class='pagination']//li[last()]/a",
                                  postXPath: "//tr[@valign='top']/td[2]//div[@class='post']")
            };
        }

    }

    public enum AnicdotResources
    {
        Standart
    }
}
