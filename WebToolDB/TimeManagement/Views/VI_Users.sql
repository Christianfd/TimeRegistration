CREATE VIEW [TimeManagement].[VI_Users]
WITH SCHEMABINDING
	AS SELECT  [PK_Id], [NK_Name],[NK_ZId] 
		FROM [TimeManagement].[Users]
