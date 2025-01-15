using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001E RID: 30
	public class PBIWinDataShapingException : BaseTelemetryEvent
	{
		// Token: 0x0600008A RID: 138 RVA: 0x000033AF File Offset: 0x000015AF
		public PBIWinDataShapingException(string ComponentName, string stackTrace, string message)
			: base("PBI.Win.DataShapingException", TelemetryUse.CriticalError)
		{
			this.ComponentName = ComponentName;
			this.stackTrace = stackTrace;
			this.message = message;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000033D2 File Offset: 0x000015D2
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000033DA File Offset: 0x000015DA
		public string ComponentName { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000033E3 File Offset: 0x000015E3
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000033EB File Offset: 0x000015EB
		public string stackTrace { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000033F4 File Offset: 0x000015F4
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000033FC File Offset: 0x000015FC
		public string message { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003408 File Offset: 0x00001608
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string componentName = this.ComponentName;
				string text = ((componentName == null) ? string.Empty : componentName.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("ComponentName", text);
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
