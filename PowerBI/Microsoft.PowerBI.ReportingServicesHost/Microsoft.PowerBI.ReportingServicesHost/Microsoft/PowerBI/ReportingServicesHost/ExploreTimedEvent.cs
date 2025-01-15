using System;
using System.Globalization;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000064 RID: 100
	public class ExploreTimedEvent : ExploreBaseEvent
	{
		// Token: 0x0600023C RID: 572 RVA: 0x00006484 File Offset: 0x00004684
		public ExploreTimedEvent(string name, bool isError, EventTarget eventTarget)
			: base(name)
		{
			this.isError = isError;
			this.UpdateProperties(eventTarget);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000649B File Offset: 0x0000469B
		// (set) Token: 0x0600023E RID: 574 RVA: 0x000064A3 File Offset: 0x000046A3
		public bool isError
		{
			get
			{
				return this.m_isError;
			}
			set
			{
				if (!value)
				{
					return;
				}
				this.m_isError = value;
				this.m_properties["isError"] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000064CC File Offset: 0x000046CC
		private void UpdateProperties(EventTarget eventTarget)
		{
			if (eventTarget != EventTarget.TelemetryAndLogs)
			{
				this.m_properties["EventTarget"] = eventTarget.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x04000159 RID: 345
		private bool m_isError;
	}
}
