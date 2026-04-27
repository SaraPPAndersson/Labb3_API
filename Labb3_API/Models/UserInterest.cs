using System.Text.Json.Serialization;

namespace Labb3_API.Models
{
    public class UserInterest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int InterestId { get; set; }
        [JsonIgnore]
        public User User { get; set; } = null!;
        [JsonIgnore]
        public Interest Interest { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Link> Links { get; set; } = new List<Link>();
    }
}
