using System;
using System.Net;
using System.ServiceModel;
using Microsoft.AnalysisServices.Azure.Common.DataContracts;
using Microsoft.Cloud.Platform.Communication;
using Microsoft.PowerBI.ServiceContracts.Api;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000A5 RID: 165
	[ServiceContract]
	[ECFContract]
	public interface IPushDataService
	{
		// Token: 0x060005EF RID: 1519
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginAppendRows(PushDataAppendRowsBatch batch, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060005F0 RID: 1520
		void EndAppendRows(IAsyncResult result);

		// Token: 0x060005F1 RID: 1521
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginPostRowsWithQuota(PushDataPostRowsWithQuotaRequest parameters, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060005F2 RID: 1522
		void EndPostRowsWithQuota(IAsyncResult result);

		// Token: 0x060005F3 RID: 1523
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginPutTable(string tenant, string dataset, string tableName, string data, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060005F4 RID: 1524
		void EndPutTable(IAsyncResult result);

		// Token: 0x060005F5 RID: 1525
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginPostDataSet(string tenantId, string datasetCreateDdl, string datasetId, PushDataRetentionPolicy retentionPolicy, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060005F6 RID: 1526
		PostDatabaseResult EndPostDataSet(IAsyncResult result);

		// Token: 0x060005F7 RID: 1527
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginDeleteRows(string tenant, string dataset, string table, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060005F8 RID: 1528
		HttpStatusCode EndDeleteRows(IAsyncResult result);

		// Token: 0x060005F9 RID: 1529
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetDatasetAttributes(DatabaseMoniker[] datasets, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060005FA RID: 1530
		PushDataDataset[] EndGetDatasetAttributes(IAsyncResult result);

		// Token: 0x060005FB RID: 1531
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetDatasetTables(DatabaseMoniker dataset, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060005FC RID: 1532
		GetDatasetTablesResult EndGetDatasetTables(IAsyncResult result);

		// Token: 0x060005FD RID: 1533
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetTableColumns(DatabaseMoniker dataset, string tableName, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060005FE RID: 1534
		Column[] EndGetTableColumns(IAsyncResult result);

		// Token: 0x060005FF RID: 1535
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginLockDatabase(DatabaseMoniker dataset, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x06000600 RID: 1536
		void EndLockDatabase(IAsyncResult result);
	}
}
