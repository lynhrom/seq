using Ardalis.Specification;
using Domain.Common;

namespace Application.Interfaces
{
    public interface IRepository<T>: IRepositoryBase<T> where T : BaseEntity
    {
    }
}
