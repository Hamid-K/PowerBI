using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000017 RID: 23
	public class PBIWinDeriveInsightsCancellationException : BaseTelemetryEvent
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00002AEA File Offset: 0x00000CEA
		public PBIWinDeriveInsightsCancellationException(string stackTrace, string message)
			: base("PBI.Win.DeriveInsightsCancellationException", TelemetryUse.CriticalError)
		{
			this.stackTrace = stackTrace;
			this.message = message;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002B06 File Offset: 0x00000D06
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002B0E File Offset: 0x00000D0E
		public string stackTrace { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002B17 File Offset: 0x00000D17
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002B1F File Offset: 0x00000D1F
		public string message { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002B28 File Offset: 0x00000D28
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
