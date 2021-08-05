using AutoMapper;
using FullCalenderDemo.Areas.Identity.Data;
using FullCalenderDemo.Services.Interfaces;
using FullCalenderDemo.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Services
{
    public class UserService:IUserService
    {
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        IMapper _autoMapper;
        public UserService(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,IMapper autoMapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _autoMapper = autoMapper;
        }
        public IEnumerable<ApplicationUser> GetAll()
        {
            return _userManager.Users.ToList();
        }
        public async Task<ApplicationUser> GetById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }
        public async Task<ApplicationUser> AddAsync(CreateUserVM newUser)
        {
            try
            {

                var newMappedUser = new ApplicationUser()
                {
                    Email = newUser.Email,
                    UserName = newUser.Email,

                };
                var createUserResult = await _userManager.CreateAsync(newMappedUser,newUser.Password);
                
                if (createUserResult.Succeeded)
                {
                    if (newUser.Roles.Count() > 0)
                    {
                        await AddUserToRolesAsync(newUser.Roles, newMappedUser);
                    }
                    return newMappedUser;

                }
                else
                    return null;
            }
            catch(Exception ex)
            {
                return null;
            }

        }
        public async Task<ApplicationUser> Update(UserVM updatedUser)
        {
            var user = await _userManager.FindByIdAsync(updatedUser.Id);
            _autoMapper.Map<UserVM,ApplicationUser>(updatedUser,user);
            user.UserName = updatedUser.Email;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await RemoveAllUserRolesAsync(user);
                if (updatedUser.Roles.Count() > 0)
                {
                    await AddUserToRolesAsync(updatedUser.Roles, user);
                }
                return user;
            }
            else
                return null;
        }
        public async Task MapUserVMRoles( ApplicationUser user, UserVMParent vm)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                Roles roleEnum = (Roles)Enum.Parse(typeof(Roles), role, true);
                vm.Roles.Add(roleEnum);
            }
        }
        private async Task AddUserToRolesAsync(List<Roles> roles, ApplicationUser user)
        {
            var rolesStrArr = roles.Select(a => a.ToString()).ToArray();
            var addToRoleResult = await _userManager.AddToRolesAsync(user, rolesStrArr);
        }
        private async Task RemoveAllUserRolesAsync(ApplicationUser user)
        {
            var oldUserRoles = await _userManager.GetRolesAsync(user);
            var removeUserRoles = await _userManager.RemoveFromRolesAsync(user, oldUserRoles);

        }

    }
}
