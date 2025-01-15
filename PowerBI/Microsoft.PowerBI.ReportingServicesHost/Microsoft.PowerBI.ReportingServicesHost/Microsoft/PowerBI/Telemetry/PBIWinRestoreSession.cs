using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000019 RID: 25
	public class PBIWinRestoreSession : BaseTelemetryEvent
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00002FBC File Offset: 0x000011BC
		public PBIWinRestoreSession(string parentId, bool isError)
			: base("PBI.Win.RestoreSession", TelemetryUse.Verbose)
		{
			this.parentId = parentId;
			this.isError = isError;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002FD8 File Offset: 0x000011D8
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002FE0 File Offset: 0x000011E0
		public string parentId { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002FE9 File Offset: 0x000011E9
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002FF1 File Offset: 0x000011F1
		public bool isError { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002FFC File Offset: 0x000011FC
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
