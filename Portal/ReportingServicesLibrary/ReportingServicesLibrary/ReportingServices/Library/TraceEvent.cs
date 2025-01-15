using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000010 RID: 16
	public sealed class TraceEvent
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000025F4 File Offset: 0x000007F4
		public TraceEvent()
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025FC File Offset: 0x000007FC
		public TraceEvent(string category, string progressiveSessionId, int traceLevel, TraceEvent.TraceEventType eventType, string message, string hostApplication, string details)
		{
			this.ClientDateTimeOffset = DateTimeOffset.Now;
			this.Category = category;
			this.ProgressiveSessionId = progressiveSessionId;
			this.TraceLevel = traceLevel;
			this.EventType = eventType;
			this.Message = message;
			this.HostApplication = hostApplication;
			this.Details = details;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000264F File Offset: 0x0000084F
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002657 File Offset: 0x00000857
		[XmlIgnore]
		public DateTimeOffset ClientDateTimeOffset { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002660 File Offset: 0x00000860
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002672 File Offset: 0x00000872
		public string ClientDateTime
		{
			get
			{
				return XmlConvert.ToString(this.ClientDateTimeOffset, "yyyy-MM-ddTHH:mm:sszzz");
			}
			set
			{
				this.ClientDateTimeOffset = XmlConvert.ToDateTimeOffset(value, "yyyy-MM-ddTHH:mm:sszzz");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002685 File Offset: 0x00000885
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000268D File Offset: 0x0000088D
		public string Category { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002696 File Offset: 0x00000896
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000269E File Offset: 0x0000089E
		public string ProgressiveSessionId { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000026A7 File Offset: 0x000008A7
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000026AF File Offset: 0x000008AF
		public int TraceLevel { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000026B8 File Offset: 0x000008B8
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000026C0 File Offset: 0x000008C0
		public TraceEvent.TraceEventType EventType { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000026C9 File Offset: 0x000008C9
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000026D1 File Offset: 0x000008D1
		public string Message { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000026DA File Offset: 0x000008DA
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000026E2 File Offset: 0x000008E2
		public string HostApplication { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000026EB File Offset: 0x000008EB
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000026F3 File Offset: 0x000008F3
		public string Details { get; set; }

		// Token: 0x0600002D RID: 45 RVA: 0x000026FC File Offset: 0x000008FC
		internal static TraceLevel ConvertToTraceLevel(TraceEvent.TraceEventType eventType)
		{
			switch (eventType)
			{
			case TraceEvent.TraceEventType.Error:
				return global::System.Diagnostics.TraceLevel.Error;
			case TraceEvent.TraceEventType.Warn:
				return global::System.Diagnostics.TraceLevel.Warning;
			case TraceEvent.TraceEventType.Perf:
				return global::System.Diagnostics.TraceLevel.Info;
			case TraceEvent.TraceEventType.Info:
				return global::System.Diagnostics.TraceLevel.Info;
			case TraceEvent.TraceEventType.Debug:
				return global::System.Diagnostics.TraceLevel.Verbose;
			default:
				return global::System.Diagnostics.TraceLevel.Off;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002725 File Offset: 0x00000925
		internal static TraceEvent.TraceEventType ConvertToTraceEventType(TraceLevel traceLevel)
		{
			switch (traceLevel)
			{
			case global::System.Diagnostics.TraceLevel.Error:
				return TraceEvent.TraceEventType.Error;
			case global::System.Diagnostics.TraceLevel.Warning:
				return TraceEvent.TraceEventType.Warn;
			case global::System.Diagnostics.TraceLevel.Info:
				return TraceEvent.TraceEventType.Info;
			case global::System.Diagnostics.TraceLevel.Verbose:
				return TraceEvent.TraceEventType.Debug;
			default:
				return TraceEvent.TraceEventType.Error;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000274C File Offset: 0x0000094C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "TraceEvent\n\tEventType: {0},\n", this.EventType);
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "\tClientDateTime: {0},\n", this.ClientDateTime ?? "(null)");
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "\tCategory: {0},\n", this.Category ?? "(null)");
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "\tHostApplication: {0},\n", this.HostApplication ?? "(null)");
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "\tMessage: {0},\n", this.Message ?? "(null)");
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "\tDetails: {0}\n", this.Details ?? "(null)");
			return stringBuilder.ToString();
		}

		// Token: 0x04000083 RID: 131
		internal const string TRACEEVENT = "TraceEvent";

		// Token: 0x04000084 RID: 132
		private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:sszzz";

		// Token: 0x02000432 RID: 1074
		public enum TraceEventType
		{
			// Token: 0x04000F01 RID: 3841
			Error,
			// Token: 0x04000F02 RID: 3842
			Warn,
			// Token: 0x04000F03 RID: 3843
			Perf,
			// Token: 0x04000F04 RID: 3844
			Info,
			// Token: 0x04000F05 RID: 3845
			Debug
		}
	}
}
