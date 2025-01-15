using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.Azure.Common.DataContracts;
using Microsoft.Cloud.Platform.Communication;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200009F RID: 159
	[ServiceContract]
	[ECFContract]
	public interface IFabricIntegratorProvisioningService
	{
		// Token: 0x06000587 RID: 1415
		[OperationContract]
		Task<BindDatabaseResult> BindDatabaseAsync([ECFKey] DatabaseMoniker databaseMoniker, [ECFKey] DatabaseType databaseType, [ECFKey] int initialLoadInMB);

		// Token: 0x06000588 RID: 1416
		[OperationContract]
		Task<BindDatabaseResult> BindDatabaseWithDBSourceAsync([ECFKey] DatabaseMoniker databaseMoniker, [ECFKey] DatabaseType databaseType, [ECFKey] int initialLoadInMB, DatabaseSource databaseSource, string restaExternalStoreConnectionInfo = null, PushDataVersion pushDataVersion = PushDataVersion.None);

		// Token: 0x06000589 RID: 1417
		[OperationContract]
		Task<BindDatabaseResult> RestoreDatabaseAsync([ECFKey] DatabaseMoniker databaseMoniker, [ECFKey] DatabaseType databaseType, [ECFKey] int initialLoadInMB, string containerId);

		// Token: 0x0600058A RID: 1418
		[OperationContract]
		Task<BindDatabaseResult> CloneDatabaseAsync([ECFKey] DatabaseMoniker sourceDatabaseMoniker, [ECFKey] DatabaseMoniker targetDatabaseMoniker, DatabaseType databaseType, int initialLoadInMB);

		// Token: 0x0600058B RID: 1419
		[OperationContract]
		Task<BindDatabaseResult> CloneDatabaseWithDBSourceAsync([ECFKey] DatabaseMoniker sourceDatabaseMoniker, [ECFKey] DatabaseMoniker targetDatabaseMoniker, DatabaseType databaseType, int initialLoadInMB, DatabaseSource databaseSource);

		// Token: 0x0600058C RID: 1420
		[OperationContract]
		Task<BindDatabaseResult> SlowCloneDatabaseWithDBSourceAsync([ECFKey] DatabaseEntity sourceDatabaseEntity, [ECFKey] DatabaseMoniker targetDatabaseMoniker, DatabaseType databaseType, int initialLoadInMB, DatabaseSource databaseSource);

		// Token: 0x0600058D RID: 1421
		[OperationContract]
		Task ImageSaveDatabaseAsync([ECFKey] DatabaseEntity databaseEntity);

		// Token: 0x0600058E RID: 1422
		[OperationContract]
		Task<string> EvictDatabaseAsync([ECFKey] DatabaseEntity datbaseEntity, DatabaseEvictionReason evictionReason);

		// Token: 0x0600058F RID: 1423
		[OperationContract]
		Task<ServiceEntity> ResurrectDatabaseAsync([ECFKey] DatabaseEntity databaseEntity, [ECFKey] DatabaseResurrectionReason resurrectionReason);

		// Token: 0x06000590 RID: 1424
		[OperationContract]
		Task<string> UnbindDatabaseAsync([ECFKey] DatabaseMoniker databaseMoniker, DatabaseDeletionOptions databaseDeletionOptions);

		// Token: 0x06000591 RID: 1425
		[OperationContract]
		Task UpdateEngineTraceAsync([ECFKey] string engineTraceXEventSession, [ECFKey] bool enableEvents);

		// Token: 0x06000592 RID: 1426
		[OperationContract]
		Task ProcessDatabaseAsync([ECFKey] PowerBIProcessDatabaseInfo processParameters);

		// Token: 0x06000593 RID: 1427
		[OperationContract]
		Task NotifyDatabaseVolatileWriteAsync([ECFKey] DatabaseMoniker databaseMoniker, [ECFKey] DatabaseVolatileWriteSource source);

		// Token: 0x06000594 RID: 1428
		[OperationContract]
		Task DeleteSingleUnboundServiceAsync([ECFKey] DeleteSingleUnboundServiceRequest requestParameters);

		// Token: 0x06000595 RID: 1429
		[OperationContract]
		Task MigrateToUserDataV2Async([ECFKey] MigrateToUserDataV2Request requestParameters);

		// Token: 0x06000596 RID: 1430
		[OperationContract]
		Task UpdateDatabaseEntityV2StorageAccountAsync([ECFKey] DatabaseEntity databaseEntity, [ECFKey] string v2StorageAccountName);

		// Token: 0x06000597 RID: 1431
		[OperationContract(AsyncPattern = true, Name = "BeginBindDatabase")]
		IAsyncResult BeginBindDatabase([ECFKey] DatabaseMoniker databaseMoniker, [ECFKey] DatabaseType databaseType, [ECFKey] int initialLoadInMB, AsyncCallback callback, object context);

		// Token: 0x06000598 RID: 1432
		BindDatabaseResult EndBindDatabase(IAsyncResult result);

		// Token: 0x06000599 RID: 1433
		[OperationContract(AsyncPattern = true, Name = "BeginBindDatabaseWithDBSource")]
		IAsyncResult BeginBindDatabaseWithDBSource([ECFKey] DatabaseMoniker databaseMoniker, [ECFKey] DatabaseType databaseType, [ECFKey] int initialLoadInMB, DatabaseSource databaseSource, AsyncCallback callback, object context);

		// Token: 0x0600059A RID: 1434
		BindDatabaseResult EndBindDatabaseWithDBSource(IAsyncResult result);

		// Token: 0x0600059B RID: 1435
		[OperationContract(AsyncPattern = true, Name = "BeginRestoreDatabase")]
		IAsyncResult BeginRestoreDatabase([ECFKey] DatabaseMoniker databaseMoniker, [ECFKey] DatabaseType databaseType, [ECFKey] int initialLoadInMB, string containerId, AsyncCallback callback, object context);

		// Token: 0x0600059C RID: 1436
		BindDatabaseResult EndRestoreDatabase(IAsyncResult result);

		// Token: 0x0600059D RID: 1437
		[OperationContract(AsyncPattern = true, Name = "BeginCloneDatabase")]
		IAsyncResult BeginCloneDatabase([ECFKey] DatabaseMoniker sourceDatabaseMoniker, [ECFKey] DatabaseMoniker targetDatabaseMoniker, DatabaseType databaseType, int initialLoadInMB, AsyncCallback callback, object context);

		// Token: 0x0600059E RID: 1438
		BindDatabaseResult EndCloneDatabase(IAsyncResult result);

		// Token: 0x0600059F RID: 1439
		[OperationContract(AsyncPattern = true, Name = "BeginCloneDatabaseWithDBSource")]
		IAsyncResult BeginCloneDatabaseWithDBSource([ECFKey] DatabaseMoniker sourceDatabaseMoniker, [ECFKey] DatabaseMoniker targetDatabaseMoniker, DatabaseType databaseType, int initialLoadInMB, DatabaseSource databaseSource, AsyncCallback callback, object context);

		// Token: 0x060005A0 RID: 1440
		BindDatabaseResult EndCloneDatabaseWithDBSource(IAsyncResult result);

		// Token: 0x060005A1 RID: 1441
		[OperationContract(AsyncPattern = true, Name = "BeginEvictDatabase")]
		IAsyncResult BeginEvictDatabase([ECFKey] DatabaseEntity datbaseEntity, DatabaseEvictionReason evictionReason, AsyncCallback callback, object context);

		// Token: 0x060005A2 RID: 1442
		string EndEvictDatabase(IAsyncResult result);

		// Token: 0x060005A3 RID: 1443
		[OperationContract(AsyncPattern = true, Name = "BeginResurrectDatabase")]
		IAsyncResult BeginResurrectDatabase([ECFKey] DatabaseEntity databaseEntity, [ECFKey] DatabaseResurrectionReason resurrectionReason, AsyncCallback callback, object context);

		// Token: 0x060005A4 RID: 1444
		ServiceEntity EndResurrectDatabase(IAsyncResult result);

		// Token: 0x060005A5 RID: 1445
		[OperationContract(AsyncPattern = true, Name = "BeginUnbindDatabase")]
		IAsyncResult BeginUnbindDatabase([ECFKey] DatabaseMoniker databaseMoniker, DatabaseDeletionOptions databaseDeletionOptions, AsyncCallback callback, object context);

		// Token: 0x060005A6 RID: 1446
		string EndUnbindDatabase(IAsyncResult result);
	}
}
