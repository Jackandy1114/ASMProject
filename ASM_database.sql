USE [master]
GO
/****** Object:  Database [Asm_C#2]    Script Date: 5/18/2022 3:05:55 AM ******/
CREATE DATABASE [Asm_C#2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Asm_C#2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.JACKANDY1114\MSSQL\DATA\Asm_C#2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Asm_C#2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.JACKANDY1114\MSSQL\DATA\Asm_C#2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Asm_C#2] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Asm_C#2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Asm_C#2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Asm_C#2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Asm_C#2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Asm_C#2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Asm_C#2] SET ARITHABORT OFF 
GO
ALTER DATABASE [Asm_C#2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Asm_C#2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Asm_C#2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Asm_C#2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Asm_C#2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Asm_C#2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Asm_C#2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Asm_C#2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Asm_C#2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Asm_C#2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Asm_C#2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Asm_C#2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Asm_C#2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Asm_C#2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Asm_C#2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Asm_C#2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Asm_C#2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Asm_C#2] SET RECOVERY FULL 
GO
ALTER DATABASE [Asm_C#2] SET  MULTI_USER 
GO
ALTER DATABASE [Asm_C#2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Asm_C#2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Asm_C#2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Asm_C#2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Asm_C#2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Asm_C#2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Asm_C#2', N'ON'
GO
ALTER DATABASE [Asm_C#2] SET QUERY_STORE = OFF
GO
USE [Asm_C#2]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 5/18/2022 3:05:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[IdClass] [int] IDENTITY(1,1) NOT NULL,
	[NameClass] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[IdClass] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 5/18/2022 3:05:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Mark] [float] NOT NULL,
	[Email] [varchar](100) NULL,
	[IdClass] [int] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Class] ON 

INSERT [dbo].[Class] ([IdClass], [NameClass]) VALUES (1, N'C#1')
INSERT [dbo].[Class] ([IdClass], [NameClass]) VALUES (2, N'C#2')
INSERT [dbo].[Class] ([IdClass], [NameClass]) VALUES (3, N'C#4')
INSERT [dbo].[Class] ([IdClass], [NameClass]) VALUES (4, N'Marketing')
INSERT [dbo].[Class] ([IdClass], [NameClass]) VALUES (5, N'UDPM')
INSERT [dbo].[Class] ([IdClass], [NameClass]) VALUES (6, N'AI')
INSERT [dbo].[Class] ([IdClass], [NameClass]) VALUES (7, N'Youtube')
INSERT [dbo].[Class] ([IdClass], [NameClass]) VALUES (8, N'C#5')
SET IDENTITY_INSERT [dbo].[Class] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([StId], [Name], [Mark], [Email], [IdClass]) VALUES (1, N'Trần Phú Đạt', 10, N'dattranphu1114@gmail.com', 5)
INSERT [dbo].[Student] ([StId], [Name], [Mark], [Email], [IdClass]) VALUES (2, N'Trương Hồng Hoa', 8.3, N'honghoa@gmail.com', 2)
INSERT [dbo].[Student] ([StId], [Name], [Mark], [Email], [IdClass]) VALUES (3, N'Sơn Hoàng', 3, N'hello@gmail.com', 3)
INSERT [dbo].[Student] ([StId], [Name], [Mark], [Email], [IdClass]) VALUES (4, N'Sơn Râu', 2, N'sonrau@gmail.com', 6)
INSERT [dbo].[Student] ([StId], [Name], [Mark], [Email], [IdClass]) VALUES (5, N'Thùy Dương', 4.42, N'thythyduong@gmail.com', 4)
INSERT [dbo].[Student] ([StId], [Name], [Mark], [Email], [IdClass]) VALUES (6, N'Thanh Quyền', 6.67, N'quyenthanh@gmail.com', 7)
INSERT [dbo].[Student] ([StId], [Name], [Mark], [Email], [IdClass]) VALUES (7, N'Người Yêu Đạt', 9.2, N'jackandy249@gmail.com', 1)
INSERT [dbo].[Student] ([StId], [Name], [Mark], [Email], [IdClass]) VALUES (8, N'Người Yêu Hoàng', 7.2, N'andy249@gmail.com', 8)
INSERT [dbo].[Student] ([StId], [Name], [Mark], [Email], [IdClass]) VALUES (9, N'Đứa không yêu Hoàng', 5.2, N'jack@gmail.com', 2)
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Class] FOREIGN KEY([IdClass])
REFERENCES [dbo].[Class] ([IdClass])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Class]
GO
USE [master]
GO
ALTER DATABASE [Asm_C#2] SET  READ_WRITE 
GO
