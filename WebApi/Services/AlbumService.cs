﻿using WebApi.Interfaces;
using AutoMapper;
using WebApi.Data.Entities;
using WebApi.Dtos.In;
using WebApi.Dtos.Out;

namespace WebApi.Services
{
    public class AlbumService : IAlbumService
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;
        IServiceHelper helper;
        public AlbumService(IMapper mapper, IUnitOfWork unitOfWork, IServiceHelper helper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.helper = helper;
        }

        public async Task<AlbumDTO> AddAlbumAsync(AlbumAddDTO albumDTO)
        {
            var album = mapper.Map<Album>(albumDTO, opt => opt.Items["creationTime"] = DateTime.Now);

            await unitOfWork.Albums.AddAsync(album);
            await unitOfWork.SaveAsync();

            return mapper.Map<AlbumDTO>(album);
        }

        public async Task<AlbumDTO> GetAlbumAsync(int albumId)
        {
            var album = await unitOfWork.Albums.GetByIdAsync(albumId);

            helper.ThrowPhotoGalleryNotFoundExceptionIfModelIsNull(album);

            return mapper.Map<AlbumDTO>(album);
        }

        public async Task<IEnumerable<AlbumDTO>> GetAlbumsAsync(int userId)
        {
            var albums = await unitOfWork.Albums.FindAsync(a => a.UserId == userId);

            return mapper.Map<IEnumerable<AlbumDTO>>(albums);
        }

        public async Task RemoveAlbumAsync(int albumId, int userId)
        {
            var album = await unitOfWork.Albums.GetByIdAsync(albumId);

            helper.ThrowPhotoGalleryNotFoundExceptionIfModelIsNull(album);
            helper.ThrowPhotoGalleryNotAllowedExceptionIfDifferentId(album.UserId, userId);

            unitOfWork.Albums.Remove(album);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAlbumAsync(AlbumUpdateDTO albumDTO)
        {
            var album = await unitOfWork.Albums.GetByIdAsync(albumDTO.Id);

            helper.ThrowPhotoGalleryNotFoundExceptionIfModelIsNull(album);
            helper.ThrowPhotoGalleryNotAllowedExceptionIfDifferentId(album.UserId, albumDTO.UserId);

            mapper.Map(albumDTO, album, opt => opt.Items["creationTime"] = DateTime.Now);

            unitOfWork.Albums.Update(album);
            await unitOfWork.SaveAsync();
        }
    }
}
