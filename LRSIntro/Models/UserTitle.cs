using System.Collections.Generic;

namespace LRSIntro.Models
{
    public partial class UserTitle
    {
        public UserTitle()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
