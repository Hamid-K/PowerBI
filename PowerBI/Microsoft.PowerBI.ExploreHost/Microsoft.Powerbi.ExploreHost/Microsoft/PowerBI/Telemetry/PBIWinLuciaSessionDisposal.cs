using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001F RID: 31
	public class PBIWinLuciaSessionDisposal : BaseTelemetryEvent
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00003449 File Offset: 0x00001649
		public PBIWinLuciaSessionDisposal(int dataIndexDiposingTaskCount, long elapsedMilliseconds, string exceptionTrace)
			: base("PBI.Win.LuciaSessionDisposal", TelemetryUse.Verbose)
		{
			this.dataIndexDiposingTaskCount = dataIndexDiposingTaskCount;
			this.elapsedMilliseconds = elapsedMilliseconds;
			this.exceptionTrace = exceptionTrace;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000346C File Offset: 0x0000166C
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003474 File Offset: 0x00001674
		public int dataIndexDiposingTaskCount { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000347D File Offset: 0x0000167D
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003485 File Offset: 0x00001685
		public long elapsedMilliseconds { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000348E File Offset: 0x0000168E
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003496 File Offset: 0x00001696
		public string exceptionTrace { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000034A0 File Offset: 0x000016A0
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				int dataIndexDiposingTaskCount = this.dataIndexDiposingTaskCount;
				string text = ((dataIndexDiposingTaskCount == null) ? string.Empty : dataIndexDiposingTaskCount.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("dataIndexDiposingTaskCount", text);
				long elapsedMilliseconds = this.elapsedMilliseconds;
				string text2 = ((elapsedMilliseconds == null) ? string.Empty : elapsedMilliseconds.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("elapsedMilliseconds", text2);
				string exceptionTrace = this.exceptionTrace;
				string text3 = ((exceptionTrace == null) ? string.Empty : exceptionTrace.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("exceptionTrace", text3);
				return dictionary;
			}
		}
	}
}
