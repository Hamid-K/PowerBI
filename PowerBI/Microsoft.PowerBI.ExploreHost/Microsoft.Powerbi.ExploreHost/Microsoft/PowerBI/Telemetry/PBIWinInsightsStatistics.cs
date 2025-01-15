using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000018 RID: 24
	public class PBIWinInsightsStatistics : BaseTelemetryEvent
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00002B92 File Offset: 0x00000D92
		public PBIWinInsightsStatistics(string baseStatistics, string analysisStatistics, string insightsStatistics)
			: base("PBI.Win.InsightsStatistics", TelemetryUse.Verbose)
		{
			this.baseStatistics = baseStatistics;
			this.analysisStatistics = analysisStatistics;
			this.insightsStatistics = insightsStatistics;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002BB5 File Offset: 0x00000DB5
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002BBD File Offset: 0x00000DBD
		public string baseStatistics { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002BC6 File Offset: 0x00000DC6
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002BCE File Offset: 0x00000DCE
		public string analysisStatistics { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002BD7 File Offset: 0x00000DD7
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002BDF File Offset: 0x00000DDF
		public string insightsStatistics { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string baseStatistics = this.baseStatistics;
				string text = ((baseStatistics == null) ? string.Empty : baseStatistics.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("baseStatistics", text);
				string analysisStatistics = this.analysisStatistics;
				string text2 = ((analysisStatistics == null) ? string.Empty : analysisStatistics.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("analysisStatistics", text2);
				string insightsStatistics = this.insightsStatistics;
				string text3 = ((insightsStatistics == null) ? string.Empty : insightsStatistics.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("insightsStatistics", text3);
				return dictionary;
			}
		}
	}
}
