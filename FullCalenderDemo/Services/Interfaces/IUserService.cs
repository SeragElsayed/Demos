using FullCalenderDemo.Areas.Identity.Data;
using FullCalenderDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalenderDemo.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<ApplicationUser> GetAll();
        Task<ApplicationUser> GetById(string id);
        Task<ApplicationUser> AddAsync(CreateUserVM newUser);
        Task<ApplicationUser> Update(UserVM updatedUser);
        Task MapUserVMRoles(ApplicationUser user, UserVMParent vm);
    }
}
