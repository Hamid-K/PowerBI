using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000568 RID: 1384
	internal class PerformanceMeasurement : IDisposable
	{
		// Token: 0x0600509A RID: 20634 RVA: 0x00152B8D File Offset: 0x00150D8D
		internal PerformanceMeasurement(PerformanceMetricType metricType, Action<PerformanceMetricType, long> callback)
		{
			this._disposeCallback = callback;
			this._metricType = metricType;
			this._startTimestamp = Stopwatch.GetTimestamp();
		}

		// Token: 0x0600509B RID: 20635 RVA: 0x00152BAE File Offset: 0x00150DAE
		public void Dispose()
		{
			this._disposeCallback(this._metricType, this._startTimestamp);
		}

		// Token: 0x0400288D RID: 10381
		private readonly Action<PerformanceMetricType, long> _disposeCallback;

		// Token: 0x0400288E RID: 10382
		private readonly PerformanceMetricType _metricType;

		// Token: 0x0400288F RID: 10383
		private readonly long _startTimestamp;
	}
}
