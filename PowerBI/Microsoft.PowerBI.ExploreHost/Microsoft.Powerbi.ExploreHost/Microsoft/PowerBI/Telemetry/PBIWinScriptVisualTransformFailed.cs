using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000023 RID: 35
	public class PBIWinScriptVisualTransformFailed : BaseTelemetryEvent
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x000039BF File Offset: 0x00001BBF
		public PBIWinScriptVisualTransformFailed(string ExceptionType, string message)
			: base("PBI.Win.ScriptVisualTransformFailed", TelemetryUse.CriticalError)
		{
			this.ExceptionType = ExceptionType;
			this.message = message;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000039DB File Offset: 0x00001BDB
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x000039E3 File Offset: 0x00001BE3
		public string ExceptionType { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000039EC File Offset: 0x00001BEC
		// (set) Token: 0x060000EB RID: 235 RVA: 0x000039F4 File Offset: 0x00001BF4
		public string message { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003A00 File Offset: 0x00001C00
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string exceptionType = this.ExceptionType;
				string text = ((exceptionType == null) ? string.Empty : exceptionType.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("ExceptionType", text);
				string message = this.message;
				string text2 = ((message == null) ? string.Empty : message.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("message", text2);
				return dictionary;
			}
		}
	}
}
