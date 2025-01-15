using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001E RID: 30
	public class PBIWinDataIndexBuilderStatistics : BaseTelemetryEvent
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000032C4 File Offset: 0x000014C4
		public PBIWinDataIndexBuilderStatistics(string status, string statistics, string warnings, string exceptionTrace, string options)
			: base("PBI.Win.DataIndexBuilderStatistics", TelemetryUse.Verbose)
		{
			this.status = status;
			this.statistics = statistics;
			this.warnings = warnings;
			this.exceptionTrace = exceptionTrace;
			this.options = options;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000032F7 File Offset: 0x000014F7
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000032FF File Offset: 0x000014FF
		public string status { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003308 File Offset: 0x00001508
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003310 File Offset: 0x00001510
		public string statistics { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003319 File Offset: 0x00001519
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003321 File Offset: 0x00001521
		public string warnings { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000332A File Offset: 0x0000152A
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003332 File Offset: 0x00001532
		public string exceptionTrace { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000333B File Offset: 0x0000153B
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003343 File Offset: 0x00001543
		public string options { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000334C File Offset: 0x0000154C
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string status = this.status;
				string text = ((status == null) ? string.Empty : status.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("status", text);
				string statistics = this.statistics;
				string text2 = ((statistics == null) ? string.Empty : statistics.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("statistics", text2);
				string warnings = this.warnings;
				string text3 = ((warnings == null) ? string.Empty : warnings.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("warnings", text3);
				string exceptionTrace = this.exceptionTrace;
				string text4 = ((exceptionTrace == null) ? string.Empty : exceptionTrace.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("exceptionTrace", text4);
				string options = this.options;
				string text5 = ((options == null) ? string.Empty : options.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("options", text5);
				return dictionary;
			}
		}
	}
}
