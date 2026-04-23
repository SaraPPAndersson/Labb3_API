using System.Text.Json.Serialization;

namespace Labb3_API.Models
{
    public class Interest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public ICollection<Link> Links { get; set; } = null!;

    }
}
