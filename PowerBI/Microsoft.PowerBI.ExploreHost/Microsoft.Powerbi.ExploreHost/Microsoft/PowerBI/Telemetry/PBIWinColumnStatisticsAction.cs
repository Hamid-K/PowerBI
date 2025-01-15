using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000021 RID: 33
	public class PBIWinColumnStatisticsAction : BaseTelemetryEvent
	{
		// Token: 0x060000CD RID: 205 RVA: 0x0000363B File Offset: 0x0000183B
		public PBIWinColumnStatisticsAction(string actionId, string status, string exceptionTrace, string duration)
			: base("PBI.Win.ColumnStatisticsAction", TelemetryUse.Verbose)
		{
			this.actionId = actionId;
			this.status = status;
			this.exceptionTrace = exceptionTrace;
			this.duration = duration;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003666 File Offset: 0x00001866
		// (set) Token: 0x060000CF RID: 207 RVA: 0x0000366E File Offset: 0x0000186E
		public string actionId { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003677 File Offset: 0x00001877
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000367F File Offset: 0x0000187F
		public string status { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003688 File Offset: 0x00001888
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00003690 File Offset: 0x00001890
		public string exceptionTrace { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003699 File Offset: 0x00001899
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x000036A1 File Offset: 0x000018A1
		public string duration { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000036AC File Offset: 0x000018AC
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string actionId = this.actionId;
				string text = ((actionId == null) ? string.Empty : actionId.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("actionId", text);
				string status = this.status;
				string text2 = ((status == null) ? string.Empty : status.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("status", text2);
				string exceptionTrace = this.exceptionTrace;
				string text3 = ((exceptionTrace == null) ? string.Empty : exceptionTrace.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("exceptionTrace", text3);
				string duration = this.duration;
				string text4 = ((duration == null) ? string.Empty : duration.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("duration", text4);
				return dictionary;
			}
		}
	}
}
