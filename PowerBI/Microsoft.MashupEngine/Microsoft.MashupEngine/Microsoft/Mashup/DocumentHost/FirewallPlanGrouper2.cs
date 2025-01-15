using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.DocumentHost
{
	// Token: 0x0200193C RID: 6460
	internal class FirewallPlanGrouper2
	{
		// Token: 0x0600A410 RID: 42000 RVA: 0x0021F761 File Offset: 0x0021D961
		public FirewallPlanGrouper2(IFirewallPlanCreator creator, IFirewallPlan firewallPlan, IMemberLetPartitionedDocument document)
		{
			this.creator = creator;
			this.firewallPlan = firewallPlan;
			this.document = document;
			this.outputs = this.firewallPlan.GetOutputs();
			this.partitionPlans = FirewallPlanGrouper2.GetPartitionPlans(this.firewallPlan);
		}

		// Token: 0x0600A411 RID: 42001 RVA: 0x0021F7A0 File Offset: 0x0021D9A0
		public IFirewallPlan GroupPlanForPartition(IPartitionKey partitionKey)
		{
			HashSet<IPartitionKey> hashSet = new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance);
			this.GroupPartitionPlans(partitionKey, hashSet);
			return this.creator.CreatePlan(this.partitionPlans.Values);
		}

		// Token: 0x0600A412 RID: 42002 RVA: 0x0021F7D8 File Offset: 0x0021D9D8
		private static Dictionary<IPartitionKey, IFirewallPartitionPlan> GetPartitionPlans(IFirewallPlan firewallPlan)
		{
			Dictionary<IPartitionKey, IFirewallPartitionPlan> dictionary = new Dictionary<IPartitionKey, IFirewallPartitionPlan>(PartitionKeyEqualityComparer.Instance);
			foreach (IFirewallPartitionPlan firewallPartitionPlan in firewallPlan.PartitionPlans)
			{
				dictionary.Add(firewallPartitionPlan.PartitionKey, firewallPartitionPlan);
			}
			return dictionary;
		}

		// Token: 0x0600A413 RID: 42003 RVA: 0x0021F838 File Offset: 0x0021DA38
		private static bool IsSource(IFirewallPartitionPlan partitionPlan)
		{
			return !partitionPlan.Inputs.Any<IPartitionKey>();
		}

		// Token: 0x0600A414 RID: 42004 RVA: 0x0021F848 File Offset: 0x0021DA48
		private void GroupPartitionPlans(IPartitionKey partitionKey, HashSet<IPartitionKey> partitionsGrouped)
		{
			if (partitionsGrouped.Add(partitionKey))
			{
				IFirewallPartitionPlan firewallPartitionPlan = this.partitionPlans[partitionKey];
				IPartitionKey[] array = firewallPartitionPlan.Inputs.ToArray<IPartitionKey>();
				foreach (IPartitionKey partitionKey2 in array)
				{
					this.GroupPartitionPlans(partitionKey2, partitionsGrouped);
				}
				if (this.CanBeConvertedToSource(firewallPartitionPlan, array))
				{
					foreach (IPartitionKey partitionKey3 in array)
					{
						IFirewallPartitionPlan firewallPartitionPlan2 = this.partitionPlans[partitionKey3];
						this.GroupPartitionPlans(partitionKey, firewallPartitionPlan, partitionKey3, firewallPartitionPlan2);
						firewallPartitionPlan = this.partitionPlans[partitionKey];
					}
				}
			}
		}

		// Token: 0x0600A415 RID: 42005 RVA: 0x0021F8DC File Offset: 0x0021DADC
		private void GroupPartitionPlans(IPartitionKey partitionKey, IFirewallPartitionPlan partitionPlan, IPartitionKey inputKey, IFirewallPartitionPlan inputPlan)
		{
			HashSet<IPartitionKey> hashSet = new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance);
			hashSet.UnionWith(partitionPlan.Inputs);
			hashSet.UnionWith(inputPlan.Inputs);
			hashSet.Remove(inputKey);
			IFirewallPartitionPlan firewallPartitionPlan = this.creator.CreatePartitionPlan(partitionKey, partitionPlan.EvaluationOrder, partitionPlan.IsCyclic || inputPlan.IsCyclic, hashSet.ToArray<IPartitionKey>());
			firewallPartitionPlan.AddResources(partitionPlan.Resources);
			firewallPartitionPlan.AddResources(inputPlan.Resources);
			this.partitionPlans.Remove(inputKey);
			this.partitionPlans[partitionKey] = firewallPartitionPlan;
		}

		// Token: 0x0600A416 RID: 42006 RVA: 0x0021F974 File Offset: 0x0021DB74
		private bool CanBeConvertedToSource(IFirewallPartitionPlan partitionPlan, IPartitionKey[] inputs)
		{
			return partitionPlan.Inputs.Any<IPartitionKey>() && inputs.All((IPartitionKey input) => this.CanBeConvertedToSource(partitionPlan, input));
		}

		// Token: 0x0600A417 RID: 42007 RVA: 0x0021F9BC File Offset: 0x0021DBBC
		private bool CanBeConvertedToSource(IFirewallPartitionPlan partitionPlan, IPartitionKey input)
		{
			IFirewallPartitionPlan firewallPartitionPlan = this.partitionPlans[input];
			return this.document.ArePartitionsOfSameMember(firewallPartitionPlan.PartitionKey, partitionPlan.PartitionKey) && FirewallPlanGrouper2.IsSource(firewallPartitionPlan) && this.IsOnlyReferencedBy(input, partitionPlan.PartitionKey) && !this.document.IsPartitionResultOfMember(input) && !firewallPartitionPlan.IsCyclic;
		}

		// Token: 0x0600A418 RID: 42008 RVA: 0x0021FA20 File Offset: 0x0021DC20
		private bool IsOnlyReferencedBy(IPartitionKey partitionKey, IPartitionKey referringKey)
		{
			HashSet<IPartitionKey> hashSet;
			return this.outputs.TryGetValue(partitionKey, out hashSet) && hashSet.Count == 1 && hashSet.Contains(referringKey);
		}

		// Token: 0x04005573 RID: 21875
		private readonly IFirewallPlanCreator creator;

		// Token: 0x04005574 RID: 21876
		private readonly IFirewallPlan firewallPlan;

		// Token: 0x04005575 RID: 21877
		private readonly IMemberLetPartitionedDocument document;

		// Token: 0x04005576 RID: 21878
		private readonly Dictionary<IPartitionKey, HashSet<IPartitionKey>> outputs;

		// Token: 0x04005577 RID: 21879
		private readonly Dictionary<IPartitionKey, IFirewallPartitionPlan> partitionPlans;
	}
}
