using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QUANLY_KHACHSAN.Models
{
    public partial class QUANLY_KHACHSANContext : DbContext
    {
        public QUANLY_KHACHSANContext()
        {
        }

        public QUANLY_KHACHSANContext(DbContextOptions<QUANLY_KHACHSANContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Hoadon> Hoadons { get; set; } = null!;
        public virtual DbSet<Khachhang> Khachhangs { get; set; } = null!;
        public virtual DbSet<Loaikhach> Loaikhaches { get; set; } = null!;
        public virtual DbSet<Loaiphong> Loaiphongs { get; set; } = null!;
        public virtual DbSet<Nhanvien> Nhanviens { get; set; } = null!;
        public virtual DbSet<Phieuthue> Phieuthues { get; set; } = null!;
        public virtual DbSet<Phong> Phongs { get; set; } = null!;
        public virtual DbSet<Phuthu> Phuthus { get; set; } = null!;
        public virtual DbSet<Taikhoan> Taikhoans { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=QUANLY_KHACHSAN;User ID=sa;Password=Minhnhut.1409");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hoadon>(entity =>
            {
                entity.HasKey(e => e.Mahd);

                entity.ToTable("HOADON");

                entity.Property(e => e.Mahd).HasColumnName("MAHD");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(12)
                    .HasColumnName("CCCD");

                entity.Property(e => e.IdphuThu).HasColumnName("IDPhuThu");

                entity.Property(e => e.Manv).HasColumnName("MANV");

                entity.Property(e => e.Ngaydat)
                    .HasColumnType("datetime")
                    .HasColumnName("NGAYDAT");

                entity.Property(e => e.Ngaylaphd)
                    .HasColumnType("datetime")
                    .HasColumnName("NGAYLAPHD");

                entity.Property(e => e.Songayo).HasColumnName("SONGAYO");

                entity.Property(e => e.Tenkh)
                    .HasMaxLength(50)
                    .HasColumnName("TENKH");

                entity.Property(e => e.Tenphong)
                    .HasMaxLength(50)
                    .HasColumnName("TENPHONG");

                entity.Property(e => e.Tongtien).HasColumnName("TONGTIEN");

                entity.Property(e => e.Tylephuthu).HasColumnName("TYLEPHUTHU");

                entity.HasOne(d => d.IdphuThuNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.IdphuThu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOADON_PHUTHU");

                entity.HasOne(d => d.ManvNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.Manv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOADON_NHANVIEN");
            });

            modelBuilder.Entity<Khachhang>(entity =>
            {
                entity.HasKey(e => e.Makh)
                    .HasName("PK_KHACH");

                entity.ToTable("KHACHHANG");

                entity.Property(e => e.Makh).HasColumnName("MAKH");

                entity.Property(e => e.Cccdkh)
                    .HasMaxLength(12)
                    .HasColumnName("CCCDKH");

                entity.Property(e => e.Diachikh)
                    .HasMaxLength(100)
                    .HasColumnName("DIACHIKH");

                entity.Property(e => e.Maloaikhach).HasColumnName("MALOAIKHACH");

                entity.Property(e => e.Map).HasColumnName("MAP");

                entity.Property(e => e.Tel)
                    .HasMaxLength(12)
                    .HasColumnName("TEL");

                entity.Property(e => e.Tenkh)
                    .HasMaxLength(30)
                    .HasColumnName("TENKH");

                entity.Property(e => e.Tuoi).HasColumnName("TUOI");

                entity.HasOne(d => d.MaloaikhachNavigation)
                    .WithMany(p => p.Khachhangs)
                    .HasForeignKey(d => d.Maloaikhach)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KHACHHANG_LOAIKHACH");

                entity.HasOne(d => d.MapNavigation)
                    .WithMany(p => p.Khachhangs)
                    .HasForeignKey(d => d.Map)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KHACHHANG_PHONG1");
            });

            modelBuilder.Entity<Loaikhach>(entity =>
            {
                entity.HasKey(e => e.Maloaikhach);

                entity.ToTable("LOAIKHACH");

                entity.Property(e => e.Maloaikhach).HasColumnName("MALOAIKHACH");

                entity.Property(e => e.Tenloaikhach)
                    .HasMaxLength(20)
                    .HasColumnName("TENLOAIKHACH")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Loaiphong>(entity =>
            {
                entity.HasKey(e => e.Maloaiphong);

                entity.ToTable("LOAIPHONG");

                entity.Property(e => e.Maloaiphong).HasColumnName("MALOAIPHONG");

                entity.Property(e => e.Dongia).HasColumnName("DONGIA");

                entity.Property(e => e.Tenloai)
                    .HasMaxLength(30)
                    .HasColumnName("TENLOAI");
            });

            modelBuilder.Entity<Nhanvien>(entity =>
            {
                entity.HasKey(e => e.Manv);

                entity.ToTable("NHANVIEN");

                entity.Property(e => e.Manv).HasColumnName("MANV");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(100)
                    .HasColumnName("DIACHI");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Gioitinh)
                    .HasMaxLength(3)
                    .HasColumnName("GIOITINH")
                    .IsFixedLength();

                entity.Property(e => e.Hoten)
                    .HasMaxLength(30)
                    .HasColumnName("HOTEN");

                entity.Property(e => e.Ngaysinh)
                    .HasColumnType("date")
                    .HasColumnName("NGAYSINH");

                entity.Property(e => e.Sdt)
                    .HasMaxLength(12)
                    .HasColumnName("SDT");
            });

            modelBuilder.Entity<Phieuthue>(entity =>
            {
                entity.HasKey(e => e.Mapt)
                    .HasName("PK_Table_1");

                entity.ToTable("PHIEUTHUE");

                entity.Property(e => e.Mapt).HasColumnName("MAPT");

                entity.Property(e => e.Cccd)
                    .HasMaxLength(12)
                    .HasColumnName("CCCD");

                entity.Property(e => e.Makh).HasColumnName("MAKH");

                entity.Property(e => e.Manv).HasColumnName("MANV");

                entity.Property(e => e.Map).HasColumnName("MAP");

                entity.Property(e => e.Ngaylappt)
                    .HasColumnType("datetime")
                    .HasColumnName("NGAYLAPPT");

                entity.HasOne(d => d.MakhNavigation)
                    .WithMany(p => p.Phieuthues)
                    .HasForeignKey(d => d.Makh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHIEUTHUE_KHACHHANG1");

                entity.HasOne(d => d.ManvNavigation)
                    .WithMany(p => p.Phieuthues)
                    .HasForeignKey(d => d.Manv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHIEUTHUE_NHANVIEN");

                entity.HasOne(d => d.MapNavigation)
                    .WithMany(p => p.Phieuthues)
                    .HasForeignKey(d => d.Map)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHIEUTHUE_PHONG");
            });

            modelBuilder.Entity<Phong>(entity =>
            {
                entity.HasKey(e => e.Map);

                entity.ToTable("PHONG");

                entity.Property(e => e.Map).HasColumnName("MAP");

                entity.Property(e => e.Ghichu)
                    .HasColumnType("text")
                    .HasColumnName("GHICHU");

                entity.Property(e => e.Maloaiphong).HasColumnName("MALOAIPHONG");

                entity.Property(e => e.Soluongkhachtoida).HasColumnName("SOLUONGKHACHTOIDA");

                entity.Property(e => e.Songayo).HasColumnName("SONGAYO");

                entity.Property(e => e.Tenphong)
                    .HasMaxLength(30)
                    .HasColumnName("TENPHONG");

                entity.Property(e => e.Tinhtrang).HasColumnName("TINHTRANG");

                entity.HasOne(d => d.MaloaiphongNavigation)
                    .WithMany(p => p.Phongs)
                    .HasForeignKey(d => d.Maloaiphong)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHONG_LOAIPHONG");
            });

            modelBuilder.Entity<Phuthu>(entity =>
            {
                entity.HasKey(e => e.Idphuthu);

                entity.ToTable("PHUTHU");

                entity.Property(e => e.Idphuthu).HasColumnName("IDPhuthu");
            });

            modelBuilder.Entity<Taikhoan>(entity =>
            {
                entity.HasKey(e => e.Matknv);

                entity.ToTable("TAIKHOAN");

                entity.Property(e => e.Matknv).HasColumnName("MATKNV");

                entity.Property(e => e.Manv).HasColumnName("MANV");

                entity.Property(e => e.Mktk)
                    .HasMaxLength(30)
                    .HasColumnName("MKTK")
                    .IsFixedLength();

                entity.Property(e => e.Tentknv)
                    .HasMaxLength(40)
                    .HasColumnName("TENTKNV")
                    .IsFixedLength();

                entity.HasOne(d => d.ManvNavigation)
                    .WithMany(p => p.Taikhoans)
                    .HasForeignKey(d => d.Manv)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TAIKHOAN_NHANVIEN");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
