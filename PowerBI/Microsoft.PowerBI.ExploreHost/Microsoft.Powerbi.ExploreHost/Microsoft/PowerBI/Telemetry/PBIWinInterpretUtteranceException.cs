using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000019 RID: 25
	public class PBIWinInterpretUtteranceException : BaseTelemetryEvent
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00002C83 File Offset: 0x00000E83
		public PBIWinInterpretUtteranceException(string options, string stackTrace, string message)
			: base("PBI.Win.InterpretUtteranceException", TelemetryUse.CriticalError)
		{
			this.options = options;
			this.stackTrace = stackTrace;
			this.message = message;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002CA6 File Offset: 0x00000EA6
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002CAE File Offset: 0x00000EAE
		public string options { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002CB7 File Offset: 0x00000EB7
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002CBF File Offset: 0x00000EBF
		public string stackTrace { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002CC8 File Offset: 0x00000EC8
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public string message { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002CDC File Offset: 0x00000EDC
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
