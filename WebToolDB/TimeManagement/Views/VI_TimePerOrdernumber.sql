CREATE VIEW [TimeManagement].[VI_TimePerOrdernumber]
	AS SELECT 
	SUM([Time]) as [timeSum],
	[Number]
	
	FROM [TimeManagement].[TimeRegistration]
	JOIN  [TimeManagement].[Users] on [TimeRegistration].[FK_UserId] = [Users].[PK_Id]
	JOIN [TimeManagement].[Projects] on [TimeRegistration].[FK_ProjectId] = [Projects].[PK_Id]
	Join TimeManagement.OrderNumber on TimeRegistration.FK_OrderId = OrderNumber.PK_Id


	GROUP BY  [FK_ProjectId], [Number]

