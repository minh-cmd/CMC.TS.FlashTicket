using CMC.TS.FT.Api.Entities;
using CMC.TS.FT.Api.Repositories.GenericRepository;

namespace CMC.TS.FT.Api.Services
{
    public class PermissionService
    {
        private readonly IRepository<Permission> _repository;
        private readonly ILogger<PermissionService> _logger;

        public PermissionService(IRepository<Permission> repository, ILogger<PermissionService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<Permission>?> DisplayAllPermission()
        {
            try
            {
                _logger.LogInformation("start display permission operation");
                List<Permission>? a = await _repository.GetAll();
                return a;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "displaying permission failed");
                return null;
            }
        }
    }
}
