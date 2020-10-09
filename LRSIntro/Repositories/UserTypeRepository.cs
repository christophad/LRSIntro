using LRSIntro.Models;

namespace LRSIntro.Repositories
{
    public class UserTypeRepository : Repository<UserType>, IUserTypeRepository
    {
        private readonly LRSIntroContext _lRSIntroContext;

        public UserTypeRepository(LRSIntroContext lRSIntroContext) : base(lRSIntroContext)
        {
            _lRSIntroContext = lRSIntroContext;
        }
    }
}
