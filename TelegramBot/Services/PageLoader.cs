using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace Services
{
    public class PageLoader
    {
        private readonly string rootUrl;
        private readonly IBrowsingContext context;
        private readonly string userName;
        private readonly string password;
        public IHtmlAllCollection HtmlContent => context.Active.All;

        public PageLoader(string rootUrl, string userName, string password)
        {
            this.rootUrl = rootUrl;
            this.userName = userName;
            this.password = password;
            var config = Configuration.Default
                                      .WithDefaultCookies()
                                      .WithDefaultLoader();
            context = BrowsingContext.New(config);
        }

        public async Task Authorize()
        {
            await context.OpenAsync(rootUrl);
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

        public async void GoToPageAsync(string newUrl)
        {
            if (!newUrl.StartsWith(rootUrl))
                return;
            await context.OpenAsync(newUrl);
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
                          .Where(x => x.ClassName != null && x.ClassName.Contains(className));
        }
    }
}