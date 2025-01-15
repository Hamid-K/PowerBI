using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001D RID: 29
	public class PBIWinDataIndexStoreStatistics : BaseTelemetryEvent
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00003054 File Offset: 0x00001254
		public PBIWinDataIndexStoreStatistics(string actionId, string status, string statistics, string warnings, string message, string exceptionTrace, string duration, string options)
			: base("PBI.Win.DataIndexStoreStatistics", TelemetryUse.Verbose)
		{
			this.actionId = actionId;
			this.status = status;
			this.statistics = statistics;
			this.warnings = warnings;
			this.message = message;
			this.exceptionTrace = exceptionTrace;
			this.duration = duration;
			this.options = options;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000030AA File Offset: 0x000012AA
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000030B2 File Offset: 0x000012B2
		public string actionId { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000030BB File Offset: 0x000012BB
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000030C3 File Offset: 0x000012C3
		public string status { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000030CC File Offset: 0x000012CC
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000030D4 File Offset: 0x000012D4
		public string statistics { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000030DD File Offset: 0x000012DD
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000030E5 File Offset: 0x000012E5
		public string warnings { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000030EE File Offset: 0x000012EE
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000030F6 File Offset: 0x000012F6
		public string message { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000030FF File Offset: 0x000012FF
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003107 File Offset: 0x00001307
		public string exceptionTrace { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003110 File Offset: 0x00001310
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00003118 File Offset: 0x00001318
		public string duration { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003121 File Offset: 0x00001321
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00003129 File Offset: 0x00001329
		public string options { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003134 File Offset: 0x00001334
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
				string statistics = this.statistics;
				string text3 = ((statistics == null) ? string.Empty : statistics.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("statistics", text3);
				string warnings = this.warnings;
				string text4 = ((warnings == null) ? string.Empty : warnings.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("warnings", text4);
				string message = this.message;
				string text5 = ((message == null) ? string.Empty : message.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("message", text5);
				string exceptionTrace = this.exceptionTrace;
				string text6 = ((exceptionTrace == null) ? string.Empty : exceptionTrace.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("exceptionTrace", text6);
				string duration = this.duration;
				string text7 = ((duration == null) ? string.Empty : duration.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("duration", text7);
				string options = this.options;
				string text8 = ((options == null) ? string.Empty : options.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("options", text8);
				return dictionary;
			}
		}
	}
}
