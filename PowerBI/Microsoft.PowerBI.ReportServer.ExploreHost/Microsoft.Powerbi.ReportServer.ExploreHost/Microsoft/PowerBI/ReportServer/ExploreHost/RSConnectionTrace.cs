using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportServer.ExploreHost
{
	// Token: 0x02000007 RID: 7
	internal class RSConnectionTrace : BaseTelemetryEvent
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002630 File Offset: 0x00000830
		public RSConnectionTrace(TraceType type, string message)
			: base("RS.Host.ConnectionInfo", TelemetryUse.Trace)
		{
			this.type = type;
			this.message = message;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000264C File Offset: 0x0000084C
		// (set) Token: 0x06000019 RID: 25 RVA: 0x00002654 File Offset: 0x00000854
		public TraceType type { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000265D File Offset: 0x0000085D
		// (set) Token: 0x0600001B RID: 27 RVA: 0x00002665 File Offset: 0x00000865
		public string message { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002670 File Offset: 0x00000870
		public override Dictionary<string, string> Properties
		{
			get
			{
				return new Dictionary<string, string>
				{
					{
						"type",
						this.type.ToString()
					},
					{
						"message",
						this.message.ToString(CultureInfo.InvariantCulture)
					}
				};
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026BC File Offset: 0x000008BC
		public static void Trace(TraceType type, string message, params object[] args)
		{
			RSConnectionTrace.Trace(type, string.Format(CultureInfo.InvariantCulture, message, args));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000026D0 File Offset: 0x000008D0
		public static void Trace(TraceType type, string message)
		{
			TelemetryService.Instance.Log(new RSConnectionTrace(type, message));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026E3 File Offset: 0x000008E3
		public static void TimedTrace(Action action, TraceType type, string message, params object[] args)
		{
			RSConnectionTrace.TimedTrace(action, type, string.Format(CultureInfo.InvariantCulture, message, args));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026F8 File Offset: 0x000008F8
		public static void TimedTrace(Action action, TraceType type, string message)
		{
			RSConnectionTrace rsconnectionTrace = new RSConnectionTrace(type, message);
			TelemetryService.Instance.StartTimedEvent(rsconnectionTrace);
			action();
			TelemetryService.Instance.EndTimedEvent(rsconnectionTrace);
		}
	}
}
