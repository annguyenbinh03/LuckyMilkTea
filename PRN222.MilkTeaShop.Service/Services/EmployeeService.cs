using Azure.Core;
using PRN222.MilkTeaShop.Repository.Enums;
using PRN222.MilkTeaShop.Repository.Models;
using PRN222.MilkTeaShop.Repository.UnitOfWork;
using PRN222.MilkTeaShop.Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN222.MilkTeaShop.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(Employee request)
        {
            await _unitOfWork.Employee.AddAsync(request);
            await _unitOfWork.SaveChanges();
        }

        public async Task Delete(int id)
        {
             var employee = await _unitOfWork.Employee.GetByIdAsync(id);
            if(employee != null)
            {
                employee.Status = EmployeeStatus.inactive.ToString();
                _unitOfWork.Employee.Update(employee);
                await _unitOfWork.SaveChanges();
            }
        }

        public async Task<Employee?> GetEmployee(int id)
        {
            return await _unitOfWork.Employee.GetByIdAsync(id);
        }

        public async Task<(IEnumerable<Employee>, int)> GetEmployees(string? search, int? page = null, int? pageSize = null)
        {
            var (employees, totalItems) = await _unitOfWork.Employee.GetAsync(null, null, null);
            return (employees, totalItems);
        }

        public async Task Update(Employee request)
        {
            _unitOfWork.Employee.Update(request);
            await _unitOfWork.SaveChanges();
        }
    }
}
