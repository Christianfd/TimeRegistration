CREATE VIEW [TimeManagement].[VI_ProjectAndOrderTools]
	AS SELECT DISTINCT 
		[Organization],
		[TimeType].[Name] as [TimeTypeName],
		[TaskType].[Name] as [TaskTypeName],
		[CustomerRef].[Name] as [CustomerRefName],
		[Requester].[Name] as [RequesterName],
		[TurbineName],
		[ProductName]



		
	FROM 
		[RequestOrg],
		[TimeType],
		[TaskType],
		[CustomerRef],
		[Requester],
		[PlatformOrProduct],
		[Turbine]

