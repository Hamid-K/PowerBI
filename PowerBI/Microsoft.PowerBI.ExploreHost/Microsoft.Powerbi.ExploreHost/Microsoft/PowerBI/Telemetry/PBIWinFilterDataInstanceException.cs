using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000020 RID: 32
	public class PBIWinFilterDataInstanceException : BaseTelemetryEvent
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00003547 File Offset: 0x00001747
		public PBIWinFilterDataInstanceException(string options, string stackTrace, string message)
			: base("PBI.Win.FilterDataInstanceException", TelemetryUse.CriticalError)
		{
			this.options = options;
			this.stackTrace = stackTrace;
			this.message = message;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000356A File Offset: 0x0000176A
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003572 File Offset: 0x00001772
		public string options { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000357B File Offset: 0x0000177B
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00003583 File Offset: 0x00001783
		public string stackTrace { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000358C File Offset: 0x0000178C
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003594 File Offset: 0x00001794
		public string message { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000035A0 File Offset: 0x000017A0
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string options = this.options;
				string text = ((options == null) ? string.Empty : options.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("options", text);
				string stackTrace = this.stackTrace;
				string text2 = ((stackTrace == null) ? string.Empty : stackTrace.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("stackTrace", text2);
				string message = this.message;
				string text3 = ((message == null) ? string.Empty : message.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("message", text3);
				return dictionary;
			}
		}
	}
}
