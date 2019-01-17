CREATE PROCEDURE [TimeManagement].[SP_AddCustomerRef]
	
	@Name nvarchar(100)
AS

INSERT INTO [TimeManagement].[CustomerRef]
([Name])

VALUES (@Name)
