USE [master]
GO
/****** Object:  Database [Cupones]    Script Date: 19/11/2024 16:36:31 ******/
CREATE DATABASE [Cupones]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Cupones', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Cupones.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Cupones_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Cupones_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Cupones] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Cupones].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Cupones] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Cupones] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Cupones] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Cupones] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Cupones] SET ARITHABORT OFF 
GO
ALTER DATABASE [Cupones] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Cupones] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Cupones] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Cupones] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Cupones] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Cupones] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Cupones] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Cupones] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Cupones] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Cupones] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Cupones] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Cupones] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Cupones] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Cupones] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Cupones] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Cupones] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Cupones] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Cupones] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Cupones] SET  MULTI_USER 
GO
ALTER DATABASE [Cupones] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Cupones] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Cupones] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Cupones] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Cupones] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Cupones] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Cupones] SET QUERY_STORE = OFF
GO
USE [Cupones]
GO
/****** Object:  Table [dbo].[Articulos]    Script Date: 19/11/2024 16:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulos](
	[Id_Articulo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Articulo] [varchar](100) NOT NULL,
	[Descripcion_Articulo] [varchar](100) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Articulos] PRIMARY KEY CLUSTERED 
(
	[Id_Articulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 19/11/2024 16:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id_Categoria] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[Id_Categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 19/11/2024 16:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[CodCliente] [varchar](12) NULL,
	[Nombre_Cliente] [varchar](100) NULL,
	[Apellido_Cliente] [varchar](100) NULL,
	[Direccion] [varchar](100) NULL,
	[Email] [varchar](100) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupones]    Script Date: 19/11/2024 16:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupones](
	[id_Cupon] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](300) NOT NULL,
	[Descripcion] [varchar](300) NOT NULL,
	[PorcentajeDto] [numeric](18, 2) NULL,
	[ImportePromo] [numeric](18, 2) NULL,
	[FechaInicio] [date] NOT NULL,
	[FechaFin] [date] NOT NULL,
	[Id_Tipo_Cupon] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Cupones_1] PRIMARY KEY CLUSTERED 
(
	[id_Cupon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupones_Categorias]    Script Date: 19/11/2024 16:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupones_Categorias](
	[Id_Cupones_Categorias] [int] IDENTITY(1,1) NOT NULL,
	[Id_Cupon] [int] NOT NULL,
	[Id_Categoria] [int] NOT NULL,
 CONSTRAINT [PK_Cupones_Categorias] PRIMARY KEY CLUSTERED 
(
	[Id_Cupones_Categorias] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupones_Clientes]    Script Date: 19/11/2024 16:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupones_Clientes](
	[id_Cupon] [int] NOT NULL,
	[NroCupon] [varchar](12) NOT NULL,
	[FechaAsignado] [datetime] NOT NULL,
	[CodCliente] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Cupones_Clientes] PRIMARY KEY CLUSTERED 
(
	[NroCupon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupones_Detalle]    Script Date: 19/11/2024 16:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupones_Detalle](
	[id_Cupon] [int] NOT NULL,
	[id_Articulo] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_Cupones_Detalle] PRIMARY KEY CLUSTERED 
(
	[id_Cupon] ASC,
	[id_Articulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cupones_Historial]    Script Date: 19/11/2024 16:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cupones_Historial](
	[id_Cupon] [int] NOT NULL,
	[NroCupon] [varchar](12) NOT NULL,
	[FechaUso] [date] NOT NULL,
	[CodCliente] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Cupones_Historial] PRIMARY KEY CLUSTERED 
(
	[id_Cupon] ASC,
	[NroCupon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Precios]    Script Date: 19/11/2024 16:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Precios](
	[Id_Precio] [int] IDENTITY(1,1) NOT NULL,
	[Id_Articulo] [int] NOT NULL,
	[Precio] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_Precios] PRIMARY KEY CLUSTERED 
(
	[Id_Precio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tipo_Cupon]    Script Date: 19/11/2024 16:36:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tipo_Cupon](
	[Id_Tipo_Cupon] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Tipo_Cupon] PRIMARY KEY CLUSTERED 
(
	[Id_Tipo_Cupon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cupones] ADD  CONSTRAINT [DF__Cupones__Activo__73BA3083]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Cupones]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Tipo_Cupon] FOREIGN KEY([Id_Tipo_Cupon])
REFERENCES [dbo].[Tipo_Cupon] ([Id_Tipo_Cupon])
GO
ALTER TABLE [dbo].[Cupones] CHECK CONSTRAINT [FK_Cupones_Tipo_Cupon]
GO
ALTER TABLE [dbo].[Cupones_Categorias]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Categorias_Categorias1] FOREIGN KEY([Id_Categoria])
REFERENCES [dbo].[Categorias] ([Id_Categoria])
GO
ALTER TABLE [dbo].[Cupones_Categorias] CHECK CONSTRAINT [FK_Cupones_Categorias_Categorias1]
GO
ALTER TABLE [dbo].[Cupones_Categorias]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Categorias_Cupones1] FOREIGN KEY([Id_Cupon])
REFERENCES [dbo].[Cupones] ([id_Cupon])
GO
ALTER TABLE [dbo].[Cupones_Categorias] CHECK CONSTRAINT [FK_Cupones_Categorias_Cupones1]
GO
ALTER TABLE [dbo].[Cupones_Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Clientes_Cupones] FOREIGN KEY([id_Cupon])
REFERENCES [dbo].[Cupones] ([id_Cupon])
GO
ALTER TABLE [dbo].[Cupones_Clientes] CHECK CONSTRAINT [FK_Cupones_Clientes_Cupones]
GO
ALTER TABLE [dbo].[Cupones_Detalle]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Detalle_Articulos] FOREIGN KEY([id_Articulo])
REFERENCES [dbo].[Articulos] ([Id_Articulo])
GO
ALTER TABLE [dbo].[Cupones_Detalle] CHECK CONSTRAINT [FK_Cupones_Detalle_Articulos]
GO
ALTER TABLE [dbo].[Cupones_Detalle]  WITH CHECK ADD  CONSTRAINT [FK_Cupones_Detalle_Cupones] FOREIGN KEY([id_Cupon])
REFERENCES [dbo].[Cupones] ([id_Cupon])
GO
ALTER TABLE [dbo].[Cupones_Detalle] CHECK CONSTRAINT [FK_Cupones_Detalle_Cupones]
GO
ALTER TABLE [dbo].[Precios]  WITH NOCHECK ADD  CONSTRAINT [FK_Precios_Articulos] FOREIGN KEY([Id_Articulo])
REFERENCES [dbo].[Articulos] ([Id_Articulo])
GO
ALTER TABLE [dbo].[Precios] NOCHECK CONSTRAINT [FK_Precios_Articulos]
GO
USE [master]
GO
ALTER DATABASE [Cupones] SET  READ_WRITE 
GO
