CREATE VIEW [TimeManagement].[VI_TimeRegistration]
	AS SELECT [TimeRegistration].[PK_Id]
	  ,[FK_UserId]
	  ,[FK_ProjectId]
	  ,[FK_OrderId]
	  ,[FK_TaskId]
	  ,[Time]
	  ,[Date]
	  ,[DateEntry]
	  ,[Comment]
	  ,[TaskType].[Name] as [TaskTypeName]
	  ,[Projects].[Name] as [ProjectName]
	  ,[Users].[NK_Name] as [UserName]
	  ,[OrderNumber].[Number] as [OrderName]
  FROM [TimeManagement].[TimeRegistration]
  JOIN 
	[TimeManagement].[Users] ON [TimeRegistration].[FK_UserId] = [Users].[PK_Id]
  JOIN 
	[TimeManagement].[Projects] ON [TimeRegistration].[FK_ProjectId] = [Projects].[PK_Id]
  JOIN 
	[TimeManagement].[TaskType] ON [TimeRegistration].[FK_TaskId] = [TaskType].[PK_Id]
  JOIN
	[TimeManagement].[OrderNumber] ON [TimeRegistration].[FK_OrderId] = [OrderNumber].[PK_Id]
		
