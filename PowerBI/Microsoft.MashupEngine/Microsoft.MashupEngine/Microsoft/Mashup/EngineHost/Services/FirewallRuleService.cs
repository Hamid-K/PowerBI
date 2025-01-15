using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Storage;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019E6 RID: 6630
	internal sealed class FirewallRuleService : IFirewallRuleService
	{
		// Token: 0x0600A7D8 RID: 42968 RVA: 0x0022B614 File Offset: 0x00229814
		public FirewallRuleService(FirewallRuleManager firewallRuleManager)
		{
			this.firewallRuleManager = firewallRuleManager;
		}

		// Token: 0x0600A7D9 RID: 42969 RVA: 0x0022B624 File Offset: 0x00229824
		public FirewallGroup2 CreateFirewallGroup(IResource resource)
		{
			FirewallGroup firewallGroup = FirewallGroup.Create(this.firewallRuleManager, new Resource(resource));
			return new FirewallGroup2((FirewallGroupType2)firewallGroup.GroupType, firewallGroup.IsTrusted, firewallGroup.Resource, firewallGroup.GroupName);
		}

		// Token: 0x0600A7DA RID: 42970 RVA: 0x0022B660 File Offset: 0x00229860
		public FirewallGroup2 UpdateFirewallGroup(IResource resource, FirewallGroup2 originalGroup, IValue traits)
		{
			return new FirewallGroup2(FirewallGroupType2.SingleUnclassified, false, resource);
		}

		// Token: 0x04005766 RID: 22374
		private readonly FirewallRuleManager firewallRuleManager;
	}
}
