using Azure;

namespace WebApi.Data.Entities
{
    public class Photo : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public DateTime Created { get; set; }

        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }

        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}
