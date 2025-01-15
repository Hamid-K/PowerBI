using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001A RID: 26
	public class PBIWinLoadReport : BaseTelemetryEvent
	{
		// Token: 0x0600006E RID: 110 RVA: 0x0000306C File Offset: 0x0000126C
		public PBIWinLoadReport(string parentId, bool isError)
			: base("PBI.Win.LoadReport", TelemetryUse.CustomerAction)
		{
			this.parentId = parentId;
			this.isError = isError;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003088 File Offset: 0x00001288
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003090 File Offset: 0x00001290
		public string parentId { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003099 File Offset: 0x00001299
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000030A1 File Offset: 0x000012A1
		public bool isError { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000030AC File Offset: 0x000012AC
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string parentId = this.parentId;
				string text = ((parentId == null) ? string.Empty : parentId.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("parentId", text);
				bool isError = this.isError;
				string text2 = ((isError == null) ? string.Empty : isError.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("isError", text2);
				return dictionary;
			}
		}
	}
}
