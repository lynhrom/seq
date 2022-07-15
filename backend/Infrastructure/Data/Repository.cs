using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Common;

namespace Infrastructure.Data
{
    public class Repository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : BaseEntity
    {
        public Repository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
