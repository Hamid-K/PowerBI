using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200002C RID: 44
	internal class DataTransformTimedEvent : BaseTelemetryEvent
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00003CB6 File Offset: 0x00001EB6
		public DataTransformTimedEvent(string name, bool isError)
			: base("PBI.DT." + name, TelemetryUse.Verbose)
		{
			this.m_properties = new Dictionary<string, string>();
			this.IsError = isError;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003CDC File Offset: 0x00001EDC
		public DataTransformTimedEvent(string name, string message)
			: base("PBI.DT." + name, TelemetryUse.Verbose)
		{
			this.m_properties = new Dictionary<string, string>();
			this.Message = message;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003D02 File Offset: 0x00001F02
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003D0A File Offset: 0x00001F0A
		public bool IsError
		{
			get
			{
				return this.m_isError;
			}
			set
			{
				this.m_isError = value;
				this.m_properties["isError"] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003D2F File Offset: 0x00001F2F
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00003D37 File Offset: 0x00001F37
		public string Message
		{
			get
			{
				return this.m_message;
			}
			set
			{
				this.m_message = value;
				this.m_properties["message"] = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003D51 File Offset: 0x00001F51
		public override Dictionary<string, string> Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x040000C9 RID: 201
		private const string NamePrefix = "PBI.DT.";

		// Token: 0x040000CA RID: 202
		private readonly Dictionary<string, string> m_properties;

		// Token: 0x040000CB RID: 203
		private bool m_isError;

		// Token: 0x040000CC RID: 204
		private string m_message;
	}
}
