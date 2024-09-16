using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QUANLY_KHACHSAN.Repositories
{ 
public interface IPhieuthueRepository
{
        Task<List<Phieuthue>> GetAllAsync();
        Task<Phieuthue> GetByIdAsync(int id);
        Task AddAsync(Phieuthue phieuthue);
        Task UpdateAsync(Phieuthue phieuthue);
        Task DeleteAsync(int id);
        Task<bool> RentExists(int id);
        Task<List<int>> GetDistinctMakhAsync();
        Task<List<int>> GetDistinctMapAsync();
        Task<List<int>> GetDistinctManvAsync();
        Task<List<Phieuthue>> GetRentsByCustomerIdAsync(int makh);
        Task<List<Phieuthue>> GetRentsCustomerIdAsync(int customerId);
        Task<List<int>> GetAllCustomerAsync();
        Task<string> GetTenkhachhangByMakhAsync(int makh);
        Task<Phieuthue> GetClientByCCCDAsync(string cccd);
        Task<Phieuthue> GetRentByIDAsync(int makh);
        Task<Phieuthue> GetRentAsync(int mapt);
        Task<List<Phieuthue>> GetRentByCCCDAsync(string cccd);
        Task<List<Phieuthue>> GetAllRentsByCCCDAsync(string cccd);
        Task<bool> CheckCCCDExistsInPhieuthueAsync(string cccd);
        Task<List<Nhanvien>> GetAllEmployeesAsync();
        Task<List<Phieuthue>> GetRentsByEmployeeIdAsync(int manv);

    }
}