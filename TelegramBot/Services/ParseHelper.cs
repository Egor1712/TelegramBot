using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using ApplicationLogic;

namespace Services
{
    public static class ParseHelper
    {
        private static readonly Regex regex = new Regex(@"(\d*,\d*)");

        public static async IAsyncEnumerable<Subject> ParseSubjects(PageLoader pageLoader)
        {
            await pageLoader.GoToPageAsync(UrFuUrls.Rates);
            var subjects = pageLoader
                           .GetAllElements<IHtmlAnchorElement>("a", "js-service-rating-link")
                           .ToList();
            var rates =
                pageLoader
                    .GetAllElements<IHtmlTableDataCellElement>("td", "js-service-rating-td td-1")
                    .ToList();
            if (rates.Count != subjects.Count)
                throw new Exception($"{nameof(rates.Count)} not equals with {subjects.Count}");
            for (var i = 0; i < subjects.Count; i++)
                yield return new Subject(subjects[i].Text,
                                         decimal.Parse(rates[i].TextContent,
                                                       CultureInfo.InvariantCulture));
        }

        public static async Task<CommonRating> ParseCommonRating(PageLoader pageLoader)
        {
            var document = await pageLoader.GoToPageAsync(UrFuUrls.Rating);
            var score = decimal.Parse(pageLoader
                                      .GetAllElements<IHtmlElement>("b", "ng-binding")
                                      .FirstOrDefault()
                                      ?.TextContent ?? "0");
            var elements = pageLoader.HtmlContent.ToList();
            // return new CommonRating(int.Parse(elements[0]), int.Parse(elements[1]), score);
            return null;
        }

        public static async Task<DormitoryService> ParseDormitoryService(PageLoader pageLoader)
        {
            var document = await pageLoader.GoToPageAsync(UrFuUrls.DormitoryServices);
            await (document.Links[4] as IHtmlAnchorElement).NavigateAsync();
            var debt = pageLoader.GetAllElements<IHtmlParagraphElement>("p", "alert alert-success")
                                 .Select(x => x.TextContent)
                                 .FirstOrDefault();
            if (debt is null || !regex.IsMatch(debt))
                throw new Exception("Cannot parse debt!");
            var match = regex.Match(debt).Value;
            var service = new DormitoryService(decimal.Parse(match, CultureInfo.CurrentCulture));
            var extractions = pageLoader.GetAllElements<IHtmlAnchorElement>("a", "btn btn-info");
            foreach (var extraction in extractions)
            {
                var text = extraction.Text.Split('\n')
                                     .Where(x => !string.IsNullOrWhiteSpace(x))
                                     .Select(x => x.Trim())
                                     .Select(x => x.Replace(" ", ""))
                                     .ToList();
                if (text.Count != 3)
                    throw new Exception($"{extraction.Text} is not mach pattern");
                var payment = regex.Match(text[1]).Value;
                if (string.IsNullOrEmpty(payment))
                    payment = "0";
                var withdrawal = regex.Match(text[2]).Value;
                if (string.IsNullOrEmpty(withdrawal))
                    withdrawal = "0";
                var parsed =
                    new Extraction(text[0], decimal.Parse(payment, CultureInfo.CurrentCulture),
                                   decimal.Parse(withdrawal, CultureInfo.CurrentCulture));
                service.AddExtraction(parsed);
            }

            return service;
        }
    }
}