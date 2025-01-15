using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000017 RID: 23
	public class PBIWinReportingServicesHostTrace : BaseTelemetryEvent
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00002E13 File Offset: 0x00001013
		public PBIWinReportingServicesHostTrace(TraceType type, string message)
			: base("PBI.Win.ReportingServicesHostTrace", TelemetryUse.Trace)
		{
			this.type = type;
			this.message = message;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002E2F File Offset: 0x0000102F
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002E37 File Offset: 0x00001037
		public TraceType type { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002E40 File Offset: 0x00001040
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002E48 File Offset: 0x00001048
		public string message { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002E54 File Offset: 0x00001054
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				TraceType type = this.type;
				string text = ((type == null) ? string.Empty : type.ToString());
				dictionary.Add("type", text);
				string message = this.message;
				string text2 = ((message == null) ? string.Empty : message.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("message", text2);
				return dictionary;
			}
		}
	}
}
