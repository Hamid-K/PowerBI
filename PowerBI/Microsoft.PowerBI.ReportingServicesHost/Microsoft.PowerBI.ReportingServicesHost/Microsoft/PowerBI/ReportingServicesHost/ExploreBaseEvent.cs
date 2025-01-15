using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.PowerBI.Telemetry;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000061 RID: 97
	public abstract class ExploreBaseEvent : BaseTelemetryEvent
	{
		// Token: 0x06000231 RID: 561 RVA: 0x0000631F File Offset: 0x0000451F
		public ExploreBaseEvent(string name)
			: base("PBI.Explore." + name, TelemetryUse.Verbose)
		{
			this.m_properties = new Dictionary<string, string>(StringComparer.Ordinal);
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00006343 File Offset: 0x00004543
		public override Dictionary<string, string> Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000634B File Offset: 0x0000454B
		public void AddProperty(string propertyName, string value)
		{
			this.m_properties[propertyName] = value;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000635A File Offset: 0x0000455A
		public void AddProperty(string propertyName, int value)
		{
			this.m_properties.Add(propertyName, value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x04000152 RID: 338
		private const string NamePrefix = "PBI.Explore.";

		// Token: 0x04000153 RID: 339
		protected readonly Dictionary<string, string> m_properties;
	}
}
