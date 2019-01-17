CREATE PROCEDURE [TimeManagement].[SP_AddTurbine]
	
	@turbineName nvarchar(50)
AS

INSERT INTO [TimeManagement].[Turbine]
([TurbineName])

VALUES (@turbineName)
