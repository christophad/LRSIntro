using LRSIntro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRSIntro.Repositories
{
    public class UserTitleRepository : Repository<UserTitle>, IUserTitleRepository
    {
        private readonly LRSIntroContext _lRSIntroContext;

        public UserTitleRepository(LRSIntroContext lRSIntroContext) : base(lRSIntroContext)
        {
            _lRSIntroContext = lRSIntroContext;
        }

        // TODO not needed
        public async Task<IEnumerable<UserTitle>> GetAllUserTitlesAsync()
        {
            var result = await _lRSIntroContext.UserTitle
                .ToListAsync()
                .ConfigureAwait(false);
            return result;
        }
    }
}
