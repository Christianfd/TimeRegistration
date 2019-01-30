CREATE VIEW [TimeManagement].[VI_UserAssignment]
	AS	SELECT [TimeManagement].[UserAssignment].[PK_Id], 
				[FK_UserId], 
				[FK_ProjectId],
				[NK_Name] as [UserName],
				[Users].[NK_ZId],
				[Projects].[Name] as [ProjectName],
				[FK_OrderNumber],
				[Number]

		FROM [TimeManagement].[UserAssignment]
		JOIN
			 [TimeManagement].[Users] on [UserAssignment].[FK_UserId] = [Users].[PK_Id]
		JOIN
			[TimeManagement].[Projects] on [UserAssignment].[FK_ProjectId] = [Projects].[PK_Id]
		JOIN
			[TimeManagement].[OrderNumber] on [FK_OrderNumber] = [OrderNumber].[PK_Id]


		
  --Needs different joins with the correct tables