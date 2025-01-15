using System;
using System.Globalization;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x0200003A RID: 58
	internal class DataShapingTimedEvent : DataShapingBaseEvent
	{
		// Token: 0x06000138 RID: 312 RVA: 0x0000499B File Offset: 0x00002B9B
		public DataShapingTimedEvent(string name, string requestId, bool isError, EventTarget eventTarget)
			: base(name, requestId, eventTarget)
		{
			this.isError = isError;
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000139 RID: 313 RVA: 0x000049AE File Offset: 0x00002BAE
		// (set) Token: 0x0600013A RID: 314 RVA: 0x000049B6 File Offset: 0x00002BB6
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

		// Token: 0x040000F4 RID: 244
		private bool m_isError;
	}
}
