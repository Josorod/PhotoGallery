using WebApi.Dtos.In;
using WebApi.Dtos.Out;

namespace WebApi.Interfaces
{
    public interface IPhotoService
    {
        Task<PhotoDTO> AddPhotoAsync(PhotoAddDTO photoDTO);
        Task<PhotoDTO> GetPhotoAsync(int photoId);

        Task<IEnumerable<PhotoDTO>> GetPhotosAsync(int albumId, int userId);
        Task LikePhotoAsync(int photoId, int userId);
        Task RemovePhotoAsync(int photoId, int userId);
        Task UpdatePhotoAsync(PhotoUpdateDTO photoDTO);
    }
}
