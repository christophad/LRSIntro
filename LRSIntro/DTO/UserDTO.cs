using System;

namespace LRSIntro.DTO
{
    public class UserDTO
    {
        /// <summary>
        /// The user identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// The user birthdate
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// The user type
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// The user title
        /// </summary>
        public string UserTitle { get; set; }

        /// <summary>
        /// The user email
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Flag that indicates whether the user is active or not 
        /// </summary>
        public bool? IsActive { get; set; }
    }
}
