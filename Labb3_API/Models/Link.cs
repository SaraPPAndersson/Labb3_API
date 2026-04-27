using System.Text.Json.Serialization;

namespace Labb3_API.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        //public int UserId { get; set; }
        //public int InterestId { get; set; }

        //[JsonIgnore]
        public int UserInterestId { get; set; }

        [JsonIgnore]
        public UserInterest UserInterest { get; set; } = null!;

        //public ICollection<Interest> Interests { get; set; } = new List<Interest>();
    }
}
