using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelegramBot
{
    public class AnicdotClient
    {
        AnicdotType _currentAnicdotType;
        readonly string _domenUrl;
        readonly Dictionary<AnicdotType, string[]> _anicdotCategories;

        protected readonly string _paginationXPath;
        protected readonly string _postXPath;

        protected string _urlWithEndp
        {
            get
            {
                return _domenUrl + '/' + ChooseRandomEndpoint(_currentAnicdotType);

            }
        }

        public List<AnicdotType> AnicdotTypes => _anicdotCategories.Keys.ToList();

        public AnicdotClient(string domenUrl, Dictionary<AnicdotType, string[]> anicdotCategories, string paginationXPath, string postXPath)
        {
            _anicdotCategories = anicdotCategories;
            _domenUrl = domenUrl;
            _paginationXPath = paginationXPath;
            _postXPath = postXPath;
        }

        public async virtual Task<string> GetRandomAnecdotAsync(AnicdotType anicdotType)
        {
            _currentAnicdotType = anicdotType;
            var  resNode = await GetRandomAnecdotNodeFromRandomPageAsync();
            return resNode.InnerText;
        }

        protected async virtual Task<HtmlNode> GetRandomAnecdotNodeFromRandomPageAsync()
        {
            var nodes = await GetAnecdotNodesFromRandomPageAsync();
            return nodes.GetRandom();
        }

        protected async virtual Task<HtmlNodeCollection> GetAnecdotNodesFromRandomPageAsync()
        {
            int rndPagNum = new Random().Next(2, await GetPaginationNumberAsync());
            return await GetAnecdotNodesFromPageAsync(rndPagNum);
        }

        protected async virtual Task<HtmlNodeCollection> GetAnecdotNodesFromPageAsync(int paginationNamber)
        {
            var htmlResponse = await GetHtmlResponseAsync(_urlWithEndp + '/' + paginationNamber);
            return htmlResponse
                .DocumentNode
                .SelectNodes(_postXPath);
        }

        protected async virtual Task<int> GetPaginationNumberAsync()
        {
            var htmlResponse = await GetHtmlResponseAsync(_urlWithEndp);
            string paginationString =  htmlResponse
                .DocumentNode
                .SelectSingleNode(_paginationXPath)
                .InnerText;
            return int.Parse(paginationString);
        }

        protected async Task<HtmlDocument> GetHtmlResponseAsync(string url)
        {
            return await new HtmlWeb().LoadFromWebAsync(url);
        }

        private string ChooseRandomEndpoint(AnicdotType anicdotType)
        {
            return _anicdotCategories[anicdotType].GetRandom();
        }
    }
}