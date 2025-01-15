using System;
using Microsoft.InfoNav.Analytics;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200002A RID: 42
	public static class DataTransformPluginFactoryHost
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00003AFD File Offset: 0x00001CFD
		public static DataTransformPluginFactory Create(IAnalyticsFeatureSwitchProvider analyticsFeatureSwitchProvider)
		{
			return new DataTransformPluginFactory(new AnalyticsEngineFactory(DataTransformTracer.Instance, DataTransformTelemetryService.Instance), analyticsFeatureSwitchProvider);
		}
	}
}
