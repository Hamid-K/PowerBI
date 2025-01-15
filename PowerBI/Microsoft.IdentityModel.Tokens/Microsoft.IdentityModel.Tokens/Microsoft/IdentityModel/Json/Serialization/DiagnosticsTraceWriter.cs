using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Serialization
{
	// Token: 0x0200007A RID: 122
	internal class DiagnosticsTraceWriter : ITraceWriter
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0001BE74 File Offset: 0x0001A074
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x0001BE7C File Offset: 0x0001A07C
		public TraceLevel LevelFilter { get; set; }

		// Token: 0x0600066E RID: 1646 RVA: 0x0001BE85 File Offset: 0x0001A085
		private TraceEventType GetTraceEventType(TraceLevel level)
		{
			switch (level)
			{
			case TraceLevel.Error:
				return TraceEventType.Error;
			case TraceLevel.Warning:
				return TraceEventType.Warning;
			case TraceLevel.Info:
				return TraceEventType.Information;
			case TraceLevel.Verbose:
				return TraceEventType.Verbose;
			default:
				throw new ArgumentOutOfRangeException("level");
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001BEB4 File Offset: 0x0001A0B4
		[NullableContext(1)]
		public void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex)
		{
			if (level == TraceLevel.Off)
			{
				return;
			}
			TraceEventCache traceEventCache = new TraceEventCache();
			TraceEventType traceEventType = this.GetTraceEventType(level);
			foreach (object obj in global::System.Diagnostics.Trace.Listeners)
			{
				TraceListener traceListener = (TraceListener)obj;
				if (!traceListener.IsThreadSafe)
				{
					TraceListener traceListener2 = traceListener;
					lock (traceListener2)
					{
						traceListener.TraceEvent(traceEventCache, "Newtonsoft.Json", traceEventType, 0, message);
						goto IL_006E;
					}
					goto IL_005F;
				}
				goto IL_005F;
				IL_006E:
				if (global::System.Diagnostics.Trace.AutoFlush)
				{
					traceListener.Flush();
					continue;
				}
				continue;
				IL_005F:
				traceListener.TraceEvent(traceEventCache, "Newtonsoft.Json", traceEventType, 0, message);
				goto IL_006E;
			}
		}
	}
}
