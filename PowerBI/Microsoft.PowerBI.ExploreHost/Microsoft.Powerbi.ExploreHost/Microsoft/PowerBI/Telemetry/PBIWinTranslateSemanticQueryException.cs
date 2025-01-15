using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000014 RID: 20
	public class PBIWinTranslateSemanticQueryException : BaseTelemetryEvent
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002856 File Offset: 0x00000A56
		public PBIWinTranslateSemanticQueryException(string stackTrace, string message)
			: base("PBI.Win.TranslateSemanticQueryException", TelemetryUse.CriticalError)
		{
			this.stackTrace = stackTrace;
			this.message = message;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002872 File Offset: 0x00000A72
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000287A File Offset: 0x00000A7A
		public string stackTrace { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002883 File Offset: 0x00000A83
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000288B File Offset: 0x00000A8B
		public string message { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002894 File Offset: 0x00000A94
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
