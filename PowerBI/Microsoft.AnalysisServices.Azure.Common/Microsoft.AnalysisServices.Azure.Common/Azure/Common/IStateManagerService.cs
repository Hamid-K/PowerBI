using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Communication;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000A3 RID: 163
	[ServiceContract]
	[ECFContract]
	public interface IStateManagerService
	{
		// Token: 0x060005BE RID: 1470
		[OperationContract]
		Task AddServiceDatabaseMappingAsync(ServiceEntity service, DatabaseEntity database);

		// Token: 0x060005BF RID: 1471
		[OperationContract]
		Task AddVirtualServerAsync(VirtualServerEntity vsEntity);

		// Token: 0x060005C0 RID: 1472
		[OperationContract]
		Task DeleteDatabaseServiceMappingAsync(string serviceKey, string databaseKey, DatabaseDeletionOptions databaseDeletionOptions);

		// Token: 0x060005C1 RID: 1473
		[OperationContract]
		Task DeleteVirtualServerAsync(string vsKey, VirtualServerDeletionOptions vsDeletionOptions);

		// Token: 0x060005C2 RID: 1474
		[OperationContract]
		Task UpdateDatabaseLastAccessedDateAsync(string databaseKey, DateTime lastAccessedDate);

		// Token: 0x060005C3 RID: 1475
		[OperationContract]
		Task UpdateDatabaseLastAccessedDatesAsync(IEnumerable<string> databaseKeys);

		// Token: 0x060005C4 RID: 1476
		[OperationContract]
		Task DeleteUnboundDatabaseAsync(string databaseKey, DatabaseDeletionOptions databaseDeletionOptions);

		// Token: 0x060005C5 RID: 1477
		[OperationContract]
		Task BindDatabaseAsync(string databaseKey, ServiceEntity service);

		// Token: 0x060005C6 RID: 1478
		[OperationContract]
		Task UnBindDatabaseAsync(string databaseKey);

		// Token: 0x060005C7 RID: 1479
		[OperationContract]
		Task UnbindDatabaseFromServiceAsync(string databaseKey, string serviceKey, bool enforceConsistencyVerification = false);

		// Token: 0x060005C8 RID: 1480
		[OperationContract]
		Task DeleteUnboundServiceAsync(string serviceKey);

		// Token: 0x060005C9 RID: 1481
		[OperationContract]
		Task UpdateLocalDatabaseIdAsync(string serviceKey, string localDatabaseId);

		// Token: 0x060005CA RID: 1482
		[OperationContract]
		Task PopulateStateFromOperationalStoreAsync();

		// Token: 0x060005CB RID: 1483
		[OperationContract]
		Task UpdateDatabaseSizeAsync(DatabaseMoniker databaseMoniker, int databaseSize);

		// Token: 0x060005CC RID: 1484
		[OperationContract]
		Task UpdateDatasourceVersionAsync(DatabaseMoniker databaseMoniker, ModelPowerBIDatasourceFormatVersion datasourceVersionInfo);

		// Token: 0x060005CD RID: 1485
		[OperationContract]
		Task UpdateDatabasePushDataVersionAndConnectionAsync(DatabaseMoniker databaseMoniker, PushDataVersion pushDataVersion, string externalStoreConnectionInfo);

		// Token: 0x060005CE RID: 1486
		[OperationContract]
		Task UpdateDatabasePBIDedicatedPropertiesAsync(DatabaseMoniker databaseMoniker, DatabaseType databaseType, string pbiDedicatedCapacity, string v2StorageAccountName);

		// Token: 0x060005CF RID: 1487
		[OperationContract]
		Task UpdateDatabaseStoragePropertiesAsync(DatabaseEntity databaseEntity);

		// Token: 0x060005D0 RID: 1488
		[OperationContract]
		Task UpdateDatabaseV2StorageAccountNameAsync(DatabaseMoniker databaseMoniker, string v2StorageAccountName);

		// Token: 0x060005D1 RID: 1489
		[OperationContract]
		Task AddDatabaseAsync(DatabaseEntity databaseEntity);

		// Token: 0x060005D2 RID: 1490
		[OperationContract]
		Task UpdateDatabasesCapacityAsync(IEnumerable<DatabaseEntity> databaseEntities, string capacityObjectId);

		// Token: 0x060005D3 RID: 1491
		[OperationContract]
		Task UpdateDatabasesEncryptionKeyAsync(IEnumerable<DatabaseEntity> databaseEntities, Guid? tenantKeyObjectId);

		// Token: 0x060005D4 RID: 1492
		[OperationContract]
		Task UpdateDatabaseStorageModeAsync(DatabaseEntity databaseEntity, DatabaseStorageMode storageMode, string storageAccountName, string containerId);

		// Token: 0x060005D5 RID: 1493
		[OperationContract]
		Task UpdateDatabaseAvailabilityAsync(DatabaseEntity databaseEntity, bool available);

		// Token: 0x060005D6 RID: 1494
		[OperationContract]
		Task MarkDatabaseUnavailableAsync(DatabaseEntity databaseEntity, bool unavailable);

		// Token: 0x060005D7 RID: 1495
		[OperationContract]
		Task<IEnumerable<DatabaseEntity>> GetExpiredBoundDatabasesAsync(DatabaseType databaseType, TimeSpan expirationDuration);

		// Token: 0x060005D8 RID: 1496
		[OperationContract]
		Task<IEnumerable<DatabaseEntity>> GetExpiredUnboundDatabasesAsync(DatabaseType databaseType, TimeSpan expirationDuration);

		// Token: 0x060005D9 RID: 1497
		[OperationContract]
		Task<IEnumerable<DatabaseEntity>> GetExpiredDatabasesAsync(DatabaseType databaseType, TimeSpan expirationDuration);

		// Token: 0x060005DA RID: 1498
		[OperationContract]
		Task<PersistableEntityFullInformation> GetEntityFullInformationByKeyAsync(string key);

		// Token: 0x060005DB RID: 1499
		[OperationContract]
		Task<PersistableEntityContainer> TryGetEntitiesOfTypeAsync(PersistableItemTypes itemType);

		// Token: 0x060005DC RID: 1500
		[OperationContract]
		Task<ServiceEntity> TryGetBoundSerivceAsync(string databaseKey);

		// Token: 0x060005DD RID: 1501
		[OperationContract]
		Task<DatabaseEntity> TryGetBoundDatabaseAsync(string serviceKey);

		// Token: 0x060005DE RID: 1502
		[OperationContract]
		Task<DatabaseEntity> TryGetDatabaseAsync(string databaseKey);

		// Token: 0x060005DF RID: 1503
		[OperationContract]
		Task<DatabaseEntity> GetDatabaseAsync(string databaseKey);

		// Token: 0x060005E0 RID: 1504
		[OperationContract]
		Task<IEnumerable<DatabaseEntity>> TryGetDatabasesAsync(IEnumerable<string> databaseKeys);

		// Token: 0x060005E1 RID: 1505
		[OperationContract]
		Task<IEnumerable<DatabaseEntity>> GetDatabasesAsync(IEnumerable<string> databaseKeys);

		// Token: 0x060005E2 RID: 1506
		[OperationContract]
		Task<ServiceEntity> TryGetServiceAsync(string serviceKey);

		// Token: 0x060005E3 RID: 1507
		[OperationContract]
		Task<VirtualServerEntity> TryGetVirtualServerAsync(string vsKey);

		// Token: 0x060005E4 RID: 1508
		[OperationContract]
		Task<Dictionary<PersistableItemTypes, int>> TryGetPersistableItemCountsAsync();

		// Token: 0x060005E5 RID: 1509
		[OperationContract]
		Task<int> TryGetDatabaseCountAsync();

		// Token: 0x060005E6 RID: 1510
		[OperationContract]
		Task ClearEntitiesCacheAsync();

		// Token: 0x060005E7 RID: 1511
		[OperationContract]
		void SetTestKey(bool value);

		// Token: 0x060005E8 RID: 1512
		[OperationContract]
		Task MarkDatabaseImageSavedAsync(string databaseKey);
	}
}
