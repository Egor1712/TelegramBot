using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationLogic;

namespace Services
{
    public class User
    {
        public bool IsAuthorize { get; private set; }
        public CommonRating Rating { get; set; }
        public DormitoryService DormitoryService { get; set; }
        public IReadOnlyCollection<Subject> Subjects => subjects;
        private List<Subject> subjects;
        private string userName;
        private string password;
        private PageLoader pageLoader;
        

        public async Task Initialize(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            pageLoader = new PageLoader( userName, password);
            await pageLoader.Authorize();
            subjects = await ParseHelper.ParseSubjects(pageLoader).ToList();
            DormitoryService = await ParseHelper.ParseDormitoryService(pageLoader);
            IsAuthorize = true;
        }
    }
}