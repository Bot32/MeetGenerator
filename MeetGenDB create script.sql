USE [master]
GO
/****** Object:  Database [MeetGenDB]    Script Date: 12.11.2015 15:13:03 ******/
CREATE DATABASE [MeetGenDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MeetGenDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\MeetGenDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MeetGenDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\MeetGenDB_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MeetGenDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MeetGenDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MeetGenDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MeetGenDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MeetGenDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MeetGenDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MeetGenDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MeetGenDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MeetGenDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MeetGenDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MeetGenDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MeetGenDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MeetGenDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MeetGenDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MeetGenDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MeetGenDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MeetGenDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MeetGenDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MeetGenDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MeetGenDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MeetGenDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MeetGenDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MeetGenDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MeetGenDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MeetGenDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MeetGenDB] SET  MULTI_USER 
GO
ALTER DATABASE [MeetGenDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MeetGenDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MeetGenDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MeetGenDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MeetGenDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [MeetGenDB]
GO
/****** Object:  Table [dbo].[Invitations]    Script Date: 12.11.2015 15:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invitations](
	[MeetingID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Invitations] PRIMARY KEY CLUSTERED 
(
	[MeetingID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Meeting]    Script Date: 12.11.2015 15:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meeting](
	[ID] [uniqueidentifier] NOT NULL,
	[OwnerID] [uniqueidentifier] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[PlaceID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Meeting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Place]    Script Date: 12.11.2015 15:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Place](
	[ID] [uniqueidentifier] NOT NULL,
	[Address] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](128) NULL,
 CONSTRAINT [PK_Place] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 12.11.2015 15:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[ID] [uniqueidentifier] NOT NULL,
	[FirstName] [varchar](128) NOT NULL,
	[LastName] [varchar](128) NULL,
	[Email] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserSocialLink]    Script Date: 12.11.2015 15:13:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSocialLink](
	[UserID] [uniqueidentifier] NOT NULL,
	[Facebook] [nvarchar](128) NULL,
	[Twitter] [nvarchar](128) NULL,
	[Google] [nvarchar](128) NULL,
	[Vkontakte] [nvarchar](128) NULL,
 CONSTRAINT [PK_UserSocialLink] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Invitations]  WITH CHECK ADD  CONSTRAINT [FK_Invitations_Meeting] FOREIGN KEY([MeetingID])
REFERENCES [dbo].[Meeting] ([ID])
GO
ALTER TABLE [dbo].[Invitations] CHECK CONSTRAINT [FK_Invitations_Meeting]
GO
ALTER TABLE [dbo].[Invitations]  WITH CHECK ADD  CONSTRAINT [FK_Invitations_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Invitations] CHECK CONSTRAINT [FK_Invitations_User]
GO
ALTER TABLE [dbo].[Meeting]  WITH CHECK ADD  CONSTRAINT [FK_Meeting_Place] FOREIGN KEY([PlaceID])
REFERENCES [dbo].[Place] ([ID])
GO
ALTER TABLE [dbo].[Meeting] CHECK CONSTRAINT [FK_Meeting_Place]
GO
ALTER TABLE [dbo].[Meeting]  WITH CHECK ADD  CONSTRAINT [FK_Meeting_User] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Meeting] CHECK CONSTRAINT [FK_Meeting_User]
GO
ALTER TABLE [dbo].[UserSocialLink]  WITH CHECK ADD  CONSTRAINT [FK_UserSocialLink_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserSocialLink] CHECK CONSTRAINT [FK_UserSocialLink_User]
GO
USE [master]
GO
ALTER DATABASE [MeetGenDB] SET  READ_WRITE 
GO
