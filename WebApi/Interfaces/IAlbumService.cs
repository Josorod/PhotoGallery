using WebApi.Dtos.In;
using WebApi.Dtos.Out;

namespace WebApi.Interfaces
{
    public interface IAlbumService
    {
        Task<AlbumDTO> AddAlbumAsync(AlbumAddDTO albumDTO);
        Task<AlbumDTO> GetAlbumAsync(int albumId);
        Task<IEnumerable<AlbumDTO>> GetAlbumsAsync(int userId);
        Task RemoveAlbumAsync(int albumId, int userId);
        Task UpdateAlbumAsync(AlbumUpdateDTO albumDTO);
    }
}
