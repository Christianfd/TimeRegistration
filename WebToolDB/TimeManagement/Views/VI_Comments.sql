CREATE VIEW [TimeManagement].[VI_Comments]
	AS SELECT [Comments].[PK_Id]
	  ,[WeekNr]
	  ,[Year]
	  ,[Text]
	  ,[FK_ProjectId]
	  ,[FK_User]
	  ,[Projects].[Name] as [ProjectName]
	  ,[Users].[NK_Name] as [UserName]
  FROM [Comments]

  JOIN [Projects] ON [Comments].[FK_ProjectId] = [Projects].[PK_Id]
  JOIN [Users] ON [Comments].[FK_User] = [Users].[PK_Id]

  --Needs different joins with the correct tables