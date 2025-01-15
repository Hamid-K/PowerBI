using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation
{
	// Token: 0x0200006C RID: 108
	public sealed class InternalContext
	{
		// Token: 0x06000350 RID: 848 RVA: 0x0000F3E4 File Offset: 0x0000D5E4
		internal InternalContext()
		{
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000F403 File Offset: 0x0000D603
		public string SdkVersion
		{
			get
			{
				if (!string.IsNullOrEmpty(this.sdkVersion))
				{
					return this.sdkVersion;
				}
				return null;
			}
			set
			{
				this.sdkVersion = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000F40C File Offset: 0x0000D60C
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000F423 File Offset: 0x0000D623
		public string AgentVersion
		{
			get
			{
				if (!string.IsNullOrEmpty(this.agentVersion))
				{
					return this.agentVersion;
				}
				return null;
			}
			set
			{
				this.agentVersion = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000F42C File Offset: 0x0000D62C
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000F443 File Offset: 0x0000D643
		public string NodeName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.nodeName))
				{
					return this.nodeName;
				}
				return null;
			}
			set
			{
				this.nodeName = value;
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000F44C File Offset: 0x0000D64C
		internal void UpdateTags(IDictionary<string, string> tags)
		{
			tags.UpdateTagValue(ContextTagKeys.Keys.InternalSdkVersion, this.SdkVersion);
			tags.UpdateTagValue(ContextTagKeys.Keys.InternalAgentVersion, this.AgentVersion);
			tags.UpdateTagValue(ContextTagKeys.Keys.InternalNodeName, this.NodeName);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000F49B File Offset: 0x0000D69B
		internal void CopyTo(InternalContext target)
		{
			Tags.CopyTagValue(this.SdkVersion, ref target.sdkVersion);
			Tags.CopyTagValue(this.AgentVersion, ref target.agentVersion);
			Tags.CopyTagValue(this.NodeName, ref target.nodeName);
		}

		// Token: 0x04000168 RID: 360
		private string sdkVersion;

		// Token: 0x04000169 RID: 361
		private string agentVersion;

		// Token: 0x0400016A RID: 362
		private string nodeName;
	}
}
