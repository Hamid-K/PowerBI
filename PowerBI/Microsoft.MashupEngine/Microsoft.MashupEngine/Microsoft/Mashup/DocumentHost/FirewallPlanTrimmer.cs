using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.DocumentHost
{
	// Token: 0x0200193F RID: 6463
	internal class FirewallPlanTrimmer
	{
		// Token: 0x0600A41E RID: 42014 RVA: 0x0021FAF5 File Offset: 0x0021DCF5
		public FirewallPlanTrimmer(IFirewallPlanCreator creator)
		{
			this.creator = creator;
		}

		// Token: 0x0600A41F RID: 42015 RVA: 0x0021FB04 File Offset: 0x0021DD04
		public IFirewallPlan RemovePartitionsWithoutDataSources(IFirewallPlan firewallPlan, IPartitionKey rootPartitionKey)
		{
			Dictionary<IPartitionKey, IFirewallPartitionPlan> partitionPlans = new Dictionary<IPartitionKey, IFirewallPartitionPlan>(PartitionKeyEqualityComparer.Instance);
			foreach (IFirewallPartitionPlan firewallPartitionPlan in firewallPlan.PartitionPlans)
			{
				partitionPlans.Add(firewallPartitionPlan.PartitionKey, firewallPartitionPlan);
			}
			Dictionary<IPartitionKey, HashSet<IPartitionKey>> outputs = firewallPlan.GetOutputs();
			HashSet<IPartitionKey> partitionsAccessingDataSources = new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance);
			foreach (IFirewallPartitionPlan firewallPartitionPlan2 in firewallPlan.PartitionPlans)
			{
				if (firewallPartitionPlan2.Resources.Any<IResource>())
				{
					FirewallPlanTrimmer.Mark(partitionPlans, partitionsAccessingDataSources, outputs, firewallPartitionPlan2.PartitionKey);
				}
			}
			partitionsAccessingDataSources.Add(rootPartitionKey);
			List<IFirewallPartitionPlan> list = new List<IFirewallPartitionPlan>();
			Func<IPartitionKey, bool> <>9__0;
			foreach (IFirewallPartitionPlan firewallPartitionPlan3 in firewallPlan.PartitionPlans)
			{
				if (partitionsAccessingDataSources.Contains(firewallPartitionPlan3.PartitionKey) || firewallPartitionPlan3.IsCyclic)
				{
					IFirewallPlanCreator firewallPlanCreator = this.creator;
					IPartitionKey partitionKey = firewallPartitionPlan3.PartitionKey;
					int evaluationOrder = firewallPartitionPlan3.EvaluationOrder;
					bool isCyclic = firewallPartitionPlan3.IsCyclic;
					IEnumerable<IPartitionKey> inputs = firewallPartitionPlan3.Inputs;
					Func<IPartitionKey, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (IPartitionKey i) => partitionsAccessingDataSources.Contains(i) || partitionPlans[i].IsCyclic);
					}
					IFirewallPartitionPlan firewallPartitionPlan4 = firewallPlanCreator.CreatePartitionPlan(partitionKey, evaluationOrder, isCyclic, inputs.Where(func));
					firewallPartitionPlan4.AddResources(firewallPartitionPlan3.Resources);
					firewallPartitionPlan4.AddException(firewallPartitionPlan3.Exception);
					list.Add(firewallPartitionPlan4);
				}
			}
			return this.creator.CreatePlan(list);
		}

		// Token: 0x0600A420 RID: 42016 RVA: 0x0021FCD4 File Offset: 0x0021DED4
		public IFirewallPlan RemoveUnreferencedPartitions(IFirewallPlan firewallPlan, IPartitionKey rootPartitionKey)
		{
			Dictionary<IPartitionKey, IFirewallPartitionPlan> partitionPlans = new Dictionary<IPartitionKey, IFirewallPartitionPlan>(PartitionKeyEqualityComparer.Instance);
			foreach (IFirewallPartitionPlan firewallPartitionPlan in firewallPlan.PartitionPlans)
			{
				partitionPlans.Add(firewallPartitionPlan.PartitionKey, firewallPartitionPlan);
			}
			HashSet<IPartitionKey> hashSet = new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance);
			Queue<IPartitionKey> queue = new Queue<IPartitionKey>();
			queue.Enqueue(rootPartitionKey);
			while (queue.Count > 0)
			{
				IPartitionKey partitionKey = queue.Dequeue();
				if (hashSet.Add(partitionKey))
				{
					foreach (IPartitionKey partitionKey2 in partitionPlans[partitionKey].Inputs)
					{
						queue.Enqueue(partitionKey2);
					}
				}
			}
			return this.creator.CreatePlan(hashSet.Select((IPartitionKey p) => partitionPlans[p]));
		}

		// Token: 0x0600A421 RID: 42017 RVA: 0x0021FDE8 File Offset: 0x0021DFE8
		private static void Mark(Dictionary<IPartitionKey, IFirewallPartitionPlan> partitionPlans, HashSet<IPartitionKey> marked, Dictionary<IPartitionKey, HashSet<IPartitionKey>> outputs, IPartitionKey partitionKey)
		{
			HashSet<IPartitionKey> hashSet;
			if (!partitionPlans[partitionKey].IsCyclic && marked.Add(partitionKey) && outputs.TryGetValue(partitionKey, out hashSet))
			{
				foreach (IPartitionKey partitionKey2 in hashSet)
				{
					FirewallPlanTrimmer.Mark(partitionPlans, marked, outputs, partitionKey2);
				}
			}
		}

		// Token: 0x0400557B RID: 21883
		private readonly IFirewallPlanCreator creator;
	}
}
