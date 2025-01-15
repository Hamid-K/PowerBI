using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Cloud.Platform.Tracing.EtwTracing
{
	// Token: 0x02000016 RID: 22
	public sealed class EtwTraceListener : TraceListener
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000034E3 File Offset: 0x000016E3
		public static string ProviderGuid
		{
			get
			{
				return "3450EFD5-1A3F-4839-85EA-EE80B75A971E";
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000034EA File Offset: 0x000016EA
		public EtwTraceListener()
		{
			this.m_eventSourceTracer = new EtwTraceListener.EventSourceTracer();
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000034FD File Offset: 0x000016FD
		public override bool IsThreadSafe
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003500 File Offset: 0x00001700
		public override void Write(string message)
		{
			this.TraceInternal(TraceEventType.Information, string.Empty, message);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003500 File Offset: 0x00001700
		public override void WriteLine(string message)
		{
			this.TraceInternal(TraceEventType.Information, string.Empty, message);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003510 File Offset: 0x00001710
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (data.Length == 8)
			{
				DateTime dateTime = (DateTime)data[0];
				string text = data[1].ToString();
				string text2 = data[2].ToString();
				string text3 = data[3].ToString();
				string text4 = data[4].ToString();
				string text5 = data[5].ToString();
				string text6 = data[6].ToString();
				string text7 = data[7].ToString();
				this.TraceInternal(eventType, source, dateTime, text, text2, text3, text4, text5, text6, text7);
				return;
			}
			this.TraceInternal(eventType, source, ExtendedText.Pack(data.Select((object d) => d.ToString()), ';', '\''));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000035C4 File Offset: 0x000017C4
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			DateTime dateTime;
			string text;
			string text2;
			string text3;
			string text4;
			string text5;
			string text6;
			string text7;
			if (Tracing.ParseTrace(data.ToString(), out dateTime, out text, out text2, out text3, out text4, out text5, out text6, out text7))
			{
				this.TraceInternal(eventType, source, dateTime, text, text4, text5, text6, text7, text2, text3);
				return;
			}
			this.TraceInternal(eventType, source, data.ToString());
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003614 File Offset: 0x00001814
		private void TraceInternal(TraceEventType eventType, string source, string trace)
		{
			this.TraceInternal(eventType, source, DateTime.UtcNow, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, trace);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003650 File Offset: 0x00001850
		private void TraceInternal(TraceEventType eventType, string directory, DateTime traceTimeStamp, string instanceId, string traceActivityId, string rootActivityId, string activityType, string clientActivityId, string sourceId, string eventText)
		{
			eventText = ((eventText.Length > 30000) ? (eventText.Substring(0, 30000) + "</pi>") : eventText);
			switch (eventType)
			{
			case TraceEventType.Critical:
				this.m_eventSourceTracer.TraceCritical(directory, traceTimeStamp, instanceId, traceActivityId, rootActivityId, activityType, clientActivityId, sourceId, eventText);
				return;
			case TraceEventType.Error:
				this.m_eventSourceTracer.TraceError(directory, traceTimeStamp, instanceId, traceActivityId, rootActivityId, activityType, clientActivityId, sourceId, eventText);
				return;
			case (TraceEventType)3:
				break;
			case TraceEventType.Warning:
				this.m_eventSourceTracer.TraceWarning(directory, traceTimeStamp, instanceId, traceActivityId, rootActivityId, activityType, clientActivityId, sourceId, eventText);
				return;
			default:
				if (eventType == TraceEventType.Information)
				{
					this.m_eventSourceTracer.TraceInformational(directory, traceTimeStamp, instanceId, traceActivityId, rootActivityId, activityType, clientActivityId, sourceId, eventText);
					return;
				}
				if (eventType == TraceEventType.Verbose)
				{
					this.m_eventSourceTracer.TraceVerbose(directory, traceTimeStamp, instanceId, traceActivityId, rootActivityId, activityType, clientActivityId, sourceId, eventText);
					return;
				}
				break;
			}
			this.m_eventSourceTracer.TraceInformational(directory, traceTimeStamp, instanceId, traceActivityId, rootActivityId, activityType, clientActivityId, sourceId, eventText);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003753 File Offset: 0x00001953
		public static string ListenerName
		{
			get
			{
				return "EventSource";
			}
		}

		// Token: 0x04000049 RID: 73
		private readonly EtwTraceListener.EventSourceTracer m_eventSourceTracer;

		// Token: 0x0400004A RID: 74
		private const string c_providerGuid = "3450EFD5-1A3F-4839-85EA-EE80B75A971E";

		// Token: 0x0400004B RID: 75
		private const int c_maxLength = 30000;

		// Token: 0x0400004C RID: 76
		private const string c_endPiiTag = "</pi>";

		// Token: 0x0200057F RID: 1407
		[EventSource(Guid = "3450EFD5-1A3F-4839-85EA-EE80B75A971E")]
		private sealed class EventSourceTracer : EventSource
		{
			// Token: 0x06002A76 RID: 10870 RVA: 0x000985D0 File Offset: 0x000967D0
			[Event(1, Level = 5, Message = "{source} {trace}", Version = 1)]
			public void TraceVerbose(string Directory, DateTime TraceTimeStamp, string InstanceId, string TraceActivityId, string RootActivityId, string ActivityType, string ClientActivityId, string SourceId, string EventText)
			{
				base.WriteEvent(1, new object[] { Directory, TraceTimeStamp, InstanceId, TraceActivityId, RootActivityId, ActivityType, ClientActivityId, SourceId, EventText });
			}

			// Token: 0x06002A77 RID: 10871 RVA: 0x0009860F File Offset: 0x0009680F
			[Event(2, Level = 4, Message = "{source} {trace}", Version = 1)]
			public void TraceInformational(string Directory, DateTime TraceTimeStamp, string InstanceId, string TraceActivityId, string RootActivityId, string ActivityType, string ClientActivityId, string SourceId, string EventText)
			{
				base.WriteEvent(2, new object[] { Directory, TraceTimeStamp, InstanceId, TraceActivityId, RootActivityId, ActivityType, ClientActivityId, SourceId, EventText });
			}

			// Token: 0x06002A78 RID: 10872 RVA: 0x0009864E File Offset: 0x0009684E
			[Event(3, Level = 3, Message = "{source} {trace}", Version = 1)]
			public void TraceWarning(string Directory, DateTime TraceTimeStamp, string InstanceId, string TraceActivityId, string RootActivityId, string ActivityType, string ClientActivityId, string SourceId, string EventText)
			{
				base.WriteEvent(3, new object[] { Directory, TraceTimeStamp, InstanceId, TraceActivityId, RootActivityId, ActivityType, ClientActivityId, SourceId, EventText });
			}

			// Token: 0x06002A79 RID: 10873 RVA: 0x0009868D File Offset: 0x0009688D
			[Event(4, Level = 2, Message = "{source} {trace}", Version = 1)]
			public void TraceError(string Directory, DateTime TraceTimeStamp, string InstanceId, string TraceActivityId, string RootActivityId, string ActivityType, string ClientActivityId, string SourceId, string EventText)
			{
				base.WriteEvent(4, new object[] { Directory, TraceTimeStamp, InstanceId, TraceActivityId, RootActivityId, ActivityType, ClientActivityId, SourceId, EventText });
			}

			// Token: 0x06002A7A RID: 10874 RVA: 0x000986CC File Offset: 0x000968CC
			[Event(5, Level = 1, Message = "{source} {trace}", Version = 1)]
			public void TraceCritical(string Directory, DateTime TraceTimeStamp, string InstanceId, string TraceActivityId, string RootActivityId, string ActivityType, string ClientActivityId, string SourceId, string EventText)
			{
				base.WriteEvent(5, new object[] { Directory, TraceTimeStamp, InstanceId, TraceActivityId, RootActivityId, ActivityType, ClientActivityId, SourceId, EventText });
			}
		}
	}
}
