DELETE FROM [dbo].[ReservationRequests]
SET IDENTITY_INSERT [ReservationRequests] ON;
INSERT [dbo].[ReservationRequests] ([Id], [NumberOfGuest], [StartDateTime], [Requirement], [ReservationSource], [GuestName], [SittingType], [PersonId], [Duration], [Email], [TimeStamp], [ReferenceNo], [TableId], [TableType], [Mobile]) VALUES 
(40, 5, N'2020-05-31 08:00:00.0000000', N'Close to Window', 2, 'Timothy Xing', 0, N'376068bc-7cf1-44f1-8e4f-fd503b3c3a87', 2, N'test1@e.com',  N'2020-05-31 08:00:00.0000000', N'62938ec0-8666-4697-aabc-55a4c70f541f', 0, 0, N'0425124578'),
(41, 10, N'2020-06-01 10:00:00.0000000', N'N/A', 2, 'Timothy Xing', 0, N'376068bc-7cf1-44f1-8e4f-fd503b3c3a87', 2, N'test1@e.com',  N'2020-05-29 08:00:00.0000000', N'09eda8b2-6cac-46c8-b811-2b6d124a849b', 0, 0, N'0425124578'),
(42, 6, N'2020-06-01 08:00:00.0000000', N'Away from Window', 2, 'Angela Dai', 0, N'688568bc-7cf1-23e1-6f6h-fd4g3b3b6b57', 2, N'test2@e.com',  N'2020-06-01 08:00:00.0000000', N'58938ec0-8666-4607-aabc-55a4c70f541f', 0, 0, N'0425135485'),
(43, 8, N'2020-06-02 08:00:00.0000000', N'Big eater', 2, 'Angela Dai', 0, N'688568bc-7cf1-23e1-6f6h-fd4g3b3b6b57', 3, N'test2@e.com',  N'2020-06-02 08:00:00.0000000', N'c472e156-ff32-46dd-84d3-e8cffdcf112e', 0, 0, N'0425135485'),
(44, 13, N'2020-06-02 07:30:00.0000000', N'Sit outside area', 2, 'Peter Austin', 0, Null, 3, N'test3@e.com',  N'2020-06-02 08:00:00.0000000', N'e8afbdf8-829a-4ef7-a13b-02e86c7625a0', 0, 0, N'0413584512'),


(45, 10, N'2020-06-03 07:30:00.0000000', N'Sit outside area', 2, 'Peter Austin', 0, Null, 4, N'test3@e.com',  N'2020-06-02 08:00:00.0000000', N'e8afbdf8-829a-4ef7-a13b-02e86c7625a0', 0, 0, N'0413584512'),
(46, 5, N'2020-06-03 08:30:00.0000000', N'Sit outside area', 2, 'Peter Austin', 0, Null, 2, N'test3@e.com',  N'2020-05-13 04:00:00.0000000', N'8ec0e913-ba80-42c2-b0a8-4f27bc7ada68', 0, 0, N'0413584512'),
(47, 15, N'2020-06-03 09:30:00.0000000', N'Sit outside area', 2, 'Peter Austin', 0, Null, 1, N'test3@e.com',  N'2020-05-22 07:00:00.0000000', N'5e07719d-2ef3-499a-afb0-7402d92d59af', 0, 0, N'0413584512'),
(48, 12, N'2020-06-03 10:00:00.0000000', N'Sit outside area', 2, 'Peter Austin', 0, Null, 2, N'test3@e.com',  N'2020-05-12 10:00:00.0000000', N'45b339a1-94ea-4c1a-85fe-e656aefd0486', 0, 0, N'0413584512');
SET IDENTITY_INSERT [ReservationRequests] OFF;