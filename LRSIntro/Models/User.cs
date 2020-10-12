using System;
using System.ComponentModel.DataAnnotations;

namespace LRSIntro.Models
{
    public class User2
    {
        /// <summary>
        /// A proper entity model... with business logic on it
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="password"></param>
        public User2(int id, string name, string password)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Password = setPasswd(password);
        }

        private string setPasswd(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (password.Length < 8)
            {
                throw new ArgumentException("Small Password");
            }
            return password;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
    }



    public partial class User
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public int UserTypeId { get; set; }
        public int UserTitleId { get; set; }
        [MaxLength(50)]
        public string EmailAddress { get; set; }
        public bool? IsActive { get; set; }
        public virtual UserTitle UserTitle { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
