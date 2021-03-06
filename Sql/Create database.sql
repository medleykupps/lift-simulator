USE [master]
GO



CREATE DATABASE [LiftSimulator] ON  PRIMARY 
( NAME = N'LiftSimulator', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\LiftSimulator.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'LiftSimulator_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\LiftSimulator_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [LiftSimulator] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LiftSimulator].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LiftSimulator] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [LiftSimulator] SET ANSI_NULLS OFF
GO
ALTER DATABASE [LiftSimulator] SET ANSI_PADDING OFF
GO
ALTER DATABASE [LiftSimulator] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [LiftSimulator] SET ARITHABORT OFF
GO
ALTER DATABASE [LiftSimulator] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [LiftSimulator] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [LiftSimulator] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [LiftSimulator] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [LiftSimulator] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [LiftSimulator] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [LiftSimulator] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [LiftSimulator] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [LiftSimulator] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [LiftSimulator] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [LiftSimulator] SET  DISABLE_BROKER
GO
ALTER DATABASE [LiftSimulator] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [LiftSimulator] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [LiftSimulator] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [LiftSimulator] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [LiftSimulator] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [LiftSimulator] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [LiftSimulator] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [LiftSimulator] SET  READ_WRITE
GO
ALTER DATABASE [LiftSimulator] SET RECOVERY SIMPLE
GO
ALTER DATABASE [LiftSimulator] SET  MULTI_USER
GO
ALTER DATABASE [LiftSimulator] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [LiftSimulator] SET DB_CHAINING OFF
GO
USE [LiftSimulator]
GO
/****** Object:  Table [dbo].[LiftMovements]    Script Date: 04/20/2016 00:42:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LiftMovements](
	[Tick] [int] NOT NULL,
	[LiftId] [nvarchar](12) NOT NULL,
	[PeopleCount] [int] NOT NULL,
	[Level] [int] NOT NULL,
	[Action] [int] NOT NULL,
	[ActionDesc] [nvarchar](64) NOT NULL,
	[Message] [nvarchar](256) NOT NULL
) ON [PRIMARY]
GO
