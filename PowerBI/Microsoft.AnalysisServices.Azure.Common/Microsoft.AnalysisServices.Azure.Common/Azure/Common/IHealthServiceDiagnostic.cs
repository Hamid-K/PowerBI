using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Communication;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x020000A0 RID: 160
	[ServiceContract]
	[ECFContract]
	public interface IHealthServiceDiagnostic
	{
		// Token: 0x060005A7 RID: 1447
		[OperationContract]
		Task SendDbLoadMetricAsync(string serverUrl, int serverCpuUtilization, int servermemoryUtilizationInMbs, DbLoadMetrics[] metrics, DateTime timestamp);

		// Token: 0x060005A8 RID: 1448
		[OperationContract]
		Task<IEnumerable<DbLoadMetrics>> GetAllDbLoadsAsync();

		// Token: 0x060005A9 RID: 1449
		[OperationContract(AsyncPattern = true, Name = "BeginSendDbLoadMetric")]
		IAsyncResult BeginSendDbLoadMetric(string serverUrl, int serverCpuUtilization, int servermemoryUtilizationInMbs, DbLoadMetrics[] metrics, DateTime timestamp, AsyncCallback callback, object asyncState);

		// Token: 0x060005AA RID: 1450
		void EndSendDbLoadMetric(IAsyncResult result);

		// Token: 0x060005AB RID: 1451
		[OperationContract(AsyncPattern = true, Name = "BeginGetAllDbLoads")]
		IAsyncResult BeginGetAllDbLoads(AsyncCallback asyncCallback, object asyncState);

		// Token: 0x060005AC RID: 1452
		IEnumerable<DbLoadMetrics> EndGetAllDbLoads(IAsyncResult result);
	}
}
