USE [master]
GO
/****** Object:  Database [MeetGenerator]    Script Date: 29.10.2015 1:23:54 ******/
CREATE DATABASE [MeetGenerator]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TEST', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\MeetGenerator.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TEST_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\MeetGenerator_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MeetGenerator] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MeetGenerator].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MeetGenerator] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MeetGenerator] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MeetGenerator] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MeetGenerator] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MeetGenerator] SET ARITHABORT OFF 
GO
ALTER DATABASE [MeetGenerator] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MeetGenerator] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MeetGenerator] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MeetGenerator] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MeetGenerator] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MeetGenerator] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MeetGenerator] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MeetGenerator] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MeetGenerator] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MeetGenerator] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MeetGenerator] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MeetGenerator] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MeetGenerator] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MeetGenerator] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MeetGenerator] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MeetGenerator] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MeetGenerator] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MeetGenerator] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MeetGenerator] SET  MULTI_USER 
GO
ALTER DATABASE [MeetGenerator] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MeetGenerator] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MeetGenerator] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MeetGenerator] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MeetGenerator] SET DELAYED_DURABILITY = DISABLED 
GO
USE [MeetGenerator]
GO
/****** Object:  Table [dbo].[INVITATIONS]    Script Date: 29.10.2015 1:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[INVITATIONS](
	[meeting_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
	[invitation_accepted] [nvarchar](5) NOT NULL,
 CONSTRAINT [PK_INVITATIONS] PRIMARY KEY CLUSTERED 
(
	[meeting_id] ASC,
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MEETINGS]    Script Date: 29.10.2015 1:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MEETINGS](
	[meeting_id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
	[date] [datetime] NULL,
	[place] [nvarchar](100) NULL,
 CONSTRAINT [PK_MEETINGS] PRIMARY KEY CLUSTERED 
(
	[meeting_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[USERS]    Script Date: 29.10.2015 1:23:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[user_id] [int] NOT NULL,
	[name] [nvarchar](50) NULL,
	[mail] [nvarchar](50) NULL,
	[facebook_link] [nvarchar](100) NULL,
	[google_link] [nvarchar](100) NULL,
	[vk_link] [nvarchar](100) NULL,
 CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[INVITATIONS]  WITH CHECK ADD  CONSTRAINT [FK_INVITATIONS_MEETINGS] FOREIGN KEY([meeting_id])
REFERENCES [dbo].[MEETINGS] ([meeting_id])
GO
ALTER TABLE [dbo].[INVITATIONS] CHECK CONSTRAINT [FK_INVITATIONS_MEETINGS]
GO
ALTER TABLE [dbo].[INVITATIONS]  WITH CHECK ADD  CONSTRAINT [FK_INVITATIONS_USERS] FOREIGN KEY([user_id])
REFERENCES [dbo].[USERS] ([user_id])
GO
ALTER TABLE [dbo].[INVITATIONS] CHECK CONSTRAINT [FK_INVITATIONS_USERS]
GO
USE [master]
GO
ALTER DATABASE [MeetGenerator] SET  READ_WRITE 
GO
