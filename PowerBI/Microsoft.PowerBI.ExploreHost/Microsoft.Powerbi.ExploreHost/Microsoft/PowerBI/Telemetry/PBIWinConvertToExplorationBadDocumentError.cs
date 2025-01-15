using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000011 RID: 17
	public class PBIWinConvertToExplorationBadDocumentError : BaseTelemetryEvent
	{
		// Token: 0x06000049 RID: 73 RVA: 0x0000265C File Offset: 0x0000085C
		public PBIWinConvertToExplorationBadDocumentError(string stackTrace, string message)
			: base("PBI.Win.ConvertToExplorationBadDocumentError", TelemetryUse.CriticalError)
		{
			this.stackTrace = stackTrace;
			this.message = message;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002678 File Offset: 0x00000878
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002680 File Offset: 0x00000880
		public string stackTrace { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002689 File Offset: 0x00000889
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002691 File Offset: 0x00000891
		public string message { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000269C File Offset: 0x0000089C
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
