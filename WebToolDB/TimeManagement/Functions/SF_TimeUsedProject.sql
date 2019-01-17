CREATE FUNCTION [TimeManagement].[SF_TimeUsedProject]
(
	@projectId int
)



RETURNS INT
AS
BEGIN
	DECLARE @timeSpent int
	SELECT @timeSpent = SUM([Time]) FROM [TimeManagement].[TimeRegistration] 
	WHERE [FK_ProjectId] = @projectId
	RETURN @timeSpent
END