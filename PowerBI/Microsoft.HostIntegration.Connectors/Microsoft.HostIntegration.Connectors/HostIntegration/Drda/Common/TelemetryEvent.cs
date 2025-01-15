using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200083C RID: 2108
	public class TelemetryEvent : ITelemetryEvent
	{
		// Token: 0x06004304 RID: 17156 RVA: 0x000E0B0A File Offset: 0x000DED0A
		public TelemetryEvent(string serverClassName, string serverProductReleaseLevel, string dataProviderName, string clientProcessName)
		{
			this.serverClassName = serverClassName;
			this.serverProductReleaseLevel = serverProductReleaseLevel;
			this.dataProviderName = dataProviderName;
			this.clientProcessName = clientProcessName;
		}

		// Token: 0x06004305 RID: 17157 RVA: 0x00002061 File Offset: 0x00000261
		public TelemetryEvent()
		{
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06004306 RID: 17158 RVA: 0x000E0B2F File Offset: 0x000DED2F
		public virtual string EventName
		{
			get
			{
				return "DrdaEvent";
			}
		}

		// Token: 0x06004307 RID: 17159 RVA: 0x000E0B38 File Offset: 0x000DED38
		public virtual void SetEventProperties(IDictionary<string, string> properties)
		{
			properties.Add("ServerClassName", string.IsNullOrEmpty(this.serverClassName) ? "null" : this.serverClassName);
			properties.Add("ServerProductReleaseLevel", string.IsNullOrEmpty(this.serverProductReleaseLevel) ? "null" : this.serverProductReleaseLevel);
			properties.Add("DataProviderName", string.IsNullOrEmpty(this.dataProviderName) ? "null" : this.dataProviderName);
			properties.Add("ClientProcessName", string.IsNullOrEmpty(this.clientProcessName) ? "null" : this.clientProcessName);
		}

		// Token: 0x04002F51 RID: 12113
		internal string serverClassName;

		// Token: 0x04002F52 RID: 12114
		internal string serverProductReleaseLevel;

		// Token: 0x04002F53 RID: 12115
		internal string dataProviderName;

		// Token: 0x04002F54 RID: 12116
		internal string clientProcessName;
	}
}
