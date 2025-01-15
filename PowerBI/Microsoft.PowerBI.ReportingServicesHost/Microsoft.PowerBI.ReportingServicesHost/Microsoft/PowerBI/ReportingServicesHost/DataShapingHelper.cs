using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.DataExtension.Msolap;
using Microsoft.PowerBI.ExploreHost.Contracts;
using Microsoft.ReportingServices.Library;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000038 RID: 56
	internal static class DataShapingHelper
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00004611 File Offset: 0x00002811
		internal static void InitializeDefaultConnectionFactory(bool enableMsolapTracing)
		{
			DataShapingHelper.DefaultConnectionFactory = DataExtensionFactory.CreateDefaultConnectionFactory(DataShapingTracer.Instance, DataExtensionPrivateInformationService.Instance, enableMsolapTracing);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004628 File Offset: 0x00002828
		internal static ExploreHostDataSourceInfo CreateDataSourceInfo(string dataSourceName, IASConnectionInfo asConnectionInfo)
		{
			return new ExploreHostDataSourceInfo(dataSourceName, "DAX", asConnectionInfo.GetConnectionString(), asConnectionInfo.ModelMetadataCatalogName, asConnectionInfo.CubeName, asConnectionInfo.ModelLocation);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000464D File Offset: 0x0000284D
		internal static QueryExecutionOptions ToDSEQueryExecutionOptions(this QueryExecutionOptionsBase queryExecutionOptions)
		{
			return new QueryExecutionOptions(3, 26, queryExecutionOptions.ConnectionAttempts, new IsRetriableFailure(queryExecutionOptions.IsRetriableFailure));
		}

		// Token: 0x040000EA RID: 234
		public const string DaxDataExtensionName = "DAX";

		// Token: 0x040000EB RID: 235
		private const int UnlimitedAsCommandMemory = 0;

		// Token: 0x040000EC RID: 236
		private const int UnlimitedAsCommandTimeout = 0;

		// Token: 0x040000ED RID: 237
		private const bool EnableRemoteErrors = true;

		// Token: 0x040000EE RID: 238
		internal static IConnectionFactory DefaultConnectionFactory = DataExtensionFactory.CreateDefaultConnectionFactory(DataShapingTracer.Instance, DataExtensionPrivateInformationService.Instance, false);
	}
}
