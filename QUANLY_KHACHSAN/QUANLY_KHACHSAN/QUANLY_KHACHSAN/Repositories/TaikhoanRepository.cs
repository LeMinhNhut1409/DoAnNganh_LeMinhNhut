using Microsoft.EntityFrameworkCore;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.InterfacesRepositories;
using System.Diagnostics;

namespace QUANLY_KHACHSAN.Repositories
{
    public class TaikhoanRepository : ITaikhoanRepository
    {
        private readonly QUANLY_KHACHSANContext _dbContext;

        public TaikhoanRepository(QUANLY_KHACHSANContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Taikhoan taikhoan)
        {
            _dbContext.Taikhoans.Add(taikhoan);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateByNv(int manv, string newEmail)
        {
            Taikhoan tk = await _dbContext.Taikhoans.FirstOrDefaultAsync(tk => tk.Manv == manv);
            if (tk != null)
            {
                tk.Tentknv = newEmail;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteByManv(int manv)
        {
            Taikhoan tk = await _dbContext.Taikhoans.FirstOrDefaultAsync(tk => tk.Manv == manv);
            if (tk != null)
            {
                Debug.WriteLine("id tk: " + tk.Matknv);
                _dbContext.Remove(tk);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<Taikhoan> GetByUsernameAndPasswordAsync(string tentknv, string mktk)
        {
            return await _dbContext.Taikhoans.FirstOrDefaultAsync(t => t.Tentknv == tentknv && t.Mktk == mktk);
        }
        public async Task CreateAccountForAllEmployee(IEnumerable<Nhanvien> employeesWithoutAccounts)
        {
            var newAccounts = employeesWithoutAccounts.Select(employee => new Taikhoan
            {
                Manv = employee.Manv,
                Tentknv = employee.Email,
                Mktk = "123456789",
            });

            _dbContext.Taikhoans.AddRange(newAccounts);
            await _dbContext.SaveChangesAsync();
        }

    }
}
