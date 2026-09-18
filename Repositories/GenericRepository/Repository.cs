using CMC.TS.FT.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CMC.TS.FT.Api.Repositories.GenericRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly SQLServerDbContext _context;
        public Repository(SQLServerDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            //return await _context.SaveChangesAsync() > 0;
        }

        public async Task Delete(Guid id)
        {
            T? entity = await GetById(id);
            if(entity == null)
            {
                return;
            }
            _context.Set<T>().Remove(entity);
            //return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<T>?> GetAll(Expression<Func<T, bool>>? a)
        {
            if (a != null) 
            { 
                return await _context.Set<T>().Where(a).ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /*public async Task<bool> Update(T? entity)
        {
            if (entity == null)
            {
                return false;
            }
            bool isSuccess = await _context.SaveChangesAsync() > 0;
            if (isSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/

        public async Task<int> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }

}
