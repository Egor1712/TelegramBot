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
        private readonly string userName;
        private readonly string password;
        private readonly PageLoader pageLoader;

        public User(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            pageLoader = new PageLoader( userName, password);
        }

        public async Task Initialize()
        {
            await pageLoader.Authorize();
            subjects = await ParseHelper.ParseSubjects(pageLoader).ToList();
            DormitoryService = await ParseHelper.ParseDormitoryService(pageLoader);
        }
    }
}