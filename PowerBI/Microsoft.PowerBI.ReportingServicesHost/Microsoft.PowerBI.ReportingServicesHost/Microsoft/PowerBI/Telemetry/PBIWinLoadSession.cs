using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001B RID: 27
	public class PBIWinLoadSession : BaseTelemetryEvent
	{
		// Token: 0x06000074 RID: 116 RVA: 0x0000311C File Offset: 0x0000131C
		public PBIWinLoadSession(string Message, string parentId, bool isError)
			: base("PBI.Win.LoadSession", TelemetryUse.CustomerAction)
		{
			this.Message = Message;
			this.parentId = parentId;
			this.isError = isError;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000313F File Offset: 0x0000133F
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003147 File Offset: 0x00001347
		public string Message { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003150 File Offset: 0x00001350
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003158 File Offset: 0x00001358
		public string parentId { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003161 File Offset: 0x00001361
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003169 File Offset: 0x00001369
		public bool isError { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003174 File Offset: 0x00001374
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string message = this.Message;
				string text = ((message == null) ? string.Empty : message.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("Message", text);
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
