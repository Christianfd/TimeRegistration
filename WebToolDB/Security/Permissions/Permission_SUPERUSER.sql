﻿GRANT CONNECT TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];

--Union Page - "Update", "Delete"
--Project Management - Insert, Update
--Users - Insert, Update
--Order Number - Insert, Update

--Time Reg - Remove(Mabye?)




--Union Page - "Delete"
GO
GRANT EXECUTE ON [TimeManagement].[SP_RemoveCustomerRef] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO
GRANT EXECUTE ON [TimeManagement].[SP_RemoveRequester] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO 
GRANT EXECUTE ON [TimeManagement].[SP_RemoveRequestOrg] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO
GRANT EXECUTE ON [TimeManagement].[SP_RemovePlatformOrProduct] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO
GRANT EXECUTE ON [TimeManagement].[SP_RemoveTaskType] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO
GRANT EXECUTE ON [TimeManagement].[SP_RemoveTimeType] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO
GRANT EXECUTE ON [TimeManagement].[SP_RemoveTurbine] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO


--Union Page - "Update"
GO
GRANT EXECUTE ON [TimeManagement].[SP_UpdateCustomerRef] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO
GRANT EXECUTE ON [TimeManagement].[SP_UpdateRequester] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO 
GRANT EXECUTE ON [TimeManagement].[SP_UpdateRequestOrg] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO
GRANT EXECUTE ON [TimeManagement].[SP_UpdatePlatformOrProduct] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO
GRANT EXECUTE ON [TimeManagement].[SP_UpdateTaskType] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO
GRANT EXECUTE ON [TimeManagement].[SP_UpdateTimeType] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO
GRANT EXECUTE ON [TimeManagement].[SP_UpdateTurbine] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO

--Project Management "Insert", "Update"
GRANT EXECUTE ON [TimeManagement].[SP_AddProject] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO 
GRANT EXECUTE ON [TimeManagement].[SP_UpdateProject] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO

--Users "Insert" "Update"
GRANT EXECUTE ON [TimeManagement].[SP_AddUser] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO
GRANT EXECUTE ON [TimeManagement].[SP_UpdateUser] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];

--Order Number "Insert" "Update"
GO
GRANT EXECUTE ON [TimeManagement].[SP_AddOrderNumber] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO
GRANT EXECUTE ON [TimeManagement].[SP_UpdateOrderNumber] TO [AD009\RA201_TMDBTIMEMANAGEMENT_SUPERUSER_L];
GO



