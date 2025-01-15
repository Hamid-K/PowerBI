using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal
{
	// Token: 0x020000E3 RID: 227
	[NullableContext(1)]
	[Nullable(0)]
	internal static class TraceUtils
	{
		// Token: 0x0600110E RID: 4366 RVA: 0x000468C0 File Offset: 0x00044AC0
		internal static void AddGlobalTraceListeners(TraceSource targetTraceSource)
		{
			RuntimeChecks.CheckValue(targetTraceSource, "targetTraceSource");
			TraceListener[] array = new TraceListener[Trace.Listeners.Count + 32];
			Trace.Listeners.CopyTo(array, 0);
			foreach (TraceListener traceListener in array)
			{
				if (traceListener == null)
				{
					break;
				}
				targetTraceSource.Listeners.Add(traceListener);
			}
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0004691C File Offset: 0x00044B1C
		internal static TraceEventType ConvertTraceVerbosityToDotNetEventType(PipelineTraceVerbosity verbosity)
		{
			TraceEventType traceEventType = TraceEventType.Critical;
			switch (verbosity)
			{
			case PipelineTraceVerbosity.Fatal:
				traceEventType = TraceEventType.Critical;
				break;
			case PipelineTraceVerbosity.Error:
				traceEventType = TraceEventType.Error;
				break;
			case PipelineTraceVerbosity.Warning:
				traceEventType = TraceEventType.Warning;
				break;
			case PipelineTraceVerbosity.Info:
				traceEventType = TraceEventType.Information;
				break;
			case PipelineTraceVerbosity.Verbose:
				traceEventType = TraceEventType.Verbose;
				break;
			}
			return traceEventType;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0004695D File Offset: 0x00044B5D
		internal static string FormatWithInvariantCulture(string format, object arg0)
		{
			return string.Format(CultureInfo.InvariantCulture, format, arg0);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0004696B File Offset: 0x00044B6B
		internal static string FormatWithInvariantCulture(string format, object arg0, object arg1)
		{
			return string.Format(CultureInfo.InvariantCulture, format, arg0, arg1);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0004697A File Offset: 0x00044B7A
		internal static string FormatWithInvariantCulture(string format, object arg0, object arg1, object arg2)
		{
			return string.Format(CultureInfo.InvariantCulture, format, arg0, arg1, arg2);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0004698A File Offset: 0x00044B8A
		internal static string FormatWithInvariantCulture(string format, object arg0, object arg1, object arg2, object arg3)
		{
			return string.Format(CultureInfo.InvariantCulture, format, new object[] { arg0, arg1, arg2, arg3 });
		}
	}
}
