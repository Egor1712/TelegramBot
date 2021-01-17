using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ApplicationLogic;

namespace Services
{
    public class User
    {
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
        }
    }
}