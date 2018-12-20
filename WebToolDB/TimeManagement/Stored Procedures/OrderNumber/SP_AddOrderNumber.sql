CREATE PROCEDURE [TimeManagement].[SP_AddOrderNumber]
	
	@NumberName nvarchar(1000)
AS

INSERT INTO [TimeManagement].[OrderNumber]
([Number])

VALUES (@NumberName)
