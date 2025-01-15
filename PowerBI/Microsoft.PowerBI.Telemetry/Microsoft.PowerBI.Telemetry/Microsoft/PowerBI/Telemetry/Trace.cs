using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000031 RID: 49
	public class Trace : BaseTelemetryEvent
	{
		// Token: 0x0600012B RID: 299 RVA: 0x000047F8 File Offset: 0x000029F8
		public Trace(TraceType type, string message)
			: base("Trace", TelemetryUse.Trace)
		{
			this.type = type;
			this.message = message;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00004814 File Offset: 0x00002A14
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000481C File Offset: 0x00002A1C
		public TraceType type { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00004825 File Offset: 0x00002A25
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000482D File Offset: 0x00002A2D
		public string message { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00004838 File Offset: 0x00002A38
		public override Dictionary<string, string> Properties
		{
			get
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				TraceType type = this.type;
				string text = ((type == null) ? string.Empty : type.ToString());
				dictionary.Add("type", text);
				string message = this.message;
				string text2 = ((message == null) ? string.Empty : message.ToString(CultureInfo.InvariantCulture));
				dictionary.Add("message", text2);
				return dictionary;
			}
		}
	}
}
