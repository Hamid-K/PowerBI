using System;
using System.Diagnostics;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Tracing
{
	// Token: 0x02000012 RID: 18
	public class ConsoleTraceListener : TraceListener
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003270 File Offset: 0x00001470
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			ConsoleTraceListener.WriteInternal(eventType, source, data.ToString());
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003280 File Offset: 0x00001480
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (data.Length == 8)
			{
				string formatedMessage = Tracing.GetFormatedMessage(data);
				ConsoleTraceListener.WriteInternal(eventType, source, formatedMessage);
				return;
			}
			ConsoleTraceListener.WriteInternal(eventType, source, string.Join("; ", data));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000032B8 File Offset: 0x000014B8
		public override void Write(string message)
		{
			ConsoleTraceListener.WriteInternal(TraceEventType.Information, "Unknown", message);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000032B8 File Offset: 0x000014B8
		public override void WriteLine(string message)
		{
			ConsoleTraceListener.WriteInternal(TraceEventType.Information, "Unknown", message);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000032C8 File Offset: 0x000014C8
		private static void WriteInternal(TraceEventType eventType, string source, string message)
		{
			ConsoleTraceListener.EventTypeProperties eventTypeProperties = new ConsoleTraceListener.EventTypeProperties(eventType);
			ExtendedConsole.WriteLine(eventTypeProperties.Color, "{0} {1,-10} {2}", new object[] { eventTypeProperties.Level, source, message });
		}

		// Token: 0x0200057E RID: 1406
		private struct EventTypeProperties
		{
			// Token: 0x06002A73 RID: 10867 RVA: 0x0009852C File Offset: 0x0009672C
			public EventTypeProperties(TraceEventType eventType)
			{
				switch (eventType)
				{
				case TraceEventType.Critical:
					this.m_color = ConsoleColor.Red;
					this.m_level = 'c';
					return;
				case TraceEventType.Error:
					this.m_color = ConsoleColor.Red;
					this.m_level = 'e';
					return;
				case (TraceEventType)3:
					break;
				case TraceEventType.Warning:
					this.m_color = ConsoleColor.Yellow;
					this.m_level = 'w';
					return;
				default:
					if (eventType == TraceEventType.Information)
					{
						this.m_color = ConsoleColor.White;
						this.m_level = 'i';
						return;
					}
					if (eventType == TraceEventType.Verbose)
					{
						this.m_color = ConsoleColor.Gray;
						this.m_level = 'v';
						return;
					}
					break;
				}
				this.m_color = ConsoleColor.Blue;
				this.m_level = 'u';
			}

			// Token: 0x170006D6 RID: 1750
			// (get) Token: 0x06002A74 RID: 10868 RVA: 0x000985C0 File Offset: 0x000967C0
			public ConsoleColor Color
			{
				get
				{
					return this.m_color;
				}
			}

			// Token: 0x170006D7 RID: 1751
			// (get) Token: 0x06002A75 RID: 10869 RVA: 0x000985C8 File Offset: 0x000967C8
			public char Level
			{
				get
				{
					return this.m_level;
				}
			}

			// Token: 0x04000EF7 RID: 3831
			private readonly ConsoleColor m_color;

			// Token: 0x04000EF8 RID: 3832
			private readonly char m_level;
		}
	}
}
