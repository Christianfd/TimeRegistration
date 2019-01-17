CREATE PROCEDURE [TimeManagement].[SP_RemoveTurbine]
	@RemoveId int 

AS
	DELETE FROM [TimeManagement].[Turbine]
	where [TimeManagement].[Turbine].[PK_Id] = @RemoveId
GO
