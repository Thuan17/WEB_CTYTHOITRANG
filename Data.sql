USE [CONGTYTHOITRANG]
GO
/****** Object:  Table [dbo].[tb_Cart]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Cart](
	[CartId] [int] IDENTITY(1,1) NOT NULL,
	[IdKhachHang] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_CartItem]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_CartItem](
	[CartItem] [int] IDENTITY(1,1) NOT NULL,
	[CartId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[PriceTotal] [decimal](18, 0) NOT NULL,
	[TemPrice] [decimal](18, 0) NOT NULL,
	[ProductDetai] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CartItem] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_ChucNang]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_ChucNang](
	[IdChucNang] [int] IDENTITY(1,1) NOT NULL,
	[TenChucNang] [nvarchar](max) NULL,
	[MaChucNang] [nvarchar](max) NULL,
	[Createby] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[Modifeby] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdChucNang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_KhachHang]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_KhachHang](
	[IdKhachHang] [int] IDENTITY(1,1) NOT NULL,
	[SDT] [varchar](15) NULL,
	[TenKhachHang] [nvarchar](max) NOT NULL,
	[Email] [varchar](100) NULL,
	[Password] [varchar](100) NULL,
	[Image] [nvarchar](250) NULL,
	[Birthday] [date] NULL,
	[DiaChi] [nvarchar](max) NULL,
	[SoLanMua] [int] NULL,
	[Code] [char](10) NULL,
	[Clock] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdKhachHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Kho]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Kho](
	[Idkho] [int] IDENTITY(1,1) NOT NULL,
	[DiaChi] [nvarchar](max) NULL,
	[Createby] [nvarchar](max) NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[Modifeby] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Idkho] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_KhoNhap]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_KhoNhap](
	[IdKhoNhap] [int] IDENTITY(1,1) NOT NULL,
	[ImportDate] [datetime] NOT NULL,
	[ImportBy] [nvarchar](max) NOT NULL,
	[ProductId] [int] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[MaPhieuNhap] [char](15) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[Modifeby] [nvarchar](max) NULL,
	[MSNV] [varchar](10) NOT NULL,
	[IdKho] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdKhoNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_KhoReturn]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_KhoReturn](
	[IdKhoXuat] [int] IDENTITY(1,1) NOT NULL,
	[ReturnDate] [datetime] NOT NULL,
	[ReturnBy] [nvarchar](max) NOT NULL,
	[MSNV] [varchar](10) NOT NULL,
	[ReturnId] [int] NOT NULL,
	[IdKho] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdKhoXuat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_KhoXuat]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_KhoXuat](
	[IdKhoXuat] [int] IDENTITY(1,1) NOT NULL,
	[OutDate] [datetime] NOT NULL,
	[OutBy] [nvarchar](max) NULL,
	[OrderId] [int] NULL,
	[Idkho] [int] NULL,
	[MSNV] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdKhoXuat] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_NhanVien]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_NhanVien](
	[MSNV] [varchar](10) NOT NULL,
	[SDT] [varchar](15) NOT NULL,
	[TenNhanVien] [nvarchar](max) NOT NULL,
	[CCCD] [char](12) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Image] [nvarchar](250) NULL,
	[Birthday] [date] NOT NULL,
	[DiaChi] [nvarchar](max) NOT NULL,
	[NgayVaoLam] [date] NULL,
	[Luong] [decimal](18, 2) NOT NULL,
	[GioiTinh] [nvarchar](7) NULL,
	[CreatedDate] [datetime] NULL,
	[IdChucNang] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[Clock] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MSNV] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_NhanVienImage]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_NhanVienImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MSNV] [varchar](10) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[IsDefault] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Order]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[CustomerName] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[TypePayment] [int] NOT NULL,
	[Email] [nvarchar](max) NULL,
	[IdKhachHang] [int] NULL,
	[typeOrder] [bit] NULL,
	[Confirm] [bit] NULL,
	[Status] [nvarchar](max) NULL,
	[typeReturn] [bit] NULL,
	[Success] [bit] NULL,
	[SuccessDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_OrderDetail]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_OrderDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[CartItem] [int] NULL,
	[damagedProduct] [bit] NULL,
	[ProductDetai] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_PhanQuyen]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_PhanQuyen](
	[MSNV] [varchar](10) NOT NULL,
	[IdChucNang] [int] NOT NULL,
	[GhiChu] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[MSNV] ASC,
	[IdChucNang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_ProductCategory]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_ProductCategory](
	[ProductCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Icon] [nvarchar](250) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[Alias] [nvarchar](150) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_ProductDetai]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_ProductDetai](
	[ProductDetai] [int] IDENTITY(1,1) NOT NULL,
	[Size] [int] NULL,
	[Quantity] [int] NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[Modifeby] [nvarchar](max) NULL,
	[Alias] [nvarchar](250) NULL,
	[ProductId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductDetai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_ProductImage]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_ProductImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Image] [nvarchar](max) NULL,
	[IsDefault] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Products]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](250) NULL,
	[ProductCode] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[Detail] [nvarchar](max) NULL,
	[Image] [nvarchar](250) NULL,
	[Price] [decimal](18, 2) NULL,
	[PriceSale] [decimal](18, 2) NULL,
	[IsHome] [bit] NULL,
	[IsSale] [bit] NULL,
	[IsFeature] [bit] NULL,
	[IsHot] [bit] NULL,
	[ProductCategoryId] [int] NULL,
	[SeoTitle] [nvarchar](250) NULL,
	[SeoDescription] [nvarchar](250) NULL,
	[CreatedBy] [nvarchar](250) NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[Modifeby] [nvarchar](max) NULL,
	[Alias] [nvarchar](250) NULL,
	[IsActive] [bit] NULL,
	[ViewCount] [int] NOT NULL,
	[OrigianlPrice] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Return]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Return](
	[ReturnId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[CreateDate] [datetime] NULL,
	[Confirm] [bit] NULL,
	[OrderId] [int] NULL,
	[IdKhachHang] [int] NULL,
	[Satus] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ReturnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_ReturnDetail]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_ReturnDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReturnId] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[ProductDetai] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_Seller]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Seller](
	[SellerId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](max) NOT NULL,
	[CustomerName] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[TypePayment] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SellerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_SellerDetail]    Script Date: 29/02/2024 3:18:08 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_SellerDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SellerId] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[ProductDetai] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tb_Cart] ON 

INSERT [dbo].[tb_Cart] ([CartId], [IdKhachHang]) VALUES (1, 1)
INSERT [dbo].[tb_Cart] ([CartId], [IdKhachHang]) VALUES (2, 2)
INSERT [dbo].[tb_Cart] ([CartId], [IdKhachHang]) VALUES (3, 3)
INSERT [dbo].[tb_Cart] ([CartId], [IdKhachHang]) VALUES (4, 4)
SET IDENTITY_INSERT [dbo].[tb_Cart] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_CartItem] ON 

INSERT [dbo].[tb_CartItem] ([CartItem], [CartId], [ProductId], [Quantity], [Price], [PriceTotal], [TemPrice], [ProductDetai]) VALUES (9, 1, 0, 1, CAST(790000 AS Decimal(18, 0)), CAST(790000 AS Decimal(18, 0)), CAST(790000 AS Decimal(18, 0)), 17)
INSERT [dbo].[tb_CartItem] ([CartItem], [CartId], [ProductId], [Quantity], [Price], [PriceTotal], [TemPrice], [ProductDetai]) VALUES (10, 3, 0, 101, CAST(560000 AS Decimal(18, 0)), CAST(560000 AS Decimal(18, 0)), CAST(560000 AS Decimal(18, 0)), 14)
INSERT [dbo].[tb_CartItem] ([CartItem], [CartId], [ProductId], [Quantity], [Price], [PriceTotal], [TemPrice], [ProductDetai]) VALUES (11, 3, 0, 1, CAST(980000 AS Decimal(18, 0)), CAST(980000 AS Decimal(18, 0)), CAST(980000 AS Decimal(18, 0)), 2)
SET IDENTITY_INSERT [dbo].[tb_CartItem] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_ChucNang] ON 

INSERT [dbo].[tb_ChucNang] ([IdChucNang], [TenChucNang], [MaChucNang], [Createby], [CreatedDate], [ModifiedDate], [Modifeby]) VALUES (1, N'admin', N'admin', N'Gia Thuan', CAST(N'2023-12-05T20:54:55.497' AS DateTime), NULL, NULL)
INSERT [dbo].[tb_ChucNang] ([IdChucNang], [TenChucNang], [MaChucNang], [Createby], [CreatedDate], [ModifiedDate], [Modifeby]) VALUES (2, N'Quản Lý', N'quan-ly', N'Gia Thuận', CAST(N'2023-12-05T21:03:33.400' AS DateTime), NULL, NULL)
INSERT [dbo].[tb_ChucNang] ([IdChucNang], [TenChucNang], [MaChucNang], [Createby], [CreatedDate], [ModifiedDate], [Modifeby]) VALUES (3, N'Quản lý kho hàng', N'quan-ly-kho-hang', N'Gia Thuận', CAST(N'2023-12-05T21:03:39.300' AS DateTime), NULL, NULL)
INSERT [dbo].[tb_ChucNang] ([IdChucNang], [TenChucNang], [MaChucNang], [Createby], [CreatedDate], [ModifiedDate], [Modifeby]) VALUES (4, N'Nhân viên bán hàng', N'nhan-vien-ban-hang', N'Gia Thuận', CAST(N'2023-12-05T21:04:16.637' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[tb_ChucNang] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_KhachHang] ON 

INSERT [dbo].[tb_KhachHang] ([IdKhachHang], [SDT], [TenKhachHang], [Email], [Password], [Image], [Birthday], [DiaChi], [SoLanMua], [Code], [Clock]) VALUES (1, N'0970034511', N'Phan Hoài Minh', N'mapuucntt3@gmail.com', N'202cb962ac59075b964b07152d234b70', NULL, NULL, N'Hóc Môn', 1, NULL, 0)
INSERT [dbo].[tb_KhachHang] ([IdKhachHang], [SDT], [TenKhachHang], [Email], [Password], [Image], [Birthday], [DiaChi], [SoLanMua], [Code], [Clock]) VALUES (2, N'0970034511', N'Phan Hoài Minh', N'mapuucntt3@gmail.com', N'202cb962ac59075b964b07152d234b70', NULL, NULL, N'Hóc Môn', 1, NULL, 1)
INSERT [dbo].[tb_KhachHang] ([IdKhachHang], [SDT], [TenKhachHang], [Email], [Password], [Image], [Birthday], [DiaChi], [SoLanMua], [Code], [Clock]) VALUES (3, N'01212454855', N'Văn Hằng', N'Hang@gmail.com', N'202cb962ac59075b964b07152d234b70', NULL, NULL, N'Hóc Môn', 1, NULL, 0)
INSERT [dbo].[tb_KhachHang] ([IdKhachHang], [SDT], [TenKhachHang], [Email], [Password], [Image], [Birthday], [DiaChi], [SoLanMua], [Code], [Clock]) VALUES (4, N'000', N'tthuaajn', NULL, NULL, NULL, NULL, NULL, 1, NULL, 0)
SET IDENTITY_INSERT [dbo].[tb_KhachHang] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_Kho] ON 

INSERT [dbo].[tb_Kho] ([Idkho], [DiaChi], [Createby], [ModifiedDate], [CreatedDate], [Modifeby]) VALUES (1, N'Hóc Môn', N'Gia Thuận ', CAST(N'2023-12-06T00:00:00.000' AS DateTime), CAST(N'2023-12-06T00:00:00.000' AS DateTime), N'Gia Thuận')
SET IDENTITY_INSERT [dbo].[tb_Kho] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_KhoXuat] ON 

INSERT [dbo].[tb_KhoXuat] ([IdKhoXuat], [OutDate], [OutBy], [OrderId], [Idkho], [MSNV]) VALUES (1, CAST(N'2023-12-18T22:56:40.993' AS DateTime), N'Phạm Lê Tuấn Anh', 1, 1, N'263445')
INSERT [dbo].[tb_KhoXuat] ([IdKhoXuat], [OutDate], [OutBy], [OrderId], [Idkho], [MSNV]) VALUES (2, CAST(N'2023-12-18T23:13:03.857' AS DateTime), N'Phạm Lê Tuấn Anh', 2, 1, N'263445')
SET IDENTITY_INSERT [dbo].[tb_KhoXuat] OFF
GO
INSERT [dbo].[tb_NhanVien] ([MSNV], [SDT], [TenNhanVien], [CCCD], [Email], [Password], [Image], [Birthday], [DiaChi], [NgayVaoLam], [Luong], [GioiTinh], [CreatedDate], [IdChucNang], [ModifiedDate], [Clock]) VALUES (N'202105', N'0329867771', N'Gia Thuận', N'123         ', N'mapuucntt3@gmail.com', N'202cb962ac59075b964b07152d234b70', NULL, CAST(N'2002-11-12' AS Date), N'ấp 4', CAST(N'2023-11-12' AS Date), CAST(10000000.00 AS Decimal(18, 2)), N'nam', CAST(N'2023-12-05T20:55:05.317' AS DateTime), 1, NULL, 0)
INSERT [dbo].[tb_NhanVien] ([MSNV], [SDT], [TenNhanVien], [CCCD], [Email], [Password], [Image], [Birthday], [DiaChi], [NgayVaoLam], [Luong], [GioiTinh], [CreatedDate], [IdChucNang], [ModifiedDate], [Clock]) VALUES (N'260761', N'0534347000', N'Lê Thành An', N'453453      ', N' An@gmail.com', N'202cb962ac59075b964b07152d234b70', NULL, CAST(N'2023-03-20' AS Date), N'Tiền Giang', CAST(N'2023-12-05' AS Date), CAST(12300000.00 AS Decimal(18, 2)), N'Nữ', CAST(N'2023-12-05T21:04:55.320' AS DateTime), 2, NULL, 0)
INSERT [dbo].[tb_NhanVien] ([MSNV], [SDT], [TenNhanVien], [CCCD], [Email], [Password], [Image], [Birthday], [DiaChi], [NgayVaoLam], [Luong], [GioiTinh], [CreatedDate], [IdChucNang], [ModifiedDate], [Clock]) VALUES (N'263262', N'0546809833', N'Phan Chí Cường ', N'0546809833  ', N'Cuong@gmail.com', N'202cb962ac59075b964b07152d234b70', NULL, CAST(N'2002-10-16' AS Date), N'Hóc Môn', CAST(N'2023-12-08' AS Date), CAST(15000000.00 AS Decimal(18, 2)), N'Nữ', CAST(N'2023-12-08T23:42:06.413' AS DateTime), 4, NULL, 0)
INSERT [dbo].[tb_NhanVien] ([MSNV], [SDT], [TenNhanVien], [CCCD], [Email], [Password], [Image], [Birthday], [DiaChi], [NgayVaoLam], [Luong], [GioiTinh], [CreatedDate], [IdChucNang], [ModifiedDate], [Clock]) VALUES (N'263445', N'089994554', N'Phạm Lê Tuấn Anh', N'124554      ', N'Anh@gmail.com', N'202cb962ac59075b964b07152d234b70', NULL, CAST(N'2003-03-10' AS Date), N'Hóc Môn', CAST(N'2023-12-05' AS Date), CAST(14000000.00 AS Decimal(18, 2)), N'Nữ', CAST(N'2023-12-05T21:06:17.110' AS DateTime), 3, NULL, 0)
INSERT [dbo].[tb_NhanVien] ([MSNV], [SDT], [TenNhanVien], [CCCD], [Email], [Password], [Image], [Birthday], [DiaChi], [NgayVaoLam], [Luong], [GioiTinh], [CreatedDate], [IdChucNang], [ModifiedDate], [Clock]) VALUES (N'288503', N'0978664412', N'Mai Ngọc Khang', N'5678678     ', N'Khang@gmail.com', N'202cb962ac59075b964b07152d234b70', NULL, CAST(N'2002-01-23' AS Date), N'Hóc Môn', CAST(N'2023-12-05' AS Date), CAST(10000000.00 AS Decimal(18, 2)), N'Nữ', CAST(N'2023-12-05T21:05:30.000' AS DateTime), 1, CAST(N'2023-12-19T00:13:07.960' AS DateTime), 0)
GO
SET IDENTITY_INSERT [dbo].[tb_Order] ON 

INSERT [dbo].[tb_Order] ([OrderId], [Code], [CustomerName], [Phone], [Address], [TotalAmount], [Quantity], [CreatedBy], [CreatedDate], [ModifiedDate], [Modifiedby], [TypePayment], [Email], [IdKhachHang], [typeOrder], [Confirm], [Status], [typeReturn], [Success], [SuccessDate]) VALUES (1, N'DH07106', N'Văn Hằng', N'01212454855', N'Hóc Môn', CAST(3870000.00 AS Decimal(18, 2)), 0, N'01212454855', CAST(N'2023-12-18T22:47:59.883' AS DateTime), CAST(N'2023-12-18T22:47:59.883' AS DateTime), NULL, 1, N'Hang@gmail.com', 3, 0, 1, NULL, 0, 1, CAST(N'2023-12-18T22:56:58.590' AS DateTime))
INSERT [dbo].[tb_Order] ([OrderId], [Code], [CustomerName], [Phone], [Address], [TotalAmount], [Quantity], [CreatedBy], [CreatedDate], [ModifiedDate], [Modifiedby], [TypePayment], [Email], [IdKhachHang], [typeOrder], [Confirm], [Status], [typeReturn], [Success], [SuccessDate]) VALUES (2, N'DH24265', N'Văn Hằng', N'01212454855', N'Hóc Môn', CAST(1210000.00 AS Decimal(18, 2)), 0, N'01212454855', CAST(N'2023-12-18T23:07:20.293' AS DateTime), CAST(N'2023-12-18T23:07:20.293' AS DateTime), NULL, 1, N'Hang@gmail.com', 3, 0, 1, NULL, 0, 0, NULL)
INSERT [dbo].[tb_Order] ([OrderId], [Code], [CustomerName], [Phone], [Address], [TotalAmount], [Quantity], [CreatedBy], [CreatedDate], [ModifiedDate], [Modifiedby], [TypePayment], [Email], [IdKhachHang], [typeOrder], [Confirm], [Status], [typeReturn], [Success], [SuccessDate]) VALUES (3, N'DH28175', N'Văn Hằng', N'01212454855', N'Hóc Môn', CAST(650000.00 AS Decimal(18, 2)), 0, N'01212454855', CAST(N'2023-12-18T23:20:09.230' AS DateTime), CAST(N'2023-12-18T23:20:09.230' AS DateTime), NULL, 1, N'Hang@gmail.com', 3, 1, 0, N'Thay đổi phương thức thanh toán', 0, 0, NULL)
INSERT [dbo].[tb_Order] ([OrderId], [Code], [CustomerName], [Phone], [Address], [TotalAmount], [Quantity], [CreatedBy], [CreatedDate], [ModifiedDate], [Modifiedby], [TypePayment], [Email], [IdKhachHang], [typeOrder], [Confirm], [Status], [typeReturn], [Success], [SuccessDate]) VALUES (4, N'DH84327', N'Văn Hằng', N'01212454855', N'Hóc Môn', CAST(1770000.00 AS Decimal(18, 2)), 0, N'01212454855', CAST(N'2023-12-19T12:42:12.163' AS DateTime), CAST(N'2023-12-19T12:42:12.163' AS DateTime), NULL, 2, N'Hang@gmail.com', 3, 0, 0, NULL, 0, 0, NULL)
INSERT [dbo].[tb_Order] ([OrderId], [Code], [CustomerName], [Phone], [Address], [TotalAmount], [Quantity], [CreatedBy], [CreatedDate], [ModifiedDate], [Modifiedby], [TypePayment], [Email], [IdKhachHang], [typeOrder], [Confirm], [Status], [typeReturn], [Success], [SuccessDate]) VALUES (5, N'DH17512', N'Văn Hằng', N'01212454855', N'Hóc Môn', CAST(650000.00 AS Decimal(18, 2)), 0, N'01212454855', CAST(N'2023-12-19T12:42:49.743' AS DateTime), CAST(N'2023-12-19T12:42:49.743' AS DateTime), NULL, 1, N'Hang@gmail.com', 3, 0, 0, NULL, 0, 0, NULL)
INSERT [dbo].[tb_Order] ([OrderId], [Code], [CustomerName], [Phone], [Address], [TotalAmount], [Quantity], [CreatedBy], [CreatedDate], [ModifiedDate], [Modifiedby], [TypePayment], [Email], [IdKhachHang], [typeOrder], [Confirm], [Status], [typeReturn], [Success], [SuccessDate]) VALUES (6, N'DH36768', N'Văn Hằng', N'01212454855', N'Hóc Môn', CAST(560000.00 AS Decimal(18, 2)), 0, N'01212454855', CAST(N'2023-12-19T12:43:05.537' AS DateTime), CAST(N'2023-12-19T12:43:05.537' AS DateTime), NULL, 2, N'Hang@gmail.com', 3, 0, 0, NULL, 0, 0, NULL)
SET IDENTITY_INSERT [dbo].[tb_Order] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_OrderDetail] ON 

INSERT [dbo].[tb_OrderDetail] ([Id], [OrderId], [Price], [Quantity], [CartItem], [damagedProduct], [ProductDetai]) VALUES (1, 1, CAST(650000.00 AS Decimal(18, 2)), 5, NULL, NULL, 17)
INSERT [dbo].[tb_OrderDetail] ([Id], [OrderId], [Price], [Quantity], [CartItem], [damagedProduct], [ProductDetai]) VALUES (2, 1, CAST(620000.00 AS Decimal(18, 2)), 1, NULL, NULL, 19)
INSERT [dbo].[tb_OrderDetail] ([Id], [OrderId], [Price], [Quantity], [CartItem], [damagedProduct], [ProductDetai]) VALUES (3, 2, CAST(650000.00 AS Decimal(18, 2)), 1, NULL, NULL, 17)
INSERT [dbo].[tb_OrderDetail] ([Id], [OrderId], [Price], [Quantity], [CartItem], [damagedProduct], [ProductDetai]) VALUES (4, 2, CAST(560000.00 AS Decimal(18, 2)), 1, NULL, NULL, 13)
INSERT [dbo].[tb_OrderDetail] ([Id], [OrderId], [Price], [Quantity], [CartItem], [damagedProduct], [ProductDetai]) VALUES (5, 3, CAST(650000.00 AS Decimal(18, 2)), 1, NULL, NULL, 17)
INSERT [dbo].[tb_OrderDetail] ([Id], [OrderId], [Price], [Quantity], [CartItem], [damagedProduct], [ProductDetai]) VALUES (6, 4, CAST(560000.00 AS Decimal(18, 2)), 1, NULL, NULL, 14)
INSERT [dbo].[tb_OrderDetail] ([Id], [OrderId], [Price], [Quantity], [CartItem], [damagedProduct], [ProductDetai]) VALUES (7, 4, CAST(560000.00 AS Decimal(18, 2)), 1, NULL, NULL, 5)
INSERT [dbo].[tb_OrderDetail] ([Id], [OrderId], [Price], [Quantity], [CartItem], [damagedProduct], [ProductDetai]) VALUES (8, 4, CAST(650000.00 AS Decimal(18, 2)), 1, NULL, NULL, 17)
INSERT [dbo].[tb_OrderDetail] ([Id], [OrderId], [Price], [Quantity], [CartItem], [damagedProduct], [ProductDetai]) VALUES (9, 5, CAST(650000.00 AS Decimal(18, 2)), 1, NULL, NULL, 16)
INSERT [dbo].[tb_OrderDetail] ([Id], [OrderId], [Price], [Quantity], [CartItem], [damagedProduct], [ProductDetai]) VALUES (10, 6, CAST(560000.00 AS Decimal(18, 2)), 1, NULL, NULL, 14)
SET IDENTITY_INSERT [dbo].[tb_OrderDetail] OFF
GO
INSERT [dbo].[tb_PhanQuyen] ([MSNV], [IdChucNang], [GhiChu]) VALUES (N'202105', 1, N'')
INSERT [dbo].[tb_PhanQuyen] ([MSNV], [IdChucNang], [GhiChu]) VALUES (N'260761', 2, NULL)
INSERT [dbo].[tb_PhanQuyen] ([MSNV], [IdChucNang], [GhiChu]) VALUES (N'263262', 4, NULL)
INSERT [dbo].[tb_PhanQuyen] ([MSNV], [IdChucNang], [GhiChu]) VALUES (N'263445', 3, NULL)
INSERT [dbo].[tb_PhanQuyen] ([MSNV], [IdChucNang], [GhiChu]) VALUES (N'288503', 2, NULL)
GO
SET IDENTITY_INSERT [dbo].[tb_ProductCategory] ON 

INSERT [dbo].[tb_ProductCategory] ([ProductCategoryId], [Title], [Description], [Icon], [CreatedBy], [CreatedDate], [ModifiedDate], [Modifiedby], [Alias]) VALUES (1, N'Quần', NULL, NULL, NULL, CAST(N'2023-12-02T14:38:26.500' AS DateTime), CAST(N'2023-12-02T14:38:26.500' AS DateTime), NULL, N'quan')
INSERT [dbo].[tb_ProductCategory] ([ProductCategoryId], [Title], [Description], [Icon], [CreatedBy], [CreatedDate], [ModifiedDate], [Modifiedby], [Alias]) VALUES (2, N'Áo', NULL, NULL, NULL, CAST(N'2023-12-02T14:38:30.117' AS DateTime), CAST(N'2023-12-02T14:38:30.117' AS DateTime), NULL, N'ao')
INSERT [dbo].[tb_ProductCategory] ([ProductCategoryId], [Title], [Description], [Icon], [CreatedBy], [CreatedDate], [ModifiedDate], [Modifiedby], [Alias]) VALUES (3, N'Hoodie', NULL, NULL, NULL, CAST(N'2023-12-02T14:38:36.280' AS DateTime), CAST(N'2023-12-02T14:38:36.280' AS DateTime), NULL, N'hoodie')
INSERT [dbo].[tb_ProductCategory] ([ProductCategoryId], [Title], [Description], [Icon], [CreatedBy], [CreatedDate], [ModifiedDate], [Modifiedby], [Alias]) VALUES (4, N'Jacket', NULL, NULL, NULL, CAST(N'2023-12-02T14:38:41.893' AS DateTime), CAST(N'2023-12-02T14:38:41.893' AS DateTime), NULL, N'jacket')
INSERT [dbo].[tb_ProductCategory] ([ProductCategoryId], [Title], [Description], [Icon], [CreatedBy], [CreatedDate], [ModifiedDate], [Modifiedby], [Alias]) VALUES (5, N'Sweater', NULL, NULL, NULL, CAST(N'2023-12-17T12:08:06.627' AS DateTime), CAST(N'2023-12-17T12:08:06.627' AS DateTime), NULL, N'sweater')
SET IDENTITY_INSERT [dbo].[tb_ProductCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_ProductDetai] ON 

INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (1, 3, 98, N'Gia Thuận', CAST(N'2023-12-14T23:19:20.993' AS DateTime), NULL, NULL, NULL, 8)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (2, 1, 98, N'Gia Thuận', CAST(N'2023-12-14T23:19:35.697' AS DateTime), NULL, NULL, NULL, 8)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (3, 2, 100, N'Gia Thuận', CAST(N'2023-12-14T23:19:38.227' AS DateTime), NULL, NULL, NULL, 8)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (4, 1, 100, N'Gia Thuận', CAST(N'2023-12-14T23:19:57.690' AS DateTime), NULL, NULL, NULL, 7)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (5, 2, 99, N'Gia Thuận', CAST(N'2023-12-14T23:20:00.370' AS DateTime), NULL, NULL, NULL, 7)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (6, 3, 100, N'Gia Thuận', CAST(N'2023-12-14T23:20:02.020' AS DateTime), NULL, NULL, NULL, 7)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (7, 1, 100, N'Gia Thuận', CAST(N'2023-12-14T23:20:10.663' AS DateTime), NULL, NULL, NULL, 6)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (8, 2, 100, N'Gia Thuận', CAST(N'2023-12-14T23:20:12.450' AS DateTime), NULL, NULL, NULL, 6)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (9, 3, 100, N'Gia Thuận', CAST(N'2023-12-14T23:20:14.890' AS DateTime), NULL, NULL, NULL, 6)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (10, 1, 100, N'Gia Thuận', CAST(N'2023-12-14T23:20:25.073' AS DateTime), NULL, NULL, NULL, 5)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (11, 2, 100, N'Gia Thuận', CAST(N'2023-12-14T23:20:26.587' AS DateTime), NULL, NULL, NULL, 5)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (12, 3, 100, N'Gia Thuận', CAST(N'2023-12-14T23:20:28.797' AS DateTime), NULL, NULL, NULL, 5)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (13, 1, 92, N'Gia Thuận', CAST(N'2023-12-14T23:20:39.107' AS DateTime), NULL, NULL, NULL, 4)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (14, 2, 97, N'Gia Thuận', CAST(N'2023-12-14T23:20:40.897' AS DateTime), NULL, NULL, NULL, 4)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (15, 1, 98, N'Gia Thuận', CAST(N'2023-12-14T23:20:50.770' AS DateTime), NULL, NULL, NULL, 3)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (16, 2, 99, N'Gia Thuận', CAST(N'2023-12-14T23:20:52.447' AS DateTime), NULL, NULL, NULL, 3)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (17, 3, 93, N'Gia Thuận', CAST(N'2023-12-14T23:20:54.120' AS DateTime), NULL, NULL, NULL, 3)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (18, 1, 100, N'Gia Thuận', CAST(N'2023-12-16T00:37:06.293' AS DateTime), NULL, NULL, NULL, 9)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (19, 2, 99, N'Gia Thuận', CAST(N'2023-12-16T00:37:10.080' AS DateTime), NULL, NULL, NULL, 9)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (20, 1, 98, N'Gia Thuận', CAST(N'2023-12-17T11:53:55.063' AS DateTime), NULL, NULL, NULL, 2)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (21, 2, 98, N'Gia Thuận', CAST(N'2023-12-17T11:53:57.930' AS DateTime), NULL, NULL, NULL, 2)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (22, 3, 97, N'Gia Thuận', CAST(N'2023-12-17T11:53:59.640' AS DateTime), NULL, NULL, NULL, 2)
INSERT [dbo].[tb_ProductDetai] ([ProductDetai], [Size], [Quantity], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [ProductId]) VALUES (23, 3, 99, N'Gia Thuận', CAST(N'2023-12-17T12:01:18.973' AS DateTime), NULL, NULL, NULL, 9)
SET IDENTITY_INSERT [dbo].[tb_ProductDetai] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_ProductImage] ON 

INSERT [dbo].[tb_ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (1, 1, N'/Uploads/images/SanPham/DC%20x%20BR%20Denim%20Racing%20Jacket%20-%20Red-Black.png', 1)
INSERT [dbo].[tb_ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (2, 2, N'/Uploads/images/SanPham/Rope%20Print%20Relaxed%20Shirt%20-%20Black-2.png', 1)
INSERT [dbo].[tb_ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (3, 2, N'/Uploads/images/SanPham/Rope%20Print%20Relaxed%20Shirt%20-%20Black.png', 1)
INSERT [dbo].[tb_ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (4, 3, N'/Uploads/images/SanPham/Academy%20Regular%20Polo%20-%20Cream.png', 1)
INSERT [dbo].[tb_ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (5, 3, N'/Uploads/images/SanPham/Academy%20Regular%20Polo%20-%20Cream-2.png', 1)
INSERT [dbo].[tb_ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (6, 4, N'/Uploads/images/SanPham/Logo%20Relaxed%20Hoodie%20-%20Blue.png', 1)
INSERT [dbo].[tb_ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (7, 5, N'/Uploads/images/SanPham/single_3.jpg', 1)
INSERT [dbo].[tb_ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (8, 6, N'/Uploads/images/SanPham/Khaki%20Work%20Shorts.png', 1)
INSERT [dbo].[tb_ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (9, 7, N'/Uploads/images/SanPham/Logo%20Relaxed%20Hoodie%20-%20Yellow.png', 1)
INSERT [dbo].[tb_ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (10, 8, N'/Uploads/images/SanPham/product_1.png', 1)
INSERT [dbo].[tb_ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (11, 9, N'/Uploads/images/SanPham/product_3.png', 1)
INSERT [dbo].[tb_ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (12, 10, N'/Uploads/images/SanPham/Letters%20Monogram%20Knit%20Sweater%20-%20Black-1.png', 1)
INSERT [dbo].[tb_ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (13, 10, N'/Uploads/images/SanPham/Letters%20Monogram%20Knit%20Sweater%20-%20Black.png', 1)
INSERT [dbo].[tb_ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (14, 11, N'/Uploads/images/SanPham/DirtyCoins%20Print%20Cardigan%20-%20Ivory1.png', 1)
INSERT [dbo].[tb_ProductImage] ([Id], [ProductId], [Image], [IsDefault]) VALUES (15, 11, N'/Uploads/images/SanPham/DirtyCoins%20Print%20Cardigan%20-%20Ivory.png', 1)
SET IDENTITY_INSERT [dbo].[tb_ProductImage] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_Products] ON 

INSERT [dbo].[tb_Products] ([ProductId], [Title], [ProductCode], [Description], [Detail], [Image], [Price], [PriceSale], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitle], [SeoDescription], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [IsActive], [ViewCount], [OrigianlPrice]) VALUES (1, N'BANDANA SHIRT JACKET - BLACK', NULL, NULL, NULL, N'/Uploads/images/SanPham/DC%20x%20BR%20Denim%20Racing%20Jacket%20-%20Red-Black.png', CAST(980000.00 AS Decimal(18, 2)), CAST(790000.00 AS Decimal(18, 2)), 1, 0, 0, NULL, 4, NULL, NULL, NULL, CAST(N'2023-12-02T14:39:33.560' AS DateTime), CAST(N'2023-12-02T14:39:33.560' AS DateTime), NULL, N'bandana-shirt-jacket--black', 1, 6, CAST(320000.00 AS Decimal(18, 2)))
INSERT [dbo].[tb_Products] ([ProductId], [Title], [ProductCode], [Description], [Detail], [Image], [Price], [PriceSale], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitle], [SeoDescription], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [IsActive], [ViewCount], [OrigianlPrice]) VALUES (2, N'ROPE PRINT RELAXED SHIRT - BLACK', NULL, NULL, NULL, N'/Uploads/images/SanPham/Rope%20Print%20Relaxed%20Shirt%20-%20Black.png', CAST(650000.00 AS Decimal(18, 2)), NULL, 1, 0, 0, NULL, 2, NULL, NULL, NULL, CAST(N'2023-12-02T14:40:08.277' AS DateTime), CAST(N'2023-12-02T14:40:08.277' AS DateTime), NULL, N'rope-print-relaxed-shirt--black', 1, 3, CAST(320000.00 AS Decimal(18, 2)))
INSERT [dbo].[tb_Products] ([ProductId], [Title], [ProductCode], [Description], [Detail], [Image], [Price], [PriceSale], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitle], [SeoDescription], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [IsActive], [ViewCount], [OrigianlPrice]) VALUES (3, N'ACADEMY REGULAR POLO - CREAM', NULL, NULL, NULL, N'/Uploads/images/SanPham/Academy%20Regular%20Polo%20-%20Cream.png', CAST(790000.00 AS Decimal(18, 2)), CAST(650000.00 AS Decimal(18, 2)), 1, 1, 0, NULL, 2, NULL, NULL, NULL, CAST(N'2023-12-02T14:40:35.993' AS DateTime), CAST(N'2023-12-02T14:40:35.993' AS DateTime), NULL, N'academy-regular-polo--cream', 1, 1, CAST(320000.00 AS Decimal(18, 2)))
INSERT [dbo].[tb_Products] ([ProductId], [Title], [ProductCode], [Description], [Detail], [Image], [Price], [PriceSale], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitle], [SeoDescription], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [IsActive], [ViewCount], [OrigianlPrice]) VALUES (4, N'LOGO RELAXED HOODIE - BLUE', NULL, NULL, NULL, N'/Uploads/images/SanPham/Logo%20Relaxed%20Hoodie%20-%20Blue.png', CAST(560000.00 AS Decimal(18, 2)), NULL, 1, 1, 0, NULL, 3, NULL, NULL, NULL, CAST(N'2023-12-02T14:41:05.633' AS DateTime), CAST(N'2023-12-02T14:41:05.633' AS DateTime), NULL, N'logo-relaxed-hoodie--blue', 1, 0, CAST(320000.00 AS Decimal(18, 2)))
INSERT [dbo].[tb_Products] ([ProductId], [Title], [ProductCode], [Description], [Detail], [Image], [Price], [PriceSale], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitle], [SeoDescription], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [IsActive], [ViewCount], [OrigianlPrice]) VALUES (5, N'FUR COLLAR SPRAY PAINT DENIM', NULL, NULL, NULL, N'/Uploads/images/SanPham/single_3.jpg', CAST(1560000.00 AS Decimal(18, 2)), CAST(980000.00 AS Decimal(18, 2)), 1, 1, 0, NULL, 4, NULL, NULL, NULL, CAST(N'2023-12-02T14:41:41.463' AS DateTime), CAST(N'2023-12-02T14:41:41.463' AS DateTime), NULL, N'fur-collar-spray-paint-denim', 1, 2, CAST(320000.00 AS Decimal(18, 2)))
INSERT [dbo].[tb_Products] ([ProductId], [Title], [ProductCode], [Description], [Detail], [Image], [Price], [PriceSale], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitle], [SeoDescription], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [IsActive], [ViewCount], [OrigianlPrice]) VALUES (6, N'KHAKI WORK SHORTS', NULL, NULL, NULL, N'/Uploads/images/SanPham/Khaki%20Work%20Shorts.png', CAST(920000.00 AS Decimal(18, 2)), CAST(890000.00 AS Decimal(18, 2)), 1, 1, 0, NULL, 1, NULL, NULL, NULL, CAST(N'2023-12-02T14:42:41.723' AS DateTime), CAST(N'2023-12-02T14:42:41.723' AS DateTime), NULL, N'khaki-work-shorts', 1, 2, CAST(320000.00 AS Decimal(18, 2)))
INSERT [dbo].[tb_Products] ([ProductId], [Title], [ProductCode], [Description], [Detail], [Image], [Price], [PriceSale], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitle], [SeoDescription], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [IsActive], [ViewCount], [OrigianlPrice]) VALUES (7, N'Logo Relaxed Hoodie - Yellow', NULL, NULL, NULL, N'/Uploads/images/SanPham/Logo%20Relaxed%20Hoodie%20-%20Yellow.png', CAST(560000.00 AS Decimal(18, 2)), NULL, 1, 0, 0, NULL, 3, NULL, NULL, NULL, CAST(N'2023-12-06T23:29:01.633' AS DateTime), CAST(N'2023-12-06T23:29:01.633' AS DateTime), NULL, N'logo-relaxed-hoodie--yellow', 1, 1, CAST(320000.00 AS Decimal(18, 2)))
INSERT [dbo].[tb_Products] ([ProductId], [Title], [ProductCode], [Description], [Detail], [Image], [Price], [PriceSale], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitle], [SeoDescription], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [IsActive], [ViewCount], [OrigianlPrice]) VALUES (8, N'BANDANA SHIRT JACKET - BLACK', NULL, NULL, NULL, N'/Uploads/images/SanPham/product_1.png', CAST(980000.00 AS Decimal(18, 2)), CAST(890000.00 AS Decimal(18, 2)), 1, 1, 0, NULL, 4, NULL, NULL, NULL, CAST(N'2023-12-11T13:30:49.157' AS DateTime), CAST(N'2023-12-11T13:30:49.157' AS DateTime), NULL, N'bandana-shirt-jacket--black', 1, 0, CAST(320000.00 AS Decimal(18, 2)))
INSERT [dbo].[tb_Products] ([ProductId], [Title], [ProductCode], [Description], [Detail], [Image], [Price], [PriceSale], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitle], [SeoDescription], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [IsActive], [ViewCount], [OrigianlPrice]) VALUES (9, N'STRIPED RELAXED SHIRT - BLUE', NULL, NULL, NULL, N'/Uploads/images/SanPham/product_3.png', CAST(620000.00 AS Decimal(18, 2)), NULL, 1, 1, 0, NULL, 2, NULL, NULL, NULL, CAST(N'2023-12-16T00:36:43.413' AS DateTime), CAST(N'2023-12-16T00:36:43.413' AS DateTime), NULL, N'striped-relaxed-shirt--blue', 1, 0, CAST(320000.00 AS Decimal(18, 2)))
INSERT [dbo].[tb_Products] ([ProductId], [Title], [ProductCode], [Description], [Detail], [Image], [Price], [PriceSale], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitle], [SeoDescription], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [IsActive], [ViewCount], [OrigianlPrice]) VALUES (10, N'Letters Monogram Knit Sweater - Black', NULL, NULL, N'Chi tiết sản phẩm:
• Chất liệu: Cotton.
• Relaxed Fit.
• Họa tiết monogram đặc trưng của DirtyCoins được dệt trên vải.
• Bo tay áo, bo cổ và bo thân được đánh rách nhẹ.', N'/Uploads/images/SanPham/Letters%20Monogram%20Knit%20Sweater%20-%20Black-1.png', CAST(1100000.00 AS Decimal(18, 2)), NULL, 1, 1, 0, NULL, 5, NULL, NULL, N'Gia Thuận', CAST(N'2023-12-17T12:15:59.023' AS DateTime), CAST(N'2023-12-17T12:15:59.217' AS DateTime), NULL, N'letters-monogram-knit-sweater--black', 1, 0, CAST(250000.00 AS Decimal(18, 2)))
INSERT [dbo].[tb_Products] ([ProductId], [Title], [ProductCode], [Description], [Detail], [Image], [Price], [PriceSale], [IsHome], [IsSale], [IsFeature], [IsHot], [ProductCategoryId], [SeoTitle], [SeoDescription], [CreatedBy], [CreateDate], [ModifiedDate], [Modifeby], [Alias], [IsActive], [ViewCount], [OrigianlPrice]) VALUES (11, N'DirtyCoins Print Cardigan - Ivory/Brown', NULL, NULL, N'Chi tiết sản phẩm:
• Chất liệu: nỉ bông.
• Relaxed Fit.
• 2 túi thân áo.
• Hoạ tiết DirtyCoins được in tràn toàn bộ thân áo.
• Ngực trái thêu logo chữ Y.', N'/Uploads/images/SanPham/DirtyCoins%20Print%20Cardigan%20-%20Ivory.png', CAST(650000.00 AS Decimal(18, 2)), NULL, 1, 0, 0, NULL, 5, NULL, NULL, N'Gia Thuận', CAST(N'2023-12-17T12:33:01.820' AS DateTime), CAST(N'2023-12-17T12:33:01.820' AS DateTime), NULL, N'dirtycoins-print-cardigan--ivorybrown', 1, 0, CAST(250000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[tb_Products] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_Seller] ON 

INSERT [dbo].[tb_Seller] ([SellerId], [Code], [CustomerName], [Phone], [TotalAmount], [Quantity], [CreatedBy], [CreatedDate], [ModifiedDate], [Modifiedby], [TypePayment]) VALUES (1, N'HD2655', N'tthuaajn', N'000', CAST(650000.00 AS Decimal(18, 2)), 0, N'Phan Chí Cường ', CAST(N'2023-12-19T00:25:16.370' AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[tb_Seller] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_SellerDetail] ON 

INSERT [dbo].[tb_SellerDetail] ([Id], [SellerId], [Price], [Quantity], [ProductDetai]) VALUES (1, 1, CAST(650000.00 AS Decimal(18, 2)), 1, 22)
SET IDENTITY_INSERT [dbo].[tb_SellerDetail] OFF
GO
ALTER TABLE [dbo].[tb_Cart]  WITH CHECK ADD  CONSTRAINT [GioHangtoKhachHang] FOREIGN KEY([IdKhachHang])
REFERENCES [dbo].[tb_KhachHang] ([IdKhachHang])
GO
ALTER TABLE [dbo].[tb_Cart] CHECK CONSTRAINT [GioHangtoKhachHang]
GO
ALTER TABLE [dbo].[tb_CartItem]  WITH CHECK ADD  CONSTRAINT [CartItemtoProducdetai] FOREIGN KEY([ProductDetai])
REFERENCES [dbo].[tb_ProductDetai] ([ProductDetai])
GO
ALTER TABLE [dbo].[tb_CartItem] CHECK CONSTRAINT [CartItemtoProducdetai]
GO
ALTER TABLE [dbo].[tb_CartItem]  WITH CHECK ADD  CONSTRAINT [ChiTietGioHangtoGioHang1] FOREIGN KEY([CartId])
REFERENCES [dbo].[tb_Cart] ([CartId])
GO
ALTER TABLE [dbo].[tb_CartItem] CHECK CONSTRAINT [ChiTietGioHangtoGioHang1]
GO
ALTER TABLE [dbo].[tb_KhoNhap]  WITH CHECK ADD  CONSTRAINT [KhoNhaptoKho] FOREIGN KEY([IdKho])
REFERENCES [dbo].[tb_Kho] ([Idkho])
GO
ALTER TABLE [dbo].[tb_KhoNhap] CHECK CONSTRAINT [KhoNhaptoKho]
GO
ALTER TABLE [dbo].[tb_KhoNhap]  WITH CHECK ADD  CONSTRAINT [KhoNhaptoNhanVien] FOREIGN KEY([MSNV])
REFERENCES [dbo].[tb_NhanVien] ([MSNV])
GO
ALTER TABLE [dbo].[tb_KhoNhap] CHECK CONSTRAINT [KhoNhaptoNhanVien]
GO
ALTER TABLE [dbo].[tb_KhoReturn]  WITH CHECK ADD  CONSTRAINT [KhoReturntoKho] FOREIGN KEY([IdKho])
REFERENCES [dbo].[tb_Kho] ([Idkho])
GO
ALTER TABLE [dbo].[tb_KhoReturn] CHECK CONSTRAINT [KhoReturntoKho]
GO
ALTER TABLE [dbo].[tb_KhoReturn]  WITH CHECK ADD  CONSTRAINT [KhoReturntoNhanVien] FOREIGN KEY([MSNV])
REFERENCES [dbo].[tb_NhanVien] ([MSNV])
GO
ALTER TABLE [dbo].[tb_KhoReturn] CHECK CONSTRAINT [KhoReturntoNhanVien]
GO
ALTER TABLE [dbo].[tb_KhoReturn]  WITH CHECK ADD  CONSTRAINT [KhoReturntoReturn] FOREIGN KEY([ReturnId])
REFERENCES [dbo].[tb_Return] ([ReturnId])
GO
ALTER TABLE [dbo].[tb_KhoReturn] CHECK CONSTRAINT [KhoReturntoReturn]
GO
ALTER TABLE [dbo].[tb_KhoXuat]  WITH CHECK ADD  CONSTRAINT [KhoXuattoKho] FOREIGN KEY([Idkho])
REFERENCES [dbo].[tb_Kho] ([Idkho])
GO
ALTER TABLE [dbo].[tb_KhoXuat] CHECK CONSTRAINT [KhoXuattoKho]
GO
ALTER TABLE [dbo].[tb_KhoXuat]  WITH CHECK ADD  CONSTRAINT [KhoXuattoNhanVien] FOREIGN KEY([MSNV])
REFERENCES [dbo].[tb_NhanVien] ([MSNV])
GO
ALTER TABLE [dbo].[tb_KhoXuat] CHECK CONSTRAINT [KhoXuattoNhanVien]
GO
ALTER TABLE [dbo].[tb_KhoXuat]  WITH CHECK ADD  CONSTRAINT [KhoXuattoOrder] FOREIGN KEY([OrderId])
REFERENCES [dbo].[tb_Order] ([OrderId])
GO
ALTER TABLE [dbo].[tb_KhoXuat] CHECK CONSTRAINT [KhoXuattoOrder]
GO
ALTER TABLE [dbo].[tb_NhanVien]  WITH CHECK ADD  CONSTRAINT [NhanVientoChucVu] FOREIGN KEY([IdChucNang])
REFERENCES [dbo].[tb_ChucNang] ([IdChucNang])
GO
ALTER TABLE [dbo].[tb_NhanVien] CHECK CONSTRAINT [NhanVientoChucVu]
GO
ALTER TABLE [dbo].[tb_NhanVienImage]  WITH CHECK ADD  CONSTRAINT [ImgNhanVien] FOREIGN KEY([MSNV])
REFERENCES [dbo].[tb_NhanVien] ([MSNV])
GO
ALTER TABLE [dbo].[tb_NhanVienImage] CHECK CONSTRAINT [ImgNhanVien]
GO
ALTER TABLE [dbo].[tb_Order]  WITH CHECK ADD  CONSTRAINT [OrdertoKhachHang] FOREIGN KEY([IdKhachHang])
REFERENCES [dbo].[tb_KhachHang] ([IdKhachHang])
GO
ALTER TABLE [dbo].[tb_Order] CHECK CONSTRAINT [OrdertoKhachHang]
GO
ALTER TABLE [dbo].[tb_OrderDetail]  WITH CHECK ADD  CONSTRAINT [OrderDetailtoProductDetail] FOREIGN KEY([ProductDetai])
REFERENCES [dbo].[tb_ProductDetai] ([ProductDetai])
GO
ALTER TABLE [dbo].[tb_OrderDetail] CHECK CONSTRAINT [OrderDetailtoProductDetail]
GO
ALTER TABLE [dbo].[tb_OrderDetail]  WITH CHECK ADD  CONSTRAINT [tb_OrderDetail_toi_tb_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[tb_Order] ([OrderId])
GO
ALTER TABLE [dbo].[tb_OrderDetail] CHECK CONSTRAINT [tb_OrderDetail_toi_tb_Order]
GO
ALTER TABLE [dbo].[tb_PhanQuyen]  WITH CHECK ADD  CONSTRAINT [PQuyentoChucNang] FOREIGN KEY([IdChucNang])
REFERENCES [dbo].[tb_ChucNang] ([IdChucNang])
GO
ALTER TABLE [dbo].[tb_PhanQuyen] CHECK CONSTRAINT [PQuyentoChucNang]
GO
ALTER TABLE [dbo].[tb_PhanQuyen]  WITH CHECK ADD  CONSTRAINT [PQuyentoNhanVien] FOREIGN KEY([MSNV])
REFERENCES [dbo].[tb_NhanVien] ([MSNV])
GO
ALTER TABLE [dbo].[tb_PhanQuyen] CHECK CONSTRAINT [PQuyentoNhanVien]
GO
ALTER TABLE [dbo].[tb_ProductDetai]  WITH CHECK ADD  CONSTRAINT [ProductDetaitoProducts] FOREIGN KEY([ProductId])
REFERENCES [dbo].[tb_Products] ([ProductId])
GO
ALTER TABLE [dbo].[tb_ProductDetai] CHECK CONSTRAINT [ProductDetaitoProducts]
GO
ALTER TABLE [dbo].[tb_ProductImage]  WITH CHECK ADD  CONSTRAINT [ProImgtoProduc] FOREIGN KEY([ProductId])
REFERENCES [dbo].[tb_Products] ([ProductId])
GO
ALTER TABLE [dbo].[tb_ProductImage] CHECK CONSTRAINT [ProImgtoProduc]
GO
ALTER TABLE [dbo].[tb_Products]  WITH CHECK ADD  CONSTRAINT [ProductstoProductCategory] FOREIGN KEY([ProductCategoryId])
REFERENCES [dbo].[tb_ProductCategory] ([ProductCategoryId])
GO
ALTER TABLE [dbo].[tb_Products] CHECK CONSTRAINT [ProductstoProductCategory]
GO
ALTER TABLE [dbo].[tb_Return]  WITH CHECK ADD  CONSTRAINT [ReturntoOrder] FOREIGN KEY([OrderId])
REFERENCES [dbo].[tb_Order] ([OrderId])
GO
ALTER TABLE [dbo].[tb_Return] CHECK CONSTRAINT [ReturntoOrder]
GO
ALTER TABLE [dbo].[tb_ReturnDetail]  WITH CHECK ADD  CONSTRAINT [ReturnDetailtoProductDetail] FOREIGN KEY([ProductDetai])
REFERENCES [dbo].[tb_ProductDetai] ([ProductDetai])
GO
ALTER TABLE [dbo].[tb_ReturnDetail] CHECK CONSTRAINT [ReturnDetailtoProductDetail]
GO
ALTER TABLE [dbo].[tb_ReturnDetail]  WITH CHECK ADD  CONSTRAINT [ReturnDetailtoReturn] FOREIGN KEY([ReturnId])
REFERENCES [dbo].[tb_Return] ([ReturnId])
GO
ALTER TABLE [dbo].[tb_ReturnDetail] CHECK CONSTRAINT [ReturnDetailtoReturn]
GO
ALTER TABLE [dbo].[tb_SellerDetail]  WITH CHECK ADD  CONSTRAINT [SellerDetailtoProductDetai] FOREIGN KEY([ProductDetai])
REFERENCES [dbo].[tb_ProductDetai] ([ProductDetai])
GO
ALTER TABLE [dbo].[tb_SellerDetail] CHECK CONSTRAINT [SellerDetailtoProductDetai]
GO
ALTER TABLE [dbo].[tb_SellerDetail]  WITH CHECK ADD  CONSTRAINT [SellerDetailtoSeller] FOREIGN KEY([SellerId])
REFERENCES [dbo].[tb_Seller] ([SellerId])
GO
ALTER TABLE [dbo].[tb_SellerDetail] CHECK CONSTRAINT [SellerDetailtoSeller]
GO
