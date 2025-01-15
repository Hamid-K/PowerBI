using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Communication;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000A1 RID: 161
	[ServiceContract]
	[ECFContract]
	public interface IHealthServiceLookup
	{
		// Token: 0x060005AD RID: 1453
		[OperationContract]
		Task<string> GetRestaDbInstanceIdForInitialPlacementAsync();

		// Token: 0x060005AE RID: 1454
		[OperationContract]
		IDictionary<string, AggregatedClusterMetrics> GetClusterMetrics();

		// Token: 0x060005AF RID: 1455
		[OperationContract]
		Task SendMetricsAsync(string serverUrl, AggregatedClusterMetrics metrics);

		// Token: 0x060005B0 RID: 1456
		[OperationContract]
		Task<AggregatedServiceMetrics[]> GetServiceMetricsAsync();

		// Token: 0x060005B1 RID: 1457
		[OperationContract]
		Task<string[]> GetOptimalServersToBindAsync(DatabaseMoniker dbName, DatabaseType dbType, int countOfServers);

		// Token: 0x060005B2 RID: 1458
		[OperationContract]
		Task ReportUnhealthyEndpointAsync(string endpointAddress);

		// Token: 0x060005B3 RID: 1459
		[OperationContract(AsyncPattern = true, Name = "BeginSendMetrics")]
		IAsyncResult BeginSendMetrics(string serverUrl, AggregatedClusterMetrics metrics, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060005B4 RID: 1460
		void EndSendMetrics(IAsyncResult result);

		// Token: 0x060005B5 RID: 1461
		[OperationContract(AsyncPattern = true, Name = "BeginGetServiceMetrics")]
		IAsyncResult BeginGetServiceMetrics(AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060005B6 RID: 1462
		AggregatedServiceMetrics[] EndGetServiceMetrics(IAsyncResult result);

		// Token: 0x060005B7 RID: 1463
		[OperationContract(AsyncPattern = true, Name = "BeginGetOptimalServersToBind")]
		IAsyncResult BeginGetOptimalServersToBind(DatabaseMoniker dbName, DatabaseType dbType, int countOfServers, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060005B8 RID: 1464
		string[] EndGetOptimalServersToBind(IAsyncResult result);

		// Token: 0x060005B9 RID: 1465
		[OperationContract(AsyncPattern = true, Name = "BeginReportUnhealthyEndpoint")]
		IAsyncResult BeginReportUnhealthyEndpoint(string endpointAddress, AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060005BA RID: 1466
		void EndReportUnhealthyEndpoint(IAsyncResult result);
	}
}
