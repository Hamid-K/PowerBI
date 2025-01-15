using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000028 RID: 40
	internal class DataTransformEvent : BaseTelemetryEvent
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00003A16 File Offset: 0x00001C16
		public DataTransformEvent(string name, string message)
			: base("PBI.DT." + name, TelemetryUse.Verbose)
		{
			this.m_properties = new Dictionary<string, string>();
			this.Message = message;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003A3C File Offset: 0x00001C3C
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00003A44 File Offset: 0x00001C44
		public string Message
		{
			get
			{
				return this.m_message;
			}
			private set
			{
				this.m_message = value;
				this.m_properties["message"] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003A68 File Offset: 0x00001C68
		public override Dictionary<string, string> Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x040000C3 RID: 195
		private const string NamePrefix = "PBI.DT.";

		// Token: 0x040000C4 RID: 196
		private readonly Dictionary<string, string> m_properties;

		// Token: 0x040000C5 RID: 197
		private string m_message;
	}
}
