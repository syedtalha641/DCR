using DAL.EntityModels;
using DCR.Helper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepos
{
    public interface IIMEIRepos
    {
        Task<IEnumerable<Imei>> GetIMEIs();
        Task<Imei> GetIMEI(int IMEIId);
        Task<IMEIViewModel> AddIMEI(IMEIViewModel model);
        Task<IMEIViewModel> UpdateIMEI(int IMEIId, IMEIViewModel model);
        Task<Imei> DeleteIMEI(int IMEIId);
    }
}
