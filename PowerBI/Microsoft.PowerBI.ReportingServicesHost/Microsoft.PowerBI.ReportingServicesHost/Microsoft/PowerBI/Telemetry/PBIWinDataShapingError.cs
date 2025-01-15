using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001F RID: 31
	public class PBIWinDataShapingError : BaseTelemetryEvent
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000034A3 File Offset: 0x000016A3
		public PBIWinDataShapingError(string ComponentName, string message)
			: base("PBI.Win.DataShapingError", TelemetryUse.CriticalError)
		{
			this.ComponentName = ComponentName;
			this.message = message;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000034BF File Offset: 0x000016BF
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000034C7 File Offset: 0x000016C7
		public string ComponentName { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000034D0 File Offset: 0x000016D0
		// (set) Token: 0x06000096 RID: 150 RVA: 0x000034D8 File Offset: 0x000016D8
		public string message { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000034E4 File Offset: 0x000016E4
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string componentName = this.ComponentName;
				string text = ((componentName == null) ? string.Empty : componentName.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("ComponentName", text);
				string message = this.message;
				string text2 = ((message == null) ? string.Empty : message.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("message", text2);
				return dictionary;
			}
		}
	}
}
