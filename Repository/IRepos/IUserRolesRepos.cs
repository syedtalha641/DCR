using DAL.EntityModels;
using DCR.Helper.ViewModel;
using DCR.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepos
{
    public interface IUserRolesRepos
    {
        Task<IEnumerable<UserRole>> GetUserRoles();
        Task<UserRole> GetUserRole(int UserRoleId);
        Task<UserRoleViewModel> AddUserRole(UserRoleViewModel model);
        Task<UserRoleViewModel> UpdateUserRole(int UserRoleId, UserRoleViewModel model);
        Task<UserRole> DeleteUserRole(int UserRoleId);

    }
}
