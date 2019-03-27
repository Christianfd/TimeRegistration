
/*
This is the post-deployment with value usable by the union page
*/

IF NOT EXISTS(SELECT TOP(1) [PK_Id] FROM TimeManagement.Requester)
BEGIN
DBCC CHECKIDENT ('[TimeManagement].[Requester]', RESEED, 0) 
INSERT INTO[TimeManagement].[Requester] VALUES ( 'Not Assigned');
END



IF NOT EXISTS(SELECT TOP(1) [PK_Id] FROM TimeManagement.RequestOrg)
BEGIN
DBCC CHECKIDENT ('[TimeManagement].[RequestOrg]', RESEED, 0) 
INSERT INTO[TimeManagement].[RequestOrg] VALUES ( 'Not Assigned');
END



IF NOT EXISTS(SELECT TOP(1) [PK_Id] FROM TimeManagement.CustomerRef)
BEGIN
DBCC CHECKIDENT ('[TimeManagement].[CustomerRef]', RESEED, 0) 
INSERT INTO[TimeManagement].CustomerRef VALUES ( 'Not Assigned');
END



IF NOT EXISTS(SELECT TOP(1) [PK_Id] FROM TimeManagement.PlatformOrProduct)
BEGIN
DBCC CHECKIDENT ('[TimeManagement].[PlatformOrProduct]', RESEED, 0) 
INSERT INTO[TimeManagement].PlatformOrProduct VALUES ( 'Not Assigned');
END



IF NOT EXISTS(SELECT TOP(1) [PK_Id] FROM TimeManagement.Turbine)
BEGIN
DBCC CHECKIDENT ('[TimeManagement].[Turbine]', RESEED, 0) 
INSERT INTO[TimeManagement].Turbine VALUES ( 'Not Assigned');
END


