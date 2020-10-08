using System;
using System.ComponentModel.DataAnnotations;

namespace LRSIntro.Models
{
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
