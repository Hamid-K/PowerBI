using System;
using Microsoft.BusinessIntelligence;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.ReportingServicesHost.Insights
{
	// Token: 0x02000068 RID: 104
	public interface IInsightsSessionFactory
	{
		// Token: 0x06000245 RID: 581
		IInsightsSession CreateInsightsSession(InsightsSessionParameters insightsSessionParameters, IConnectionFactory connectionFactory, IConnectionPool connectionPool, IDataSourceInfo dataSourceInfo, FeatureSwitches featureSwitches, ITelemetryService telemetryService, Func<string, ParseConceptualSchema, IConceptualSchema> getConceptualSchema);
	}
}
