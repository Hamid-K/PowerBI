using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Azure.Common.DataContracts;
using Microsoft.PowerBI.ContentProviders;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200009C RID: 156
	public interface IAMOIntegrator
	{
		// Token: 0x06000579 RID: 1401
		IAsyncResult BeginPreparePowerBIDatabase(DatabaseMoniker databaseMoniker, string alterXmla, string tabularJsonSchema, IEnumerable<DataSourceMapping> dataSources, AsyncCallback callback, object asyncState);

		// Token: 0x0600057A RID: 1402
		string EndPreparePowerBIDatabase(IAsyncResult asyncResult);

		// Token: 0x0600057B RID: 1403
		IAsyncResult BeginGetConnectionStrings(DatabaseMoniker databaseMoniker, IntendedUsage intendedUsage, bool forModelPublishing, AsyncCallback callback, object asyncState);

		// Token: 0x0600057C RID: 1404
		ModelConnectionStrings EndGetConnectionStrings(IAsyncResult asyncResult);

		// Token: 0x0600057D RID: 1405
		IAsyncResult BeginApplyPowerBIDatabaseParameters(DatabaseMoniker databaseMoniker, IEnumerable<DatabaseParameter> parameters, AsyncCallback callback, object asyncState);

		// Token: 0x0600057E RID: 1406
		void EndApplyPowerBIDatabaseParameters(IAsyncResult asyncResult);

		// Token: 0x0600057F RID: 1407
		IAsyncResult BeginProcessAsAzureDatabase(DatabaseMoniker databaseMoniker, string connectionString, int maxParallel, int maxRetry, TimeSpan retryBackoff, bool refreshIncrementally, DateTime effectiveIncrementalRefresh, AsyncCallback callback, object asyncState);

		// Token: 0x06000580 RID: 1408
		void EndProcessAsAzureDatabase(IAsyncResult asyncResult);

		// Token: 0x06000581 RID: 1409
		IAsyncResult BeginGetMExpressions(DatabaseMoniker databaseMoniker, IntendedUsage intendedUsage, AsyncCallback callback, object asyncState);

		// Token: 0x06000582 RID: 1410
		Dictionary<string, string> EndGetMExpressions(IAsyncResult asyncResult);

		// Token: 0x06000583 RID: 1411
		IAsyncResult BeginSetMExpressions(DatabaseMoniker databaseMoniker, IntendedUsage intendedUsage, Dictionary<string, string> expressions, AsyncCallback callback, object asyncState);

		// Token: 0x06000584 RID: 1412
		void EndSetMExpressions(IAsyncResult asyncResult);
	}
}
