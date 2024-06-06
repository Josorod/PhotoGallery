using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.In;
using WebApi.Filters;
using WebApi.Interfaces;
using WebApi.Models.In;
using WebApi.Models.Out;
using WebApi.Models;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [ApiController]
    [Authorize]
    public class AlbumController : ControllerBase
    {
        IMapper mapper;
        IAlbumService albumService;
        public AlbumController(IMapper mapper, IAlbumService albumService)
        {
            this.mapper = mapper;
            this.albumService = albumService;
        }

        [HttpGet]
        [Route("api/users/{userId}/albums")]
        public async Task<ActionResult<IEnumerable<AlbumModel>>> GetAlbums([FromRoute] int userId)
        {
            var albumDTOs = await albumService.GetAlbumsAsync(userId);

            return Ok(mapper.Map<IEnumerable<AlbumModel>>(albumDTOs));
        }

        [HttpGet]
        [Route("api/albums/{albumId}")]
        public async Task<ActionResult<AlbumModel>> GetAlbum([FromRoute] int albumId)
        {
            var albumDTO = await albumService.GetAlbumAsync(albumId);

            return Ok(mapper.Map<AlbumModel>(albumDTO));
        }

        [HttpPost]
        [Route("api/albums")]
        [ModelStateValidation]
        public async Task<ActionResult<AlbumModel>> PostAlbum([FromBody] AlbumAddModel albumAddModel)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out int UserId))
            {
                // Handle the case where UserId is not a valid integer
                return BadRequest("UserId is not a valid integer.");
            }
            var albumAddDTO = mapper.Map<AlbumAddDTO>(albumAddModel, opt => opt.Items["userId"] = UserId);

            return Ok(mapper.Map<AlbumModel>(await albumService.AddAlbumAsync(albumAddDTO)));
        }

        [HttpPut]
        [Route("api/albums/{albumId}")]
        [ModelStateValidation]
        public async Task<ActionResult> PutAlbum([FromRoute] int albumId, [FromBody] AlbumUpdateModel albumUpdateModel)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out int UserId))
            {
                // Handle the case where UserId is not a valid integer
                return BadRequest("UserId is not a valid integer.");
            }
            var albumUpdateDTO = mapper.Map<AlbumUpdateDTO>(albumUpdateModel,
                opt => { opt.Items["albumId"] = albumId; opt.Items["userId"] = UserId; });

            await albumService.UpdateAlbumAsync(albumUpdateDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("api/albums/{albumId}")]
        public async Task<ActionResult> DeleteAlbum([FromRoute] int albumId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out int UserId))
            {
                // Handle the case where UserId is not a valid integer
                return BadRequest("UserId is not a valid integer.");
            }
            await albumService.RemoveAlbumAsync(albumId, UserId);

            return Ok();
        }
    }
}
