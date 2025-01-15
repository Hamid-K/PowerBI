using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000013 RID: 19
	public class PBIWinExecuteSemanticQueryException : BaseTelemetryEvent
	{
		// Token: 0x06000055 RID: 85 RVA: 0x000027AE File Offset: 0x000009AE
		public PBIWinExecuteSemanticQueryException(string stackTrace, string message)
			: base("PBI.Win.ExecuteSemanticQueryException", TelemetryUse.CriticalError)
		{
			this.stackTrace = stackTrace;
			this.message = message;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000027CA File Offset: 0x000009CA
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000027D2 File Offset: 0x000009D2
		public string stackTrace { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000027DB File Offset: 0x000009DB
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000027E3 File Offset: 0x000009E3
		public string message { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000027EC File Offset: 0x000009EC
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
