using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200000F RID: 15
	public class PBIWinExploreHostTrace : BaseTelemetryEvent
	{
		// Token: 0x0600003B RID: 59 RVA: 0x000024B5 File Offset: 0x000006B5
		public PBIWinExploreHostTrace(TraceType type, string message)
			: base("PBI.Win.ExploreHostTrace", TelemetryUse.Trace)
		{
			this.type = type;
			this.message = message;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000024D1 File Offset: 0x000006D1
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000024D9 File Offset: 0x000006D9
		public TraceType type { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000024E2 File Offset: 0x000006E2
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000024EA File Offset: 0x000006EA
		public string message { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000024F4 File Offset: 0x000006F4
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
