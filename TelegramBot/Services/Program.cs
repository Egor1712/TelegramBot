using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace Services
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var url = "https://istudent.urfu.ru/s/http-urfu-ru-ru-students-study-brs/";
            var userName = "chusoveg17@gmail.com";
            var passsword = "6E2tjFtp";
            var pageLoader = new PageLoader(url ,userName, passsword );
            await pageLoader.Authorize();
            var subjects = pageLoader
                           .GetAllElements<IHtmlAnchorElement>("a", "js-service-rating-link")
                           .ToList();
            var rates = pageLoader
                        .GetAllElements<IHtmlTableDataCellElement>("td", "js-service-rating-td td-1")
                        .ToList();
            for (var i = 0; i < subjects.Count; i++)
                Console.WriteLine($"{subjects[i].Text} ---- {rates[i].TextContent}");
        }
    }
}