CREATE VIEW [TimeManagement].[VI_Users]
WITH SCHEMABINDING
	AS SELECT  [PK_Id], [NK_Name],[NK_ZId], [NK_Rights]
		FROM [TimeManagement].[Users]
