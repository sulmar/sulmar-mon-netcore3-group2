using System.Collections.Generic;

namespace Resources.Domain.Models
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public Address WorkAddress { get; set; }
        public string UserName { get; set; }
        public string HashPassword { get; set; }

    }

    public enum Gender
    {
        Woman,
        Man
    }
}
