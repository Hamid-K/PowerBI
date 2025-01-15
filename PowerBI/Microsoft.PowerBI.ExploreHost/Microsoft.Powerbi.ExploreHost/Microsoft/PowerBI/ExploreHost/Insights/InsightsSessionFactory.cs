using System;
using Microsoft.BusinessIntelligence;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.ReportingServicesHost;
using Microsoft.PowerBI.ReportingServicesHost.Insights;

namespace Microsoft.PowerBI.ExploreHost.Insights
{
	// Token: 0x02000085 RID: 133
	public sealed class InsightsSessionFactory : IInsightsSessionFactory
	{
		// Token: 0x06000391 RID: 913 RVA: 0x0000B714 File Offset: 0x00009914
		public IInsightsSession CreateInsightsSession(InsightsSessionParameters insightsSessionParameters, IConnectionFactory connectionFactory, IConnectionPool connectionPool, IDataSourceInfo dataSourceInfo, FeatureSwitches featureSwitches, ITelemetryService telemetryService, Func<string, ParseConceptualSchema, IConceptualSchema> getConceptualSchema)
		{
			InsightsSessionStorage insightsSessionStorage = new InsightsSessionStorage();
			CancellationTokenManager cancellationTokenManager = new CancellationTokenManager();
			return new InsightsSession(insightsSessionParameters, connectionFactory, connectionPool, dataSourceInfo, featureSwitches, telemetryService, getConceptualSchema, insightsSessionStorage, cancellationTokenManager);
		}
	}
}
