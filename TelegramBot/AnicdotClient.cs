using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TelegramBot
{
    public class AnicdotClient
    {
        AnicdotType _currentAnicdotType;
        readonly string _domenUrl;
        readonly Dictionary<AnicdotType, string[]> _anicdotCategories;
        
        protected readonly string _paginationXPath;
        protected readonly string _postXPath;

        protected string _urlWithEndp { 
            get {
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

        public virtual string GetRandomAnecdot(AnicdotType anicdotType)
        {
            _currentAnicdotType = anicdotType;
            return GetRandomAnecdotNodeFromRandomPage().InnerText;
        }

        protected virtual HtmlNode GetRandomAnecdotNodeFromRandomPage()
        {
            return GetAnecdotNodesFromRandomPage().GetRandom();
        }

        protected virtual HtmlNodeCollection GetAnecdotNodesFromRandomPage()
        {
            int rndPagNum = new Random().Next(2, GetPaginationNumber());
            return GetAnecdotNodesFromPage(rndPagNum);
        }

        protected virtual HtmlNodeCollection GetAnecdotNodesFromPage(int paginationNamber)
        {
            return GetHtmlResponse(_urlWithEndp + '/' + paginationNamber)
                .DocumentNode
                .SelectNodes(_postXPath);
        }

        protected virtual int GetPaginationNumber()
        {
            string paginationString = GetHtmlResponse(_urlWithEndp)
                .DocumentNode
                .SelectSingleNode(_paginationXPath)
                .InnerText;
            return int.Parse(paginationString);
        }

        protected HtmlDocument GetHtmlResponse(string url)
        {
            return new HtmlWeb().Load(url);
        }

        private string ChooseRandomEndpoint(AnicdotType anicdotType)
        {
            return _anicdotCategories[anicdotType].GetRandom();
        }
    }
}