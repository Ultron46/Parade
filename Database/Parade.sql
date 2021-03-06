USE [master]
GO
/****** Object:  Database [Parade]    Script Date: 02-06-2020 14:50:25 ******/
CREATE DATABASE [Parade]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Parade', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Parade.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Parade_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Parade_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Parade] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Parade].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Parade] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Parade] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Parade] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Parade] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Parade] SET ARITHABORT OFF 
GO
ALTER DATABASE [Parade] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Parade] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Parade] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Parade] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Parade] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Parade] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Parade] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Parade] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Parade] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Parade] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Parade] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Parade] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Parade] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Parade] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Parade] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Parade] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Parade] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Parade] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Parade] SET  MULTI_USER 
GO
ALTER DATABASE [Parade] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Parade] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Parade] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Parade] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Parade] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Parade] SET QUERY_STORE = OFF
GO
USE [Parade]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 02-06-2020 14:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[First Name] [nvarchar](100) NULL,
	[Middle Name] [nvarchar](100) NULL,
	[Last Name] [nvarchar](100) NULL,
	[Taluko] [nvarchar](100) NULL,
	[District] [nvarchar](100) NULL,
	[Date of Birth] [date] NULL,
	[Medical History] [nvarchar](max) NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [Parade] SET  READ_WRITE 
GO
