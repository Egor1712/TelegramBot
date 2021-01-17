using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using ApplicationLogic;

namespace Services
{
    public class PageLoader
    {
        private readonly IConfiguration configuration = AngleSharp.Configuration.Default
            .WithDefaultCookies()
            .WithDefaultLoader();

        private IBrowsingContext context;
        private readonly string userName;
        private readonly string password;
        public IHtmlAllCollection HtmlContent => context.Active.All;

        public PageLoader(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public async Task Authorize()
        {
            context = new BrowsingContext(configuration);
            await context.OpenAsync(UrFuUrls.Rates);
            var login =
                context.Active
                       .QuerySelector<IHtmlInputElement>("input#userNameInput");
            login.Value = userName;
            var passwordInput =
                context.Active
                       .QuerySelector<IHtmlInputElement>("input#passwordInput");
            passwordInput.Value = password;
            var button = context.Active.QuerySelector<IHtmlFormElement>("form");
            await button.SubmitAsync();
        }
        
        public async Task<IDocument> GoToPageAsync(string newUrl)
        {
            return await context.OpenAsync(newUrl);
        }

        public string GetContentOfElement<T>(string id, string elementType)
            where T : class, IElement
        {
            var element = context.Active.QuerySelector<T>($"{elementType}#{id}");
            if (element is null)
                throw new Exception($"Element with id: \"{id}\" was not found");
            return element.NodeValue;
        }

        public IEnumerable<T> GetAllElements<T>(string type, string className)
            where T : class, IElement
        {
            return context.Active.QuerySelectorAll<T>(type)
                          .Where(x => x.ClassName != null 
                                      && x.ClassName.Contains(className));
        }
    }
}