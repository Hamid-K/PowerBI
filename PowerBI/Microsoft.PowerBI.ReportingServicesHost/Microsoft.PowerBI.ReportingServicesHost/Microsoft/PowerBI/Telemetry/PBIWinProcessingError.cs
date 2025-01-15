using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001D RID: 29
	public class PBIWinProcessingError : BaseTelemetryEvent
	{
		// Token: 0x06000082 RID: 130 RVA: 0x000032BE File Offset: 0x000014BE
		public PBIWinProcessingError(string ExceptionType, string stackTrace, string message)
			: base("PBI.Win.ProcessingError", TelemetryUse.CriticalError)
		{
			this.ExceptionType = ExceptionType;
			this.stackTrace = stackTrace;
			this.message = message;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000032E1 File Offset: 0x000014E1
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000032E9 File Offset: 0x000014E9
		public string ExceptionType { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000032F2 File Offset: 0x000014F2
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000032FA File Offset: 0x000014FA
		public string stackTrace { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003303 File Offset: 0x00001503
		// (set) Token: 0x06000088 RID: 136 RVA: 0x0000330B File Offset: 0x0000150B
		public string message { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003314 File Offset: 0x00001514
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string exceptionType = this.ExceptionType;
				string text = ((exceptionType == null) ? string.Empty : exceptionType.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("ExceptionType", text);
				string stackTrace = this.stackTrace;
				string text2 = ((stackTrace == null) ? string.Empty : stackTrace.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("stackTrace", text2);
				string message = this.message;
				string text3 = ((message == null) ? string.Empty : message.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("message", text3);
				return dictionary;
			}
		}
	}
}
