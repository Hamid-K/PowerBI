using System;
using Microsoft.BusinessIntelligence;
using Microsoft.PowerBI.ExploreHost.Contracts;
using Microsoft.PowerBI.ReportingServicesHost.Insights;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200005F RID: 95
	internal sealed class ReportingSessionCreator : IReportingSessionCreator
	{
		// Token: 0x0600022C RID: 556 RVA: 0x000062BD File Offset: 0x000044BD
		internal ReportingSessionCreator(ILuciaSessionFactory luciaSessionFactory, IInsightsSessionFactory insightsSessionFactory)
		{
			this.m_luciaSessionFactory = luciaSessionFactory;
			this.m_insightsSessionFactory = insightsSessionFactory;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000062D4 File Offset: 0x000044D4
		public IReportingSession CreateReportingSession(IModelMetadataProvider modelMetadataProvider, IASConnectionInfo asConnectionInfo, FeatureSwitches featureSwitches, LuciaSessionParameters luciaSessionParameters, QueryExecutionOptionsBase queryExecutionOptions, SchemaOptions schemaOptions, TelemetrySettings telemetrySettings = null, InsightsSessionParameters insightsSessionParameters = null)
		{
			return new ReportingSession(modelMetadataProvider, asConnectionInfo, featureSwitches, this.m_luciaSessionFactory, luciaSessionParameters, queryExecutionOptions, schemaOptions, telemetrySettings, this.m_insightsSessionFactory, insightsSessionParameters);
		}

		// Token: 0x0400014F RID: 335
		private readonly ILuciaSessionFactory m_luciaSessionFactory;

		// Token: 0x04000150 RID: 336
		private readonly IInsightsSessionFactory m_insightsSessionFactory;
	}
}
