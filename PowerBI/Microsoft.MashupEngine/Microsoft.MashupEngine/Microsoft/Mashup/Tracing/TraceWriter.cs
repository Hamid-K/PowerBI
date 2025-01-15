using System;
using System.Diagnostics;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Tracing
{
	// Token: 0x020020BB RID: 8379
	internal class TraceWriter
	{
		// Token: 0x0600CD35 RID: 52533 RVA: 0x0028CDAC File Offset: 0x0028AFAC
		public TraceWriter(TraceSource traceSource)
		{
			this.traceSource = traceSource;
		}

		// Token: 0x0600CD36 RID: 52534 RVA: 0x0028CDBB File Offset: 0x0028AFBB
		public void Flush()
		{
			this.traceSource.Flush();
		}

		// Token: 0x0600CD37 RID: 52535 RVA: 0x0028CDC8 File Offset: 0x0028AFC8
		public void Close()
		{
			this.traceSource.Close();
		}

		// Token: 0x0600CD38 RID: 52536 RVA: 0x0028CDD8 File Offset: 0x0028AFD8
		public void TraceEvent(string action, string message, TraceEventType severityLevel, Guid activityId, string correlationId, EventID traceCode)
		{
			this.SetActivityId(activityId);
			try
			{
				this.traceSource.TraceEvent(severityLevel, (int)traceCode, message);
			}
			catch (Exception ex)
			{
				if (!SafeExceptions.IsSafeException(ex))
				{
					throw;
				}
			}
			try
			{
				if (traceCode == EventID.UserTrace)
				{
					switch (severityLevel)
					{
					case TraceEventType.Critical:
						EtwEventSource.Log.UserTraceCritical(TraceWriter.ProductVersion, TraceWriter.ProcessName, TraceWriter.ProcessId, activityId, action, message, correlationId);
						goto IL_02AE;
					case TraceEventType.Error:
						EtwEventSource.Log.UserTraceError(TraceWriter.ProductVersion, TraceWriter.ProcessName, TraceWriter.ProcessId, activityId, action, message, correlationId);
						goto IL_02AE;
					case (TraceEventType)3:
						break;
					case TraceEventType.Warning:
						EtwEventSource.Log.UserTraceWarning(TraceWriter.ProductVersion, TraceWriter.ProcessName, TraceWriter.ProcessId, activityId, action, message, correlationId);
						goto IL_02AE;
					default:
						if (severityLevel == TraceEventType.Information)
						{
							EtwEventSource.Log.UserTraceInformational(TraceWriter.ProductVersion, TraceWriter.ProcessName, TraceWriter.ProcessId, activityId, action, message, correlationId);
							goto IL_02AE;
						}
						break;
					}
					EtwEventSource.Log.UserTraceVerbose(TraceWriter.ProductVersion, TraceWriter.ProcessName, TraceWriter.ProcessId, activityId, action, message, correlationId);
				}
				else if (traceCode == EventID.TraceWithoutPii)
				{
					switch (severityLevel)
					{
					case TraceEventType.Critical:
						EtwEventSource.Log.CriticalWithoutPii(TraceWriter.ProductVersion, TraceWriter.ProcessName, TraceWriter.ProcessId, activityId, action, message, correlationId);
						goto IL_02AE;
					case TraceEventType.Error:
						EtwEventSource.Log.ErrorWithoutPii(TraceWriter.ProductVersion, TraceWriter.ProcessName, TraceWriter.ProcessId, activityId, action, message, correlationId);
						goto IL_02AE;
					case (TraceEventType)3:
						break;
					case TraceEventType.Warning:
						EtwEventSource.Log.WarningWithoutPii(TraceWriter.ProductVersion, TraceWriter.ProcessName, TraceWriter.ProcessId, activityId, action, message, correlationId);
						goto IL_02AE;
					default:
						if (severityLevel == TraceEventType.Information)
						{
							EtwEventSource.Log.InformationalWithoutPii(TraceWriter.ProductVersion, TraceWriter.ProcessName, TraceWriter.ProcessId, activityId, action, message, correlationId);
							goto IL_02AE;
						}
						break;
					}
					EtwEventSource.Log.VerboseWithoutPii(TraceWriter.ProductVersion, TraceWriter.ProcessName, TraceWriter.ProcessId, activityId, action, message, correlationId);
				}
				else
				{
					switch (severityLevel)
					{
					case TraceEventType.Critical:
						EtwEventSource.Log.Critical(TraceWriter.ProductVersion, TraceWriter.ProcessName, TraceWriter.ProcessId, activityId, action, message, correlationId);
						goto IL_02AE;
					case TraceEventType.Error:
						EtwEventSource.Log.Error(TraceWriter.ProductVersion, TraceWriter.ProcessName, TraceWriter.ProcessId, activityId, action, message, correlationId);
						goto IL_02AE;
					case (TraceEventType)3:
						break;
					case TraceEventType.Warning:
						EtwEventSource.Log.Warning(TraceWriter.ProductVersion, TraceWriter.ProcessName, TraceWriter.ProcessId, activityId, action, message, correlationId);
						goto IL_02AE;
					default:
						if (severityLevel == TraceEventType.Information)
						{
							EtwEventSource.Log.Informational(TraceWriter.ProductVersion, TraceWriter.ProcessName, TraceWriter.ProcessId, activityId, action, message, correlationId);
							goto IL_02AE;
						}
						break;
					}
					EtwEventSource.Log.Verbose(TraceWriter.ProductVersion, TraceWriter.ProcessName, TraceWriter.ProcessId, activityId, action, message, correlationId);
				}
				IL_02AE:;
			}
			catch (Exception ex2)
			{
				if (!SafeExceptions.IsSafeException(ex2))
				{
					throw;
				}
			}
		}

		// Token: 0x0600CD39 RID: 52537 RVA: 0x0028D0D4 File Offset: 0x0028B2D4
		private void SetActivityId(Guid activityId)
		{
			EventSource.SetCurrentThreadActivityId(activityId);
		}

		// Token: 0x17003153 RID: 12627
		// (get) Token: 0x0600CD3A RID: 52538 RVA: 0x0028D0DC File Offset: 0x0028B2DC
		private static string ProductVersion
		{
			get
			{
				if (TraceWriter.productVersion == null)
				{
					TraceWriter.productVersion = DiagnosticsUtil.EvaluatorVersionNumber.ToString();
				}
				return TraceWriter.productVersion;
			}
		}

		// Token: 0x17003154 RID: 12628
		// (get) Token: 0x0600CD3B RID: 52539 RVA: 0x0028D0F9 File Offset: 0x0028B2F9
		private static string ProcessName
		{
			get
			{
				if (TraceWriter.processName == null)
				{
					TraceWriter.processName = CurrentProcess.GetProcessName();
				}
				return TraceWriter.processName;
			}
		}

		// Token: 0x17003155 RID: 12629
		// (get) Token: 0x0600CD3C RID: 52540 RVA: 0x0028D111 File Offset: 0x0028B311
		private static int ProcessId
		{
			get
			{
				if (TraceWriter.processId == null)
				{
					TraceWriter.processId = new int?(CurrentProcess.GetProcessID());
				}
				return TraceWriter.processId.Value;
			}
		}

		// Token: 0x040067D3 RID: 26579
		private static string productVersion;

		// Token: 0x040067D4 RID: 26580
		private static string processName;

		// Token: 0x040067D5 RID: 26581
		private static int? processId;

		// Token: 0x040067D6 RID: 26582
		private readonly TraceSource traceSource;
	}
}
