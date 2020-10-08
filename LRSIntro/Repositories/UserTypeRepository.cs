using LRSIntro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LRSIntro.Repositories
{
    public class UserTypeRepository : Repository<UserType>, IUserTypeRepository
    {
        private readonly LRSIntroContext _lRSIntroContext;

        public UserTypeRepository(LRSIntroContext lRSIntroContext) : base(lRSIntroContext)
        {
            _lRSIntroContext = lRSIntroContext;
        }

        // TODO not needed
        public async Task<IEnumerable<UserType>> GetAllUserTypesAsync()
        {
            var result = await _lRSIntroContext.UserType
                .ToListAsync()
                .ConfigureAwait(false);
            return result;
        }
    }
}
