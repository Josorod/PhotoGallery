using Azure;
using Microsoft.AspNetCore.Identity;
using WebApi.Data.Entities;
using WebApi.Data.Repo;
using WebApi.Interfaces;
using WebApi.Models;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dc;

        public UnitOfWork(DataContext dc)
        {
            this.dc = dc;
        }

        IGenericRepository<Album> albumRepository;
        IGenericRepository<Like> likeRepository;
        IGenericRepository<Photo> photoRepository;


        public IGenericRepository<Album> Albums => albumRepository ??= new GenericRepository<Album>(dc);
        public IGenericRepository<Like> Likes => likeRepository ??= new GenericRepository<Like>(dc);
        public IGenericRepository<Photo> Photos => photoRepository ??= new GenericRepository<Photo>(dc);


        public IUserRepository UserRepository =>
            new UserRepository(dc);

        public async Task<bool> SaveAsync()
        {
            return await dc.SaveChangesAsync() > 0;
        }
    }
}
