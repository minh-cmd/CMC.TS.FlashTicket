using CMC.TS.FT.Api.DTO.Auth;
using CMC.TS.FT.Api.DTO.User;
using CMC.TS.FT.Api.Entities;
using CMC.TS.FT.Api.HelperClass;
using CMC.TS.FT.Api.Repositories;
using CMC.TS.FT.Api.Repositories.IRepositories;

namespace CMC.TS.FT.Api.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;
        public AuthService(IUserRepository userRepository, ILogger<AuthService> logger, IConfiguration configuration) 
        { 
            _userRepository = userRepository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<string?> Login(LoginDTO login)
        {
            try
            {
                _logger.LogInformation("login operation start");
                User? user = await _userRepository.GetUserByEmail(login.Email);

                //check if user exist
                if (user == null)
                {
                    _logger.LogError("no user found");
                    return null;
                }
                string? hashPassword = await _userRepository.GetPasswordById(user.UserId);
                if (hashPassword == null ||  PasswordHash.Verify(hashPassword, login.Password) == false)
                {
                    _logger.LogError("no password found");
                    return null;
                }
                //verify password
                if (PasswordHash.Verify(hashPassword, login.Password))
                {
                    _logger.LogInformation("correct password");
                    //create JWT then return back to user
                    List<string>? permissionList = await _userRepository.GetRoleNameByUserId(user.UserId);
                    if (permissionList == null)
                    {
                        _logger.LogError("there is no role for the user");
                        return null;
                    }
                        
                    return JwtGenerate.CreateToken(_configuration, user.Email, user.Name, permissionList, user.UserId.ToString());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "login operation failed");
                return null;
            }
        }

        public async Task<bool> Register(RegisterDTO createUser)
        {
            try
            {
                _logger.LogInformation("login operation start");
                var id = Guid.NewGuid();
                User user = new User
                {
                    UserId = id,
                    Name = createUser.Name,
                    Email = createUser.Email,
                    PasswordHash = PasswordHash.Hash(createUser.Password),
                    IsActive = true,
                    IsDeleted = false,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = null,
                    CreateBy = id,
                    UpdateBy = Guid.Empty
                };
                _userRepository.Create(user);
                await _userRepository.AssignRoleGuest(user.UserId);
                return await _userRepository.SaveChangeAsync() > 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "register operation failed");
                return false;
            }
        }

        public void ChangePassWord()
        {

        }
    }
}
