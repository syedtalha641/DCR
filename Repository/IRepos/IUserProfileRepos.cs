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
    public interface IUserProfileRepos
    {
        Task<IEnumerable<UserProfile>> GetUserprofiles();
        Task<UserProfile> GetUserProfile(int UserProfileId);
        Task<UserProfileViewModel> AddUserProfile(UserProfileViewModel model);
        Task<UserProfileViewModel> UpdateUserProfile(int UserProfileId, UserProfileViewModel model);
        Task<UserProfile> DeleteUserProfile(int UserProfileId);
    }
}
