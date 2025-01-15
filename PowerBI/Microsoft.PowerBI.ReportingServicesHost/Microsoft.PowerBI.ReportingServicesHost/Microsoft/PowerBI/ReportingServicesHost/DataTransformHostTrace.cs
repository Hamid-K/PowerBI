using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000029 RID: 41
	public class DataTransformHostTrace : BaseTelemetryEvent
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x00003A70 File Offset: 0x00001C70
		public DataTransformHostTrace(TraceType type, string message)
			: base("PBI.DT.HostTrace", TelemetryUse.Trace)
		{
			this.Type = type;
			this.Message = message;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003A8C File Offset: 0x00001C8C
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003A94 File Offset: 0x00001C94
		public TraceType Type { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003A9D File Offset: 0x00001C9D
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003AA5 File Offset: 0x00001CA5
		public string Message { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public override Dictionary<string, string> Properties
		{
			get
			{
				return new Dictionary<string, string>(2)
				{
					{
						"type",
						this.Type.ToString()
					},
					{
						"message",
						this.Message.ToString(CultureInfo.InvariantCulture)
					}
				};
			}
		}
	}
}
