CREATE PROCEDURE [TimeManagement].[SP_AddPlatformOrProduct]
	
	@platformOrProject nvarchar(50)


AS
BEGIN 
	begin try -- Try Code
INSERT INTO [TimeManagement].[PlatformOrProduct]
([ProductName])

VALUES (@platformOrProject)
	end try

	begin catch -- Catch syntax
	end catch
END
GO