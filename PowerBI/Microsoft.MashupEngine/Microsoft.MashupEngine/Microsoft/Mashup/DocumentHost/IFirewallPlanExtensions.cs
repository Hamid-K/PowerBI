using System;
using System.Collections.Generic;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.DocumentHost
{
	// Token: 0x02001945 RID: 6469
	internal static class IFirewallPlanExtensions
	{
		// Token: 0x0600A432 RID: 42034 RVA: 0x0021FE90 File Offset: 0x0021E090
		public static Dictionary<IPartitionKey, HashSet<IPartitionKey>> GetOutputs(this IFirewallPlan firewallPlan)
		{
			Dictionary<IPartitionKey, HashSet<IPartitionKey>> dictionary = new Dictionary<IPartitionKey, HashSet<IPartitionKey>>(PartitionKeyEqualityComparer.Instance);
			foreach (IFirewallPartitionPlan firewallPartitionPlan in firewallPlan.PartitionPlans)
			{
				foreach (IPartitionKey partitionKey in firewallPartitionPlan.Inputs)
				{
					HashSet<IPartitionKey> hashSet;
					if (!dictionary.TryGetValue(partitionKey, out hashSet))
					{
						hashSet = new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance);
						dictionary.Add(partitionKey, hashSet);
					}
					hashSet.Add(firewallPartitionPlan.PartitionKey);
				}
			}
			return dictionary;
		}
	}
}
