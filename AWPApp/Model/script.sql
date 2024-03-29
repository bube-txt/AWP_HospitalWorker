USE [AWP_Hospital_Worker]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 30.05.2022 6:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TableDepartment] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FixedRoom]    Script Date: 30.05.2022 6:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FixedRoom](
	[FixedRoomId] [int] IDENTITY(1,1) NOT NULL,
	[RoomId] [int] NOT NULL,
	[PatientId] [int] NOT NULL,
 CONSTRAINT [PK_FixedRoom] PRIMARY KEY CLUSTERED 
(
	[FixedRoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 30.05.2022 6:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Job](
	[JobId] [int] IDENTITY(1,1) NOT NULL,
	[JobName] [nvarchar](100) NOT NULL,
	[JobAccessLevel] [int] NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 30.05.2022 6:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[PatientId] [int] IDENTITY(7,1) NOT NULL,
	[PatientFirstName] [nvarchar](100) NOT NULL,
	[PatientLastName] [nvarchar](100) NOT NULL,
	[PatientPatronicName] [nvarchar](100) NOT NULL,
	[PatientDate] [date] NOT NULL,
	[PatientInArchive] [tinyint] NOT NULL,
 CONSTRAINT [PK_TablePatient] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Record]    Script Date: 30.05.2022 6:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Record](
	[RecordId] [int] IDENTITY(1,1) NOT NULL,
	[PacientId] [int] NOT NULL,
	[RecordHeader] [nvarchar](100) NOT NULL,
	[RecordText] [nvarchar](4000) NOT NULL,
	[DoctorId] [int] NOT NULL,
	[RecordDate] [date] NOT NULL,
 CONSTRAINT [PK_Record] PRIMARY KEY CLUSTERED 
(
	[RecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 30.05.2022 6:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[RoomId] [int] IDENTITY(1,1) NOT NULL,
	[RoomNumber] [int] NOT NULL,
	[RoomDepartmentId] [int] NOT NULL,
	[DoctorId] [int] NULL,
 CONSTRAINT [PK_TableRooms] PRIMARY KEY CLUSTERED 
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Special]    Script Date: 30.05.2022 6:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Special](
	[SpecialId] [int] IDENTITY(1,1) NOT NULL,
	[SpecialJobId] [int] NOT NULL,
	[SpecialName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Special] PRIMARY KEY CLUSTERED 
(
	[SpecialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 30.05.2022 6:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[StatusId] [char](1) NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TempertureList]    Script Date: 30.05.2022 6:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TempertureList](
	[TempertureListId] [int] IDENTITY(1,1) NOT NULL,
	[TempertureListData] [text] NULL,
	[TempertureListPatientId] [int] NOT NULL,
 CONSTRAINT [PK_TempertureList] PRIMARY KEY CLUSTERED 
(
	[TempertureListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 30.05.2022 6:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[TicketId] [int] IDENTITY(1,1) NOT NULL,
	[TicketPatientId] [int] NOT NULL,
	[TicketDate] [date] NOT NULL,
	[TicketDoctorId] [int] NOT NULL,
	[TicketRoomId] [int] NOT NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TicketHistory]    Script Date: 30.05.2022 6:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketHistory](
	[TicketHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[TicketHistoryPatientId] [int] NOT NULL,
	[TicketHistoryDate] [date] NOT NULL,
	[TicketHistoryDoctorId] [int] NOT NULL,
	[TicketHistoryRoomId] [int] NOT NULL,
	[TicketHistoryFinishDate] [date] NULL,
	[TicketHistoryStatusId] [char](1) NOT NULL,
 CONSTRAINT [PK_TicketHistory] PRIMARY KEY CLUSTERED 
(
	[TicketHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 30.05.2022 6:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](100) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[UserJobId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Worker]    Script Date: 30.05.2022 6:05:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Worker](
	[WorkerId] [int] IDENTITY(1,1) NOT NULL,
	[WorkerFirstName] [nvarchar](100) NOT NULL,
	[WorkerLastName] [nvarchar](100) NOT NULL,
	[WorkerPatronicName] [nvarchar](100) NOT NULL,
	[WorkerJobId] [int] NOT NULL,
	[WorkerScheduleStart] [time](0) NULL,
	[WorkerScheduleEnd] [time](0) NULL,
	[WorkerUserId] [int] NULL,
	[WorkerSpecialId] [int] NULL,
 CONSTRAINT [PK_TableWorker] PRIMARY KEY CLUSTERED 
(
	[WorkerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (1, N'Хирургическое')
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (2, N'Терапевтическое')
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (3, N'Кардиалогическоe')
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (4, N'Гинекологическое')
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (1002, N'Гастроэнтрологическое')
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (1003, N'Неврологическое')
INSERT [dbo].[Department] ([DepartmentId], [DepartmentName]) VALUES (1004, N'Реанимационное')
SET IDENTITY_INSERT [dbo].[Department] OFF
SET IDENTITY_INSERT [dbo].[Job] ON 

INSERT [dbo].[Job] ([JobId], [JobName], [JobAccessLevel]) VALUES (1, N'Мед. работник', 2)
INSERT [dbo].[Job] ([JobId], [JobName], [JobAccessLevel]) VALUES (2, N'Главврач', 1)
INSERT [dbo].[Job] ([JobId], [JobName], [JobAccessLevel]) VALUES (3, N'Администратор', 3)
INSERT [dbo].[Job] ([JobId], [JobName], [JobAccessLevel]) VALUES (4, N'Система', 0)
INSERT [dbo].[Job] ([JobId], [JobName], [JobAccessLevel]) VALUES (5, N'Персонал', 4)
SET IDENTITY_INSERT [dbo].[Job] OFF
SET IDENTITY_INSERT [dbo].[Patient] ON 

INSERT [dbo].[Patient] ([PatientId], [PatientFirstName], [PatientLastName], [PatientPatronicName], [PatientDate], [PatientInArchive]) VALUES (2, N'Леонид', N'Дитов', N'Константинович', CAST(N'2022-06-11' AS Date), 0)
INSERT [dbo].[Patient] ([PatientId], [PatientFirstName], [PatientLastName], [PatientPatronicName], [PatientDate], [PatientInArchive]) VALUES (3, N'Шамиль', N'Ахбаев', N'Павлович', CAST(N'2012-06-12' AS Date), 0)
INSERT [dbo].[Patient] ([PatientId], [PatientFirstName], [PatientLastName], [PatientPatronicName], [PatientDate], [PatientInArchive]) VALUES (6, N'Даниил', N'Леминцев', N'Михайлович', CAST(N'2023-01-12' AS Date), 0)
INSERT [dbo].[Patient] ([PatientId], [PatientFirstName], [PatientLastName], [PatientPatronicName], [PatientDate], [PatientInArchive]) VALUES (7, N'Давид', N'Жуков', N'Жуков', CAST(N'2000-01-10' AS Date), 0)
INSERT [dbo].[Patient] ([PatientId], [PatientFirstName], [PatientLastName], [PatientPatronicName], [PatientDate], [PatientInArchive]) VALUES (8, N'Михаил', N'Жуков', N'Жуков', CAST(N'2000-01-11' AS Date), 1)
INSERT [dbo].[Patient] ([PatientId], [PatientFirstName], [PatientLastName], [PatientPatronicName], [PatientDate], [PatientInArchive]) VALUES (9, N'Никита', N'Шахир', N'Шахир', CAST(N'2003-04-06' AS Date), 0)
INSERT [dbo].[Patient] ([PatientId], [PatientFirstName], [PatientLastName], [PatientPatronicName], [PatientDate], [PatientInArchive]) VALUES (10, N'b', N'a', N'cc', CAST(N'1999-01-01' AS Date), 1)
SET IDENTITY_INSERT [dbo].[Patient] OFF
SET IDENTITY_INSERT [dbo].[Record] ON 

INSERT [dbo].[Record] ([RecordId], [PacientId], [RecordHeader], [RecordText], [DoctorId], [RecordDate]) VALUES (2, 2, N'Заговок', N'<FlowDocument PagePadding="5,0,5,0" AllowDrop="True" NumberSubstitution.CultureSource="User" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"><Paragraph><Run xml:lang="ru-ru">Текст записи</Run></Paragraph></FlowDocument>', 2004, CAST(N'2022-05-07' AS Date))
INSERT [dbo].[Record] ([RecordId], [PacientId], [RecordHeader], [RecordText], [DoctorId], [RecordDate]) VALUES (3, 2, N'Заговок', N'<FlowDocument PagePadding="5,0,5,0" AllowDrop="True" NumberSubstitution.CultureSource="User" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"><Paragraph><Run xml:lang="ru-ru">Текст записи</Run></Paragraph></FlowDocument>', 2004, CAST(N'2022-05-07' AS Date))
INSERT [dbo].[Record] ([RecordId], [PacientId], [RecordHeader], [RecordText], [DoctorId], [RecordDate]) VALUES (4, 2, N'Новый заголовок', N'<FlowDocument PagePadding="5,0,5,0" AllowDrop="True" NumberSubstitution.CultureSource="User" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"><Paragraph><Run xml:lang="ru-ru">Новый текст</Run></Paragraph></FlowDocument>', 2004, CAST(N'2022-05-07' AS Date))
INSERT [dbo].[Record] ([RecordId], [PacientId], [RecordHeader], [RecordText], [DoctorId], [RecordDate]) VALUES (5, 2, N'Ещё один заголовок', N'<FlowDocument PagePadding="5,0,5,0" AllowDrop="True" NumberSubstitution.CultureSource="User" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"><Paragraph><Run xml:lang="ru-ru">Ещё много текста</Run></Paragraph></FlowDocument>', 2004, CAST(N'2022-05-07' AS Date))
INSERT [dbo].[Record] ([RecordId], [PacientId], [RecordHeader], [RecordText], [DoctorId], [RecordDate]) VALUES (6, 2, N'2324', N'<FlowDocument PagePadding="5,0,5,0" AllowDrop="True" NumberSubstitution.CultureSource="User" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"><Paragraph>gfgfbfgbfghgh</Paragraph><Paragraph>232323</Paragraph></FlowDocument>', 2004, CAST(N'2022-05-08' AS Date))
INSERT [dbo].[Record] ([RecordId], [PacientId], [RecordHeader], [RecordText], [DoctorId], [RecordDate]) VALUES (7, 2, N'Тест', N'<FlowDocument PagePadding="5,0,5,0" AllowDrop="True" NumberSubstitution.CultureSource="User" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"><Paragraph><Run xml:lang="ru-ru">Тест</Run></Paragraph></FlowDocument>', 2, CAST(N'2022-05-08' AS Date))
INSERT [dbo].[Record] ([RecordId], [PacientId], [RecordHeader], [RecordText], [DoctorId], [RecordDate]) VALUES (8, 2, N'123', N'<FlowDocument PagePadding="5,0,5,0" AllowDrop="True" NumberSubstitution.CultureSource="User" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"><Paragraph><Run xml:lang="ru-ru">етекст</Run></Paragraph><Paragraph><Run xml:lang="ru-ru">авпва</Run></Paragraph><Paragraph><Run xml:lang="ru-ru">п</Run></Paragraph><Paragraph><Run xml:lang="ru-ru">паваппапва</Run></Paragraph><Paragraph><Run xml:lang="ru-ru" xml:space="preserve" /></Paragraph></FlowDocument>', 2004, CAST(N'2022-05-11' AS Date))
INSERT [dbo].[Record] ([RecordId], [PacientId], [RecordHeader], [RecordText], [DoctorId], [RecordDate]) VALUES (9, 7, N'Title', N'<FlowDocument PagePadding="5,0,5,0" AllowDrop="True" NumberSubstitution.CultureSource="User" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"><Paragraph>Text</Paragraph></FlowDocument>', 2, CAST(N'2022-05-11' AS Date))
INSERT [dbo].[Record] ([RecordId], [PacientId], [RecordHeader], [RecordText], [DoctorId], [RecordDate]) VALUES (10, 2, N'123', N'<FlowDocument PagePadding="5,0,5,0" AllowDrop="True" NumberSubstitution.CultureSource="User" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"><Paragraph>2313 123 dg fdgfdgfdg</Paragraph></FlowDocument>', 2004, CAST(N'2022-05-28' AS Date))
INSERT [dbo].[Record] ([RecordId], [PacientId], [RecordHeader], [RecordText], [DoctorId], [RecordDate]) VALUES (11, 10, N'123', N'<FlowDocument PagePadding="5,0,5,0" AllowDrop="True" NumberSubstitution.CultureSource="User" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"><Paragraph>321</Paragraph></FlowDocument>', 2004, CAST(N'2022-05-28' AS Date))
INSERT [dbo].[Record] ([RecordId], [PacientId], [RecordHeader], [RecordText], [DoctorId], [RecordDate]) VALUES (12, 9, N'123', N'<FlowDocument PagePadding="5,0,5,0" AllowDrop="True" NumberSubstitution.CultureSource="User" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"><Paragraph>321</Paragraph></FlowDocument>', 2004, CAST(N'2022-05-28' AS Date))
INSERT [dbo].[Record] ([RecordId], [PacientId], [RecordHeader], [RecordText], [DoctorId], [RecordDate]) VALUES (1010, 2, N'hello', N'<FlowDocument PagePadding="5,0,5,0" AllowDrop="True" NumberSubstitution.CultureSource="User" xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"><Paragraph>World</Paragraph></FlowDocument>', 2004, CAST(N'2022-05-29' AS Date))
SET IDENTITY_INSERT [dbo].[Record] OFF
SET IDENTITY_INSERT [dbo].[Room] ON 

INSERT [dbo].[Room] ([RoomId], [RoomNumber], [RoomDepartmentId], [DoctorId]) VALUES (2, 106, 3, 1003)
INSERT [dbo].[Room] ([RoomId], [RoomNumber], [RoomDepartmentId], [DoctorId]) VALUES (1004, 302, 2, 2)
INSERT [dbo].[Room] ([RoomId], [RoomNumber], [RoomDepartmentId], [DoctorId]) VALUES (2004, 100, 3, 1003)
INSERT [dbo].[Room] ([RoomId], [RoomNumber], [RoomDepartmentId], [DoctorId]) VALUES (2005, 204, 4, 1003)
INSERT [dbo].[Room] ([RoomId], [RoomNumber], [RoomDepartmentId], [DoctorId]) VALUES (3002, 303, 2, 1003)
INSERT [dbo].[Room] ([RoomId], [RoomNumber], [RoomDepartmentId], [DoctorId]) VALUES (4002, 403, 1002, 2003)
INSERT [dbo].[Room] ([RoomId], [RoomNumber], [RoomDepartmentId], [DoctorId]) VALUES (4003, 402, 1002, 2003)
INSERT [dbo].[Room] ([RoomId], [RoomNumber], [RoomDepartmentId], [DoctorId]) VALUES (5002, 401, 1002, 2003)
INSERT [dbo].[Room] ([RoomId], [RoomNumber], [RoomDepartmentId], [DoctorId]) VALUES (5003, 400, 1002, 2003)
INSERT [dbo].[Room] ([RoomId], [RoomNumber], [RoomDepartmentId], [DoctorId]) VALUES (5004, 105, 3, 2)
SET IDENTITY_INSERT [dbo].[Room] OFF
SET IDENTITY_INSERT [dbo].[Special] ON 

INSERT [dbo].[Special] ([SpecialId], [SpecialJobId], [SpecialName]) VALUES (1, 5, N'Архивариус')
INSERT [dbo].[Special] ([SpecialId], [SpecialJobId], [SpecialName]) VALUES (2, 1, N'Хирург')
INSERT [dbo].[Special] ([SpecialId], [SpecialJobId], [SpecialName]) VALUES (3, 1, N'Мед. сестра')
INSERT [dbo].[Special] ([SpecialId], [SpecialJobId], [SpecialName]) VALUES (6, 1, N'Мед. брат')
INSERT [dbo].[Special] ([SpecialId], [SpecialJobId], [SpecialName]) VALUES (7, 1, N'Терапевт')
INSERT [dbo].[Special] ([SpecialId], [SpecialJobId], [SpecialName]) VALUES (8, 1, N'Кардиолог')
INSERT [dbo].[Special] ([SpecialId], [SpecialJobId], [SpecialName]) VALUES (9, 1, N'Гениколог')
INSERT [dbo].[Special] ([SpecialId], [SpecialJobId], [SpecialName]) VALUES (10, 1, N'Гастроэнтролог')
INSERT [dbo].[Special] ([SpecialId], [SpecialJobId], [SpecialName]) VALUES (11, 1, N'Невролог')
INSERT [dbo].[Special] ([SpecialId], [SpecialJobId], [SpecialName]) VALUES (13, 1, N'Реаниматолог')
INSERT [dbo].[Special] ([SpecialId], [SpecialJobId], [SpecialName]) VALUES (14, 4, N'Система')
SET IDENTITY_INSERT [dbo].[Special] OFF
INSERT [dbo].[Status] ([StatusId], [StatusName]) VALUES (N'C', N'Завершено')
INSERT [dbo].[Status] ([StatusId], [StatusName]) VALUES (N'P', N'В процессе')
INSERT [dbo].[Status] ([StatusId], [StatusName]) VALUES (N'R', N'Возврат')
SET IDENTITY_INSERT [dbo].[Ticket] ON 

INSERT [dbo].[Ticket] ([TicketId], [TicketPatientId], [TicketDate], [TicketDoctorId], [TicketRoomId]) VALUES (14, 2, CAST(N'2022-05-20' AS Date), 1003, 3002)
INSERT [dbo].[Ticket] ([TicketId], [TicketPatientId], [TicketDate], [TicketDoctorId], [TicketRoomId]) VALUES (15, 7, CAST(N'2022-05-10' AS Date), 2, 1004)
INSERT [dbo].[Ticket] ([TicketId], [TicketPatientId], [TicketDate], [TicketDoctorId], [TicketRoomId]) VALUES (17, 6, CAST(N'2022-05-29' AS Date), 1003, 3002)
INSERT [dbo].[Ticket] ([TicketId], [TicketPatientId], [TicketDate], [TicketDoctorId], [TicketRoomId]) VALUES (1016, 6, CAST(N'2022-05-14' AS Date), 1003, 2004)
SET IDENTITY_INSERT [dbo].[Ticket] OFF
SET IDENTITY_INSERT [dbo].[TicketHistory] ON 

INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (0, 2, CAST(N'2022-05-02' AS Date), 2, 1004, CAST(N'2022-05-06' AS Date), N'R')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (1, 6, CAST(N'2022-05-12' AS Date), 1003, 3002, CAST(N'2022-05-06' AS Date), N'P')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (2, 3, CAST(N'2022-06-01' AS Date), 2003, 5002, CAST(N'2022-05-08' AS Date), N'C')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (3, 2, CAST(N'2022-05-27' AS Date), 1003, 2004, CAST(N'2022-05-06' AS Date), N'R')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (4, 6, CAST(N'2022-07-16' AS Date), 2, 1004, CAST(N'2022-05-06' AS Date), N'C')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (5, 2, CAST(N'2022-07-27' AS Date), 1003, 2005, CAST(N'2022-05-06' AS Date), N'R')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (6, 3, CAST(N'2022-05-14' AS Date), 1003, 2, CAST(N'2022-05-08' AS Date), N'C')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (7, 2, CAST(N'2022-05-14' AS Date), 1003, 2004, CAST(N'2022-05-08' AS Date), N'C')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (8, 2, CAST(N'2022-05-27' AS Date), 2, 1004, CAST(N'2022-05-08' AS Date), N'C')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (9, 6, CAST(N'2022-05-06' AS Date), 2003, 5003, CAST(N'2022-05-08' AS Date), N'C')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (10, 7, CAST(N'2022-05-09' AS Date), 1003, 3002, CAST(N'2022-06-08' AS Date), N'C')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (11, 7, CAST(N'2022-05-08' AS Date), 2003, 4003, CAST(N'2022-05-08' AS Date), N'R')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (12, 7, CAST(N'2022-05-08' AS Date), 2, 1004, CAST(N'2022-05-08' AS Date), N'C')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (13, 7, CAST(N'2022-05-28' AS Date), 1003, 3002, CAST(N'2022-05-08' AS Date), N'C')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (14, 2, CAST(N'2022-05-20' AS Date), 1003, 3002, CAST(N'0001-01-01' AS Date), N'P')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (15, 7, CAST(N'2022-05-10' AS Date), 2, 1004, CAST(N'0001-01-01' AS Date), N'P')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (16, 10, CAST(N'2022-05-14' AS Date), 2003, 5002, CAST(N'2022-05-28' AS Date), N'R')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (17, 6, CAST(N'2022-05-29' AS Date), 1003, 3002, CAST(N'0001-01-01' AS Date), N'P')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (18, 9, CAST(N'2022-06-04' AS Date), 2003, 5002, CAST(N'2022-05-28' AS Date), N'C')
INSERT [dbo].[TicketHistory] ([TicketHistoryId], [TicketHistoryPatientId], [TicketHistoryDate], [TicketHistoryDoctorId], [TicketHistoryRoomId], [TicketHistoryFinishDate], [TicketHistoryStatusId]) VALUES (1016, 6, CAST(N'2022-05-14' AS Date), 1003, 2004, CAST(N'0001-01-01' AS Date), N'P')
SET IDENTITY_INSERT [dbo].[TicketHistory] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Login], [Password], [UserJobId]) VALUES (1, N'qwe', N'qwe', 1)
INSERT [dbo].[Users] ([UserId], [Login], [Password], [UserJobId]) VALUES (2, N'leo', N'cam', 1)
INSERT [dbo].[Users] ([UserId], [Login], [Password], [UserJobId]) VALUES (1002, N'grisha', N'sahar', 1)
INSERT [dbo].[Users] ([UserId], [Login], [Password], [UserJobId]) VALUES (1003, N'system', N'system', 4)
SET IDENTITY_INSERT [dbo].[Users] OFF
SET IDENTITY_INSERT [dbo].[Worker] ON 

INSERT [dbo].[Worker] ([WorkerId], [WorkerFirstName], [WorkerLastName], [WorkerPatronicName], [WorkerJobId], [WorkerScheduleStart], [WorkerScheduleEnd], [WorkerUserId], [WorkerSpecialId]) VALUES (2, N'Олег', N'Газманов', N'Игоревич', 1, CAST(N'12:00:00' AS Time), CAST(N'20:00:00' AS Time), 1, 10)
INSERT [dbo].[Worker] ([WorkerId], [WorkerFirstName], [WorkerLastName], [WorkerPatronicName], [WorkerJobId], [WorkerScheduleStart], [WorkerScheduleEnd], [WorkerUserId], [WorkerSpecialId]) VALUES (1003, N'Леонид', N'Камаев', N'Иванович', 1, CAST(N'06:00:00' AS Time), CAST(N'16:00:00' AS Time), 2, 6)
INSERT [dbo].[Worker] ([WorkerId], [WorkerFirstName], [WorkerLastName], [WorkerPatronicName], [WorkerJobId], [WorkerScheduleStart], [WorkerScheduleEnd], [WorkerUserId], [WorkerSpecialId]) VALUES (2003, N'Григорий', N'Захаров', N'Николаевич', 1, CAST(N'09:00:00' AS Time), CAST(N'17:00:00' AS Time), 1002, 8)
INSERT [dbo].[Worker] ([WorkerId], [WorkerFirstName], [WorkerLastName], [WorkerPatronicName], [WorkerJobId], [WorkerScheduleStart], [WorkerScheduleEnd], [WorkerUserId], [WorkerSpecialId]) VALUES (2004, N'Имя', N'Фамилия', N'Отчество', 4, CAST(N'00:00:00' AS Time), CAST(N'00:00:00' AS Time), 1003, 14)
INSERT [dbo].[Worker] ([WorkerId], [WorkerFirstName], [WorkerLastName], [WorkerPatronicName], [WorkerJobId], [WorkerScheduleStart], [WorkerScheduleEnd], [WorkerUserId], [WorkerSpecialId]) VALUES (2005, N'Джон', N'Йор', N'Стюарт', 1, CAST(N'12:00:00' AS Time), CAST(N'00:00:00' AS Time), NULL, 11)
SET IDENTITY_INSERT [dbo].[Worker] OFF
ALTER TABLE [dbo].[FixedRoom]  WITH CHECK ADD  CONSTRAINT [FK_FixedRoom_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([PatientId])
GO
ALTER TABLE [dbo].[FixedRoom] CHECK CONSTRAINT [FK_FixedRoom_Patient]
GO
ALTER TABLE [dbo].[FixedRoom]  WITH CHECK ADD  CONSTRAINT [FK_FixedRoom_Room] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[FixedRoom] CHECK CONSTRAINT [FK_FixedRoom_Room]
GO
ALTER TABLE [dbo].[Record]  WITH CHECK ADD  CONSTRAINT [FK_Record_Patient] FOREIGN KEY([PacientId])
REFERENCES [dbo].[Patient] ([PatientId])
GO
ALTER TABLE [dbo].[Record] CHECK CONSTRAINT [FK_Record_Patient]
GO
ALTER TABLE [dbo].[Record]  WITH CHECK ADD  CONSTRAINT [FK_Record_Worker] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Worker] ([WorkerId])
GO
ALTER TABLE [dbo].[Record] CHECK CONSTRAINT [FK_Record_Worker]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_Room_Worker] FOREIGN KEY([DoctorId])
REFERENCES [dbo].[Worker] ([WorkerId])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_Room_Worker]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_TableRooms_TableDepartment] FOREIGN KEY([RoomDepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_TableRooms_TableDepartment]
GO
ALTER TABLE [dbo].[Special]  WITH CHECK ADD  CONSTRAINT [FK_Special_Job] FOREIGN KEY([SpecialJobId])
REFERENCES [dbo].[Job] ([JobId])
GO
ALTER TABLE [dbo].[Special] CHECK CONSTRAINT [FK_Special_Job]
GO
ALTER TABLE [dbo].[TempertureList]  WITH CHECK ADD  CONSTRAINT [FK_TempertureList_Patient] FOREIGN KEY([TempertureListPatientId])
REFERENCES [dbo].[Patient] ([PatientId])
GO
ALTER TABLE [dbo].[TempertureList] CHECK CONSTRAINT [FK_TempertureList_Patient]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Patient] FOREIGN KEY([TicketPatientId])
REFERENCES [dbo].[Patient] ([PatientId])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Patient]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Room] FOREIGN KEY([TicketRoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Room]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Worker] FOREIGN KEY([TicketDoctorId])
REFERENCES [dbo].[Worker] ([WorkerId])
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Worker]
GO
ALTER TABLE [dbo].[TicketHistory]  WITH CHECK ADD  CONSTRAINT [FK_TicketHistory_Patient] FOREIGN KEY([TicketHistoryPatientId])
REFERENCES [dbo].[Patient] ([PatientId])
GO
ALTER TABLE [dbo].[TicketHistory] CHECK CONSTRAINT [FK_TicketHistory_Patient]
GO
ALTER TABLE [dbo].[TicketHistory]  WITH CHECK ADD  CONSTRAINT [FK_TicketHistory_Room] FOREIGN KEY([TicketHistoryRoomId])
REFERENCES [dbo].[Room] ([RoomId])
GO
ALTER TABLE [dbo].[TicketHistory] CHECK CONSTRAINT [FK_TicketHistory_Room]
GO
ALTER TABLE [dbo].[TicketHistory]  WITH CHECK ADD  CONSTRAINT [FK_TicketHistory_Status] FOREIGN KEY([TicketHistoryStatusId])
REFERENCES [dbo].[Status] ([StatusId])
GO
ALTER TABLE [dbo].[TicketHistory] CHECK CONSTRAINT [FK_TicketHistory_Status]
GO
ALTER TABLE [dbo].[TicketHistory]  WITH CHECK ADD  CONSTRAINT [FK_TicketHistory_Worker] FOREIGN KEY([TicketHistoryDoctorId])
REFERENCES [dbo].[Worker] ([WorkerId])
GO
ALTER TABLE [dbo].[TicketHistory] CHECK CONSTRAINT [FK_TicketHistory_Worker]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_User_Job] FOREIGN KEY([UserJobId])
REFERENCES [dbo].[Job] ([JobId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_User_Job]
GO
ALTER TABLE [dbo].[Worker]  WITH CHECK ADD  CONSTRAINT [FK_Worker_Job] FOREIGN KEY([WorkerJobId])
REFERENCES [dbo].[Job] ([JobId])
GO
ALTER TABLE [dbo].[Worker] CHECK CONSTRAINT [FK_Worker_Job]
GO
ALTER TABLE [dbo].[Worker]  WITH CHECK ADD  CONSTRAINT [FK_Worker_Special] FOREIGN KEY([WorkerSpecialId])
REFERENCES [dbo].[Special] ([SpecialId])
GO
ALTER TABLE [dbo].[Worker] CHECK CONSTRAINT [FK_Worker_Special]
GO
ALTER TABLE [dbo].[Worker]  WITH CHECK ADD  CONSTRAINT [FK_Worker_User] FOREIGN KEY([WorkerUserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Worker] CHECK CONSTRAINT [FK_Worker_User]
GO
