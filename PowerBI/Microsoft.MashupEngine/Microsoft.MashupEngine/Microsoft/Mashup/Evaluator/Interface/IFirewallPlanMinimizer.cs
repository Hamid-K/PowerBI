using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E08 RID: 7688
	public interface IFirewallPlanMinimizer
	{
		// Token: 0x0600BDCC RID: 48588
		IFirewallPlan GroupPlanForPartitions(IFirewallPlan firewallPlan, IPartitionedDocument document, IEnumerable<IPartitionKey> partitionKeys);

		// Token: 0x0600BDCD RID: 48589
		IFirewallPlan TrimPlanForPartition(IFirewallPlan firewallPlan, IPartitionedDocument document, IPartitionKey partitionKey);
	}
}
