CREATE PROCEDURE [TimeManagement].[SP_AddOrderNumber]
	
	@NumberName nvarchar(1000),

	@title	nvarchar(100)
AS

INSERT INTO [TimeManagement].[OrderNumber]
([Number], [Title])

VALUES (@NumberName, @title)
