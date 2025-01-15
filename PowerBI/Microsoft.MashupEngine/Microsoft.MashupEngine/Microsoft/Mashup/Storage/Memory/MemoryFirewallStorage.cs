using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Storage.Memory
{
	// Token: 0x0200209E RID: 8350
	public class MemoryFirewallStorage : FirewallStorage
	{
		// Token: 0x0600CC82 RID: 52354 RVA: 0x0028AB75 File Offset: 0x00288D75
		public MemoryFirewallStorage()
		{
			this.firewallRules = new List<FirewallRule>();
		}

		// Token: 0x0600CC83 RID: 52355 RVA: 0x0028AB88 File Offset: 0x00288D88
		public MemoryFirewallStorage(IEnumerable<FirewallRule> firewallRules)
		{
			this.firewallRules = new List<FirewallRule>(firewallRules);
		}

		// Token: 0x0600CC84 RID: 52356 RVA: 0x0028AB9C File Offset: 0x00288D9C
		public override FirewallRule[] GetFirewallRules()
		{
			return this.firewallRules.ToArray<FirewallRule>();
		}

		// Token: 0x0600CC85 RID: 52357 RVA: 0x0028ABAC File Offset: 0x00288DAC
		public override void SetFirewallRules(FirewallRule[] firewallRules)
		{
			foreach (FirewallRule firewallRule in firewallRules)
			{
				int num;
				if (this.TryFindFirewallRule(firewallRule.Resource, out num))
				{
					this.firewallRules[num] = firewallRule;
				}
				else
				{
					this.firewallRules.Add(firewallRule);
				}
			}
		}

		// Token: 0x0600CC86 RID: 52358 RVA: 0x0028ABF8 File Offset: 0x00288DF8
		public override void ClearFirewallRules(params Resource[] resources)
		{
			foreach (Resource resource in resources)
			{
				int num;
				if (this.TryFindFirewallRule(resource, out num))
				{
					this.firewallRules.RemoveAt(num);
				}
			}
		}

		// Token: 0x0600CC87 RID: 52359 RVA: 0x0028AC30 File Offset: 0x00288E30
		private bool TryFindFirewallRule(Resource resource, out int index)
		{
			for (int i = 0; i < this.firewallRules.Count; i++)
			{
				if (this.firewallRules[i].Matches(resource))
				{
					index = i;
					return true;
				}
			}
			index = -1;
			return false;
		}

		// Token: 0x04006793 RID: 26515
		private readonly IList<FirewallRule> firewallRules;
	}
}
