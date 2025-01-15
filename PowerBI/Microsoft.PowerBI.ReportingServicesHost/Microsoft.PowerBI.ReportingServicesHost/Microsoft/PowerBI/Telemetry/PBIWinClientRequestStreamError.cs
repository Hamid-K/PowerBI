using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001C RID: 28
	public class PBIWinClientRequestStreamError : BaseTelemetryEvent
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003214 File Offset: 0x00001414
		public PBIWinClientRequestStreamError(string stackTrace, string message)
			: base("PBI.Win.ClientRequestStreamError", TelemetryUse.CriticalError)
		{
			this.stackTrace = stackTrace;
			this.message = message;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003230 File Offset: 0x00001430
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00003238 File Offset: 0x00001438
		public string stackTrace { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003241 File Offset: 0x00001441
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003249 File Offset: 0x00001449
		public string message { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003254 File Offset: 0x00001454
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
