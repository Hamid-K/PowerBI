using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.DocumentHost
{
	// Token: 0x02001937 RID: 6455
	internal class FirewallPlanGrouper
	{
		// Token: 0x0600A3FC RID: 41980 RVA: 0x0021EF78 File Offset: 0x0021D178
		public FirewallPlanGrouper(IFirewallPlanCreator creator, IFirewallPlan firewallPlan, IMemberLetPartitionedDocument document)
		{
			this.creator = creator;
			this.firewallPlan = firewallPlan;
			this.document = document;
			this.outputs = this.firewallPlan.GetOutputs();
			this.equivalentPartitions = FirewallPlanGrouper.GetEquivalentPartitions(this.firewallPlan);
			this.partitionPlans = FirewallPlanGrouper.GetPartitionPlans(this.firewallPlan);
			this.partitionsGrouped = new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance);
			this.partitionsBeingGrouped = new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance);
		}

		// Token: 0x0600A3FD RID: 41981 RVA: 0x0021EFF4 File Offset: 0x0021D1F4
		public IFirewallPlan GroupPlanForPartitions(IEnumerable<IPartitionKey> rootPartitionKeys)
		{
			foreach (IPartitionKey partitionKey in rootPartitionKeys)
			{
				IFirewallPartitionPlan firewallPartitionPlan;
				FirewallPlanGrouper.PartitionKind partitionKind;
				this.GroupPartitionPlans(partitionKey, true, out firewallPartitionPlan, out partitionKind);
			}
			this.EnsurePartitionsExist(rootPartitionKeys);
			return this.creator.CreatePlan(this.partitionPlans.Values);
		}

		// Token: 0x0600A3FE RID: 41982 RVA: 0x0021F060 File Offset: 0x0021D260
		private static Dictionary<IPartitionKey, HashSet<IPartitionKey>> GetEquivalentPartitions(IFirewallPlan firewallPlan)
		{
			Dictionary<IPartitionKey, HashSet<IPartitionKey>> dictionary = new Dictionary<IPartitionKey, HashSet<IPartitionKey>>(PartitionKeyEqualityComparer.Instance);
			foreach (IFirewallPartitionPlan firewallPartitionPlan in firewallPlan.PartitionPlans)
			{
				HashSet<IPartitionKey> hashSet = new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance);
				hashSet.Add(firewallPartitionPlan.PartitionKey);
				dictionary.Add(firewallPartitionPlan.PartitionKey, hashSet);
			}
			return dictionary;
		}

		// Token: 0x0600A3FF RID: 41983 RVA: 0x0021F0D8 File Offset: 0x0021D2D8
		private static Dictionary<IPartitionKey, IFirewallPartitionPlan> GetPartitionPlans(IFirewallPlan firewallPlan)
		{
			Dictionary<IPartitionKey, IFirewallPartitionPlan> dictionary = new Dictionary<IPartitionKey, IFirewallPartitionPlan>(PartitionKeyEqualityComparer.Instance);
			foreach (IFirewallPartitionPlan firewallPartitionPlan in firewallPlan.PartitionPlans)
			{
				dictionary.Add(firewallPartitionPlan.PartitionKey, firewallPartitionPlan);
			}
			return dictionary;
		}

		// Token: 0x0600A400 RID: 41984 RVA: 0x0021F138 File Offset: 0x0021D338
		private void GroupPartitionPlans(IPartitionKey partitionKey, bool okIfNoPlanFound, out IFirewallPartitionPlan partitionPlan, out FirewallPlanGrouper.PartitionKind partitionKind)
		{
			partitionPlan = null;
			partitionKind = FirewallPlanGrouper.PartitionKind.None;
			if (partitionKey != null)
			{
				if (!this.TryGetPlanAndKind(partitionKey, out partitionPlan, out partitionKind, okIfNoPlanFound))
				{
					return;
				}
				if (this.partitionsGrouped.Add(partitionKey))
				{
					this.partitionsBeingGrouped.Add(partitionKey);
					IEnumerable<IPartitionKey> inputs = partitionPlan.Inputs;
					Func<IPartitionKey, bool> <>9__0;
					Func<IPartitionKey, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (IPartitionKey p) => !p.Equals(partitionKey));
					}
					foreach (IPartitionKey partitionKey2 in inputs.Where(func))
					{
						IFirewallPartitionPlan firewallPartitionPlan = null;
						FirewallPlanGrouper.PartitionKind partitionKind2;
						this.GroupPartitionPlans(partitionKey2, false, out firewallPartitionPlan, out partitionKind2);
						partitionPlan = this.partitionPlans[partitionKey];
						if (this.CanBeGrouped(partitionKey, partitionKind, partitionKey2, partitionKind2))
						{
							partitionKind = this.GroupPartitionPlans(partitionKey, partitionPlan, partitionKind, partitionKey2, firewallPartitionPlan, partitionKind2);
							partitionPlan = this.partitionPlans[partitionKey];
						}
					}
					if (this.CanBeConvertedToSource(partitionPlan, partitionKind))
					{
						foreach (IPartitionKey partitionKey3 in partitionPlan.Inputs.ToArray<IPartitionKey>())
						{
							IFirewallPartitionPlan firewallPartitionPlan2 = null;
							FirewallPlanGrouper.PartitionKind partitionKind3;
							if (!this.TryGetPlanAndKind(partitionKey3, out firewallPartitionPlan2, out partitionKind3, false))
							{
								return;
							}
							partitionKind = this.GroupPartitionPlans(partitionKey, partitionPlan, partitionKind, partitionKey3, firewallPartitionPlan2, partitionKind3);
							partitionPlan = this.partitionPlans[partitionKey];
						}
					}
					this.partitionsBeingGrouped.Remove(partitionKey);
				}
			}
		}

		// Token: 0x0600A401 RID: 41985 RVA: 0x0021F2E8 File Offset: 0x0021D4E8
		private bool CanBeGrouped(IPartitionKey partitionKey, FirewallPlanGrouper.PartitionKind partitionKind, IPartitionKey inputKey, FirewallPlanGrouper.PartitionKind inputKind)
		{
			return this.document.ArePartitionsOfSameMember(inputKey, partitionKey) && this.IsOnlyReferencedBy(inputKey, partitionKey) && !this.document.IsPartitionResultOfMember(inputKey) && !this.partitionsBeingGrouped.Contains(inputKey) && (inputKind == partitionKind || inputKind == FirewallPlanGrouper.PartitionKind.Neutral || partitionKind == FirewallPlanGrouper.PartitionKind.Neutral);
		}

		// Token: 0x0600A402 RID: 41986 RVA: 0x0021F33C File Offset: 0x0021D53C
		private bool CanBeConvertedToSource(IFirewallPartitionPlan partitionPlan, FirewallPlanGrouper.PartitionKind partitionKind)
		{
			return partitionKind == FirewallPlanGrouper.PartitionKind.QueryOrMultiStepReference && partitionPlan.Inputs.All((IPartitionKey inputKey) => this.document.ArePartitionsOfSameMember(inputKey, partitionPlan.PartitionKey) && this.IsSource(inputKey) && this.IsOnlyReferencedBy(inputKey, partitionPlan) && !this.document.IsPartitionResultOfMember(inputKey) && !this.partitionsBeingGrouped.Contains(inputKey));
		}

		// Token: 0x0600A403 RID: 41987 RVA: 0x0021F380 File Offset: 0x0021D580
		private FirewallPlanGrouper.PartitionKind GroupPartitionPlans(IPartitionKey partitionKey, IFirewallPartitionPlan partitionPlan, FirewallPlanGrouper.PartitionKind partitionKind, IPartitionKey inputKey, IFirewallPartitionPlan inputPlan, FirewallPlanGrouper.PartitionKind inputKind)
		{
			if (inputKind != FirewallPlanGrouper.PartitionKind.Neutral)
			{
				partitionKind = inputKind;
			}
			HashSet<IPartitionKey> hashSet = new HashSet<IPartitionKey>(PartitionKeyEqualityComparer.Instance);
			hashSet.UnionWith(partitionPlan.Inputs);
			hashSet.UnionWith(inputPlan.Inputs);
			hashSet.Remove(inputKey);
			IFirewallPartitionPlan firewallPartitionPlan = this.creator.CreatePartitionPlan(partitionKey, partitionPlan.EvaluationOrder, partitionPlan.IsCyclic || inputPlan.IsCyclic, hashSet.ToArray<IPartitionKey>());
			firewallPartitionPlan.AddResources(partitionPlan.Resources);
			firewallPartitionPlan.AddResources(inputPlan.Resources);
			this.partitionPlans.Remove(inputKey);
			this.partitionPlans[partitionKey] = firewallPartitionPlan;
			this.equivalentPartitions[partitionKey].UnionWith(this.equivalentPartitions[inputKey]);
			this.equivalentPartitions.Remove(inputKey);
			return partitionKind;
		}

		// Token: 0x0600A404 RID: 41988 RVA: 0x0021F44F File Offset: 0x0021D64F
		private bool TryGetPlanAndKind(IPartitionKey partitionKey, out IFirewallPartitionPlan partitionPlan, out FirewallPlanGrouper.PartitionKind partitionKind, bool okIfNoPlanFound)
		{
			return this.TryGetPlanAndKind(partitionKey, out partitionPlan, out partitionKind);
		}

		// Token: 0x0600A405 RID: 41989 RVA: 0x0021F460 File Offset: 0x0021D660
		private bool TryGetPlanAndKind(IPartitionKey partitionKey, out IFirewallPartitionPlan partitionPlan, out FirewallPlanGrouper.PartitionKind partitionKind)
		{
			if (this.partitionPlans.TryGetValue(partitionKey, out partitionPlan))
			{
				int num = partitionPlan.Inputs.Count<IPartitionKey>();
				if (num == 0)
				{
					partitionKind = FirewallPlanGrouper.PartitionKind.Source;
				}
				else if (num == 1 && this.document.ArePartitionsOfSameMember(partitionPlan.Inputs.Single<IPartitionKey>(), partitionKey))
				{
					partitionKind = FirewallPlanGrouper.PartitionKind.Neutral;
				}
				else
				{
					partitionKind = FirewallPlanGrouper.PartitionKind.QueryOrMultiStepReference;
				}
				return true;
			}
			partitionKind = FirewallPlanGrouper.PartitionKind.None;
			return false;
		}

		// Token: 0x0600A406 RID: 41990 RVA: 0x0021F4C0 File Offset: 0x0021D6C0
		private bool IsSource(IPartitionKey partitionKey)
		{
			IFirewallPartitionPlan firewallPartitionPlan;
			FirewallPlanGrouper.PartitionKind partitionKind;
			return this.TryGetPlanAndKind(partitionKey, out firewallPartitionPlan, out partitionKind) && partitionKind == FirewallPlanGrouper.PartitionKind.Source;
		}

		// Token: 0x0600A407 RID: 41991 RVA: 0x0021F4E0 File Offset: 0x0021D6E0
		private bool IsOnlyReferencedBy(IPartitionKey partitionKey, IPartitionKey referringKey)
		{
			HashSet<IPartitionKey> hashSet;
			return this.outputs.TryGetValue(partitionKey, out hashSet) && hashSet.Count == 1 && hashSet.Contains(referringKey);
		}

		// Token: 0x0600A408 RID: 41992 RVA: 0x0021F514 File Offset: 0x0021D714
		private bool IsOnlyReferencedBy(IPartitionKey partitionKey, IFirewallPartitionPlan referringPartition)
		{
			HashSet<IPartitionKey> hashSet;
			return this.outputs.TryGetValue(partitionKey, out hashSet) && hashSet.Count > 0 && hashSet.All((IPartitionKey key) => this.equivalentPartitions[referringPartition.PartitionKey].Contains(key));
		}

		// Token: 0x0600A409 RID: 41993 RVA: 0x0021F564 File Offset: 0x0021D764
		private void EnsurePartitionsExist(IEnumerable<IPartitionKey> partitionKeys)
		{
			Dictionary<IPartitionKey, IPartitionKey> dictionary = new Dictionary<IPartitionKey, IPartitionKey>(PartitionKeyEqualityComparer.Instance);
			foreach (IPartitionKey partitionKey in this.partitionPlans.Keys)
			{
				foreach (IPartitionKey partitionKey2 in this.equivalentPartitions[partitionKey])
				{
					dictionary.Add(partitionKey2, partitionKey);
				}
			}
			foreach (IPartitionKey partitionKey3 in partitionKeys)
			{
				IPartitionKey partitionKey4 = dictionary[partitionKey3];
				if (!partitionKey3.Equals(partitionKey4))
				{
					IFirewallPartitionPlan firewallPartitionPlan = this.partitionPlans[partitionKey4];
					IFirewallPartitionPlan firewallPartitionPlan2 = this.creator.CreatePartitionPlan(partitionKey3, firewallPartitionPlan.EvaluationOrder, firewallPartitionPlan.IsCyclic, firewallPartitionPlan.Inputs);
					firewallPartitionPlan2.AddResources(firewallPartitionPlan.Resources);
					this.partitionPlans.Add(partitionKey3, firewallPartitionPlan2);
					dictionary[partitionKey3] = partitionKey3;
				}
			}
		}

		// Token: 0x04005560 RID: 21856
		private readonly IFirewallPlanCreator creator;

		// Token: 0x04005561 RID: 21857
		private readonly IFirewallPlan firewallPlan;

		// Token: 0x04005562 RID: 21858
		private readonly IMemberLetPartitionedDocument document;

		// Token: 0x04005563 RID: 21859
		private readonly Dictionary<IPartitionKey, HashSet<IPartitionKey>> outputs;

		// Token: 0x04005564 RID: 21860
		private readonly Dictionary<IPartitionKey, HashSet<IPartitionKey>> equivalentPartitions;

		// Token: 0x04005565 RID: 21861
		private readonly Dictionary<IPartitionKey, IFirewallPartitionPlan> partitionPlans;

		// Token: 0x04005566 RID: 21862
		private readonly HashSet<IPartitionKey> partitionsGrouped;

		// Token: 0x04005567 RID: 21863
		private readonly HashSet<IPartitionKey> partitionsBeingGrouped;

		// Token: 0x02001938 RID: 6456
		private enum PartitionKind
		{
			// Token: 0x04005569 RID: 21865
			None,
			// Token: 0x0400556A RID: 21866
			Neutral,
			// Token: 0x0400556B RID: 21867
			Source,
			// Token: 0x0400556C RID: 21868
			QueryOrMultiStepReference
		}
	}
}
