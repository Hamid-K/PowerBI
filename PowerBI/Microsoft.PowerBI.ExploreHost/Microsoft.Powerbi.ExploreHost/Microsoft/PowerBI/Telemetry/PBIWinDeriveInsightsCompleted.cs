using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000015 RID: 21
	public class PBIWinDeriveInsightsCompleted : BaseTelemetryEvent
	{
		// Token: 0x06000061 RID: 97 RVA: 0x000028FE File Offset: 0x00000AFE
		public PBIWinDeriveInsightsCompleted(string analysis, string errorCode, string errorSource, bool isDQModel)
			: base("PBI.Win.DeriveInsightsCompleted", TelemetryUse.Verbose)
		{
			this.analysis = analysis;
			this.errorCode = errorCode;
			this.errorSource = errorSource;
			this.isDQModel = isDQModel;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002929 File Offset: 0x00000B29
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002931 File Offset: 0x00000B31
		public string analysis { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000293A File Offset: 0x00000B3A
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002942 File Offset: 0x00000B42
		public string errorCode { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000294B File Offset: 0x00000B4B
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002953 File Offset: 0x00000B53
		public string errorSource { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000295C File Offset: 0x00000B5C
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00002964 File Offset: 0x00000B64
		public bool isDQModel { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002970 File Offset: 0x00000B70
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string analysis = this.analysis;
				string text = ((analysis == null) ? string.Empty : analysis.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("analysis", text);
				string errorCode = this.errorCode;
				string text2 = ((errorCode == null) ? string.Empty : errorCode.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("errorCode", text2);
				string errorSource = this.errorSource;
				string text3 = ((errorSource == null) ? string.Empty : errorSource.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("errorSource", text3);
				bool isDQModel = this.isDQModel;
				string text4 = ((isDQModel == null) ? string.Empty : isDQModel.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("isDQModel", text4);
				return dictionary;
			}
		}
	}
}
