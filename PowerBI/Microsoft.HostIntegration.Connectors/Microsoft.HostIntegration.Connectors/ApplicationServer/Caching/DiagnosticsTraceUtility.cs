using System;
using System.Diagnostics;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001C0 RID: 448
	internal static class DiagnosticsTraceUtility
	{
		// Token: 0x06000ED1 RID: 3793 RVA: 0x000323C0 File Offset: 0x000305C0
		internal static void WriteEntry(string src, TraceEventType msgType, string msgText)
		{
			if (msgType <= TraceEventType.Start)
			{
				if (msgType <= TraceEventType.Information)
				{
					switch (msgType)
					{
					case TraceEventType.Critical:
						Trace.TraceError(DiagnosticsTraceUtility.GetMessageString(TraceEventType.Critical, src, msgText));
						return;
					case TraceEventType.Error:
						Trace.TraceError(DiagnosticsTraceUtility.GetMessageString(TraceEventType.Error, src, msgText));
						return;
					case (TraceEventType)3:
						break;
					case TraceEventType.Warning:
						Trace.TraceWarning(DiagnosticsTraceUtility.GetMessageString(TraceEventType.Warning, src, msgText));
						return;
					default:
						if (msgType != TraceEventType.Information)
						{
							return;
						}
						goto IL_008D;
					}
				}
				else if (msgType != TraceEventType.Verbose)
				{
					if (msgType != TraceEventType.Start)
					{
						return;
					}
					goto IL_008D;
				}
				else
				{
					Trace.TraceInformation(DiagnosticsTraceUtility.GetMessageString(TraceEventType.Verbose, src, msgText));
				}
				return;
			}
			if (msgType <= TraceEventType.Suspend)
			{
				if (msgType != TraceEventType.Stop && msgType != TraceEventType.Suspend)
				{
					return;
				}
			}
			else if (msgType != TraceEventType.Resume && msgType != TraceEventType.Transfer)
			{
				return;
			}
			IL_008D:
			Trace.TraceInformation(DiagnosticsTraceUtility.GetMessageString(TraceEventType.Information, src, msgText));
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00032478 File Offset: 0x00030678
		private static string GetMessageString(TraceEventType eventType, string src, string msg)
		{
			string text = string.Empty;
			if (!string.IsNullOrEmpty(src))
			{
				text = "<" + src + "> ";
			}
			return eventType.ToString().ToUpperInvariant() + ": " + text + msg;
		}
	}
}
