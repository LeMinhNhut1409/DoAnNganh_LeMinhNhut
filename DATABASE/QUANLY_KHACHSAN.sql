Create database QUANLY_KHACHSAN
go
USE [QUANLY_KHACHSAN]
GO

ALTER DATABASE [QUANLY_KHACHSAN] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET ARITHABORT OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET RECOVERY FULL 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET  MULTI_USER 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF) 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QUANLY_KHACHSAN', N'ON'
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET QUERY_STORE = ON
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200)
GO


USE [QUANLY_KHACHSAN]
GO
-- /****** Object:  Table [dbo].[__EFMigrationsHistory]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HOADON] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HOADON](
	[MAHD] [int] IDENTITY(1,1) NOT NULL,
	[SONGAYO] [int] NOT NULL,
	[MANV] [int] NOT NULL,
	[TONGTIEN] [int] NOT NULL,
	[TENKH] [nvarchar](50) NOT NULL,
	[TENPHONG] [nvarchar](50) NOT NULL,
	[NGAYLAPHD] [datetime] NOT NULL,
	[NGAYDAT] [datetime] NOT NULL,
	[TYLEPHUTHU] [float] NOT NULL,
	[IDPhuThu] [int] NOT NULL,
	[CCCD] [nvarchar](12) NOT NULL,
 CONSTRAINT [PK_HOADON] PRIMARY KEY CLUSTERED 
(
	[MAHD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[KHACHHANG] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KHACHHANG](
	[MAKH] [int] IDENTITY(1,1) NOT NULL,
	[TENKH] [nvarchar](30) NOT NULL,
	[TUOI] [int] NOT NULL,
	[TEL] [nvarchar](12) NOT NULL,
	[DIACHIKH] [nvarchar](100) NULL,
	[CCCDKH] [nvarchar](12) NOT NULL,
	[MALOAIKHACH] [int] NOT NULL,
	[MAP] [int] NOT NULL,
 CONSTRAINT [PK_KHACH] PRIMARY KEY CLUSTERED 
(
	[MAKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LOAIKHACH]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOAIKHACH](
	[MALOAIKHACH] [int] IDENTITY(1,1) NOT NULL,
	[TENLOAIKHACH] [nchar](20) NULL,
 CONSTRAINT [PK_LOAIKHACH] PRIMARY KEY CLUSTERED 
(
	[MALOAIKHACH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[LOAIPHONG]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOAIPHONG](
	[MALOAIPHONG] [int] IDENTITY(1,1) NOT NULL,
	[TENLOAI] [nvarchar](30) NOT NULL,
	[DONGIA] [int] NOT NULL,
 CONSTRAINT [PK_LOAIPHONG] PRIMARY KEY CLUSTERED 
(
	[MALOAIPHONG] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NHANVIEN] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NHANVIEN](
	[MANV] [int] IDENTITY(1,1) NOT NULL,
	[HOTEN] [nvarchar](30) NOT NULL,
	[GIOITINH] [nchar](3) NOT NULL,
	[NGAYSINH] [date] NOT NULL,
	[SDT] [nvarchar](12) NOT NULL,
	[EMAIL] [nvarchar](100) NOT NULL,
	[DIACHI] [nvarchar](100) NULL,
	[CHUCVU] [nvarchar](50) NOT NULL
 CONSTRAINT [PK_NHANVIEN] PRIMARY KEY CLUSTERED 
(
	[MANV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO 

INSERT INTO [dbo].[NHANVIEN] (HOTEN, GIOITINH, NGAYSINH, SDT, EMAIL, DIACHI, CHUCVU)
VALUES (N'Lê Minh Nhựt', 'Nam', '2003-09-14', '0981298952', 'nhut.lm@gmail.com', N'497/6 Phan Văn Trị, P5, Gò Vấp',N'Quản Lý')
		
/****** Object:  Table [dbo].[BAOVE] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BAOVE](
	[MABV] [int] IDENTITY(1,1) NOT NULL,
	[LicensePlate] [nchar](40) NOT NULL,
	[CheckInDate] [datetime] NOT NULL,
	[CheckOutDate] [datetime] NULL,
	[MANV] [int] NOT NULL,
 CONSTRAINT [PK_BAOVE] PRIMARY KEY CLUSTERED 
(
	[MABV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BAOVE] ON;
INSERT INTO [dbo].[BAOVE] (MABV,LicensePlate, CheckInDate, CheckOutDate, MANV)
VALUES (1,N'70L1-123.45','2024-10-01', N'2024-10-03', 4),
(2,N'70L1-438.43','2024-10-02', N'2024-10-03', 4),
(3,N'70L1-869.10','2024-10-03', N'2024-10-05', 4)
SET IDENTITY_INSERT [dbo].[BAOVE] OFF;
/****** Object:  Table [dbo].[PHIEUTHUE]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHIEUTHUE](
	[MAPT] [int] IDENTITY(1,1) NOT NULL,
	[NGAYLAPPT] [datetime] NOT NULL,
	[MAKH] [int] NOT NULL,
	[MAP] [int] NOT NULL,
	[MANV] [int] NOT NULL,
	[CCCD] [nvarchar](12) NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[MAPT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PHONG]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHONG](
	[MAP] [int] IDENTITY(1,1) NOT NULL,
	[TENPHONG] [nvarchar](30) NOT NULL,
	[TINHTRANG] [int] NOT NULL,
	[SOLUONGKHACHTOIDA] [int] NOT NULL,
	[GHICHU] [text] NULL,
	[MALOAIPHONG] [int] NOT NULL,

	[SONGAYO] [int] NOT NULL,
 CONSTRAINT [PK_PHONG] PRIMARY KEY CLUSTERED 
(
	[MAP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PHUTHU]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PHUTHU](
	[IDPhuthu] [int] IDENTITY(1,1) NOT NULL,
	[Giatriphuthu] [float] NOT NULL,
 CONSTRAINT [PK_PHUTHU] PRIMARY KEY CLUSTERED 
(
	[IDPhuthu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TAIKHOAN] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAIKHOAN](
	[MATKNV] [int] IDENTITY(1,1) NOT NULL,
	[TENTKNV] [nchar](40) NOT NULL,
	[MKTK] [nchar](30) NOT NULL,
	[MANV] [int] NOT NULL,
 CONSTRAINT [PK_TAIKHOAN] PRIMARY KEY CLUSTERED 
(
	[MATKNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TAIKHOAN] ON;
INSERT INTO [dbo].[TAIKHOAN] (MATKNV,TENTKNV, MKTK, MANV)
VALUES (1,N'nhut.quanly@gmail.com', N'123456789', 1)
SET IDENTITY_INSERT [dbo].[TAIKHOAN] OFF;
/****** Object:  Table [dbo].[MONAN] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MONAN](
    [MAMONAN] [int] IDENTITY(1,1) NOT NULL,
    [TENMON] [nvarchar](100) NOT NULL, -- Đặt kích thước cho TenMon
    [MOTA] [nvarchar](255) NULL, -- Đặt kích thước cho MoTa
    [GIA] [decimal](18, 2) NOT NULL, -- Đặt định dạng cho Gia
    [THOIGIANCHEBIEN] [int] NOT NULL, -- Đơn vị là phút
    [MANV] [int] NOT NULL, -- Khóa ngoại đến nhân viên nhà bếp
    CONSTRAINT [PK_MONAN] PRIMARY KEY CLUSTERED 
(
	[MAMONAN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
DROP table [dbo].[MONAN]
SET IDENTITY_INSERT [dbo].[MONAN] ON;
INSERT INTO [dbo].[MONAN] (MAMONAN,TENMON, MOTA, GIA, THOIGIANCHEBIEN, MANV)
VALUES (1,N'Bánh canh trảng bàng','Thịt, rau,...',35000, 20, 3)
SET IDENTITY_INSERT [dbo].[MONAN] OFF;
/****** Object:  Table [dbo].[TAPVU] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TAPVU](
    [MATAPVU] [int] IDENTITY(1,1) NOT NULL, -- Khóa chính
    [DADONDEP] [bit] NOT NULL, -- Trạng thái dọn dẹp
    [DATHEMDODUNG] [bit] NOT NULL, -- Trạng thái thêm đồ dùng
    [SOLUONGKHAN] [int] NULL, -- Số lượng khăn trong phòng
    [SOLUONGGAGIUONG] [int]  NULL, -- Số lượng ga giường trong phòng
    [SOLUONGDUNGCUVESINH] [int]  NULL, -- Số lượng dụng cụ vệ sinh trong phòng
    [MAP] [int] NOT NULL, -- Khóa ngoại đến bảng PHONG
    [MANV] [int] NOT NULL, -- Khóa ngoại đến bảng NHANVIEN
    CONSTRAINT [PK_TAPVU] PRIMARY KEY CLUSTERED 
    (
        [MATAPVU] ASC
    ) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, 
            IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, 
            ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) 
    ON [PRIMARY]) ON [PRIMARY]
GO
INSERT INTO [dbo].[TAPVU] 
    ([DADONDEP], [DATHEMDODUNG], [SOLUONGKHAN], [SOLUONGGAGIUONG], [SOLUONGDUNGCUVESINH], [MAP], [MANV])
VALUES 
    (1, 0, 10, 5, 3, 1, 5),
	(1, 0, 10, 5, 3, 3, 5),
	(1, 0, 10, 5, 3, 4, 5),
	(1, 0, 10, 5, 3, 5, 5),
	(1, 0, 10, 5, 3, 6, 5),
	(1, 0, 10, 5, 3, 7, 5),
	(1, 0, 10, 5, 3, 8, 5),
	(1, 0, 10, 5, 3, 9, 5),
	(1, 0, 10, 5, 3, 10, 5),
	(1, 0, 10, 5, 3, 11, 5),
	(1, 0, 10, 5, 3, 12, 5),
	(1, 0, 10, 5, 3, 13, 5),
	(1, 0, 10, 5, 3, 14, 5),
	(1, 0, 10, 5, 3, 15, 5),
	(1, 0, 10, 5, 3, 16, 5)
SELECT * FROM PHONG;
--RÀNG BUỘC KHOÁ NGOẠI CHO BẢNG HOADON CÓ KHOÁ NGOẠI LÀ MANV
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [FK_HOADON_NHANVIEN] FOREIGN KEY([MANV])
REFERENCES [dbo].[NHANVIEN] ([MANV])
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [FK_HOADON_NHANVIEN]
GO
--RÀNG BUỘC KHOÁ NGOẠI CHO BẢNG BAOVE CÓ KHOÁ NGOẠI LÀ MANV
ALTER TABLE [dbo].[BAOVE]  WITH CHECK ADD  CONSTRAINT [FK_BAOVE_NHANVIEN] FOREIGN KEY([MANV])
REFERENCES [dbo].[NHANVIEN] ([MANV])
GO
ALTER TABLE [dbo].[BAOVE] CHECK CONSTRAINT [FK_BAOVE_NHANVIEN]
GO
--RÀNG BUỘC KHOÁ NGOẠI CHO BẢNG MONAN CÓ KHOÁ NGOẠI LÀ MANV
ALTER TABLE [dbo].[MONAN]  WITH CHECK ADD  CONSTRAINT [FK_MONAN_NHANVIEN] FOREIGN KEY([MANV])
REFERENCES [dbo].[NHANVIEN] ([MANV])
GO
ALTER TABLE [dbo].[MONAN] CHECK CONSTRAINT [FK_MONAN_NHANVIEN]
GO
--RÀNG BUỘC KHOÁ NGOẠI CHO BẢNG TAPVU CÓ KHOÁ NGOẠI LÀ MANV
ALTER TABLE [dbo].[TAPVU]  WITH CHECK ADD  CONSTRAINT [FK_TAPVU_NHANVIEN] FOREIGN KEY([MANV])
REFERENCES [dbo].[NHANVIEN] ([MANV])
GO
ALTER TABLE [dbo].[TAPVU] CHECK CONSTRAINT [FK_TAPVU_NHANVIEN]
GO
--RÀNG BUỘC KHOÁ NGOẠI CHO BẢNG TAPVU CÓ KHOÁ NGOẠI LÀ MAP
ALTER TABLE [dbo].[TAPVU]  WITH CHECK ADD  CONSTRAINT [FK_TAPVU_PHONG] FOREIGN KEY([MAP])
REFERENCES [dbo].[PHONG] ([MAP])
GO
ALTER TABLE [dbo].[TAPVU] CHECK CONSTRAINT [FK_TAPVU_PHONG]
GO
--RÀNG BUỘC KHOÁ NGOẠI CHO BẢNG HOADON CÓ KHOÁ NGOẠI LÀ IDPhuThu CỦA BẢNG PHUTHU
ALTER TABLE [dbo].[HOADON]  WITH CHECK ADD  CONSTRAINT [FK_HOADON_PHUTHU] FOREIGN KEY([IDPhuthu])
REFERENCES [dbo].[PHUTHU] ([IDPhuthu])
GO
ALTER TABLE [dbo].[HOADON] CHECK CONSTRAINT [FK_HOADON_PHUTHU]
GO
--RÀNG BUỘC KHOÁ NGOẠI CHO BẢNG KHACHHANG CÓ KHOÁ NGOẠI LÀ MALOAIKHACH CỦA BANG LOAIKHACH
ALTER TABLE [dbo].[KHACHHANG]  WITH CHECK ADD  CONSTRAINT [FK_KHACHHANG_LOAIKHACH] FOREIGN KEY([MALOAIKHACH])
REFERENCES [dbo].[LOAIKHACH] ([MALOAIKHACH])
GO
ALTER TABLE [dbo].[KHACHHANG] CHECK CONSTRAINT [FK_KHACHHANG_LOAIKHACH]
GO

--RÀNG BUỘC KHOÁ NGOẠI CHO BẢNG KHACHHANG CÓ KHOÁ NGOẠI LÀ MAP CỦA BẢNG PHONG
ALTER TABLE [dbo].[KHACHHANG]  WITH CHECK ADD  CONSTRAINT [FK_KHACHHANG_PHONG1] FOREIGN KEY([MAP])
REFERENCES [dbo].[PHONG] ([MAP])
GO
ALTER TABLE [dbo].[KHACHHANG] CHECK CONSTRAINT [FK_KHACHHANG_PHONG1]
GO

--RÀNG BUỘC KHOÁ NGOẠI CHO BẢNG PHIEUTHUE CÓ KHOÁ NGOẠI LÀ MAKH CỦA BẢNG KHACHHANG
ALTER TABLE [dbo].[PHIEUTHUE]  WITH CHECK ADD  CONSTRAINT [FK_PHIEUTHUE_KHACHHANG1] FOREIGN KEY([MAKH])
REFERENCES [dbo].[KHACHHANG] ([MAKH])
GO
ALTER TABLE [dbo].[PHIEUTHUE] CHECK CONSTRAINT [FK_PHIEUTHUE_KHACHHANG1]
GO
--RÀNG BUỘC KHOÁ NGOẠI CHO BẢNG PHIEUTHUE CÓ KHOÁ NGOẠI LÀ MAP CỦA BẢNG PHONG
ALTER TABLE [dbo].[PHIEUTHUE]  WITH CHECK ADD  CONSTRAINT [FK_PHIEUTHUE_PHONG] FOREIGN KEY([MAP])
REFERENCES [dbo].[PHONG] ([MAP])
GO
ALTER TABLE [dbo].[PHIEUTHUE] CHECK CONSTRAINT [FK_PHIEUTHUE_PHONG]
GO
--RÀNG BUỘC KHOÁ NGOẠI CHO BẢNG PHIEUTHUE CÓ KHOÁ NGOẠI LÀ MANV CỦA BẢNG NHANVIEN 
ALTER TABLE [dbo].[PHIEUTHUE]  WITH CHECK ADD  CONSTRAINT [FK_PHIEUTHUE_NHANVIEN] FOREIGN KEY([MANV])
REFERENCES [dbo].[NHANVIEN] ([MANV])
GO
ALTER TABLE [dbo].[PHIEUTHUE] CHECK CONSTRAINT [FK_PHIEUTHUE_PHONG]
GO
--RÀNG BUỘC KHOÁ NGOẠI CHO BẢNG PHONG CÓ KHOÁ NGOẠI LÀ MALOAIPHONG CỦA BẢNG LOAIPHONG
ALTER TABLE [dbo].[PHONG]  WITH CHECK ADD  CONSTRAINT [FK_PHONG_LOAIPHONG] FOREIGN KEY([MALOAIPHONG])
REFERENCES [dbo].[LOAIPHONG] ([MALOAIPHONG])
GO
ALTER TABLE [dbo].[PHONG] CHECK CONSTRAINT [FK_PHONG_LOAIPHONG]
GO
--RÀNG BUỘC KHOÁ NGOẠI CHO BẢNG TAIKHOAN CÓ KHOÁ NGOẠI LÀ MANV CỦA BẢNG NHANVIEN
ALTER TABLE [dbo].[TAIKHOAN]  WITH CHECK ADD  CONSTRAINT [FK_TAIKHOAN_NHANVIEN] FOREIGN KEY([MANV])
REFERENCES [dbo].[NHANVIEN] ([MANV])
GO
ALTER TABLE [dbo].[TAIKHOAN] CHECK CONSTRAINT [FK_TAIKHOAN_NHANVIEN]
GO
USE master
GO
ALTER DATABASE [QUANLY_KHACHSAN] SET  READ_WRITE 
GO

USE [QUANLY_KHACHSAN]
GO 
INSERT INTO PHUTHU(Giatriphuthu) VALUES (30);
