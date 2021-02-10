/****** Object:  Database [T3RMSWS]    Script Date: 12/03/2020 10:02:26 AM ******/
CREATE DATABASE [T3RMSWS]
GO
USE [T3RMSWS]
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12/03/2020 10:02:26 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 12/03/2020 10:02:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 12/03/2020 10:02:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 12/03/2020 10:02:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 12/03/2020 10:02:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 12/03/2020 10:02:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 12/03/2020 10:02:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 12/03/2020 10:02:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[People]    Script Date: 12/03/2020 10:02:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[Id] [nvarchar](450) NOT NULL,
	[Age] [int] NOT NULL,
	[RestaurantId] [int] NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservationRequests]    Script Date: 12/03/2020 10:02:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservationRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumberOfGuest] [int] NOT NULL,
	[StartDateTime] [datetime2](7) NOT NULL,
	[EndDateTime] [datetime2](7) NOT NULL,
	[Requirement] [nvarchar](max) NULL,
	[ReservationSource] [int] NOT NULL,
	[GuestName] [nvarchar](max) NOT NULL,
	[SittingType] [int] NOT NULL,
	[SittingId] [int] NULL,
	[PersonId] [nvarchar](450) NULL,
 CONSTRAINT [PK_ReservationRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Restaurants]    Script Date: 12/03/2020 10:02:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Restaurants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Phone] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
 CONSTRAINT [PK_Restaurants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sittings]    Script Date: 12/03/2020 10:02:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sittings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StartTime] [datetime2](7) NOT NULL,
	[EndTime] [datetime2](7) NOT NULL,
	[Capacity] [int] NOT NULL,
	[SittingType] [int] NOT NULL,
 CONSTRAINT [PK_Sittings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'3.1.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200304225943_Inititial', N'3.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200305085537_AddSittingAndReservation', N'3.1.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200305100559_AddGuestName', N'3.1.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200305105243_RemoveSittingId', N'3.1.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200305121943_AddSittingType', N'3.1.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200306090942_AddRestaurantAndPerson', N'3.1.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200306102844_AddSittingTypeToSitting', N'3.1.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200306103702_updateSitting', N'3.1.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200310082936_AddDuration', N'3.1.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200311095628_AddReservationsToPerson', N'3.1.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200311102504_AddPersonId', N'3.1.0')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'9e5f5f76-1bc7-423f-83d0-48197a00ff3e', N'Manager', N'MANAGER', N'756cadc6-a5a4-4cfa-bb9a-3222e1f76a46')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'bfd6daa2-75cd-41c9-83f4-9bde8f3f8fbd', N'Member', N'MEMBER', N'f63cbe0d-de06-4dea-b2a9-aef607fb96cb')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b66ee88e-0f3d-40b6-990d-500c45689c75', N'9e5f5f76-1bc7-423f-83d0-48197a00ff3e')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd8333f3d-7fbf-4283-a4e2-aa35811b67f3', N'9e5f5f76-1bc7-423f-83d0-48197a00ff3e')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'376068bc-7cf1-44f1-8e4f-fd503b3c3a87', N'bfd6daa2-75cd-41c9-83f4-9bde8f3f8fbd')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6bb2e745-3840-4d4b-8399-918ca7251144', N'bfd6daa2-75cd-41c9-83f4-9bde8f3f8fbd')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fbce0425-71ae-4b41-99f9-b39d4a446cf5', N'bfd6daa2-75cd-41c9-83f4-9bde8f3f8fbd')
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'376068bc-7cf1-44f1-8e4f-fd503b3c3a87', N'm2@e.com', N'M2@E.COM', N'm2@e.com', N'M2@E.COM', 0, N'AQAAAAEAACcQAAAAEAe/igTe6+EILPUuhrhHYV9ec4F3bOkn0l5HoA7G9T5r+WRSC3rz8SN0+Fyzl6TXLg==', N'REYTS7HKIUHSCZCP4AHWVKR2SVXVQJHG', N'2ebf4960-c86d-4349-a953-62be25b99881', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'6bb2e745-3840-4d4b-8399-918ca7251144', N'm1@e.com', N'M1@E.COM', N'm1@e.com', N'M1@E.COM', 0, N'AQAAAAEAACcQAAAAEJWSy4iM5SQGQzC6ZYvDpDSfa4co5Y12cixObIUXbNph2p5W8ntZnFHgbFHP5u1zqg==', N'P3CEDJ577VRFAFJNI5RIYJX4ZKYKIKRE', N'52080860-eb21-4c57-b91a-8ceab952e256', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'b66ee88e-0f3d-40b6-990d-500c45689c75', N'manager@e.com', N'MANAGER@E.COM', N'manager@e.com', N'MANAGER@E.COM', 0, N'AQAAAAEAACcQAAAAEEdEKCNwRRZuMyU4qzAz19pOa0VxaDWcccE6Xb7WLS6hRli3iUgEy0MMg6rUUoyS2A==', N'F3ZLFDFHOD45DN3CXCICDAHI4W654VZ5', N'df8beab2-ea51-4293-8172-10471c02da00', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd8333f3d-7fbf-4283-a4e2-aa35811b67f3', N'manager2@e.com', N'MANAGER2@E.COM', N'manager2@e.com', N'MANAGER2@E.COM', 0, N'AQAAAAEAACcQAAAAEHC4pqWCBpTXFcKXk4TI98tJKnh+QGAlJd+SM+/3eFnLwf+G5yIbEq0gF3qsR08yGw==', N'UR4FBIJGACAIUNIMT2K4L3Z4GYT6LFMU', N'1197dbfe-38de-4333-b97e-567de808e7d5', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'dc33619c-25db-45d6-ba2e-cd825fa59f36', N'a@e.com', N'A@E.COM', N'a@e.com', N'A@E.COM', 1, N'AQAAAAEAACcQAAAAEL6fyjHp5MVpPnbv95YGERVBFQA2/9f/wbupJx11OYcY67pN6kGd5BuTsZOy0NlGSw==', N'N5NKMIA33G3JRNUB7DIIWD6X732RNVNL', N'c62e0318-f934-4f01-acbe-15068603e743', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'fbce0425-71ae-4b41-99f9-b39d4a446cf5', N'm3@e.com', N'M3@E.COM', N'm3@e.com', N'M3@E.COM', 0, N'AQAAAAEAACcQAAAAEOS7QR76Z0T311NEkNcUoEC6L2zX/R6TuYOZyMypkM24uZRdnWzNJtyRtRHnjq+Xig==', N'OQI2JQ4O6SJZS66OIHMORXFKHLBNQ6R3', N'ea808b15-297b-4672-8fbb-3a9e315f2cca', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[People] ([Id], [Age], [RestaurantId]) VALUES (N'376068bc-7cf1-44f1-8e4f-fd503b3c3a87', 30, 1)
INSERT [dbo].[People] ([Id], [Age], [RestaurantId]) VALUES (N'423c6ed6-be60-456c-841b-7e4d9e10ff43', 6, 1)
INSERT [dbo].[People] ([Id], [Age], [RestaurantId]) VALUES (N'4b78e8db-6c44-4330-bb56-9725a7998064', 23, 1)
INSERT [dbo].[People] ([Id], [Age], [RestaurantId]) VALUES (N'6bb2e745-3840-4d4b-8399-918ca7251144', 25, 1)
INSERT [dbo].[People] ([Id], [Age], [RestaurantId]) VALUES (N'b66ee88e-0f3d-40b6-990d-500c45689c75', 10, 1)
INSERT [dbo].[People] ([Id], [Age], [RestaurantId]) VALUES (N'd8333f3d-7fbf-4283-a4e2-aa35811b67f3', 24, 1)
INSERT [dbo].[People] ([Id], [Age], [RestaurantId]) VALUES (N'fbce0425-71ae-4b41-99f9-b39d4a446cf5', 35, 1)
SET IDENTITY_INSERT [dbo].[ReservationRequests] ON 

INSERT [dbo].[ReservationRequests] ([Id], [NumberOfGuest], [StartDateTime], [EndDateTime], [Requirement], [ReservationSource], [GuestName], [SittingType], [SittingId], [PersonId]) VALUES (26, 3, CAST(N'2020-03-12T11:11:00.0000000' AS DateTime2), CAST(N'2020-03-12T14:22:00.0000000' AS DateTime2), NULL, 1, N'Tim', 1, 5, N'fbce0425-71ae-4b41-99f9-b39d4a446cf5')
INSERT [dbo].[ReservationRequests] ([Id], [NumberOfGuest], [StartDateTime], [EndDateTime], [Requirement], [ReservationSource], [GuestName], [SittingType], [SittingId], [PersonId]) VALUES (27, 7, CAST(N'2020-03-12T14:22:00.0000000' AS DateTime2), CAST(N'2020-03-12T15:33:00.0000000' AS DateTime2), NULL, 1, N'Xing', 1, 5, N'6bb2e745-3840-4d4b-8399-918ca7251144')
INSERT [dbo].[ReservationRequests] ([Id], [NumberOfGuest], [StartDateTime], [EndDateTime], [Requirement], [ReservationSource], [GuestName], [SittingType], [SittingId], [PersonId]) VALUES (28, 4, CAST(N'2020-03-13T11:11:00.0000000' AS DateTime2), CAST(N'2020-03-13T14:22:00.0000000' AS DateTime2), NULL, 0, N'Xing', 1, 5, N'6bb2e745-3840-4d4b-8399-918ca7251144')
INSERT [dbo].[ReservationRequests] ([Id], [NumberOfGuest], [StartDateTime], [EndDateTime], [Requirement], [ReservationSource], [GuestName], [SittingType], [SittingId], [PersonId]) VALUES (29, 2, CAST(N'2020-03-13T18:00:00.0000000' AS DateTime2), CAST(N'2020-03-13T20:00:00.0000000' AS DateTime2), N'Near Window', 0, N'Kim', 2, 6, N'6bb2e745-3840-4d4b-8399-918ca7251144')
INSERT [dbo].[ReservationRequests] ([Id], [NumberOfGuest], [StartDateTime], [EndDateTime], [Requirement], [ReservationSource], [GuestName], [SittingType], [SittingId], [PersonId]) VALUES (30, 3, CAST(N'2020-03-06T18:00:00.0000000' AS DateTime2), CAST(N'2020-03-06T20:00:00.0000000' AS DateTime2), NULL, 2, N'Paul', 2, 6, N'6bb2e745-3840-4d4b-8399-918ca7251144')
SET IDENTITY_INSERT [dbo].[ReservationRequests] OFF
SET IDENTITY_INSERT [dbo].[Restaurants] ON 

INSERT [dbo].[Restaurants] ([Id], [Phone], [Email]) VALUES (1, N'123456', N'hello@beanscene.com.au')
SET IDENTITY_INSERT [dbo].[Restaurants] OFF
SET IDENTITY_INSERT [dbo].[Sittings] ON 

INSERT [dbo].[Sittings] ([Id], [StartTime], [EndTime], [Capacity], [SittingType]) VALUES (2, CAST(N'1900-01-01T08:00:00.0000000' AS DateTime2), CAST(N'1900-01-01T10:00:00.0000000' AS DateTime2), 100, 0)
INSERT [dbo].[Sittings] ([Id], [StartTime], [EndTime], [Capacity], [SittingType]) VALUES (5, CAST(N'1900-01-01T12:00:00.0000000' AS DateTime2), CAST(N'1900-01-01T14:00:00.0000000' AS DateTime2), 100, 1)
INSERT [dbo].[Sittings] ([Id], [StartTime], [EndTime], [Capacity], [SittingType]) VALUES (6, CAST(N'1900-01-01T17:00:00.0000000' AS DateTime2), CAST(N'1900-01-01T20:00:00.0000000' AS DateTime2), 100, 2)
SET IDENTITY_INSERT [dbo].[Sittings] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 12/03/2020 10:02:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 12/03/2020 10:02:26 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 12/03/2020 10:02:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 12/03/2020 10:02:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 12/03/2020 10:02:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 12/03/2020 10:02:26 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 12/03/2020 10:02:26 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_People_RestaurantId]    Script Date: 12/03/2020 10:02:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_People_RestaurantId] ON [dbo].[People]
(
	[RestaurantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ReservationRequests_PersonId]    Script Date: 12/03/2020 10:02:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_ReservationRequests_PersonId] ON [dbo].[ReservationRequests]
(
	[PersonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ReservationRequests_SittingId]    Script Date: 12/03/2020 10:02:26 AM ******/
CREATE NONCLUSTERED INDEX [IX_ReservationRequests_SittingId] ON [dbo].[ReservationRequests]
(
	[SittingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ReservationRequests] ADD  DEFAULT (N'') FOR [GuestName]
GO
ALTER TABLE [dbo].[ReservationRequests] ADD  DEFAULT ((0)) FOR [SittingType]
GO
ALTER TABLE [dbo].[Sittings] ADD  DEFAULT ((0)) FOR [SittingType]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_People_Restaurants_RestaurantId] FOREIGN KEY([RestaurantId])
REFERENCES [dbo].[Restaurants] ([Id])
GO
ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_People_Restaurants_RestaurantId]
GO
ALTER TABLE [dbo].[ReservationRequests]  WITH CHECK ADD  CONSTRAINT [FK_ReservationRequests_People_PersonId] FOREIGN KEY([PersonId])
REFERENCES [dbo].[People] ([Id])
GO
ALTER TABLE [dbo].[ReservationRequests] CHECK CONSTRAINT [FK_ReservationRequests_People_PersonId]
GO
ALTER TABLE [dbo].[ReservationRequests]  WITH CHECK ADD  CONSTRAINT [FK_ReservationRequests_Sittings_SittingId] FOREIGN KEY([SittingId])
REFERENCES [dbo].[Sittings] ([Id])
GO
ALTER TABLE [dbo].[ReservationRequests] CHECK CONSTRAINT [FK_ReservationRequests_Sittings_SittingId]
GO
USE [master]
GO
ALTER DATABASE [T3RMSWS] SET  READ_WRITE 
GO
