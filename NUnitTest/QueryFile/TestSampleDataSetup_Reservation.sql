SET IDENTITY_INSERT [ReservationRequests] ON;
INSERT [dbo].[ReservationRequests] ([Id], [NumberOfGuest], [StartDateTime], [Requirement], [ReservationSource], [GuestName], [SittingType], [SittingId], [Duration], [Email], [TimeStamp], [ReferenceNo], [TableId], [TableType], [Mobile]) VALUES 
(40, 5, N'2020-05-31 08:00:00.0000000', N'Close to Window', 2, 'Tim Xing', 0, 13, 2, N'test1@e.com',  N'2020-05-31 08:00:00.0000000', N'62938ec0-8666-4697-aabc-55a4c70f541f', 0, 0, N'0425124578'),
(41, 6, N'2020-06-01 08:00:00.0000000', N'Away from Window', 2, 'Angela Dai', 0, 13, 2, N'test2@e.com',  N'2020-06-01 08:00:00.0000000', N'58938ec0-8666-4607-aabc-55a4c70f541f', 0, 0, N'0425135485'),
(42, 8, N'2020-06-02 08:00:00.0000000', N'Big eater', 2, 'Angela Dai', 0, 13, 3, N'test2@e.com',  N'2020-06-02 08:00:00.0000000', N'c472e156-ff32-46dd-84d3-e8cffdcf112e', 0, 0, N'0425135485');
SET IDENTITY_INSERT [ReservationRequests] OFF;