using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using ApplicationLogic;

namespace Services
{
    public class PageLoader
    {
        private readonly HttpClient client;
        private readonly HttpContent content;

        public PageLoader(string userName, string password)
        {
            var values = new Dictionary<string, string>
                         {
                             ["UserName"] = userName,
                             ["Password"] = password,
                             ["AuthMethod"] = "FormsAuthentication"
                         };
            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler
                          {
                              CookieContainer = cookieContainer,
                              UseCookies = true
                          };
            client = new HttpClient(handler);
            content = new FormUrlEncodedContent(values);
        }


        public async Task<bool> TryAuthorize()
        {
            var httpResponseMessage = await client.PostAsync(UrFuUrls.Authorize, content);
            return httpResponseMessage.IsSuccessStatusCode;
        }
        
        public async Task<IDocument> GoToPageAsync(string url)
        {
            var document = await client.GetAsync(url);
            var html = await document.Content.ReadAsStringAsync();
            return new HtmlParser().ParseDocument(html);
        }

        public IEnumerable<T> GetAllElements<T>(IDocument document, string type, string className)
            where T : class, IElement
        {
            if (document is null || type is null || className is null)
                return new T[0];
            return document.QuerySelectorAll<T>(type)
                          .Where(x => x.ClassName != null 
                                      && x.ClassName.Contains(className));
        }
    }
}