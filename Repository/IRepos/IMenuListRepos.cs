using DAL.EntityModels;
using DCR.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepos
{
    public interface IMenuListRepos
    {
        Task<IEnumerable<MenuList>> GetMenuLists();
        Task<MenuList> GetMenuList(int MenuId);
        Task<MenuListViewModel> AddMenu(MenuListViewModel model);
        Task<MenuListViewModel> UpdateMenu(int MenuId, MenuListViewModel model);
        Task<MenuList> DeleteMenu(int MenuId);
    }
}
