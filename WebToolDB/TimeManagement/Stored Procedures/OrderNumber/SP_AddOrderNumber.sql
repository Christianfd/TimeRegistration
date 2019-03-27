CREATE PROCEDURE [TimeManagement].[SP_AddOrderNumber]
	
	@NumberName nvarchar(1000),
	@FK_RequestOrg int,
	@FK_Requester int,
	@FK_CustomerRef int,
	@title	nvarchar(100)
AS

INSERT INTO [TimeManagement].[OrderNumber]
([Number], [FK_RequestOrg], [FK_Requester], [FK_CustomerRef], [Title])

VALUES (@NumberName, @FK_RequestOrg, @FK_Requester, @FK_CustomerRef, @title)
