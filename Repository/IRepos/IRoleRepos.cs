using DAL.EntityModels;
using DCR.Helper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepos
{
    public interface IRoleRepos
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role> GetRole(int RoleId);
        Task<RoleViewModel> AddRole(RoleViewModel model);
        Task<RoleViewModel> UpdateRole(int RoleId, RoleViewModel model);
        Task<Role> DeleteRole(int RoleId);

    }
}
