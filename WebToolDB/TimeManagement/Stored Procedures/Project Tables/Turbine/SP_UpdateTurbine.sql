CREATE PROCEDURE [TimeManagement].[SP_UpdateTurbine]
	@UpdateId  int,
	@TurbineName nvarchar(50)
AS
	UPDATE [TimeManagement].[Turbine] 
	SET	[TimeManagement].[Turbine].[TurbineName] = @TurbineName
	WHERE [TimeManagement].[Turbine].[PK_Id] = @UpdateId

	GO