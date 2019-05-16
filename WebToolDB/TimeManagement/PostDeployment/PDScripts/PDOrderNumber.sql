/*
This is the post-deployment with value usable by the union page
*/

IF NOT EXISTS(SELECT TOP(1) [PK_Id] FROM TimeManagement.Users)
BEGIN
DBCC CHECKIDENT ('[TimeManagement].[Users]', RESEED, 1) 
INSERT INTO[TimeManagement].[Users] VALUES ( 'To Be Decided', 'N/A', '3');
END

IF NOT EXISTS(SELECT TOP(1) [PK_Id] FROM TimeManagement.OrderNumber)
BEGIN
DBCC CHECKIDENT ('[TimeManagement].[OrderNumber]', RESEED, 1) 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '---', 'Missing OrderNumber');
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '1100', 'Internal Training'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2497', 'Blood Donor'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2370', 'Dentist'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2360', 'Funeral'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2350', 'Death in close family'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2300', 'Time off without pay'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2250', 'Parental leave'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2240', 'Paternity leave'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2230', 'Maternity leave'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2220', 'Pregnancy leave'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2170', 'Time of in lieu'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2160', 'Vacation feriefri'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2150', 'Vacation'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2100', 'Child illness 1st day'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2040', 'Doctor examination'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2010', 'Party illness special'); 
INSERT INTO[TimeManagement].[OrderNumber] ([Number], [Title]) VALUES ( '2000', 'Illness');
END
