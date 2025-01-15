using System;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.Mashup.Tracing
{
	// Token: 0x020020B5 RID: 8373
	internal static class TraceExtensions
	{
		// Token: 0x0600CCF5 RID: 52469 RVA: 0x0028C0B4 File Offset: 0x0028A2B4
		public static void AddAction(this Trace trace, string action)
		{
			if (trace.IsEnabled)
			{
				using (TraceValue2 traceValue = new TraceValue2(trace, "Action"))
				{
					traceValue.Add(action);
				}
			}
		}

		// Token: 0x0600CCF6 RID: 52470 RVA: 0x0028C100 File Offset: 0x0028A300
		public static void AddException(this Trace trace, Exception e, TraceEventType type = TraceEventType.Error, bool hasPii = true)
		{
			if (trace.IsEnabled)
			{
				trace.SeverityLevel = type;
				using (TraceValue2 traceValue = new TraceValue2(trace, "Exception"))
				{
					if (e != null)
					{
						using (TraceStringWriter traceStringWriter = traceValue.BeginString())
						{
							DiagnosticsUtil.WriteException(traceStringWriter, e, hasPii);
							return;
						}
					}
					traceValue.AddNull();
				}
			}
		}

		// Token: 0x0600CCF7 RID: 52471 RVA: 0x0028C17C File Offset: 0x0028A37C
		public static void AddProductVersion(this Trace trace)
		{
			if (trace.IsEnabled)
			{
				using (TraceValue2 traceValue = new TraceValue2(trace, "ProductVersion"))
				{
					traceValue.Add(DiagnosticsUtil.EvaluatorVersionNumber.ToString());
				}
			}
		}

		// Token: 0x0600CCF8 RID: 52472 RVA: 0x0028C1D0 File Offset: 0x0028A3D0
		public static void AddProcessId(this Trace trace)
		{
			if (trace.IsEnabled)
			{
				using (TraceValue2 traceValue = new TraceValue2(trace, "Pid"))
				{
					traceValue.Add(CurrentProcess.GetProcessID());
				}
			}
		}

		// Token: 0x0600CCF9 RID: 52473 RVA: 0x0028C220 File Offset: 0x0028A420
		public static void AddProcessName(this Trace trace)
		{
			if (trace.IsEnabled)
			{
				using (TraceValue2 traceValue = new TraceValue2(trace, "Process"))
				{
					traceValue.Add(CurrentProcess.GetProcessName());
				}
			}
		}

		// Token: 0x0600CCFA RID: 52474 RVA: 0x0028C270 File Offset: 0x0028A470
		public static void AddThreadId(this Trace trace)
		{
			if (trace.IsEnabled)
			{
				using (TraceValue2 traceValue = new TraceValue2(trace, "Tid"))
				{
					traceValue.Add(Thread.CurrentThread.ManagedThreadId);
				}
			}
		}

		// Token: 0x0600CCFB RID: 52475 RVA: 0x0028C2C4 File Offset: 0x0028A4C4
		public static void AddActivityId(this Trace trace, Guid activityId)
		{
			if (trace.IsEnabled)
			{
				using (TraceValue2 traceValue = new TraceValue2(trace, "ActivityId"))
				{
					traceValue.Add(activityId);
				}
			}
		}

		// Token: 0x0600CCFC RID: 52476 RVA: 0x0028C310 File Offset: 0x0028A510
		public static void AddCorrelationId(this Trace trace, string correlationId)
		{
			if (trace.IsEnabled)
			{
				using (TraceValue2 traceValue = new TraceValue2(trace, "CorrelationId"))
				{
					traceValue.Add(correlationId);
				}
			}
		}

		// Token: 0x0600CCFD RID: 52477 RVA: 0x0028C35C File Offset: 0x0028A55C
		public static void AddStart(this Trace trace, DateTime start)
		{
			if (trace.IsEnabled)
			{
				using (TraceValue2 traceValue = new TraceValue2(trace, "Start"))
				{
					traceValue.Add(start);
				}
			}
		}

		// Token: 0x0600CCFE RID: 52478 RVA: 0x0028C3A8 File Offset: 0x0028A5A8
		public static void AddDuration(this Trace trace, TimeSpan duration)
		{
			if (trace.IsEnabled)
			{
				using (TraceValue2 traceValue = new TraceValue2(trace, "Duration"))
				{
					traceValue.Add(duration);
				}
			}
		}
	}
}
