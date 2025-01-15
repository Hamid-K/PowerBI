using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000021 RID: 33
	public class PBIWinDataExtensionException : BaseTelemetryEvent
	{
		// Token: 0x0600009E RID: 158 RVA: 0x000035FC File Offset: 0x000017FC
		public PBIWinDataExtensionException(string ExceptionType, string DataExtensionErrorDetails, string stackTrace, string message)
			: base("PBI.Win.DataExtensionException", TelemetryUse.CriticalError)
		{
			this.ExceptionType = ExceptionType;
			this.DataExtensionErrorDetails = DataExtensionErrorDetails;
			this.stackTrace = stackTrace;
			this.message = message;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003627 File Offset: 0x00001827
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x0000362F File Offset: 0x0000182F
		public string ExceptionType { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003638 File Offset: 0x00001838
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00003640 File Offset: 0x00001840
		public string DataExtensionErrorDetails { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003649 File Offset: 0x00001849
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003651 File Offset: 0x00001851
		public string stackTrace { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000365A File Offset: 0x0000185A
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00003662 File Offset: 0x00001862
		public string message { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000366C File Offset: 0x0000186C
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string exceptionType = this.ExceptionType;
				string text = ((exceptionType == null) ? string.Empty : exceptionType.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("ExceptionType", text);
				string dataExtensionErrorDetails = this.DataExtensionErrorDetails;
				string text2 = ((dataExtensionErrorDetails == null) ? string.Empty : dataExtensionErrorDetails.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("DataExtensionErrorDetails", text2);
				string stackTrace = this.stackTrace;
				string text3 = ((stackTrace == null) ? string.Empty : stackTrace.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("stackTrace", text3);
				string message = this.message;
				string text4 = ((message == null) ? string.Empty : message.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("message", text4);
				return dictionary;
			}
		}
	}
}
