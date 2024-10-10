using System;
using QUANLY_KHACHSAN.Models;

namespace QUANLY_KHACHSAN.InterfacesRepositories
{
	public interface IMonanRepository
	{
        Task<List<Monan>> GetAllAsync();
        Task<Monan> GetByIdAsync(int id);
        Task AddAsync(Monan monan);
        Task UpdateAsync(Monan monan);
        Task DeleteAsync(int id);
        Task<bool> MonanExists(int id);

    }
}

