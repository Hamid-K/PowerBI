using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000033 RID: 51
	internal abstract class DataShapingBaseEvent : BaseTelemetryEvent
	{
		// Token: 0x06000109 RID: 265 RVA: 0x000043DF File Offset: 0x000025DF
		public DataShapingBaseEvent(string name, string requestId, EventTarget eventTarget)
			: base("PBI.DS." + name, TelemetryUse.Verbose)
		{
			this.m_properties = new Dictionary<string, string>(StringComparer.Ordinal);
			this.UpdateProperties(eventTarget, requestId);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000440B File Offset: 0x0000260B
		public override Dictionary<string, string> Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004413 File Offset: 0x00002613
		private void UpdateProperties(EventTarget eventTarget, string requestId)
		{
			if (eventTarget != EventTarget.TelemetryAndLogs)
			{
				this.m_properties["EventTarget"] = eventTarget.ToString(CultureInfo.InvariantCulture);
			}
			if (requestId != null)
			{
				this.m_properties["RequestId"] = requestId;
			}
		}

		// Token: 0x040000E3 RID: 227
		private const string NamePrefix = "PBI.DS.";

		// Token: 0x040000E4 RID: 228
		protected readonly Dictionary<string, string> m_properties;
	}
}
