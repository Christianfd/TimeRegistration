CREATE PROCEDURE [TimeManagement].[SP_AddComment]
	
	@weekNumber int,
	@year int,
	@text nvarchar(1000),
	@projectKey int,
	@userKey int

AS

INSERT INTO [TimeManagement].[Comments]
([WeekNr], [Year], [Text], [FK_ProjectId], [FK_User])

VALUES (@weekNumber, @year, @text, @projectKey, @userKey)
GO