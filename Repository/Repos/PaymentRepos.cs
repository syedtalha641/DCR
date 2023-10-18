using DAL.EntityModels;
using DCR.ViewModel.ViewModel;
using Microsoft.EntityFrameworkCore;
using Repository.IRepos;


namespace Repository.Repos
{
    public class PaymentRepos : IPaymentRepos
    {
        private readonly EMS_ITCContext _context;

        public PaymentRepos(EMS_ITCContext context)
        {
            _context = context;
        }
        public async Task<PaymentViewModel> AddPayment(PaymentViewModel model)
        {
            try
            {
                if (model.DistributorId != null) 
                {
                    var distributor = await _context.Distributors.FirstOrDefaultAsync(b=>b.DistributorId == model.DistributorId && b.IsActive == true);

                    var payment = new Payment 
                    {
                       DistributorId  = distributor.DistributorId
                    };
                    if (model.DistributorId == distributor.DistributorId)
                    {
                        var NewPayment = new Payment 
                        {
                        PaymentAmount = model.PaymentAmount,
                            PaymentDate = model.PaymentDate,
                            PaymentDescription = model.PaymentDescription,
                        CreatedBy = "Admin"

                        };
                        _context.Payments.Add(NewPayment);
                        await _context.SaveChangesAsync();
                    }
                
                }
                return model;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Payment> DeletePayment(int Paymentid)
        {
            var result = await _context.Payments.Where(a => a.PaymentId == Paymentid).FirstOrDefaultAsync();
            if (result != null) 
            {
                result.IsActive = false;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Payment> GetPayment(int Paymentid)
        {
           return await _context.Payments.FirstOrDefaultAsync(a => a.PaymentId == Paymentid && a.IsActive == true);
        }

        public async Task<IEnumerable<Payment>> GetPayments()
        {
          return await _context.Payments.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<PaymentViewModel> UpdatePayment(int Paymentid, PaymentViewModel model)
        {
            var result = await _context.Payments.Where(a => a.PaymentId == Paymentid).FirstOrDefaultAsync();
            if (result != null)
            {
                result.PaymentAmount = model.PaymentAmount;
                result.PaymentDate = model.PaymentDate;
                result.PaymentDescription = model.PaymentDescription;

                result.UpdatedOn = DateTime.Now; // Set the appropriate value
                // Save changes to the database
                await _context.SaveChangesAsync();


                return model;


            }
            else
            {
                //  not found
                return null;
            }
        }
    }
}
