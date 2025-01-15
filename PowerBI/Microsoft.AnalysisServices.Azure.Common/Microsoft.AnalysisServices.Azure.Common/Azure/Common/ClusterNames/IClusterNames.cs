using System;
using Microsoft.Cloud.Platform.Azure.Dns;

namespace Microsoft.AnalysisServices.Azure.Common.ClusterNames
{
	// Token: 0x0200015B RID: 347
	public interface IClusterNames
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06001216 RID: 4630
		DnsName DeploymentClusterName { get; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06001217 RID: 4631
		DnsName RedirectClusterName { get; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06001218 RID: 4632
		DnsName CurrentClusterName { get; }
	}
}
