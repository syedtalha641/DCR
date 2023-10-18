using DAL.EntityModels;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepos
{
    public interface IPaymentRepos
    {
        Task<IEnumerable<Payment>> GetPayments();
        Task<Payment> GetPayment(int Paymentid);
        Task<PaymentViewModel> AddPayment(PaymentViewModel model);

        Task<PaymentViewModel> UpdatePayment(int Paymentid,PaymentViewModel model);
        Task<Payment> DeletePayment(int Paymentid);
    }
}
