/*
This is the post-deployment with value usable by the union page
*/

IF NOT EXISTS(SELECT TOP(1) [PK_Id] FROM TimeManagement.Users)
BEGIN
DBCC CHECKIDENT ('[TimeManagement].[Users]', RESEED, 0) 
INSERT INTO[TimeManagement].[Users] VALUES ( 'To Be Decided', 'N/A', '3');
END

IF NOT EXISTS(SELECT TOP(1) [PK_Id] FROM TimeManagement.OrderNumber)
BEGIN
DBCC CHECKIDENT ('[TimeManagement].[OrderNumber]', RESEED, 0) 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '1100', 1,1,1,'Internal Training'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2497', 1,1,1,'Blood Donor'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2370', 1,1,1,'Dentist'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2360', 1,1,1,'Funeral'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2350', 1,1,1,'Death in close family'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2300', 1,1,1,'Time off without pay'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2250', 1,1,1,'Parental leave'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2240', 1,1,1,'Paternity leave'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2230', 1,1,1,'Maternity leave'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2220', 1,1,1,'Pregnancy leave'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2170', 1,1,1,'Time of in lieu'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2160', 1,1,1,'Vacation feriefri'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2150', 1,1,1,'Vacation'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2100', 1,1,1,'Child illness 1st day'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2040', 1,1,1,'Doctor examination'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2010', 1,1,1,'Party illness special'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number],[FK_RequestOrg],[FK_Requester],[FK_CustomerRef], [Title]) VALUES ( '2000', 1,1,1,'Illness');
END
