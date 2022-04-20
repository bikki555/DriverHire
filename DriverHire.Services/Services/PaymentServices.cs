using DriverHire.Entity.Dto;
using DriverHire.Entity.Entity;
using DriverHire.Repository;
using DriverHire.Repository.Interfaces;
using DriverHire.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Services.Services
{
    public interface IPaymentServices
    {
       // public Task<PaymentDto> Save(PaymentDto Dto);

    }
    public class PaymentServices : IPaymentServices
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IPaymentRepository _PaymentRepository;
        public PaymentServices(IUnitofWork unitofWork, IPaymentRepository paymentRepository)
        {
            _unitofWork = unitofWork;
            _PaymentRepository = paymentRepository;

        }

        //public Task<PaymentDto> Save(PaymentDto Dto)
        //{
        //    var result = (await _PaymentRepository.Insert(Dto)).Entity;
        //    await _unitofWork.SaveAsync();
        //    return result;
        //}
    }
    


}
