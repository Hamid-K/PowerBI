using System;
using System.Globalization;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000037 RID: 55
	internal sealed class DataShapingEvent : DataShapingBaseEvent
	{
		// Token: 0x06000124 RID: 292 RVA: 0x000045CC File Offset: 0x000027CC
		public DataShapingEvent(string name, string requestId, string message, EventTarget eventTarget)
			: base(name, requestId, eventTarget)
		{
			this.Message = message;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000045DF File Offset: 0x000027DF
		// (set) Token: 0x06000126 RID: 294 RVA: 0x000045E7 File Offset: 0x000027E7
		public string Message
		{
			get
			{
				return this.m_message;
			}
			set
			{
				this.m_message = value;
				this.m_properties["message"] = ((value != null) ? value.ToString(CultureInfo.InvariantCulture) : null);
			}
		}

		// Token: 0x040000E9 RID: 233
		private string m_message;
	}
}
