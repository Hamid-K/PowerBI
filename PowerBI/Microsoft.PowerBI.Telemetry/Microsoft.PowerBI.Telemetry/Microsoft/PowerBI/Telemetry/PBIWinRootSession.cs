using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200002F RID: 47
	public class PBIWinRootSession : BaseTelemetryEvent
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00004749 File Offset: 0x00002949
		public PBIWinRootSession(string parentId, bool isError)
			: base("PBI.Win.RootSession", TelemetryUse.CustomerAction)
		{
			this.parentId = parentId;
			this.isError = isError;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00004765 File Offset: 0x00002965
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000476D File Offset: 0x0000296D
		public string parentId { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00004776 File Offset: 0x00002976
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000477E File Offset: 0x0000297E
		public bool isError { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00004788 File Offset: 0x00002988
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
