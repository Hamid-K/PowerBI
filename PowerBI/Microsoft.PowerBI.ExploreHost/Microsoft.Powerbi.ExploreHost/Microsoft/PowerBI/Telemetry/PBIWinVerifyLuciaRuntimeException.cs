using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001B RID: 27
	public class PBIWinVerifyLuciaRuntimeException : BaseTelemetryEvent
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00002E6B File Offset: 0x0000106B
		public PBIWinVerifyLuciaRuntimeException(string options, string stackTrace, string message)
			: base("PBI.Win.VerifyLuciaRuntimeException", TelemetryUse.CriticalError)
		{
			this.options = options;
			this.stackTrace = stackTrace;
			this.message = message;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002E8E File Offset: 0x0000108E
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00002E96 File Offset: 0x00001096
		public string options { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002E9F File Offset: 0x0000109F
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00002EA7 File Offset: 0x000010A7
		public string stackTrace { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00002EB0 File Offset: 0x000010B0
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00002EB8 File Offset: 0x000010B8
		public string message { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002EC4 File Offset: 0x000010C4
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
