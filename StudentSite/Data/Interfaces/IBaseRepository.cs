using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentSite.Data.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task Create(T entity);

        IQueryable<T> GetAll();

        Task Delete(T entity);

        Task<T> Update(T entity);
    }
}
