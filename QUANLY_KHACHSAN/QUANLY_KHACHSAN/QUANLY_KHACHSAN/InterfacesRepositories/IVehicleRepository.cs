using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.ViewModels;

namespace QUANLY_KHACHSAN.InterfacesRepositories
{
    public interface IVehicleRepository
    {
        IQueryable<Baove> GetAllAsync();
        Task<Baove> GetByIdAsync(int id);
        Task AddAsync(Baove baove);
        Task UpdateAsync(Baove baoveUpdate);
        Task DeleteAsync(int id);
        Task<bool> VehicleExists(int id);
        Task<List<Nhanvien>> GetAllEmployeesAsync();
    }
}