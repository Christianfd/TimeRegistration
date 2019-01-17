CREATE PROCEDURE [TimeManagement].[SP_UpdateRequestOrg]
	@UpdateId  int,
	@Organization nvarchar(100)
AS
	UPDATE [TimeManagement].[RequestOrg] 
	SET	[TimeManagement].[RequestOrg].[Organization] = @Organization
	WHERE [TimeManagement].[RequestOrg].[PK_Id] = @UpdateId

	GO