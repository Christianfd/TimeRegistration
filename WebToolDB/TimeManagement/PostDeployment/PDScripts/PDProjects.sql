/*
This is the post-deployment with value usable by the union page
*/

IF NOT EXISTS(SELECT TOP(1) [PK_Id] FROM TimeManagement.Projects)
BEGIN
DBCC CHECKIDENT ('[TimeManagement].[Projects]', RESEED, 1) 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 1,1,5,'Internal Training'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 2,1,7,'Blood Donor'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 3,1,7,'Dentist'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 4,1,7,'Funeral'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 5,1,7,'Death in close family'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 6,1,7,'Time off without pay'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 7,1,7,'Parental leave'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 8,1,7,'Paternity leave'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 9,1,7,'Maternity leave'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 10,1,7,'Pregnancy leave'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 11,1,7,'Time of in lieu'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 12,1,6,'Vacation feriefri'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 13,1,6,'Vacation'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 14,1,7,'Child illness 1st day'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 15,1,7,'Doctor examination'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 16,1,4,'Party illness special'); 
INSERT INTO[TimeManagement].[Projects] ([FK_OrderNumber], [TimeEstimation], [FK_TimeType], [Name]) VALUES ( 17,1,4,'Illness');
END