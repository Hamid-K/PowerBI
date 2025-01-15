using System;
using System.Collections.Generic;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CC9 RID: 7369
	internal class FirewallPlanBuilder
	{
		// Token: 0x0600B7AB RID: 47019 RVA: 0x002542C7 File Offset: 0x002524C7
		public FirewallPlanBuilder(IPartitionedDocument document)
		{
			this.document = document;
			this.walked = new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance);
			this.beingWalked = new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance);
			this.partitionPlans = new Dictionary<IPartitionKey, IFirewallPartitionPlan>(PartitionKeyEqualityComparer.Instance);
		}

		// Token: 0x0600B7AC RID: 47020 RVA: 0x00254308 File Offset: 0x00252508
		public void AddPartition(IPartitionKey partitionKey)
		{
			int num = this.evaluationOrder;
			this.cycleFound = false;
			this.AddPartitionCore(partitionKey);
			if (this.cycleFound)
			{
				foreach (IFirewallPartitionPlan firewallPartitionPlan in this.partitionPlans.Values)
				{
					FirewallPartitionPlan firewallPartitionPlan2 = (FirewallPartitionPlan)firewallPartitionPlan;
					if (firewallPartitionPlan2.EvaluationOrder >= num)
					{
						firewallPartitionPlan2.IsCyclic = true;
					}
				}
			}
		}

		// Token: 0x0600B7AD RID: 47021 RVA: 0x0025438C File Offset: 0x0025258C
		public FirewallPlan ToPlan()
		{
			FirewallPlan firewallPlan = new FirewallPlan();
			foreach (IFirewallPartitionPlan firewallPartitionPlan in this.partitionPlans.Values)
			{
				FirewallPartitionPlan firewallPartitionPlan2 = (FirewallPartitionPlan)firewallPartitionPlan;
				firewallPlan.AddPartitionPlan(firewallPartitionPlan2);
			}
			return firewallPlan;
		}

		// Token: 0x0600B7AE RID: 47022 RVA: 0x002543F0 File Offset: 0x002525F0
		private void AddPartitionCore(IPartitionKey partitionKey)
		{
			if (this.walked.Add(partitionKey))
			{
				this.beingWalked.Add(partitionKey);
				foreach (IPartitionKey partitionKey2 in this.document.GetPartitionInputs(partitionKey))
				{
					if (this.beingWalked.Contains(partitionKey2))
					{
						this.cycleFound = true;
					}
					this.AddPartitionCore(partitionKey2);
				}
				FirewallPartitionPlan firewallPartitionPlan = new FirewallPartitionPlan(partitionKey, this.evaluationOrder, this.document.GetPartitionInputs(partitionKey));
				this.evaluationOrder++;
				this.partitionPlans.Add(partitionKey, firewallPartitionPlan);
				this.beingWalked.Remove(partitionKey);
			}
		}

		// Token: 0x04005DB7 RID: 23991
		private readonly IPartitionedDocument document;

		// Token: 0x04005DB8 RID: 23992
		private readonly HashSet<IPartitionKey> walked;

		// Token: 0x04005DB9 RID: 23993
		private readonly HashSet<IPartitionKey> beingWalked;

		// Token: 0x04005DBA RID: 23994
		private readonly Dictionary<IPartitionKey, IFirewallPartitionPlan> partitionPlans;

		// Token: 0x04005DBB RID: 23995
		private int evaluationOrder;

		// Token: 0x04005DBC RID: 23996
		private bool cycleFound;
	}
}
