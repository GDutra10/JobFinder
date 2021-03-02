using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Infrastructure.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        T Get(int pId);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T pObject);
        void Update(T pObject);
        void Delete(T pObject);

    }
}
