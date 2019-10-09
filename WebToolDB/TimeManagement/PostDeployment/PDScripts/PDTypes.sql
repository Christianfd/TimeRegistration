

/*
Creates the neccessary entries for TaskTypes and TimeTypes
*/

/*
Task Type:
*/

IF NOT EXISTS(SELECT TOP(1) [Name] FROM TimeManagement.TaskType)
BEGIN
DBCC CHECKIDENT ('[TimeManagement].[TaskType]', RESEED, 1) 
INSERT INTO[TimeManagement].TaskType VALUES ( 'Development');
INSERT INTO[TimeManagement].TaskType VALUES ( 'Purchase & Logistics');
INSERT INTO[TimeManagement].TaskType VALUES ( 'Electrical Design');
INSERT INTO[TimeManagement].TaskType VALUES ( 'Network & Fiber Technology');
INSERT INTO[TimeManagement].TaskType VALUES ( 'LiDAR');
INSERT INTO[TimeManagement].TaskType VALUES ( 'Department');
INSERT INTO[TimeManagement].TaskType VALUES ( 'Technical Support');
END

/*
Time Type:
*/

IF NOT EXISTS(SELECT TOP(1) [Name] FROM TimeManagement.TimeType)
BEGIN
DBCC CHECKIDENT ('[TimeManagement].[TimeType]', RESEED, 1) 
INSERT INTO[TimeManagement].TimeType VALUES ( 'Not Assigned');
INSERT INTO[TimeManagement].TimeType VALUES ( 'Intern');
INSERT INTO[TimeManagement].TimeType VALUES ( 'Extern');
INSERT INTO[TimeManagement].TimeType VALUES ( 'Illness');
INSERT INTO[TimeManagement].TimeType VALUES ( 'Training');
INSERT INTO[TimeManagement].TimeType VALUES ( 'Vacation');
INSERT INTO[TimeManagement].TimeType VALUES ( 'Other');


END