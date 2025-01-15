using System;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000315 RID: 789
	internal class ETWSink : IEventSink
	{
		// Token: 0x06001CC4 RID: 7364 RVA: 0x00002B16 File Offset: 0x00000D16
		public bool Load(string id)
		{
			return true;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x000576A8 File Offset: 0x000558A8
		public void WriteEntry(string src, TraceEventType msgType, string msgText)
		{
			if (msgText == null)
			{
				return;
			}
			if (msgType <= TraceEventType.Start)
			{
				if (msgType <= TraceEventType.Information)
				{
					switch (msgType)
					{
					case TraceEventType.Critical:
						Provider.EventWriteCriticalEvent(src, msgText);
						return;
					case TraceEventType.Error:
						Provider.EventWriteErrorEvent(src, msgText);
						return;
					case (TraceEventType)3:
						break;
					case TraceEventType.Warning:
						Provider.EventWriteWarningEvent(src, msgText);
						return;
					default:
						if (msgType != TraceEventType.Information)
						{
							return;
						}
						goto IL_0082;
					}
				}
				else if (msgType != TraceEventType.Verbose)
				{
					if (msgType != TraceEventType.Start)
					{
						return;
					}
					goto IL_0082;
				}
				else
				{
					Provider.EventWriteVerboseEvent(src, msgText);
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
			IL_0082:
			Provider.EventWriteInformationalEvent(src, msgText);
		}
	}
}
