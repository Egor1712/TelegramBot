using System.Collections.Generic;
using ApplicationLogic;

namespace Services
{
    public class User
    {
        public CommonRating Rating { get; }
        public DormitoryService DormitoryService { get; }
        public IReadOnlyCollection<Subject> Subjects => subjects;
        private readonly List<Subject> subjects = new List<Subject>();
        private readonly string userName;
        private readonly string password;
        private readonly PageLoader pageLoader;

        public User(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            pageLoader = new PageLoader(UrFuUrls.Rates, userName, password);
            pageLoader.Authorize();
        }
        
    }
}