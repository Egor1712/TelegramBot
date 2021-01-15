using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Html.Dom;
using ApplicationLogic;

namespace Services
{
    public static class ParseHelper
    {
        public static IEnumerable<Subject> ParseSubjects(PageLoader pageLoader)
        {
            pageLoader.GoToPageAsync(UrFuUrls.Rates);
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
                yield return new Subject(subjects[i].Text, decimal.Parse(rates[i].TextContent));
        }

        public static CommonRating ParseCommonRating(PageLoader pageLoader)
        {
            pageLoader.GoToPageAsync(UrFuUrls.Rating);
            var score = pageLoader.GetContentOfElement<IHtmlParagraphElement>("ng-scope", "p");
        }
        
    }
}