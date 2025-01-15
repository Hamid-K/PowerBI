using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CC5 RID: 7365
	internal class FirewallPlan : IFirewallPlan
	{
		// Token: 0x0600B78C RID: 46988 RVA: 0x00253FD0 File Offset: 0x002521D0
		public FirewallPlan()
		{
			this.partitionPlans = new Dictionary<IPartitionKey, FirewallPartitionPlan>(PartitionKeyEqualityComparer.Instance);
		}

		// Token: 0x17002DA5 RID: 11685
		// (get) Token: 0x0600B78D RID: 46989 RVA: 0x00253FE8 File Offset: 0x002521E8
		public IEnumerable<IFirewallPartitionPlan> PartitionPlans
		{
			get
			{
				return this.partitionPlans.Values.Cast<IFirewallPartitionPlan>();
			}
		}

		// Token: 0x0600B78E RID: 46990 RVA: 0x00253FFA File Offset: 0x002521FA
		public bool TryGetPartitionPlan(IPartitionKey partitionKey, out FirewallPartitionPlan partitionPlan)
		{
			return this.partitionPlans.TryGetValue(partitionKey, out partitionPlan);
		}

		// Token: 0x0600B78F RID: 46991 RVA: 0x00254009 File Offset: 0x00252209
		public void AddPartitionPlan(FirewallPartitionPlan partitionPlan)
		{
			this.partitionPlans.Add(partitionPlan.PartitionKey, partitionPlan);
		}

		// Token: 0x0600B790 RID: 46992 RVA: 0x00254020 File Offset: 0x00252220
		public void Serialize(BinaryWriter writer)
		{
			writer.WriteDictionary(this.partitionPlans, delegate(BinaryWriter w, IPartitionKey k)
			{
				w.WriteIPartitionKey(k);
			}, delegate(BinaryWriter w, FirewallPartitionPlan v)
			{
				w.WriteFirewallPartitionPlan(v);
			});
		}

		// Token: 0x0600B791 RID: 46993 RVA: 0x00254078 File Offset: 0x00252278
		public void Deserialize(BinaryReader reader)
		{
			reader.ReadDictionary(this.partitionPlans, (BinaryReader r) => r.ReadIPartitionKey(), (BinaryReader r) => r.ReadFirewallPartitionPlan());
		}

		// Token: 0x04005DA6 RID: 23974
		private Dictionary<IPartitionKey, FirewallPartitionPlan> partitionPlans;
	}
}
