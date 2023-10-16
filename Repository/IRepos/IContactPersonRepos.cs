using DAL.EntityModels;
using DCR.Helper.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepos
{
    public interface IContactPersonRepos
    {
        Task<IEnumerable<ContactPerson>> GetPersons();
        Task<ContactPerson> GetPerson(int ContactPersonId);
        Task<ContactPersonViewModel> AddPerson(ContactPersonViewModel model);
        Task<ContactPersonViewModel> UpdatePerson(int ContactPersonId, ContactPersonViewModel model);
        Task<ContactPerson> DeletePerson(int ContactPersonId);
    }
}
