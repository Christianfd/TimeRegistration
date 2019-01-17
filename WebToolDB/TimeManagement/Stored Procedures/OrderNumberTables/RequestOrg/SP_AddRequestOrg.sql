CREATE PROCEDURE [TimeManagement].[SP_AddRequestOrg]
	
	@Organization nvarchar(100)
AS

INSERT INTO [TimeManagement].[RequestOrg]
([Organization])

VALUES (@Organization)
