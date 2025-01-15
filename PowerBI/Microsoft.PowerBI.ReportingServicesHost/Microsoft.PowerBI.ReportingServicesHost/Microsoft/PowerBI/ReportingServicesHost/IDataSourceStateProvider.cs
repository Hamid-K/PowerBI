using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200003E RID: 62
	internal interface IDataSourceStateProvider
	{
		// Token: 0x0600015A RID: 346
		IConnectionFactory GetConnectionFactory(string databaseID);

		// Token: 0x0600015B RID: 347
		IConnectionPool GetConnectionPool(string databaseID);

		// Token: 0x0600015C RID: 348
		IDataShapingDataSourceInfo GetDataSourceInfo(string databaseId);

		// Token: 0x0600015D RID: 349
		IConnectionUserImpersonator GetConnectionUserImpersonator(string databaseId);

		// Token: 0x0600015E RID: 350
		ITelemetryService CreateTelemetryServiceForRequest(string databaseId);
	}
}
