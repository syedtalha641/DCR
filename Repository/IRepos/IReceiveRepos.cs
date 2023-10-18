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
    public interface IReceiveRepos
    {
        Task<IEnumerable<Receive>> GetReceives();
        Task<Receive> GetReceive(int ReceiveId);
        Task<ReceiveViewModel> AddReceive(ReceiveViewModel model);
        Task<ReceiveViewModel> UpdateReceive(int ReceiveId, ReceiveViewModel model);
        Task<Receive> DeleteReceive(int ReceiveId);

    }
}
