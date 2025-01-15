using System;
using System.Collections.Generic;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CCA RID: 7370
	public sealed class FirewallPlanCreator : IFirewallPlanCreator
	{
		// Token: 0x0600B7AF RID: 47023 RVA: 0x002544B8 File Offset: 0x002526B8
		public IFirewallPlan CreatePlanForPartitions(IPartitionedDocument document, IEnumerable<IPartitionKey> partitionKeys)
		{
			FirewallPlanBuilder firewallPlanBuilder = new FirewallPlanBuilder(document);
			foreach (IPartitionKey partitionKey in partitionKeys)
			{
				firewallPlanBuilder.AddPartition(partitionKey);
			}
			return firewallPlanBuilder.ToPlan();
		}

		// Token: 0x0600B7B0 RID: 47024 RVA: 0x00254510 File Offset: 0x00252710
		public IFirewallPlan CreatePlan(IEnumerable<IFirewallPartitionPlan> partitionPlans)
		{
			FirewallPlan firewallPlan = new FirewallPlan();
			foreach (IFirewallPartitionPlan firewallPartitionPlan in partitionPlans)
			{
				FirewallPartitionPlan firewallPartitionPlan2 = (FirewallPartitionPlan)firewallPartitionPlan;
				firewallPlan.AddPartitionPlan(firewallPartitionPlan2);
			}
			return firewallPlan;
		}

		// Token: 0x0600B7B1 RID: 47025 RVA: 0x00254564 File Offset: 0x00252764
		public IFirewallPartitionPlan CreatePartitionPlan(IPartitionKey partitionKey, int evaluationOrder, bool isCyclic, IEnumerable<IPartitionKey> inputs)
		{
			return new FirewallPartitionPlan(partitionKey, evaluationOrder, inputs)
			{
				IsCyclic = isCyclic
			};
		}
	}
}
