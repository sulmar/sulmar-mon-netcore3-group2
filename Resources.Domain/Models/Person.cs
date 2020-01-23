namespace Resources.Domain.Models
{
    public class Person : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
    }

    public enum Gender
    {
        Woman,
        Man
    }
}
