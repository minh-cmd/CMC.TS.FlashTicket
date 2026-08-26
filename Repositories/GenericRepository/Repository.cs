using CMC.TS.FT.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace CMC.TS.FT.Api.Repositories.GenericRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        SQLServerDbContext _context;
        ILogger<Repository<T>> _logger;

        public Repository(SQLServerDbContext context, ILogger<Repository<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Create(T? entity)
        {
            try
            {
                _logger.LogInformation("create operation start");
                if (entity == null)
                {
                    _logger.LogError("entity of create operation can't be null");
                    return false;
                }
                _context.Set<T>().Add(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "create operation failed");
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                _logger.LogInformation("find by id operation start");
                T? entity = await GetById(id);
                if (entity == null) 
                {
                    _logger.LogError("there is no object {id}", id);
                    return false;
                }
                _context.Set<T>().Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "find by id operation failed");
                return false;
            }
        }

        public async Task<List<T>?> GetAll()
        {
            return await _context.Set<T>().Where(a=>true).ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            try
            {
                _logger.LogInformation("find by id operation start");
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "find by id operation failed");
                return null;
            }
        }

        public async Task<bool> Update(T? entity)
        {
            try
            {
                _logger.LogInformation("update operation start");
                if (entity == null)
                {
                    _logger.LogError("there is no object");
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
                    _logger.LogError("update operation affect 0 row");
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "find by id operation failed");
                return false;
            }
        }
    }
}
