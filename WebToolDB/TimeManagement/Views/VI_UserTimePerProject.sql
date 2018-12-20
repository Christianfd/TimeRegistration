CREATE VIEW [TimeManagement].[VI_UserTimePerProject]
	AS SELECT 
		[Users].[NK_Name] as [UserName],
		SUM([Time]) as [timeSum],
		[FK_ProjectId]
	
	
	 FROM [TimeRegistration]
	JOIN  [Users] on [TimeRegistration].[FK_UserId] = [Users].[PK_Id]
	GROUP BY [NK_Name], [FK_ProjectId]



