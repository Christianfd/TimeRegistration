CREATE PROCEDURE [TimeManagement].[SP_AddRequester]
	
	@Name nvarchar(100)
AS

INSERT INTO [TimeManagement].[Requester]
([Name])

VALUES (@Name)
