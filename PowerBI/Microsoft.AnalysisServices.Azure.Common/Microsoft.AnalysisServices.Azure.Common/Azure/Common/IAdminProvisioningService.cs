using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.Azure.Common.DataContracts;
using Microsoft.ASAzure.ASCommon;
using Microsoft.Cloud.ModelCommon;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.PowerBI.ContentProviders;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200009B RID: 155
	[ServiceContract]
	[ECFContract]
	public interface IAdminProvisioningService
	{
		// Token: 0x0600053A RID: 1338
		[OperationContract]
		Task<PowerBIModelPopulateInfo> PopulatePowerBIDatabaseAsync(PowerBIModelProvisioningInfo powerBIModelInfo);

		// Token: 0x0600053B RID: 1339
		[OperationContract]
		Task<IEnumerable<ASConnectionInfo>> GetConnectionStringsAsync(DatabaseMoniker dbMoniker);

		// Token: 0x0600053C RID: 1340
		[OperationContract(Name = "GetConnectionStringsV2Async")]
		Task<IEnumerable<ASConnectionInfo>> GetConnectionStringsAsync(DatabaseMoniker dbMoniker, IntendedUsage intendedUsage);

		// Token: 0x0600053D RID: 1341
		[OperationContract(Name = "GetConnectionStringsV3Async")]
		Task<ModelConnectionStrings> GetConnectionStringsAsync(DatabaseMoniker dbMoniker, IntendedUsage intendedUsage, bool forModelPublishing);

		// Token: 0x0600053E RID: 1342
		[OperationContract]
		Task<Dictionary<string, string>> GetMExpressionsAsync(DatabaseMoniker dbMoniker, IntendedUsage intendedUsage);

		// Token: 0x0600053F RID: 1343
		[OperationContract]
		Task SetMExpressionsAsync(DatabaseMoniker dbMoniker, IntendedUsage intendedUsage, Dictionary<string, string> expressions);

		// Token: 0x06000540 RID: 1344
		[OperationContract]
		Task DeleteDatabaseAsync(DatabaseMoniker databaseMoniker, DatabaseDeletionOptions databaseDeletionOptions);

		// Token: 0x06000541 RID: 1345
		[OperationContract]
		Task DeleteDatabaseWithContainerDeletionOptionAsync(DatabaseMoniker databaseMoniker, DatabaseDeletionOptions databaseDeletionOptions, bool deleteContainer);

		// Token: 0x06000542 RID: 1346
		[OperationContract]
		Task<Uri> GetDatabaseURIAsync(DatabaseMoniker databaseMoniker);

		// Token: 0x06000543 RID: 1347
		[OperationContract]
		[ServiceKnownType(typeof(GenericIdentity))]
		Task<string> GetDatabaseAccessTokenAsync(DatabaseMoniker databaseMoniker, ASModelType modelType, IIdentity userIdentity, BIAzureServiceType serviceType, ASDbAccessType accessType, int accessTokenValidityPeriodMinutes);

		// Token: 0x06000544 RID: 1348
		[OperationContract]
		[ServiceKnownType(typeof(GenericIdentity))]
		Task<string> GetDatabasesAccessTokenAsync(IEnumerable<DatabaseMoniker> databaseMonikers, ASModelType modelType, IIdentity userIdentity, BIAzureServiceType serviceType, ASDbAccessType accessType, int accessTokenValidityPeriodMinutes);

		// Token: 0x06000545 RID: 1349
		[OperationContract]
		Task<DatabaseLicensingData> GetDatabaseLicensingRequirementsAsync(DatabaseMoniker databaseMoniker);

		// Token: 0x06000546 RID: 1350
		[OperationContract]
		Task<Uri> EvictDatabaseAsync(DatabaseMoniker databaseMoniker, DatabaseEvictionReason evictionReason);

		// Token: 0x06000547 RID: 1351
		[OperationContract]
		Task<BindDatabaseResult> ProvisionDatabaseAsync(DatabaseMoniker databaseMoniker, string createdBy, DatabaseType databaseType, int initialLoadInMB);

		// Token: 0x06000548 RID: 1352
		[OperationContract]
		Task<BindDatabaseResult> ProvisionDatabaseWithDBSourceAsync(DatabaseMoniker databaseMoniker, string createdBy, DatabaseType databaseType, int initialLoadInMB, DatabaseSource databaseSource);

		// Token: 0x06000549 RID: 1353
		[OperationContract]
		Task<BindDatabaseResult> CloneDatabaseAsync(DatabaseMoniker sourceDatabaseMoniker, DatabaseMoniker targetDatabaseMoniker, string createdBy);

		// Token: 0x0600054A RID: 1354
		[OperationContract]
		Task<ServiceEntity> ResurrectDatabaseAsync([ECFKey] DatabaseEntity databaseEntity, DatabaseResurrectionReason resurrectionReason);

		// Token: 0x0600054B RID: 1355
		[OperationContract]
		Task<string> CreateVirtualServerBySubscriptionIdAsync(string subscriptionId, string authorityId, string suffix, string option);

		// Token: 0x0600054C RID: 1356
		[OperationContract]
		Task<string> CreateVirtualServerAsync(string vsName);

		// Token: 0x0600054D RID: 1357
		[OperationContract]
		Task DeleteVirtualServerAsync(string virtualServerKey, VirtualServerDeletionOptions deletionOptions);

		// Token: 0x0600054E RID: 1358
		[OperationContract]
		Task<IEnumerable<ModelInfo>> GetMultipleDatabaseModelInfoAsync(IEnumerable<DatabaseMoniker> databaseMonikers);

		// Token: 0x0600054F RID: 1359
		[OperationContract]
		Task<BindDatabaseResult> RestorePowerBIDatabaseAsync(DatabaseMoniker dbMoniker, DatabaseType type, int initialLoadInMB, string containerId);

		// Token: 0x06000550 RID: 1360
		[OperationContract]
		Task ApplyPowerBIDatabaseParametersAsync(DatabaseMoniker databaseMoniker, IEnumerable<DatabaseParameter> parameters);

		// Token: 0x06000551 RID: 1361
		[OperationContract]
		Task UpdateEngineEventsAsync(string traceDefXml, bool enableEvents);

		// Token: 0x06000552 RID: 1362
		[OperationContract]
		Task NotifyDatabaseVolatileWriteAsync(DatabaseMoniker databaseMoniker, DatabaseVolatileWriteSource source);

		// Token: 0x06000553 RID: 1363
		[OperationContract]
		Task<IEnumerable<DatabaseMigrationFailureResult>> MigrateDatabasesAsync([ECFKey] IEnumerable<DatabaseMigrationMoniker> migrationMonikers, string capacityObjectId, string capacityTenantKeyObjectId, string tenantName);

		// Token: 0x06000554 RID: 1364
		[OperationContract(Name = "ProcessPowerBIDatabaseV2Async")]
		Task ProcessPowerBIDatabaseAsync([ECFKey] DatabaseMoniker databaseMoniker, PowerBIProcessDatabaseInfo processInfo);

		// Token: 0x06000555 RID: 1365
		[OperationContract]
		Task<bool> TestDirectQueryProviderConnectionAsync(DataAccessInfo dataAccessInfo);

		// Token: 0x06000556 RID: 1366
		[OperationContract]
		Task<DirectQueryPackageContentInfo> GetPackageContentAsync(DataAccessInfo dataAccessInfo);

		// Token: 0x06000557 RID: 1367
		[OperationContract]
		Task<ConfigurationPageDetails> GetConfigurationPageDetailsAsync(DataAccessInfo dataAccessInfo);

		// Token: 0x06000558 RID: 1368
		[OperationContract]
		Task<PackageContentDescription> GetPackageContentDescriptionAsync(DataAccessInfo dataAccessInfo);

		// Token: 0x06000559 RID: 1369
		[OperationContract(AsyncPattern = true, Name = "BeginPopulatePowerBIDatabase")]
		IAsyncResult BeginPopulatePowerBIDatabase(PowerBIModelProvisioningInfo powerBIModelInfo, AsyncCallback callback, object context);

		// Token: 0x0600055A RID: 1370
		PowerBIModelPopulateInfo EndPopulatePowerBIDatabase(IAsyncResult result);

		// Token: 0x0600055B RID: 1371
		[OperationContract(AsyncPattern = true, Name = "BeginGetConnectionStrings")]
		IAsyncResult BeginGetConnectionStrings(DatabaseMoniker dbMoniker, AsyncCallback callback, object context);

		// Token: 0x0600055C RID: 1372
		IEnumerable<ASConnectionInfo> EndGetConnectionStrings(IAsyncResult result);

		// Token: 0x0600055D RID: 1373
		[OperationContract(AsyncPattern = true, Name = "BeginDeleteDatabase")]
		IAsyncResult BeginDeleteDatabase(DatabaseMoniker databaseMoniker, DatabaseDeletionOptions databaseDeletionOptions, AsyncCallback callback, object context);

		// Token: 0x0600055E RID: 1374
		void EndDeleteDatabase(IAsyncResult result);

		// Token: 0x0600055F RID: 1375
		[OperationContract(AsyncPattern = true, Name = "BeginGetDatabaseURI")]
		IAsyncResult BeginGetDatabaseURI(DatabaseMoniker databaseMoniker, AsyncCallback callback, object context);

		// Token: 0x06000560 RID: 1376
		Uri EndGetDatabaseURI(IAsyncResult result);

		// Token: 0x06000561 RID: 1377
		[OperationContract(AsyncPattern = true, Name = "BeginGetDatabaseLicensingRequirements")]
		IAsyncResult BeginGetDatabaseLicensingRequirements(DatabaseMoniker databaseMoniker, AsyncCallback callback, object context);

		// Token: 0x06000562 RID: 1378
		DatabaseLicensingData EndGetDatabaseLicensingRequirements(IAsyncResult result);

		// Token: 0x06000563 RID: 1379
		[OperationContract(AsyncPattern = true, Name = "BeginEvictDatabase")]
		IAsyncResult BeginEvictDatabase(DatabaseMoniker databaseMoniker, DatabaseEvictionReason evictionReason, AsyncCallback callback, object context);

		// Token: 0x06000564 RID: 1380
		Uri EndEvictDatabase(IAsyncResult result);

		// Token: 0x06000565 RID: 1381
		[OperationContract(AsyncPattern = true, Name = "BeginProvisionDatabase")]
		IAsyncResult BeginProvisionDatabase(DatabaseMoniker databaseMoniker, string createdBy, DatabaseType databaseType, int initialLoadInMB, AsyncCallback callback, object context);

		// Token: 0x06000566 RID: 1382
		BindDatabaseResult EndProvisionDatabase(IAsyncResult result);

		// Token: 0x06000567 RID: 1383
		[OperationContract(AsyncPattern = true, Name = "BeginProvisionDatabaseWithDBSource")]
		IAsyncResult BeginProvisionDatabaseWithDBSource(DatabaseMoniker databaseMoniker, string createdBy, DatabaseType databaseType, int initialLoadInMB, DatabaseSource databaseSource, AsyncCallback callback, object context);

		// Token: 0x06000568 RID: 1384
		BindDatabaseResult EndProvisionDatabaseWithDBSource(IAsyncResult result);

		// Token: 0x06000569 RID: 1385
		[OperationContract(AsyncPattern = true, Name = "BeginCloneDatabase")]
		IAsyncResult BeginCloneDatabase(DatabaseMoniker sourceDatabaseMoniker, DatabaseMoniker targetDatabaseMoniker, string createdBy, AsyncCallback callback, object context);

		// Token: 0x0600056A RID: 1386
		BindDatabaseResult EndCloneDatabase(IAsyncResult result);

		// Token: 0x0600056B RID: 1387
		[OperationContract(AsyncPattern = true, Name = "BeginResurrectDatabase")]
		IAsyncResult BeginResurrectDatabase(DatabaseEntity databaseEntity, DatabaseResurrectionReason resurrectionReason, AsyncCallback callback, object context);

		// Token: 0x0600056C RID: 1388
		ServiceEntity EndResurrectDatabase(IAsyncResult result);

		// Token: 0x0600056D RID: 1389
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginProcessPowerBIDatabase(PowerBIProcessDatabaseInfo processInfo, AsyncCallback callback, object context);

		// Token: 0x0600056E RID: 1390
		void EndProcessPowerBIDatabase(IAsyncResult result);

		// Token: 0x0600056F RID: 1391
		[OperationContract(AsyncPattern = true, Name = "BeginCreateVirtualServerBySubscriptionId")]
		IAsyncResult BeginCreateVirtualServerBySubscriptionId(string subscriptionId, string authorityId, string suffix, string option, AsyncCallback callback, object context);

		// Token: 0x06000570 RID: 1392
		string EndCreateVirtualServerBySubscriptionId(IAsyncResult result);

		// Token: 0x06000571 RID: 1393
		[OperationContract(AsyncPattern = true, Name = "BeginCreateVirtualServer")]
		IAsyncResult BeginCreateVirtualServer(string vsName, AsyncCallback callback, object context);

		// Token: 0x06000572 RID: 1394
		string EndCreateVirtualServer(IAsyncResult result);

		// Token: 0x06000573 RID: 1395
		[OperationContract(AsyncPattern = true, Name = "BeginDeleteVirtualServer")]
		IAsyncResult BeginDeleteVirtualServer(string virtualServerKey, VirtualServerDeletionOptions deletionOptions, AsyncCallback callback, object context);

		// Token: 0x06000574 RID: 1396
		void EndDeleteVirtualServer(IAsyncResult result);

		// Token: 0x06000575 RID: 1397
		[OperationContract(AsyncPattern = true, Name = "BeginRestorePowerBIDatabase")]
		IAsyncResult BeginRestorePowerBIDatabase(DatabaseMoniker dbMoniker, DatabaseType type, int initialLoadInMB, string containerId, AsyncCallback asyncCallback, object asyncContext);

		// Token: 0x06000576 RID: 1398
		BindDatabaseResult EndRestorePowerBIDatabase(IAsyncResult asyncResult);

		// Token: 0x06000577 RID: 1399
		[OperationContract(AsyncPattern = true, Name = "BeginApplyPowerBIDatabaseParameters")]
		IAsyncResult BeginApplyPowerBIDatabaseParameters(DatabaseMoniker databaseMoniker, IEnumerable<DatabaseParameter> parameters, AsyncCallback callback, object asyncState);

		// Token: 0x06000578 RID: 1400
		void EndApplyPowerBIDatabaseParameters(IAsyncResult asyncResult);
	}
}
