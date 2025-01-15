using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000016 RID: 22
	public class PBIWinDeriveInsightException : BaseTelemetryEvent
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00002A41 File Offset: 0x00000C41
		public PBIWinDeriveInsightException(string stackTrace, string message)
			: base("PBI.Win.DeriveInsightException", TelemetryUse.CriticalError)
		{
			this.stackTrace = stackTrace;
			this.message = message;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002A5D File Offset: 0x00000C5D
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00002A65 File Offset: 0x00000C65
		public string stackTrace { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002A6E File Offset: 0x00000C6E
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002A76 File Offset: 0x00000C76
		public string message { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002A80 File Offset: 0x00000C80
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string stackTrace = this.stackTrace;
				string text = ((stackTrace == null) ? string.Empty : stackTrace.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("stackTrace", text);
				string message = this.message;
				string text2 = ((message == null) ? string.Empty : message.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("message", text2);
				return dictionary;
			}
		}
	}
}
