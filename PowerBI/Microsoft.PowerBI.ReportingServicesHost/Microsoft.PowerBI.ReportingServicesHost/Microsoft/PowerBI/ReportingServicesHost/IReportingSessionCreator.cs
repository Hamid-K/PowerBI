using System;
using Microsoft.BusinessIntelligence;
using Microsoft.PowerBI.ExploreHost.Contracts;
using Microsoft.PowerBI.ReportingServicesHost.Insights;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000044 RID: 68
	internal interface IReportingSessionCreator
	{
		// Token: 0x06000187 RID: 391
		IReportingSession CreateReportingSession(IModelMetadataProvider modelMetadataProvider, IASConnectionInfo asConnectionInfo, FeatureSwitches featureSwitches, LuciaSessionParameters luciaSessionParameters, QueryExecutionOptionsBase queryExecutionOptions, SchemaOptions schemaOptions, TelemetrySettings telemetrySettings = null, InsightsSessionParameters insightsSessionParameters = null);
	}
}
