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
        private static readonly CultureInfo CultureInfo = CultureInfo.InvariantCulture;
        public static async IAsyncEnumerable<Subject> ParseSubjects(PageLoader pageLoader)
        {
            var document = await pageLoader.GoToPageAsync(UrFuUrls.Rates);
            var subjects = pageLoader
                           .GetAllElements<IHtmlAnchorElement>(document, "a", "js-service-rating-link")
                           .ToList();
            var rates =
                pageLoader
                    .GetAllElements<IHtmlTableDataCellElement>(document, "td", "js-service-rating-td td-1")
                    .ToList();
            if (rates.Count != subjects.Count)
                throw new Exception($"{nameof(rates.Count)} not equals with {subjects.Count}");
            for (var i = 0; i < subjects.Count; i++)
                yield return new Subject(subjects[i].Text,
                                         decimal.Parse(rates[i].TextContent,
                                                       CultureInfo));
        }

        public static async Task<CommonRating> ParseCommonRating(PageLoader pageLoader)
        {
            var document = await pageLoader.GoToPageAsync(UrFuUrls.Rating);
            var score = decimal.Parse(pageLoader
                                      .GetAllElements<IHtmlElement>(document, "b", "ng-binding")
                                      .FirstOrDefault()
                                      ?.TextContent ?? "0");
            // return new CommonRating(int.Parse(elements[0]), int.Parse(elements[1]), score);
            return null;
        }

        public static async Task<DormitoryService> ParseDormitoryService(PageLoader pageLoader)
        {
            var document = await pageLoader.GoToPageAsync(UrFuUrls.DormitoryServices);
            document = await pageLoader.GoToPageAsync(UrFuUrls.DormitoryServices);
            if (document is null)
                return  new DormitoryService();
            var debtString = pageLoader.GetAllElements<IHtmlParagraphElement>(document, "p", "alert alert-success")
                                 .Select(x => x.TextContent)
                                 .FirstOrDefault();
            var debt = decimal.Zero;
            if (debtString != null && regex.IsMatch(debtString))
                debt = decimal.Parse(regex.Match(debtString).Value, CultureInfo);
            var service = new DormitoryService(debt);
            var extractions = pageLoader.GetAllElements<IHtmlAnchorElement>(document, "a", "btn btn-info");
            foreach (var extraction in extractions)
            {
                var parsed = ParseExtraction(extraction.Text);
                if (parsed is null)
                    continue;
                service.AddExtraction(parsed);
            }

            return service;
        }

        private static Extraction ParseExtraction(string value)
        {
            var items = value.Split('\n')
                                 .Where(x => !string.IsNullOrWhiteSpace(x))
                                 .Select(x => x.Trim())
                                 .Select(x => x.Replace(" ", ""))
                                 .ToList();
            if (items.Count < 3)
                return null;
            var date = items[0];
            var payment = decimal.Zero;
            if (regex.IsMatch(items[1]))
            {
                var pay = regex.Match(items[1]).Value;
                payment = decimal.Parse(pay, CultureInfo);
            }

            var withdrawal = decimal.Zero;
            if (regex.IsMatch(items[2]))
                withdrawal = decimal.Parse(regex.Match(items[2]).Value, CultureInfo);
            return new Extraction(date, payment, withdrawal);
        }
        
    }
}