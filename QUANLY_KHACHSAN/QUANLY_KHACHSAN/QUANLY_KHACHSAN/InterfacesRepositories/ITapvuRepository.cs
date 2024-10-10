using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITapvuRepository
{
    IQueryable<Tapvu> GetAllAsync();
    Task<Tapvu> GetByIdAsync(int id);
    Task UpdateAsync(Tapvu tapvuUpdate);
    Task<List<Tapvu>> GetTapvuByRoomIdAsync(int roomId);
    Task<List<Tapvu>> GetTapvuByEmployeeIdAsync(int employeeId);
}