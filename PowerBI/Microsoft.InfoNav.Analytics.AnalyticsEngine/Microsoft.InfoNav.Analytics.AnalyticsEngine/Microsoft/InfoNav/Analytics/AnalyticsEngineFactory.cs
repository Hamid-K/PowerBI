using System;
using System.ComponentModel;
using Microsoft.InfoNav.Analytics.Clustering;
using Microsoft.InfoNav.Analytics.Forecast;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x02000006 RID: 6
	[ImmutableObject(true)]
	public sealed class AnalyticsEngineFactory
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		public AnalyticsEngineFactory(ITracer tracer = null, ITelemetryService telemetryService = null)
		{
			tracer = tracer ?? DefaultAnalyticsTracer.Instance;
			telemetryService = telemetryService ?? DefaultAnalyticsTelemetryService.Instance;
			this._serviceRuntimeContext = new ServiceRuntimeContext(tracer, telemetryService);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002094 File Offset: 0x00000294
		public IDataTransformPlugin CreateForecastPlugin()
		{
			ForecastTransform forecastTransform = new ForecastTransform(this._serviceRuntimeContext, 0.95f);
			return new DataTransformPlugin(forecastTransform, forecastTransform);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020BC File Offset: 0x000002BC
		public IDataTransformPlugin CreateNoOpForecastPlugin()
		{
			NoOpForecastTransform noOpForecastTransform = new NoOpForecastTransform(this._serviceRuntimeContext);
			return new DataTransformPlugin(noOpForecastTransform, noOpForecastTransform);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020DC File Offset: 0x000002DC
		public IDataTransformPlugin CreateSpatialClustererPlugin()
		{
			SpatialClusterer spatialClusterer = new SpatialClusterer(this._serviceRuntimeContext);
			return new DataTransformPlugin(spatialClusterer, spatialClusterer);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020FC File Offset: 0x000002FC
		public IDataTransformPlugin CreateOutlierDetectorPlugin()
		{
			OutlierDetector outlierDetector = new OutlierDetector(this._serviceRuntimeContext);
			return new DataTransformPlugin(outlierDetector, outlierDetector);
		}

		// Token: 0x0400002F RID: 47
		private readonly ServiceRuntimeContext _serviceRuntimeContext;
	}
}
