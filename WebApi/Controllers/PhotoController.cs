using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Dtos.In;
using WebApi.Filters;
using WebApi.Interfaces;
using WebApi.Models.In;
using WebApi.Models.Out;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        IMapper mapper;
        IPhotoService photoService;
        public PhotoController(IMapper mapper, IPhotoService photoService)
        {
            this.mapper = mapper;
            this.photoService = photoService;
        }

        [HttpGet]
        [Route("api/albums/{albumId}/photos")]
        public async Task<ActionResult<IEnumerable<PhotoModel>>> GetPhotos([FromRoute] int albumId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out int UserId))
            {
                // Handle the case where UserId is not a valid integer
                return BadRequest("UserId is not a valid integer.");
            }
            var photoDTOs = await photoService.GetPhotosAsync(albumId, UserId);

            return Ok(mapper.Map<IEnumerable<PhotoModel>>(photoDTOs));
        }

        [HttpGet]
        [Route("api/photos/{photoId}")]
        public async Task<ActionResult<PhotoModel>> GetPhoto([FromRoute] int photoId)
        {
            var photoDTO = await photoService.GetPhotoAsync(photoId);

            return Ok(mapper.Map<PhotoModel>(photoDTO));
        }

        [HttpPost]
        [Route("api/albums/{albumId}/photos")]
        [ModelStateValidation]
        public async Task<ActionResult<PhotoModel>> PostPhoto([FromRoute] int albumId, [FromBody] PhotoAddModel photoAddModel)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var photoAddDTO = mapper.Map<PhotoAddDTO>(photoAddModel,
                opt => { opt.Items["albumId"] = albumId; opt.Items["userId"] = UserId; });

            return Ok(mapper.Map<PhotoModel>(await photoService.AddPhotoAsync(photoAddDTO)));
        }

        [HttpPost]
        [Route("api/photos/{photoId}/like")]
        public async Task<ActionResult> LikePhoto([FromRoute] int photoId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out int UserId))
            {
                // Handle the case where UserId is not a valid integer
                return BadRequest("UserId is not a valid integer.");
            }
            await photoService.LikePhotoAsync(photoId, UserId);

            return Ok();
        }

        [HttpPut]
        [Route("api/photos/{photoId}")]
        [ModelStateValidation]
        public async Task<ActionResult> PutPhoto([FromRoute] int photoId, [FromBody] PhotoUpdateModel photoUpdateModel)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out int UserId))
            {
                // Handle the case where UserId is not a valid integer
                return BadRequest("UserId is not a valid integer.");
            }
            var photoUpdateDTO = mapper.Map<PhotoUpdateDTO>(photoUpdateModel,
                opt => { opt.Items["photoId"] = photoId; opt.Items["userId"] = UserId; });

            await photoService.UpdatePhotoAsync(photoUpdateDTO);

            return Ok();
        }

        [HttpDelete]
        [Route("api/photos/{photoId}")]
        public async Task<ActionResult> DeletePhoto([FromRoute] int photoId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out int UserId))
            {
                // Handle the case where UserId is not a valid integer
                return BadRequest("UserId is not a valid integer.");
            }
            await photoService.RemovePhotoAsync(photoId, UserId);

            return Ok();
        }




        [HttpGet("admintest")]
        [Authorize(Policy = "RequireAdminRole")]
        [Route("api/photo/admintest")]
        public int Test()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int UserId = int.Parse(userIdString);
            return UserId;
        }
    }
}
