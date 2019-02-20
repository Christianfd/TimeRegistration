CREATE PROCEDURE [TimeManagement].[SP_AddNewUser]
	@Name nvarchar(100),
	@Auth nvarchar(50)

AS
	INSERT INTO [TimeManagement].[Users]
([NK_Name], [NK_ZId])

VALUES (@Name, @Auth)

GO
