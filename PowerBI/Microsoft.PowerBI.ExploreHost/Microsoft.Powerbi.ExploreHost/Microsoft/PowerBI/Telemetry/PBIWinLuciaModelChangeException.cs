using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001C RID: 28
	public class PBIWinLuciaModelChangeException : BaseTelemetryEvent
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00002F5F File Offset: 0x0000115F
		public PBIWinLuciaModelChangeException(string options, string stackTrace, string message)
			: base("PBI.Win.LuciaModelChangeException", TelemetryUse.CriticalError)
		{
			this.options = options;
			this.stackTrace = stackTrace;
			this.message = message;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00002F82 File Offset: 0x00001182
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00002F8A File Offset: 0x0000118A
		public string options { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002F93 File Offset: 0x00001193
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00002F9B File Offset: 0x0000119B
		public string stackTrace { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002FA4 File Offset: 0x000011A4
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00002FAC File Offset: 0x000011AC
		public string message { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002FB8 File Offset: 0x000011B8
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
