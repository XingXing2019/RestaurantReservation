DELETE FROM [Restaurants];
SET IDENTITY_INSERT [Restaurants] ON;
INSERT [dbo].[Restaurants] ([Id], [Phone], [Email]) VALUES (5, N'02 9752 5428', N'bookings@phoenix.com.au')
SET IDENTITY_INSERT [Restaurants] OFF;

DELETE FROM [Sittings];
SET IDENTITY_INSERT sittings ON;
INSERT [dbo].[Sittings] ([Id], [StartTime], [EndTime], [Capacity], [SittingType]) VALUES (13, CAST(N'2020-01-01T07:00:00.0000000' AS DateTime2), CAST(N'2020-01-01T11:00:00.0000000' AS DateTime2), 15, 0)
INSERT [dbo].[Sittings] ([Id], [StartTime], [EndTime], [Capacity], [SittingType]) VALUES (14, CAST(N'2020-01-01T11:00:00.0000000' AS DateTime2), CAST(N'2020-01-01T16:00:00.0000000' AS DateTime2), 15, 1)
INSERT [dbo].[Sittings] ([Id], [StartTime], [EndTime], [Capacity], [SittingType]) VALUES (15, CAST(N'2020-01-01T16:00:00.0000000' AS DateTime2), CAST(N'2020-01-01T23:00:00.0000000' AS DateTime2), 15, 2)
SET IDENTITY_INSERT sittings OFF;