CREATE PROCEDURE [TimeManagement].[SP_RemoveCustomerRef]
	@RemoveId int 

AS
	DELETE FROM [TimeManagement].[CustomerRef]
	where [TimeManagement].[CustomerRef].[PK_Id] = @RemoveId
GO
