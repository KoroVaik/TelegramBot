using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TelegramBot
{
    public class AnicdotClient
    {
        public readonly AnicdotResources AnicdotResourceType;
        readonly string _url;
        protected readonly string _paginationXPath;
        protected readonly string _postXPath;

        public AnicdotClient(string url, AnicdotResources anicdotResourceType, string paginationXPath, string postXPath)
        {
            AnicdotResourceType = anicdotResourceType;
            _url = url;
            _paginationXPath = paginationXPath;
            _postXPath = postXPath;
        }

        public virtual string GetRandomAnecdot()
        {
            return GetRandomAnecdot(GetRandomAnecdotsCollection());
        }

        public virtual string GetRandomAnecdot(List<string> anecdots)
        {
            int rndAnecdotNum = new Random().Next(0, anecdots.Count - 1);
            return anecdots[rndAnecdotNum];
        }

        protected virtual List<string> GetRandomAnecdotsCollection()
        {
            return GetRandomAnecdotNodes().Select(a => a.InnerText).ToList();
        }

        protected HtmlNodeCollection GetRandomAnecdotNodes()
        {
            int rndPagNum = new Random().Next(2, GetPaginationNumber());
            return GetAnecdotNodes(rndPagNum);
        }

        protected HtmlNodeCollection GetAnecdotNodes(int paginationNamber)
        {
            return GetHtmlResponse(_url + '/' + paginationNamber)
                .DocumentNode
                .SelectNodes(_postXPath);
        }

        protected virtual int GetPaginationNumber()
        {
            string paginationString = GetHtmlResponse(_url)
                .DocumentNode
                .SelectSingleNode(_paginationXPath)
                .InnerText;
            return int.Parse(paginationString);
        }

        protected HtmlDocument GetHtmlResponse(string url)
        {
            return new HtmlWeb().Load(url);
        }
    }
}