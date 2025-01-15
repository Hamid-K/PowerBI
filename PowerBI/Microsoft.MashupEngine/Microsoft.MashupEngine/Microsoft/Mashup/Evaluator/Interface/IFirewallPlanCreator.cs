using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E07 RID: 7687
	public interface IFirewallPlanCreator
	{
		// Token: 0x0600BDC9 RID: 48585
		IFirewallPlan CreatePlanForPartitions(IPartitionedDocument document, IEnumerable<IPartitionKey> partitionKeys);

		// Token: 0x0600BDCA RID: 48586
		IFirewallPlan CreatePlan(IEnumerable<IFirewallPartitionPlan> partitionPlans);

		// Token: 0x0600BDCB RID: 48587
		IFirewallPartitionPlan CreatePartitionPlan(IPartitionKey partitionKey, int evaluationOrder, bool isCyclic, IEnumerable<IPartitionKey> inputs);
	}
}
