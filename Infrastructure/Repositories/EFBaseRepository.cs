using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobFinder.Models.Contexts;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Infrastructure.Repositories
{
    public class EFBaseRepository<T> : IDisposable, IBaseRepository<T> where T : class
    {
        public void Add(T pObject)
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
            {
                context.Set<T>().Add(pObject);
                context.SaveChanges();
            }
        }

        public void Delete(T pObject)
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
            {
                context.Set<T>().Remove(pObject);
                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
                context.Dispose();
        }

        public T Get(int pId)
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
                return context.Set<T>().Find(pId);
        }

        public IEnumerable<T> GetAll()
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
                return context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
                return await context.Set<T>().ToListAsync<T>();
        }

        public void Update(T pObject)
        {
            using (JobFinderDbContext context = new JobFinderDbContext())
            {
                context.Entry(pObject).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
