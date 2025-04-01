﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN222.MilkTeaShop.Repository.Models;

namespace PRN222.MilkTeaShop.Service.Services.Interface
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetPaymentsByOrderIdAsync(int orderId);
        Task<Payment> CreatePaymentAsync(Payment payment);
    }
}
