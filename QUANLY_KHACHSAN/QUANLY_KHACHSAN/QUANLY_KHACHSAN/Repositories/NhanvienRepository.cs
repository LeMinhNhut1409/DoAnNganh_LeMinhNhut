using static QUANLY_KHACHSAN.Repositories.NhanvienRepository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using QUANLY_KHACHSAN.InterfacesRepositories;
using QUANLY_KHACHSAN.Models;
using QUANLY_KHACHSAN.Repositories;

namespace QUANLY_KHACHSAN.Repositories
{
    public class NhanvienRepository : INhanvienRepository
    {
        private readonly QUANLY_KHACHSANContext _dbContext;
        private readonly ITaikhoanRepository _tkrepo;

        public NhanvienRepository(QUANLY_KHACHSANContext dbContext, ITaikhoanRepository tkrepo)
        {
            _dbContext = dbContext;
            _tkrepo = tkrepo;
        }
        public async Task AddAsync(Nhanvien nhanvien)
        {
            await _dbContext.Nhanviens.AddAsync(nhanvien);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            Debug.WriteLine("id nhan vien: " + Id);
            await _tkrepo.DeleteByManv(Id);
            Nhanvien nhanvien = await _dbContext.Nhanviens.FindAsync(Id);
            _dbContext.Nhanviens.Remove(nhanvien);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> NhanvienExists(int id)
        {
            return await _dbContext.Nhanviens.AnyAsync(b => b.Manv == id);
        }
        public async Task<IQueryable<Nhanvien>> GetAllAsync()
        {
            var nhanviens = _dbContext.Nhanviens
                .Select(nhanvien => new Nhanvien
                {
                    Manv = nhanvien.Manv,
                    Hoten = nhanvien.Hoten,
                    Gioitinh = nhanvien.Gioitinh,
                    Ngaysinh = nhanvien.Ngaysinh,
                    Sdt = nhanvien.Sdt,
                    Email = nhanvien.Email,
                    Diachi = nhanvien.Diachi,
                    Chucvu = nhanvien.Chucvu
                });

            return nhanviens;
        }

        public async Task<Nhanvien> GetByIdAsync(int id)
        {
            var nhanvien = await _dbContext.Nhanviens.FindAsync(id);
            return nhanvien;
        }

        public async Task UpdateAsync(Nhanvien nhanvienUpdate, int nhanvienid)
        {
            await _tkrepo.UpdateByNv(nhanvienid, nhanvienUpdate.Email);
            nhanvienUpdate.Manv = nhanvienid;
            _dbContext.Nhanviens.Update(nhanvienUpdate);
            var taikhoan = await _dbContext.Taikhoans.FirstOrDefaultAsync(t => t.Manv == nhanvienid);

            if (taikhoan != null)
            {
                taikhoan.Tentknv = nhanvienUpdate.Email; // Assuming Tentknv is the email field in Taikhoan
            }
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Nhanvien> GetByEmailAsync(string email)
        {
            return await _dbContext.Nhanviens.FirstOrDefaultAsync(n => n.Email == email);
        }

        public async Task<Nhanvien> CheckEmailExist(string email, int nhanvienid)
        {
            return await _dbContext.Nhanviens.FirstOrDefaultAsync(x => x.Email == email && x.Manv != nhanvienid);
        }

        public async Task<Nhanvien> GetEmployeeByIdAsync(int id)
        {
            return await _dbContext.Nhanviens
                .FirstOrDefaultAsync(nhanvien => nhanvien.Manv == id);
        }
        public async Task<IQueryable<Nhanvien>> GetAllEmployeesAsync()
        {
            return _dbContext.Nhanviens.AsQueryable();
        }
        public async Task<IQueryable<Nhanvien>> GetEmployeeNoAccount()
        {
            var employeesWithAccounts = await _dbContext.Taikhoans.Select(t => t.Manv).ToListAsync();

            var employeesWithoutAccounts = _dbContext.Nhanviens
                .Where(nv => !employeesWithAccounts.Contains(nv.Manv)) // Filter employees without accounts
                .Select(nhanvien => new Nhanvien
                {
                    Manv = nhanvien.Manv,
                    Hoten = nhanvien.Hoten,
                    Gioitinh = nhanvien.Gioitinh,
                    Ngaysinh = nhanvien.Ngaysinh,
                    Sdt = nhanvien.Sdt,
                    Email = nhanvien.Email,
                    Diachi = nhanvien.Diachi,
                });

            return employeesWithoutAccounts;
        }
        public async Task<IQueryable<Nhanvien>> GetAllEmAsync()
        {
            var nhanviens = _dbContext.Nhanviens
                .Select(nhanvien => new Nhanvien
                {
                    Manv = nhanvien.Manv,
                    Hoten = nhanvien.Hoten,
                    Gioitinh = nhanvien.Gioitinh,
                    Ngaysinh = nhanvien.Ngaysinh,
                    Sdt = nhanvien.Sdt,
                    Email = nhanvien.Email,
                    Diachi = nhanvien.Diachi,
                });

            return nhanviens;
        }
        public async Task<List<string>> GetDistinctEmAsync()
        {
            return await _dbContext.Nhanviens.Select(r => r.Hoten).Distinct().ToListAsync();
        }
        public async Task<int?> GetManvByEmailAsync(string email)
        {
            var nhanvien = await _dbContext.Nhanviens.FirstOrDefaultAsync(n => n.Email == email);
            return nhanvien?.Manv; // Trả về mã nhân viên nếu tồn tại
        }

    }
}

