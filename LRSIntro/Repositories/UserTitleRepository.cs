using LRSIntro.Models;

namespace LRSIntro.Repositories
{
    public class UserTitleRepository : Repository<UserTitle>, IUserTitleRepository
    {
        private readonly LRSIntroContext _lRSIntroContext;

        public UserTitleRepository(LRSIntroContext lRSIntroContext) : base(lRSIntroContext)
        {
            _lRSIntroContext = lRSIntroContext;
        }

    }
}
