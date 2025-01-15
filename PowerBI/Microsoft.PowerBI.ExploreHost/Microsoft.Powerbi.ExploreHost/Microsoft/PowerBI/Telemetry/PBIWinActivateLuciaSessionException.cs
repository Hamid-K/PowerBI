using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001A RID: 26
	public class PBIWinActivateLuciaSessionException : BaseTelemetryEvent
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00002D77 File Offset: 0x00000F77
		public PBIWinActivateLuciaSessionException(string options, string stackTrace, string message)
			: base("PBI.Win.ActivateLuciaSessionException", TelemetryUse.CriticalError)
		{
			this.options = options;
			this.stackTrace = stackTrace;
			this.message = message;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00002D9A File Offset: 0x00000F9A
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00002DA2 File Offset: 0x00000FA2
		public string options { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002DAB File Offset: 0x00000FAB
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002DB3 File Offset: 0x00000FB3
		public string stackTrace { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002DBC File Offset: 0x00000FBC
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public string message { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002DD0 File Offset: 0x00000FD0
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
