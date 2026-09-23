using CMC.TS.FT.Api.DTO.User;
using CMC.TS.FT.Api.Entities;
using CMC.TS.FT.Api.HelperClass;
using CMC.TS.FT.Api.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace CMC.TS.FT.Api.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        { 
            _userRepository = userRepository;
            _logger = logger;
        }

        //admin
        public async Task<bool> CreateUser(Guid id, CreateUserDTO createUser)
        {
            try
            {
                _logger.LogInformation("create user operation start");
                User user = new User
                {
                    UserId = Guid.NewGuid(),
                    Name = createUser.Name,
                    PasswordHash = PasswordHash.Hash(createUser.PassWord),
                    CreateAt = DateTime.UtcNow,
                    CreateBy = id,
                    IsActive = true,
                    IsDeleted = false,
                    Email = createUser.Email
                };

                _userRepository.Create(user);
                await AssignRoleToUser(user.UserId, createUser.RoleIds);
                bool isSuccess = await _userRepository.SaveChangeAsync() > 0;

                if (isSuccess)
                {
                    _logger.LogInformation("create user success");
                    return true;
                }
                else
                {
                    _logger.LogError("create user operation failed");
                    return false;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "create user operation failed");
                return false;
            }
        }

        public async Task<bool> UpdateUser(Guid Id, CreateUserDTO createUser, Guid updateById)
        {
            try
            {
                _logger.LogError("update user operation start");
                User? user = await _userRepository.GetById(Id);
                if(user == null)
                {
                    _logger.LogError("can't find the user by id");
                    return false;
                }
                user.Name = createUser.Name;
                user.Email = createUser.Email;
                user.UpdateBy = updateById;
                user.UpdateAt = DateTime.UtcNow;
                user.PasswordHash = PasswordHash.Hash(createUser.PassWord);

                return await _userRepository.SaveChangeAsync() > 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "update user operation failed");
                return false;
            }
        }

        //admin and user
        public async Task<bool> DeleteUser(Guid id, Guid updateBy)
        {
            try
            {
                _logger.LogError("delete user operation start");
                User? user = await _userRepository.GetById(id);
                if (user == null)
                {
                    _logger.LogError("can't find the user by id");
                    return false;
                }
                return await _userRepository.UserSoftDelete(id, updateBy);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "delete user operation failed");
                return false;
            }
        }

        //admin
        public async Task<List<DisplayUserDTO>?> DisplayAllUser()
        {
            try
            {
                _logger.LogInformation("display all user operation start");
                List<User>? users = await _userRepository.GetAll(u=>u.IsDeleted==false);
                if(users == null)
                {
                    _logger.LogInformation("user list is empty");
                    return null;
                }
                List<DisplayUserDTO>? userDTO = await _userRepository.GetAllUserWithRoleName();
                return userDTO;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "display all user operation failed");
                return null;
            }
        }

        //user and admin
        public async Task<User?> DisplayProfile(Guid id)
        {
            try
            {
                _logger.LogInformation("display user operation start");
                return await _userRepository.GetById(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "display user operation failed");
                return null;
            }
        }

        //user and admin
        public async Task<bool> ChangePassword(Guid id, string newPassword)
        {
            try
            {
                _logger.LogInformation("change password operation start");
                User? user = await _userRepository.GetById(id);
                if(user == null)
                {
                    _logger.LogError("can't find user");
                    return false;
                }
                user.PasswordHash = PasswordHash.Hash(newPassword);
                return await _userRepository.SaveChangeAsync() > 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "change password operation failed");
                return false;
            }
        }

        //user and admin
        public async Task<bool> UpdateProfile(Guid id, UpdateProfileDTO newProfile)
        {
            try
            {
                _logger.LogInformation("update profile operation start {id}", id);
                User? user = await _userRepository.GetById(id);
                if (user == null)
                {
                    _logger.LogError("can't find user {id}", id);
                    return false;
                }
                user.Name = newProfile.Name;
                user.UpdateBy = id;
                user.UpdateAt = DateTime.UtcNow;
                return await _userRepository.SaveChangeAsync() > 0;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "change password operation failed");
                return false;
            }
        }
        
        //can't use outside
        private async Task AssignRoleToUser(Guid UserId, List<Guid>? RoleIds)
        {
            try
            {
                _logger.LogInformation("assign role to user operation start {id}", UserId);
                User? user = await _userRepository.GetById(UserId);
                if (user == null)
                {
                    _logger.LogError("can't find user by userid: {UserId}", UserId);
                    return;
                }
                if(RoleIds == null)
                {
                    _logger.LogError("Role Ids are null");
                    return;
                }
                if(RoleIds.Count == 0)
                {
                    _logger.LogError("role ids is empty");
                    return;
                }

                await _userRepository.AssignRoleAdmin(UserId, RoleIds);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "assign role to user operation failed");
                return;
            }
        }
        private async Task AutoAssignRoleToUser(Guid UserId)
        {
            try
            {
                _logger.LogInformation("assign customer role to user start {UserId}", UserId);
                User? user = await _userRepository.GetById(UserId);
                if(user == null)
                {
                    _logger.LogError("can't find user by userid: {UserId}", UserId);
                    return;
                }
                await _userRepository.AssignRoleGuest(UserId);
                /*bool isSuccess = await _userRepository.SaveChangeAsync() > 0;
                if (isSuccess)
                {
                    return true;
                }
                return false;*/
            }
            catch (Exception e)
            {
                _logger.LogError(e, "assign customer role to user failed");
                return;
            }
        }
    }
}
