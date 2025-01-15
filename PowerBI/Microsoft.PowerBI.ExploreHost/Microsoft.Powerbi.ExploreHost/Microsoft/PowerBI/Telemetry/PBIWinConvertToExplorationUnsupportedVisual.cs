using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000010 RID: 16
	public class PBIWinConvertToExplorationUnsupportedVisual : BaseTelemetryEvent
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002565 File Offset: 0x00000765
		public PBIWinConvertToExplorationUnsupportedVisual(string message, string parentId, bool isError)
			: base("PBI.Win.ConvertToExplorationUnsupportedVisual", TelemetryUse.CustomerAction)
		{
			this.message = message;
			this.parentId = parentId;
			this.isError = isError;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002588 File Offset: 0x00000788
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002590 File Offset: 0x00000790
		public string message { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002599 File Offset: 0x00000799
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000025A1 File Offset: 0x000007A1
		public string parentId { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000025AA File Offset: 0x000007AA
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000025B2 File Offset: 0x000007B2
		public bool isError { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000025BC File Offset: 0x000007BC
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string message = this.message;
				string text = ((message == null) ? string.Empty : message.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("message", text);
				string parentId = this.parentId;
				string text2 = ((parentId == null) ? string.Empty : parentId.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("parentId", text2);
				bool isError = this.isError;
				string text3 = ((isError == null) ? string.Empty : isError.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("isError", text3);
				return dictionary;
			}
		}
	}
}
