using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights.Channel;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x02000084 RID: 132
	public sealed class TelemetryProcessorChain : IDisposable
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x00012E5A File Offset: 0x0001105A
		internal TelemetryProcessorChain()
		{
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00012E70 File Offset: 0x00011070
		internal TelemetryProcessorChain(IEnumerable<ITelemetryProcessor> telemetryProcessors)
		{
			foreach (ITelemetryProcessor telemetryProcessor in telemetryProcessors)
			{
				this.telemetryProcessors.Add(telemetryProcessor);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00012ED0 File Offset: 0x000110D0
		internal ITelemetryProcessor FirstTelemetryProcessor
		{
			get
			{
				return this.telemetryProcessors.First<ITelemetryProcessor>();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00012EDD File Offset: 0x000110DD
		internal SnapshottingList<ITelemetryProcessor> TelemetryProcessors
		{
			get
			{
				return this.telemetryProcessors;
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00012EE5 File Offset: 0x000110E5
		public void Process(ITelemetry item)
		{
			this.telemetryProcessors.First<ITelemetryProcessor>().Process(item);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x00012EF8 File Offset: 0x000110F8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00012F08 File Offset: 0x00011108
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				SnapshottingList<ITelemetryProcessor> snapshottingList = this.telemetryProcessors;
				if (snapshottingList != null)
				{
					foreach (ITelemetryProcessor telemetryProcessor in snapshottingList)
					{
						IDisposable disposable = telemetryProcessor as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x040001A6 RID: 422
		private readonly SnapshottingList<ITelemetryProcessor> telemetryProcessors = new SnapshottingList<ITelemetryProcessor>();
	}
}
