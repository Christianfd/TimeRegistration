CREATE VIEW [TimeManagement].[VI_ProjectAndOrderToolsUnion]
AS SELECT 'Requester' as Type, Requester.Name, PK_Id
FROM TimeManagement.Requester
UNION ALL
SELECT 'RequestOrg' as Type, Organization as Name, PK_Id
FROM TimeManagement.RequestOrg
UNION ALL
SELECT 'CustomerRef' as Type, CustomerRef.Name, PK_Id
FROM TimeManagement.CustomerRef
UNION ALL
SELECT 'PlatformOrProduct' as Type, ProductName as Name, PK_Id
FROM TimeManagement.PlatformOrProduct
UNION ALL
SELECT 'Turbine' as Type, TurbineName as Name, PK_Id
FROM TimeManagement.Turbine
UNION ALL
SELECT 'TimeType' as Type, Name, PK_Id
FROM TimeManagement.TimeType
UNION ALL
SELECT 'TaskType' as Type, Name, PK_Id
FROM TimeManagement.TaskType 

