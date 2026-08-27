using CMC.TS.FT.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace CMC.TS.FT.Api.Repositories.GenericRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        SQLServerDbContext _context;
        public Repository(SQLServerDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(T? entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Set<T>().Add(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            T? entity = await GetById(id);
            if (entity == null) 
            {
                return false;
            }
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<T>?> GetAll()
        {
            return await _context.Set<T>().Where(a=>true).ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> Update(T? entity)
        {
            if (entity == null)
            {
                return false;
            }
            _context.Set<T>().Update(entity);   
            bool isSuccess = await _context.SaveChangesAsync() > 0;
            if (isSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
