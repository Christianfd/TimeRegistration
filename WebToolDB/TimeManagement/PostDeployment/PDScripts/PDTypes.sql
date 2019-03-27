

/*
Creates the neccessary entries for TaskTypes and TimeTypes
*/

/*
Task Type:
*/

IF NOT EXISTS(SELECT TOP(1) [Name] FROM TimeManagement.TaskType)
BEGIN
DBCC CHECKIDENT ('[TimeManagement].[Country]', RESEED, 0) 
INSERT INTO[TimeManagement].TaskType VALUES ( 'Not Assigned');
INSERT INTO[TimeManagement].TaskType VALUES ( 'Development');
INSERT INTO[TimeManagement].TaskType VALUES ( 'Eldesign');
INSERT INTO[TimeManagement].TaskType VALUES ( 'Intern');
INSERT INTO[TimeManagement].TaskType VALUES ( 'LiDAR');
INSERT INTO[TimeManagement].TaskType VALUES ( 'Project');
INSERT INTO[TimeManagement].TaskType VALUES ( 'Support');
END

/*
Time Type:
*/

IF NOT EXISTS(SELECT TOP(1) [Name] FROM TimeManagement.TimeType)
BEGIN
DBCC CHECKIDENT ('[TimeManagement].[Country]', RESEED, 0) 
INSERT INTO[TimeManagement].TimeType VALUES ( 'Not Assigned');
INSERT INTO[TimeManagement].TimeType VALUES ( 'Intern');
INSERT INTO[TimeManagement].TimeType VALUES ( 'Extern');
INSERT INTO[TimeManagement].TimeType VALUES ( 'Illness');
INSERT INTO[TimeManagement].TimeType VALUES ( 'Training');
INSERT INTO[TimeManagement].TimeType VALUES ( 'Vacation');
INSERT INTO[TimeManagement].TimeType VALUES ( 'Other');


END