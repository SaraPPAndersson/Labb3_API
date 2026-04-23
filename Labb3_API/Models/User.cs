using System.Text.Json.Serialization;

namespace Labb3_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        //public Interest Interest { get; set; } = null!;
        //[JsonIgnore]
        public ICollection<Link> Links { get; set; } = null!;
    }
}
