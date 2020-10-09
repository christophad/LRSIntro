using LRSIntro.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRSIntro.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly LRSIntroContext _lRSIntroContext;

        public UserRepository(LRSIntroContext lRSIntroContext) : base(lRSIntroContext)
        {
            _lRSIntroContext = lRSIntroContext;
        }

        public async Task<IEnumerable<User>> GetAllUsersWithDetailsAsync()
        {
            var result = await _lRSIntroContext.User
                .Include(x => x.UserTitle)
                .Include(x => x.UserType)
                .Where(x => x.IsActive == true)
                .ToListAsync()
                .ConfigureAwait(false);
            return result;

        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var result = await _lRSIntroContext.User
                .Include(x => x.UserTitle)
                .Include(x => x.UserType)
                .Where(x => x.Id == id && x.IsActive == true)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            return result;

        }

        public void DeleteUser(User user)
        {
            user.IsActive = false;
            _lRSIntroContext.Update<User>(user);
            _lRSIntroContext.SaveChanges();
        }
    }
}
