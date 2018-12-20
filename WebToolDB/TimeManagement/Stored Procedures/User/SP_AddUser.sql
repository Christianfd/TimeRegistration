CREATE PROCEDURE [TimeManagement].[SP_AddUser]
	
	@UserName nvarchar(100),
	@UserAuthentity nvarchar(50)

AS

INSERT INTO [TimeManagement].[Users]
([NK_Name],[NK_ZId])

VALUES (@UserName, @UserAuthentity)
