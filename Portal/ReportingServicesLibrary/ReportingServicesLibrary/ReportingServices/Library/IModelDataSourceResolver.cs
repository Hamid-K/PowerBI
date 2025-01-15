using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Internal;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200017E RID: 382
	internal interface IModelDataSourceResolver
	{
		// Token: 0x06000DF8 RID: 3576
		void CancelModelRetrieval();

		// Token: 0x06000DF9 RID: 3577
		string GetModelFromDataExtension(DataSourceInfo dsInfo, IDbConnectionPool connectionPool, string dataSourceName, string modelMetadataVersion, ModelRetrieval modelRetrievalAdditionalInfo);

		// Token: 0x06000DFA RID: 3578
		DataSourceInfo LoadDataSourceInfoForItem(ProgressiveCacheEntry entry, string dataSourceName, bool isDataSourcesPresent);

		// Token: 0x06000DFB RID: 3579
		DataSourceInfo RebuildAndResolveDataSource(ProgressiveCacheEntry entry, string dataSourceName, bool isDataSourcesPresent, bool cacheItem, RlsUserInfo rlsUserInfo);

		// Token: 0x06000DFC RID: 3580
		DataSourceInfo ResolveDataSource(ProgressiveCacheEntry entry, string dataSourceName, bool isDataSourcesPresent, bool cacheItem);

		// Token: 0x06000DFD RID: 3581
		string ResolveModel(DataSourceInfo dsInfo, ProgressiveCacheEntry entry, string modelMetadataVersion, ModelRetrieval modelRetrievalAdditionalInfo);

		// Token: 0x06000DFE RID: 3582
		bool TryResolveInternalDataSource(ProgressiveCacheEntry entry, out DataSourceInfo dsInfo);

		// Token: 0x06000DFF RID: 3583
		void ProcessDataSourceInfoForSecureStoreCredentials(DataSourceInfo dsInfo);

		// Token: 0x06000E00 RID: 3584
		void RebuildInternalDataSource(ProgressiveCacheEntry entry, RlsUserInfo rlsUserInfo);

		// Token: 0x06000E01 RID: 3585
		void ResolveOnPremiseModelConnectionString(DataSourceInfo dsInfo, string connectionString, string identityClaims, IDataProtection dataProtection);
	}
}
