CREATE VIEW [TimeManagement].[VI_UserTimePerProject]
	AS SELECT 
		[TimeManagement].[Users].[PK_Id],
		[TimeManagement].[Users].[NK_Name] as [UserName],
		[TimeManagement].[Users].[NK_ZId],
		SUM([Time]) as [timeSum],
		[FK_ProjectId],
		[Date],
		[Number],
		[Name]
	
	
	FROM [TimeManagement].[TimeRegistration]
	JOIN  [TimeManagement].[Users] on [TimeRegistration].[FK_UserId] = [Users].[PK_Id]
	JOIN [TimeManagement].[Projects] on [TimeRegistration].[FK_ProjectId] = [Projects].[PK_Id]
	Join TimeManagement.OrderNumber on TimeRegistration.FK_OrderId = OrderNumber.PK_Id

	GROUP BY [NK_Name], [FK_ProjectId], [TimeManagement].[Users].[PK_Id], [NK_ZId],[Name], [Date], [Number]

