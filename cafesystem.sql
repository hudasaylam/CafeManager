USE [master]
GO
/****** Object:  Database [cafesystem]    Script Date: 19.08.2024 21:13:23 ******/
CREATE DATABASE [cafesystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'cafesystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\cafesystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'cafesystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\cafesystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [cafesystem] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [cafesystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [cafesystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [cafesystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [cafesystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [cafesystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [cafesystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [cafesystem] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [cafesystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [cafesystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [cafesystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [cafesystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [cafesystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [cafesystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [cafesystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [cafesystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [cafesystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [cafesystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [cafesystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [cafesystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [cafesystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [cafesystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [cafesystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [cafesystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [cafesystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [cafesystem] SET  MULTI_USER 
GO
ALTER DATABASE [cafesystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [cafesystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [cafesystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [cafesystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [cafesystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [cafesystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [cafesystem] SET QUERY_STORE = ON
GO
ALTER DATABASE [cafesystem] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [cafesystem]
GO
/****** Object:  Table [dbo].[Admins]    Script Date: 19.08.2024 21:13:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[AdminID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_username] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[discounts]    Script Date: 19.08.2024 21:13:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[discounts](
	[DiscountID] [int] IDENTITY(1,1) NOT NULL,
	[DiscountName] [nvarchar](255) NOT NULL,
	[DiscountType] [nvarchar](50) NOT NULL,
	[DiscountValue] [decimal](10, 2) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[ProductID] [int] NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[DiscountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orderlist]    Script Date: 19.08.2024 21:13:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orderlist](
	[OrderListID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 19.08.2024 21:13:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[Total] [decimal](10, 2) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 19.08.2024 21:13:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategory] [varchar](50) NOT NULL,
	[ProductName] [varchar](50) NOT NULL,
	[Size] [varchar](50) NOT NULL,
	[Price] [decimal](10, 2) NOT NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[discounts] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[discounts] ADD  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[orderlist] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[orderlist] ADD  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[orders] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[orders] ADD  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT (getdate()) FOR [updated_at]
GO
ALTER TABLE [dbo].[discounts]  WITH CHECK ADD  CONSTRAINT [FK_ProductID] FOREIGN KEY([ProductID])
REFERENCES [dbo].[products] ([ProductID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[discounts] CHECK CONSTRAINT [FK_ProductID]
GO
ALTER TABLE [dbo].[orderlist]  WITH CHECK ADD  CONSTRAINT [FK_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[orders] ([OrderID])
GO
ALTER TABLE [dbo].[orderlist] CHECK CONSTRAINT [FK_Order]
GO
ALTER TABLE [dbo].[orderlist]  WITH CHECK ADD  CONSTRAINT [FK_Prod] FOREIGN KEY([ProductID])
REFERENCES [dbo].[products] ([ProductID])
GO
ALTER TABLE [dbo].[orderlist] CHECK CONSTRAINT [FK_Prod]
GO
ALTER TABLE [dbo].[discounts]  WITH CHECK ADD CHECK  (([DiscountType]='fixed_amount' OR [DiscountType]='percentage'))
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [CHK_ProductCategory] CHECK  (([ProductCategory]='HotChocolate' OR [ProductCategory]='Pastry' OR [ProductCategory]='Juice' OR [ProductCategory]='Milkshake' OR [ProductCategory]='Water' OR [ProductCategory]='Tea' OR [ProductCategory]='Coffee'))
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [CHK_ProductCategory]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [CHK_Size] CHECK  (([Size]='Standard' OR [Size]='Large' OR [Size]='Medium' OR [Size]='Small'))
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [CHK_Size]
GO
/****** Object:  DdlTrigger [trg_SetOrderIDOnColumnAdd]    Script Date: 19.08.2024 21:13:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [trg_SetOrderIDOnColumnAdd]
ON DATABASE
FOR ALTER_TABLE
AS
BEGIN
    DECLARE @LastOrderID INT;

    -- Check if the altered table is OrdersList
    IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrdersList]') AND type = 'U')
    BEGIN
        -- Get the last OrderID from the orders table
        SELECT @LastOrderID = MAX(OrderID) FROM orders;

        -- Update the OrderID in OrdersList with the last OrderID
        UPDATE OrdersList
        SET OrderID = @LastOrderID
        WHERE OrderID IS NULL;
    END
END;
GO
DISABLE TRIGGER [trg_SetOrderIDOnColumnAdd] ON DATABASE
GO
/****** Object:  DdlTrigger [trg_SetOrderIDOnColumnAdd2]    Script Date: 19.08.2024 21:13:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE TRIGGER [trg_SetOrderIDOnColumnAdd2]
ON DATABASE
FOR ALTER_TABLE
AS
BEGIN
    DECLARE @LastOrderID INT;

    -- Check if the altered table is OrdersList
    IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OrdersList]') AND type = 'U')
    BEGIN
        -- Get the last OrderID from the orders table
        SELECT @LastOrderID = MAX(OrderID) FROM orders;

        -- Only update rows where OrderID is NULL
        IF @LastOrderID IS NOT NULL
        BEGIN
            UPDATE OrdersList
            SET OrderID = @LastOrderID
            WHERE OrderID IS NULL;
        END
    END
END;
GO
DISABLE TRIGGER [trg_SetOrderIDOnColumnAdd2] ON DATABASE
GO
ENABLE TRIGGER [trg_SetOrderIDOnColumnAdd] ON DATABASE
GO
ENABLE TRIGGER [trg_SetOrderIDOnColumnAdd2] ON DATABASE
GO
USE [master]
GO
ALTER DATABASE [cafesystem] SET  READ_WRITE 
GO
