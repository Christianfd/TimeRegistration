CREATE PROCEDURE [TimeManagement].[SP_RemoveOrderNumber]
	@RemoveId int 

AS
	DELETE FROM [TimeManagement].[OrderNumber]
	where [TimeManagement].[OrderNumber].[PK_Id] = @RemoveId
GO
