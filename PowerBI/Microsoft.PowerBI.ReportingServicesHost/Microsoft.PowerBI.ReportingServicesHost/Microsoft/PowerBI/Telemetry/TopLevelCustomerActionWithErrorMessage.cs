using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000018 RID: 24
	public class TopLevelCustomerActionWithErrorMessage : BaseTelemetryEvent
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00002EC5 File Offset: 0x000010C5
		public TopLevelCustomerActionWithErrorMessage(string Message, string parentId, bool isError)
			: base("TopLevelCustomerActionWithErrorMessage", TelemetryUse.CustomerAction)
		{
			this.Message = Message;
			this.parentId = parentId;
			this.isError = isError;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002EE8 File Offset: 0x000010E8
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002EF0 File Offset: 0x000010F0
		public string Message { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002EF9 File Offset: 0x000010F9
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002F01 File Offset: 0x00001101
		public string parentId { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002F0A File Offset: 0x0000110A
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002F12 File Offset: 0x00001112
		public bool isError { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002F1C File Offset: 0x0000111C
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
