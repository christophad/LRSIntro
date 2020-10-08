using System;

namespace LRSIntro.DTO
{
    public class UserAddOrUpdateDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public int UserType { get; set; }
        public int UserTitle { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsActive { get; set; }
    }
}
