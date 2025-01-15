using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000020 RID: 32
	public class PBIWinDuplicateKeyAddedToModelCache : BaseTelemetryEvent
	{
		// Token: 0x06000098 RID: 152 RVA: 0x0000354E File Offset: 0x0000174E
		public PBIWinDuplicateKeyAddedToModelCache(string parentId, bool isError)
			: base("PBI.Win.DuplicateKeyAddedToModelCache", TelemetryUse.CustomerAction)
		{
			this.parentId = parentId;
			this.isError = isError;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000356A File Offset: 0x0000176A
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003572 File Offset: 0x00001772
		public string parentId { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000357B File Offset: 0x0000177B
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00003583 File Offset: 0x00001783
		public bool isError { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000358C File Offset: 0x0000178C
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
