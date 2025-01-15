using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Microsoft.ApplicationInsights.Shared.Extensibility.Implementation
{
	// Token: 0x0200004F RID: 79
	internal class BroadcastProcessor : ITelemetryProcessor
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000CED8 File Offset: 0x0000B0D8
		public BroadcastProcessor(IEnumerable<TelemetrySink> children)
		{
			if (children == null)
			{
				throw new ArgumentNullException("children");
			}
			int num = children.Count<TelemetrySink>();
			if (num < 2)
			{
				throw new ArgumentException("BroadcastProcessor requires two or more children", "children");
			}
			this.childrenDispatchers = new BroadcastProcessor.TelemetryDispatcher[num];
			bool flag = true;
			int num2 = 0;
			foreach (TelemetrySink telemetrySink in children)
			{
				this.childrenDispatchers[num2++] = new BroadcastProcessor.TelemetryDispatcher(telemetrySink, !flag);
				flag = false;
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000CF74 File Offset: 0x0000B174
		public void Process(ITelemetry item)
		{
			for (int i = this.childrenDispatchers.Length - 1; i >= 0; i--)
			{
				this.childrenDispatchers[i].SendItemToSink(item);
			}
		}

		// Token: 0x0400011F RID: 287
		private BroadcastProcessor.TelemetryDispatcher[] childrenDispatchers;

		// Token: 0x020000FB RID: 251
		private class TelemetryDispatcher
		{
			// Token: 0x060008BA RID: 2234 RVA: 0x0001C347 File Offset: 0x0001A547
			public TelemetryDispatcher(TelemetrySink sink, bool cloneBeforeDispatch)
			{
				this.sink = sink;
				this.cloneBeforeDispatch = cloneBeforeDispatch;
			}

			// Token: 0x060008BB RID: 2235 RVA: 0x0001C360 File Offset: 0x0001A560
			public void SendItemToSink(ITelemetry telemetry)
			{
				ITelemetry telemetry2 = (this.cloneBeforeDispatch ? telemetry.DeepClone() : telemetry);
				if (telemetry2 != null)
				{
					this.sink.Process(telemetry2);
				}
			}

			// Token: 0x04000384 RID: 900
			private bool cloneBeforeDispatch;

			// Token: 0x04000385 RID: 901
			private TelemetrySink sink;
		}
	}
}
