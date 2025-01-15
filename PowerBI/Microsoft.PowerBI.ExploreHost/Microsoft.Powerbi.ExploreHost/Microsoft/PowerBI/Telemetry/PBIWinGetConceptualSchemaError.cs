using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000012 RID: 18
	public class PBIWinGetConceptualSchemaError : BaseTelemetryEvent
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00002706 File Offset: 0x00000906
		public PBIWinGetConceptualSchemaError(string stackTrace, string message)
			: base("PBI.Win.GetConceptualSchemaError", TelemetryUse.CriticalError)
		{
			this.stackTrace = stackTrace;
			this.message = message;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002722 File Offset: 0x00000922
		// (set) Token: 0x06000051 RID: 81 RVA: 0x0000272A File Offset: 0x0000092A
		public string stackTrace { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002733 File Offset: 0x00000933
		// (set) Token: 0x06000053 RID: 83 RVA: 0x0000273B File Offset: 0x0000093B
		public string message { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002744 File Offset: 0x00000944
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
