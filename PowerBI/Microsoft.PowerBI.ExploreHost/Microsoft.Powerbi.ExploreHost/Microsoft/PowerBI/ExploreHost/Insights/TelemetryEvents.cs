using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Experimental.Insights.ServiceContracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using Microsoft.PowerBI.Telemetry;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.ExploreHost.Insights
{
	// Token: 0x02000087 RID: 135
	internal sealed class TelemetryEvents : ITelemetryEvents, ITelemetryIdsProvider
	{
		// Token: 0x0600039A RID: 922 RVA: 0x0000B811 File Offset: 0x00009A11
		public TelemetryEvents(Microsoft.DataShaping.ServiceContracts.ITelemetryService telemetryService)
		{
			this.m_telemetryService = telemetryService;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000B820 File Offset: 0x00009A20
		public void TraceInsightStatistics(InsightTelemetryStatistics statistics)
		{
			InsightTelemetryStatistics insightTelemetryStatistics = statistics.ShallowCopy();
			insightTelemetryStatistics.AnalysisStatistics = null;
			insightTelemetryStatistics.InsightsStatistics = null;
			PBIWinInsightsStatistics pbiwinInsightsStatistics = new PBIWinInsightsStatistics(JsonConvert.SerializeObject(insightTelemetryStatistics), JsonConvert.SerializeObject(statistics.AnalysisStatistics), JsonConvert.SerializeObject(statistics.InsightsStatistics));
			TelemetryService.Instance.Log(pbiwinInsightsStatistics);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000B86D File Offset: 0x00009A6D
		public bool TryGetTelemetryIds(out string clientActivityId, out string currentActivityId, out string rootActivityId, out string applicationContext)
		{
			applicationContext = null;
			return this.m_telemetryService.TryGetTelemetryIDs(out clientActivityId, out currentActivityId, out rootActivityId);
		}

		// Token: 0x040001AD RID: 429
		private readonly Microsoft.DataShaping.ServiceContracts.ITelemetryService m_telemetryService;
	}
}
