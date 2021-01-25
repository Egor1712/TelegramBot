using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ApplicationLogic;

namespace Services
{
    public class User
    {
        public bool IsAuthorize { get; private set; }
        public CommonRating Rating { get; set; }
        public DormitoryService DormitoryService => service ??= ParseHelper.ParseDormitoryService(pageLoader).Result;
        public IReadOnlyCollection<Subject> Subjects => subjects ??= ParseHelper.ParseSubjects(pageLoader).ToList().Result;
        private List<Subject> subjects;
        private DormitoryService service;
        private string userName;
        private string password;
        private PageLoader pageLoader;
        

        public async Task<bool> Initialize(string userName, string password)
        {
            pageLoader = new PageLoader( userName, password);
            IsAuthorize = await pageLoader.TryAuthorize();
            if (!IsAuthorize) return IsAuthorize;
            this.userName = userName;
            this.password = password;
            return IsAuthorize;
        }
    }
}